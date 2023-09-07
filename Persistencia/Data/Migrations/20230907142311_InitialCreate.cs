using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistencia.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    Id_Area = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Namearea = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descriptionarea = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Id_Area);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ContactType",
                columns: table => new
                {
                    Id_ContactType = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NameContact = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description_ContactType = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactType", x => x.Id_ContactType);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "countries",
                columns: table => new
                {
                    IdPais = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombrePais = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_countries", x => x.IdPais);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LevelIncidence",
                columns: table => new
                {
                    Id_LevelIncidence = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name_LevelIncidence = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description_LevelIcidence = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LevelIncidence", x => x.Id_LevelIncidence);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    Id_Rol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NameRol = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descRol = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.Id_Rol);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TypeIncidence",
                columns: table => new
                {
                    Id_TypeIncidence = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NameTypeIncidence = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DescroptionTypeIncidence = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeIncidence", x => x.Id_TypeIncidence);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id_User = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NameUser = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id_User);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Place",
                columns: table => new
                {
                    Id_Place = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NamePlace = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DescriptionPlace = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_AreaOrigin = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Place", x => x.Id_Place);
                    table.ForeignKey(
                        name: "FK_Place_Areas_Id_AreaOrigin",
                        column: x => x.Id_AreaOrigin,
                        principalTable: "Areas",
                        principalColumn: "Id_Area",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "regions",
                columns: table => new
                {
                    Id_Region = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreRegion = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Pais = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_regions", x => x.Id_Region);
                    table.ForeignKey(
                        name: "FK_regions_countries_Id_Pais",
                        column: x => x.Id_Pais,
                        principalTable: "countries",
                        principalColumn: "IdPais",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UsuariosRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RolId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosRoles", x => new { x.UserId, x.RolId });
                    table.ForeignKey(
                        name: "FK_UsuariosRoles_Rol_RolId",
                        column: x => x.RolId,
                        principalTable: "Rol",
                        principalColumn: "Id_Rol",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuariosRoles_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id_User",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cities",
                columns: table => new
                {
                    Id_City = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NameCity = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Region = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cities", x => x.Id_City);
                    table.ForeignKey(
                        name: "FK_cities_regions_Id_Region",
                        column: x => x.Id_Region,
                        principalTable: "regions",
                        principalColumn: "Id_Region");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "addresses",
                columns: table => new
                {
                    Id_Address = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nameneigborhood = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TypeWay = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QuadranPrefix = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NumberWay = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NameVenereableWay = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NumberPlate = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id_Pa = table.Column<int>(type: "int", nullable: false),
                    Id_CityA = table.Column<int>(type: "int", nullable: false),
                    PaisId = table.Column<int>(type: "int", nullable: true),
                    CiudadId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_addresses", x => x.Id_Address);
                    table.ForeignKey(
                        name: "FK_addresses_cities_CiudadId",
                        column: x => x.CiudadId,
                        principalTable: "cities",
                        principalColumn: "Id_City");
                    table.ForeignKey(
                        name: "FK_addresses_countries_PaisId",
                        column: x => x.PaisId,
                        principalTable: "countries",
                        principalColumn: "IdPais");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AreaUser",
                columns: table => new
                {
                    Id_Area_User = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Area = table.Column<int>(type: "int", nullable: false),
                    Id_Persona = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaUser", x => x.Id_Area_User);
                    table.ForeignKey(
                        name: "FK_AreaUser_Areas_Id_Area",
                        column: x => x.Id_Area,
                        principalTable: "Areas",
                        principalColumn: "Id_Area",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CategoryContact",
                columns: table => new
                {
                    Id_CategoryContact = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ContactoId = table.Column<int>(type: "int", nullable: true),
                    Id_Category = table.Column<int>(type: "int", nullable: false),
                    Name_CategoryContact = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryContact", x => x.Id_CategoryContact);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id_Contact = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Persona = table.Column<int>(type: "int", nullable: false),
                    Type_Contact = table.Column<int>(type: "int", nullable: false),
                    Category_Contact = table.Column<int>(type: "int", nullable: false),
                    Description_Contact = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id_Contact);
                    table.ForeignKey(
                        name: "FK_Contact_CategoryContact_Category_Contact",
                        column: x => x.Category_Contact,
                        principalTable: "CategoryContact",
                        principalColumn: "Id_CategoryContact",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contact_ContactType_Type_Contact",
                        column: x => x.Type_Contact,
                        principalTable: "ContactType",
                        principalColumn: "Id_ContactType",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DocumentType",
                columns: table => new
                {
                    Id_DocumentType = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NameDocumentType = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContactoId = table.Column<int>(type: "int", nullable: true),
                    AbreviationDocumentTye = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentType", x => x.Id_DocumentType);
                    table.ForeignKey(
                        name: "FK_DocumentType_Contact_ContactoId",
                        column: x => x.ContactoId,
                        principalTable: "Contact",
                        principalColumn: "Id_Contact");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id_User = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    lastname = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    document_type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id_User);
                    table.ForeignKey(
                        name: "FK_Person_DocumentType_document_type",
                        column: x => x.document_type,
                        principalTable: "DocumentType",
                        principalColumn: "Id_DocumentType",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DetailIncidence",
                columns: table => new
                {
                    Id_DetailIncidence = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Incidence = table.Column<int>(type: "int", nullable: false),
                    Id_Peripheral = table.Column<int>(type: "int", nullable: false),
                    Id_TypeIncidence = table.Column<int>(type: "int", nullable: false),
                    Id_LevelIncidence = table.Column<int>(type: "int", nullable: false),
                    Id_State = table.Column<int>(type: "int", nullable: false),
                    Nameneigborhood = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailIncidence", x => x.Id_DetailIncidence);
                    table.ForeignKey(
                        name: "FK_DetailIncidence_LevelIncidence_Id_LevelIncidence",
                        column: x => x.Id_LevelIncidence,
                        principalTable: "LevelIncidence",
                        principalColumn: "Id_LevelIncidence",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetailIncidence_TypeIncidence_Id_TypeIncidence",
                        column: x => x.Id_TypeIncidence,
                        principalTable: "TypeIncidence",
                        principalColumn: "Id_TypeIncidence",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Peripheral",
                columns: table => new
                {
                    Id_Peripheral = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NamenePeripheral = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peripheral", x => x.Id_Peripheral);
                    table.ForeignKey(
                        name: "FK_Peripheral_DetailIncidence_Id_Peripheral",
                        column: x => x.Id_Peripheral,
                        principalTable: "DetailIncidence",
                        principalColumn: "Id_DetailIncidence",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    Id_State = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DetalleIncidenciaId = table.Column<int>(type: "int", nullable: true),
                    Description_State = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.Id_State);
                    table.ForeignKey(
                        name: "FK_State_DetailIncidence_DetalleIncidenciaId",
                        column: x => x.DetalleIncidenciaId,
                        principalTable: "DetailIncidence",
                        principalColumn: "Id_DetailIncidence");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Incidence",
                columns: table => new
                {
                    Id_Incidence = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id_Persona = table.Column<int>(type: "int", nullable: false),
                    Id_State = table.Column<int>(type: "int", nullable: false),
                    Id_Area = table.Column<int>(type: "int", nullable: false),
                    Id_Place = table.Column<int>(type: "int", nullable: false),
                    LugarId = table.Column<int>(type: "int", nullable: true),
                    DateIncidence = table.Column<DateTime>(type: "Date", nullable: false),
                    DescriptionIncidence = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Detail_Incidence = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidence", x => x.Id_Incidence);
                    table.ForeignKey(
                        name: "FK_Incidence_Areas_Id_Area",
                        column: x => x.Id_Area,
                        principalTable: "Areas",
                        principalColumn: "Id_Area",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Incidence_Person_Id_Persona",
                        column: x => x.Id_Persona,
                        principalTable: "Person",
                        principalColumn: "Id_User",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Incidence_Place_LugarId",
                        column: x => x.LugarId,
                        principalTable: "Place",
                        principalColumn: "Id_Place");
                    table.ForeignKey(
                        name: "FK_Incidence_State_Id_State",
                        column: x => x.Id_State,
                        principalTable: "State",
                        principalColumn: "Id_State",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_addresses_CiudadId",
                table: "addresses",
                column: "CiudadId");

            migrationBuilder.CreateIndex(
                name: "IX_addresses_Id_Pa",
                table: "addresses",
                column: "Id_Pa");

            migrationBuilder.CreateIndex(
                name: "IX_addresses_PaisId",
                table: "addresses",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_AreaUser_Id_Area",
                table: "AreaUser",
                column: "Id_Area");

            migrationBuilder.CreateIndex(
                name: "IX_AreaUser_Id_Persona",
                table: "AreaUser",
                column: "Id_Persona");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryContact_ContactoId",
                table: "CategoryContact",
                column: "ContactoId");

            migrationBuilder.CreateIndex(
                name: "IX_cities_Id_Region",
                table: "cities",
                column: "Id_Region");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_Category_Contact",
                table: "Contact",
                column: "Category_Contact");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_Id_Persona",
                table: "Contact",
                column: "Id_Persona");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_Type_Contact",
                table: "Contact",
                column: "Type_Contact");

            migrationBuilder.CreateIndex(
                name: "IX_DetailIncidence_Id_LevelIncidence",
                table: "DetailIncidence",
                column: "Id_LevelIncidence");

            migrationBuilder.CreateIndex(
                name: "IX_DetailIncidence_Id_State",
                table: "DetailIncidence",
                column: "Id_State");

            migrationBuilder.CreateIndex(
                name: "IX_DetailIncidence_Id_TypeIncidence",
                table: "DetailIncidence",
                column: "Id_TypeIncidence");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentType_ContactoId",
                table: "DocumentType",
                column: "ContactoId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidence_Id_Area",
                table: "Incidence",
                column: "Id_Area");

            migrationBuilder.CreateIndex(
                name: "IX_Incidence_Id_Persona",
                table: "Incidence",
                column: "Id_Persona");

            migrationBuilder.CreateIndex(
                name: "IX_Incidence_Id_State",
                table: "Incidence",
                column: "Id_State");

            migrationBuilder.CreateIndex(
                name: "IX_Incidence_LugarId",
                table: "Incidence",
                column: "LugarId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_document_type",
                table: "Person",
                column: "document_type");

            migrationBuilder.CreateIndex(
                name: "IX_Place_Id_AreaOrigin",
                table: "Place",
                column: "Id_AreaOrigin");

            migrationBuilder.CreateIndex(
                name: "IX_regions_Id_Pais",
                table: "regions",
                column: "Id_Pais");

            migrationBuilder.CreateIndex(
                name: "IX_State_DetalleIncidenciaId",
                table: "State",
                column: "DetalleIncidenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_User_NameUser",
                table: "User",
                column: "NameUser",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosRoles_RolId",
                table: "UsuariosRoles",
                column: "RolId");

            migrationBuilder.AddForeignKey(
                name: "FK_addresses_Person_Id_Pa",
                table: "addresses",
                column: "Id_Pa",
                principalTable: "Person",
                principalColumn: "Id_User",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AreaUser_Person_Id_Persona",
                table: "AreaUser",
                column: "Id_Persona",
                principalTable: "Person",
                principalColumn: "Id_User",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryContact_Contact_ContactoId",
                table: "CategoryContact",
                column: "ContactoId",
                principalTable: "Contact",
                principalColumn: "Id_Contact");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Person_Id_Persona",
                table: "Contact",
                column: "Id_Persona",
                principalTable: "Person",
                principalColumn: "Id_User",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetailIncidence_Incidence_Id_DetailIncidence",
                table: "DetailIncidence",
                column: "Id_DetailIncidence",
                principalTable: "Incidence",
                principalColumn: "Id_Incidence",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetailIncidence_State_Id_State",
                table: "DetailIncidence",
                column: "Id_State",
                principalTable: "State",
                principalColumn: "Id_State",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Person_Id_Persona",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Incidence_Person_Id_Persona",
                table: "Incidence");

            migrationBuilder.DropForeignKey(
                name: "FK_Incidence_Areas_Id_Area",
                table: "Incidence");

            migrationBuilder.DropForeignKey(
                name: "FK_Place_Areas_Id_AreaOrigin",
                table: "Place");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryContact_Contact_ContactoId",
                table: "CategoryContact");

            migrationBuilder.DropForeignKey(
                name: "FK_DetailIncidence_Incidence_Id_DetailIncidence",
                table: "DetailIncidence");

            migrationBuilder.DropForeignKey(
                name: "FK_DetailIncidence_LevelIncidence_Id_LevelIncidence",
                table: "DetailIncidence");

            migrationBuilder.DropForeignKey(
                name: "FK_DetailIncidence_State_Id_State",
                table: "DetailIncidence");

            migrationBuilder.DropTable(
                name: "addresses");

            migrationBuilder.DropTable(
                name: "AreaUser");

            migrationBuilder.DropTable(
                name: "Peripheral");

            migrationBuilder.DropTable(
                name: "UsuariosRoles");

            migrationBuilder.DropTable(
                name: "cities");

            migrationBuilder.DropTable(
                name: "Rol");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "regions");

            migrationBuilder.DropTable(
                name: "countries");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "DocumentType");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "CategoryContact");

            migrationBuilder.DropTable(
                name: "ContactType");

            migrationBuilder.DropTable(
                name: "Incidence");

            migrationBuilder.DropTable(
                name: "Place");

            migrationBuilder.DropTable(
                name: "LevelIncidence");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DropTable(
                name: "DetailIncidence");

            migrationBuilder.DropTable(
                name: "TypeIncidence");
        }
    }
}
