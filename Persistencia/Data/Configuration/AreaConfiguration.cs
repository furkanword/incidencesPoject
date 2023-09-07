using Dominio;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Configuration;
public class AreaConfiguration : IEntityTypeConfiguration<Area>
{
    public void Configure(EntityTypeBuilder<Area> builder)
    {
       
        builder.ToTable("Areas");

            builder.Property(p => p.Id)
            .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
            .HasColumnName("Id_Area")
            .HasColumnType("int")
            .IsRequired();

            builder.Property(p => p.Name_Area)
            .HasColumnName("Namearea")
            .HasColumnType("string")
            .IsRequired();


            builder.Property(p => p.Description_Area)
            .HasColumnName("Descriptionarea")
            .HasColumnType("varchar")
            .HasMaxLength(200)
            .IsRequired();


            builder.Property(p => p.Description_Incidence)
            .HasColumnName("Id_desc_Incidence")
            .HasColumnType("string")
            .IsRequired();
    }
}