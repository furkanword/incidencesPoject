using Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Configuration;
public class UserConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("User");


        builder.Property(p => p.Id)
        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
        .HasColumnType("int")
        .IsRequired();


        builder.Property(p => p.Username)
        .HasColumnName("NameUser")
        .HasColumnType("varchar")
        .HasMaxLength(150)
        .IsRequired();


        builder.Property(p => p.Password)
       .HasColumnName("Password")
       .HasColumnType("varchar")
       .HasMaxLength(150)
       .IsRequired();

        builder.Property(p => p.Email)
        .HasColumnName("Email")
        .HasColumnType("varchar")
        .HasMaxLength(150)
        .IsRequired();

        builder
        .HasMany(p => p.Roles)
        .WithMany(r => r.Usuario)
        .UsingEntity<UsuarioRol>(

           j => j
           .HasOne(pt => pt.Rol)
           .WithMany(t => t.UsuarioRoles)
           .HasForeignKey(ut => ut.RolId),


           j => j
           .HasOne(et => et.Usuario)
           .WithMany(et => et.UsuariosRol)
           .HasForeignKey(el => el.UserId),

           j =>
           {
               j.HasKey(t => new { t.UserId, t.RolId });

           });
    }
}