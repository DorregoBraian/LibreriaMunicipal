using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Template.AccessData2.Migrations
{
    public partial class LibreriaDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    ClienteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DNI = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.ClienteId);
                });

            migrationBuilder.CreateTable(
                name: "EstadoDeAlquileres",
                columns: table => new
                {
                    EstadoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoDeAlquileres", x => x.EstadoId);
                });

            migrationBuilder.CreateTable(
                name: "Libros",
                columns: table => new
                {
                    ISBNId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Autor = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Editorial = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Edicion = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libros", x => x.ISBNId);
                });

            migrationBuilder.CreateTable(
                name: "Alquileres",
                columns: table => new
                {
                    AlquileresId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cliente_idx = table.Column<int>(type: "int", nullable: false),
                    ISBN_idx = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Estado_idx = table.Column<int>(type: "int", nullable: false),
                    FechaAlquiler = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaReserva = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaDevolucion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alquileres", x => x.AlquileresId);
                    table.ForeignKey(
                        name: "FK_Alquileres_Cliente_Cliente_idx",
                        column: x => x.Cliente_idx,
                        principalTable: "Cliente",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Alquileres_EstadoDeAlquileres_Estado_idx",
                        column: x => x.Estado_idx,
                        principalTable: "EstadoDeAlquileres",
                        principalColumn: "EstadoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Alquileres_Libros_ISBN_idx",
                        column: x => x.ISBN_idx,
                        principalTable: "Libros",
                        principalColumn: "ISBNId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cliente",
                columns: new[] { "ClienteId", "Apellido", "DNI", "Email", "Nombre" },
                values: new object[] { 1, "Dorrego", "123456", "asd@hotmail.com", "Braian" });

            migrationBuilder.InsertData(
                table: "EstadoDeAlquileres",
                columns: new[] { "EstadoId", "Descripcion" },
                values: new object[,]
                {
                    { 1, "Reservado" },
                    { 2, "Alquilado" },
                    { 3, "Cancelado" }
                });

            migrationBuilder.InsertData(
                table: "Libros",
                columns: new[] { "ISBNId", "Autor", "Edicion", "Editorial", "Imagen", "Stock", "Titulo" },
                values: new object[,]
                {
                    { "9788416365166", "Homero", "Año de Edicion: 1906-1935", "Mestas Ediciones", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR8zUlIPFtdj6g4-x5LEbsQRxOJ1htoQ5Fr9w&usqp=CAU", 5, "Odisea" },
                    { "9788426105134", "Miguel de Cervantes", "Año de Edicion: 1605", "Mestas Ediciones", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSLAhuDxdCEH6S89mqvUI55Z5mD8e3W8E4UnQ&usqp=CAU", 17, "Don Quijote De La Mancha" },
                    { "9788893672573", "J. K. Rowling", "Año de Edicion: 2017", "Magazzini Salani", "https://http2.mlstatic.com/D_NQ_NP_998415-MLA47726243958_102021-V.jpg", 24, "Harry Potter y la Piedra Filosofal" },
                    { "9789875669062", "George Orwell", "Año de Edicion: 1945", "DEBOLS!LLO", "https://contentv2.tap-commerce.com/cover/large/9789875669062_1.jpg?id_com=1113", 9, "Rebelión En La Granja - Debolsillo" },
                    { "9789915404752", "Federico Lavagna", "Año de Edicion: 2011", "Edicion Del Autor", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSOv5FVmYrkoDfmKFsoiLRtrdj3dlf2WiWeTSKjXJ6OUZVDYaTApd5gj3-x76KpFP7Kfl4&usqp=CAU", 1, "Cómo Jubilarte A Los 40" },
                    { "9789974718456", "Diego Zas", "Año de Edicion: 2021", "EDICIONES B", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8SQ20DY4YDa0e9VzH7F7aOqLw3lBqWyu_nIa3CjsW5W8Qg9yK8i5SqR93h1eVVojAWwc&usqp=CAU", 24, "Los 90" },
                    { "9789974748637", "José Luis Inciarte", "Año de Edicion: 2017", "SUDAMERICANA", "https://m.media-amazon.com/images/I/51gSPaWeIHL.jpg", 5, "Memorias De Los Andes" },
                    { "9789974881891", "Darwin Desbocatti", "Año de Edicion: 2017", "SUDAMERICANA", "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1514618090l/37666512._SY475_.jpg", 3, "No Es Digno, Pero Es Legal" },
                    { "9789974903432", "Diego Fischer", "Año de Edicion:2006", "SUDAMERICANA", "https://www.planetadelibros.com.ar/usuaris/libros/fotos/338/m_libros/portada_el-robo-de-la-historia_diego-fischer_202107011404.jpg", 13, "El Robo De La Historia" },
                    { "9789974907874", "Haberkorn Leonardo", "Año de Edicion:2020", "Planeta", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQW3N7TRmp4vVK597UgzUrvscYtuN1DnoDLFOyV5V5Ki1-wE-17Bztc3pDWBsW8AfO9XFs&usqp=CAU", 11, "Herencia Maldita Historias De Los Años Duros" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alquileres_Cliente_idx",
                table: "Alquileres",
                column: "Cliente_idx");

            migrationBuilder.CreateIndex(
                name: "IX_Alquileres_Estado_idx",
                table: "Alquileres",
                column: "Estado_idx");

            migrationBuilder.CreateIndex(
                name: "IX_Alquileres_ISBN_idx",
                table: "Alquileres",
                column: "ISBN_idx");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alquileres");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "EstadoDeAlquileres");

            migrationBuilder.DropTable(
                name: "Libros");
        }
    }
}
