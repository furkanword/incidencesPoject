using Dominio;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Configuration;
public class ContactoConfiguration : IEntityTypeConfiguration<Contacto>
{
    public void Configure(EntityTypeBuilder<Contacto> builder)
    {
        builder.ToTable("Contact");



                builder.Property(p => p.Id)
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasColumnName("Id_Contact")
                .HasColumnType("int")
                .IsRequired();


                builder.Property(p => p.Id_TypeContact)
                .HasColumnName("Type_Contact")
                .HasColumnType("int")
                .IsRequired();

                builder.Property(p => p.Id_CategoryContact)
                .HasColumnName("Category_Contact")
                .HasColumnType("int")
                .IsRequired();


                builder.Property(p => p.Description_Contact)
                .HasColumnName("Description_Contact")
                .HasColumnType("varchar")
                .HasMaxLength(200)
                .IsRequired();

                builder.HasOne(y => y.Persona)
                .WithMany(l => l.Contactos)
                .HasForeignKey(z => z.Id_Persona)
                .IsRequired();

                builder.HasOne(y => y.TipoContacto)
                .WithMany(l => l.Contactos)
                .HasForeignKey(z => z.Id_TypeContact)
                .IsRequired();

                builder.HasOne(y => y.CategoriaContacto)
                .WithMany(l => l.Contactos)
                .HasForeignKey(z => z.Id_CategoryContact)
                .IsRequired();

            

    }
}