using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Dtos;
using ApiIncidencias.Helpers;
using Aplicacion.Contratos;
using Dominio;
using Dominio.Interfaces;
using iText.Forms.Xfdf;
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
                    usuario.Rol.Add(rolPredeterminado);
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
                datosUsuarioDto.EstaAutenticado = true;
                //JwtSecurityToken jwtSecurityToken = CreateJwtToken(usuario);
                datosUsuarioDto.Token = _jwtGenerador.CrearToken(usuario);//new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                datosUsuarioDto.Email = usuario.Email;
                datosUsuarioDto.UserName = usuario.Username;
                datosUsuarioDto.Roles = usuario.Rol.Select(u => u.Name_Rol).ToList()!;
                return datosUsuarioDto;
            }
            
            // Si la contraseña es incorrecta, se indica que no está autenticado.
            datosUsuarioDto.EstaAutenticado = false;
            datosUsuarioDto.Mensaje = $"Credenciales incorrectas para el usuario {usuario.Username}.";
            return datosUsuarioDto;
        }

        // Método privado para crear un token JWT.
        private JwtSecurityToken CreateJwtToken(Usuario usuario)
        {
            var roles = usuario.Rol;
            var roleClaims = new List<Claim>();
            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("rol", role.Name_Rol!));
            }
            
            // Se definen las reclamaciones del token, como el nombre de usuario, correo electrónico, etc.
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Username!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email!),
                new Claim("uid", usuario.Id.ToString())
            }
            .Union(roleClaims);
            
            // Se crea una clave de seguridad basada en la clave secreta definida en la configuración.
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key!));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            
            // Se crea y devuelve el token JWT.
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);
            
            return jwtSecurityToken;
        }

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
            var userHasRol = usuario.Rol?.Any(x => x.Id == existingRol.Id);
            if (userHasRol == false)
            {            
                usuario.Rol?.Add(existingRol);            ;

                _unitOfWork.Usuarios.Update(usuario);
                await _unitOfWork.SaveAsync();
            }
            return $"Rol {model.Role} agregado a la cuenta {model.Username} de forma exitosa.";
            
        
        }
        // Otros métodos como UserLogin y AddRoleAsync no están implementados aquí.
        public async Task<LoginDto> UserLogin(LoginDto model)
    {
        var usuario = await _unitOfWork.Usuarios.GetByUsernameAsync(model.Username);
        var resultado = _passwordHasher.VerifyHashedPassword(usuario, usuario.Password, model.Password);

        if (resultado == PasswordVerificationResult.Success)
        {
            return model;
        }
        return null;
    }
    }
}
