using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Api.Dtos;
using ApiIncidencias.Helpers;
using Aplicacion.Contratos;
using Dominio;
using Dominio.Interfaces;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ApiIncidencias.Services
{
    public class UserService : IUserService
    {
        private readonly JWT _jwt;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<Usuario> _passwordHasher;
        private readonly IJwtGenerador _jwtGenerador;

        public UserService(IUnitOfWork unitOfWork, IOptions<JWT> jwt,
            IPasswordHasher<Usuario> passwordHasher, IJwtGenerador jwtGenerador)
        {
            // Se inyectan las dependencias y se almacenan en campos privados para su uso posterior.
            _jwt = jwt.Value;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _jwtGenerador = jwtGenerador;
        }

        // Método para registrar un nuevo usuario.
        public async Task<string> RegisterAsync(RegisterDto registerDto)
        {
            // Se crea un nuevo objeto de usuario con los datos proporcionados en el DTO.
            var usuario = new Usuario
            {
                Email = registerDto.Email,
                Username = registerDto.Username,
            };


            // Se hashea la contraseña del usuario y se almacena en el objeto.
            usuario.Password = _passwordHasher.HashPassword(usuario, registerDto.Password!);

            // Se verifica si ya existe un usuario con el mismo nombre de usuario.
            Usuario? usuarioExiste = null;
            try
            {
                usuarioExiste = _unitOfWork.Usuarios
                .Find(u => u.Username!.ToLower() == registerDto.Username!.ToLower())
                .FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (usuarioExiste == null)
            {
                // Si el usuario no existe, se le asigna un rol predeterminado y se guarda en la base de datos.
                try
                {
                    var rolPredeterminado = _unitOfWork.Roles
                        .Find(u => u.Name_Rol == Autorizacion.rol_predeterminado.ToString())
                        .First();
                    usuario.Roles.Add(rolPredeterminado);
                    _unitOfWork.Usuarios.Add(usuario);
                    await _unitOfWork.SaveAsync();

                    return $"El usuario {registerDto.Username} ha sido registrado exitosamente";
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                    return $"Error: {message}";
                }
            }
            else
            {
                return $"El usuario con {registerDto.Username} ya se encuentra registrado.";
            }
        }

        // Método para autenticar y generar un token JWT para un usuario.
        public async Task<DatosUsuarioDto> GetTokenAsync(LoginDto model)
        {
            DatosUsuarioDto datosUsuarioDto = new();
            
            // Se busca al usuario por su nombre de usuario en la base de datos.
            var usuario = await _unitOfWork.Usuarios.GetByUsernameAsync(model.Username!);

            if (usuario == null)
            {
                // Si el usuario no existe, se indica que no está autenticado.
                datosUsuarioDto.EstaAutenticado = false;
                datosUsuarioDto.Mensaje = $"No existe ningún usuario con el username {model.Username}.";
                return datosUsuarioDto;
            }

            // Se verifica la contraseña proporcionada con la almacenada en la base de datos.
            var resultado = _passwordHasher.VerifyHashedPassword(usuario, usuario.Password!, model.Password!);

            if (resultado == PasswordVerificationResult.Success)
            {
                // Si la contraseña es correcta, se genera un token JWT y se devuelve información del usuario.
                datosUsuarioDto.Mensaje = "OK";
                datosUsuarioDto.EstaAutenticado = true;
                JwtSecurityToken jwtSecurityToken = CreateJwtToken(usuario);
                //datosUsuarioDto.AccessToken = _jwtGenerador.CrearToken(usuario);//new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                datosUsuarioDto.AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                datosUsuarioDto.Email = usuario.Email;
                datosUsuarioDto.UserName = usuario.Username;
                datosUsuarioDto.Roles = usuario.Roles.Select(u => u.Name_Rol).ToList()!;
                datosUsuarioDto.Expiry = DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes);
            
                //Fomar 2 de obtener el refreshToken
                datosUsuarioDto.RefreshToken = GenerateRefreshToken(usuario.Username).ToString("D");
            
            //Fomar 1 de obtener el refreshToken
            //datosUsuarioDto.RefreshToken = RandomRefreshTokenString();
                return datosUsuarioDto;
            }
            
            // Si la contraseña es incorrecta, se indica que no está autenticado.
            datosUsuarioDto.EstaAutenticado = false;
            datosUsuarioDto.Mensaje = $"Credenciales incorrectas para el usuario {usuario.Username}.";
            return datosUsuarioDto;
        }

        // Método privado para crear un token JWT.
        public async Task<string> AddRoleAsync(AddRolDto model){
            Usuario ? usuario = await _unitOfWork.Usuarios.GetByUsernameAsync(model.Username!);        
            if (usuario == null){
                return $"No existe algún usuario registrado con la cuenta {model.Username}.";            
            }else if(_passwordHasher.VerifyHashedPassword(usuario, usuario.Password!, model.Password!) != PasswordVerificationResult.Success ){
                return $"Credenciales incorrectas para el usuario {model.Username}.";
            }
            var existingRol = await _unitOfWork.Roles.GetRolByName(model.Role!);
            if (existingRol == null){
                return $"Rol {model.Role} agregado a la cuenta {model.Username} de forma exitosa.";
            }
            var userHasRol = usuario.Roles?.Any(x => x.Id == existingRol.Id);
            if (userHasRol == false)
            {            
                usuario.Roles?.Add(existingRol);            ;

                _unitOfWork.Usuarios.Update(usuario);
                await _unitOfWork.SaveAsync();
            }
            return $"Rol {model.Role} agregado a la cuenta {model.Username} de forma exitosa.";
            
        
        }
        // Otros métodos como UserLogin y AddRoleAsync no están implementados aquí.
        public async Task<LoginDto> UserLogin(LoginDto model)
        {
            var usuario = await _unitOfWork.Usuarios.GetByUsernameAsync(model.Username!);
            var resultado = _passwordHasher.VerifyHashedPassword(usuario, usuario.Password!, model.Password!);

            if (resultado == PasswordVerificationResult.Success)
            {
                return model;
            }
            return null!;
        }
        
         public async Task<DatosUsuarioDto> GetTokenAsync(AuthenticationTokenResultDto model)
        {
            if (!IsValid(model, out string Username))
            {
                return null!;
            }

            DatosUsuarioDto datosUsuarioDto = new DatosUsuarioDto();
            var usuario = await _unitOfWork.Usuarios
                                                    .GetByUsernameAsync(Username);

            if (usuario == null)
            {
                datosUsuarioDto.EstaAutenticado = false;
                datosUsuarioDto.Mensaje = $"No existe ningun usuario con el username {Username}.";
                return datosUsuarioDto;
            }

            datosUsuarioDto.Mensaje = "OK";
            datosUsuarioDto.EstaAutenticado = true;
            JwtSecurityToken jwtSecurityToken = CreateJwtToken(usuario);
            datosUsuarioDto.AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            datosUsuarioDto.UserName = usuario.Username;
            datosUsuarioDto.Email = usuario.Email;
            //datosUsuarioDto.Token = _jwtGenerador.CrearToken(usuario);
            datosUsuarioDto.Roles = usuario.Roles
                                                .Select(p => p.Name_Rol!)
                                                .ToList();

            datosUsuarioDto.Expiry = DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes);
            
            //Fomar 2 de obtener el refreshToken
            datosUsuarioDto.RefreshToken = GenerateRefreshToken(usuario.Username).ToString("D");
            
            //Fomar 1 de obtener el refreshToken
            //datosUsuarioDto.RefreshToken = RandomRefreshTokenString();

            return datosUsuarioDto; 
        }

        private bool IsValid(AuthenticationTokenResultDto authResult, out string Username)
        {
            Username = string.Empty;

            ClaimsPrincipal principal = GetPrincipalFromExpiredToken(authResult.AccessToken);

            if (principal is null)
            {
                throw new UnauthorizedAccessException("No hay token de Acceso");
            }

            Username = principal.FindFirstValue(ClaimTypes.NameIdentifier)!;

            if (string.IsNullOrEmpty(Username))
            {
                throw new UnauthorizedAccessException("En UserName es nulo o esta vacio");
            }

            if (!Guid.TryParse(authResult.RefreshToken, out Guid givenRefreshToken))
            {
                throw new UnauthorizedAccessException("El Refresh Token esta mal formado");
            }

            if (!_refreshToken.TryGetValue(Username, out Guid currentRefreshToken))
            {
                throw new UnauthorizedAccessException("El Refresh Token no es valido en el sistema");
            }

            //se compara que los RefreshToquen sean identicos
            if (currentRefreshToken != givenRefreshToken)
            {
                throw new UnauthorizedAccessException("El Refresh Token enviado es Invalido");
            }

            return true;
        }
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string? accessToken)
        {
            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidIssuer = _jwt.Issuer,
                ValidAudience = _jwt.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key!))
            };

            //se valida en Token
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            ClaimsPrincipal principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature, StringComparison.InvariantCulture))
            {
                throw new UnauthorizedAccessException("El token es Invalido");
            }

            return principal;
        }

        private JwtSecurityToken CreateJwtToken(Usuario usuario)
        {
            var roles = usuario.Roles;
            var roleClaims = new List<Claim>();
            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role.Name_Rol!));
            }
            var claims = new[]
            {
                    new Claim(JwtRegisteredClaimNames.Sub, usuario.Username!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("uid", usuario.Id.ToString())
            }
            .Union(roleClaims);
            
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            Console.WriteLine("", symmetricSecurityKey);

            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var JwtSecurityToken = new JwtSecurityToken(
                issuer : _jwt.Issuer,
                audience : _jwt.Audience,
                claims : claims,
                expires : DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials : signingCredentials);

            return JwtSecurityToken;
        }

        //Forma 1 para generar el RefreshToken (para cualquier usuario)-----------------------------
         private static readonly ConcurrentDictionary<string, Guid> _refreshToken = new ConcurrentDictionary<string, Guid>();
        private Guid GenerateRefreshToken(string username)
        {
            Guid newRefreshToken = _refreshToken.AddOrUpdate(username, u => Guid.NewGuid(), (u, o) => Guid.NewGuid());
            return newRefreshToken;
        }
    }
}
