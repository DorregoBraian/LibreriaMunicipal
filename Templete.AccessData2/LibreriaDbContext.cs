using Microsoft.EntityFrameworkCore;
using Template.Domain2.Entities;

namespace Template.AccessData2
{
    public class LibreriaDbContext : DbContext
    {
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Alquiler> Alquileres { get; set; }
        public DbSet<EstadoDeAlquiler> EstadoDeAlquileres { get; set; }

        public LibreriaDbContext() { }

        public LibreriaDbContext(DbContextOptions<LibreriaDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alquiler>(entity =>
            {
                entity.ToTable("Alquileres");
                entity.HasKey(a => a.AlquileresId);
                entity.Property(a => a.Cliente_idx);
                entity.Property(a => a.ISBN_idx);
                entity.Property(a => a.Estado_idx);
                entity.Property(a => a.FechaAlquiler);
                entity.Property(a => a.FechaReserva);
                entity.Property(a => a.FechaDevolucion);

                // un cliente tiene muchos alquileres
                entity.HasOne(c => c.Cliente).WithMany(a => a.Alquileres).HasForeignKey((c => c.Cliente_idx));
                // un estado tiene muchos alquileres
                entity.HasOne(e => e.Estado).WithMany(a => a.Alquileres).HasForeignKey((e => e.Estado_idx));
                // un ISBN tiene muchos alquileres
                entity.HasOne(l => l.ISBN).WithMany(a => a.Alquileres).HasForeignKey((l => l.ISBN_idx));
            });
            modelBuilder.Entity<EstadoDeAlquiler>(entity =>
            {
                entity.ToTable("EstadoDeAlquileres");
                entity.HasKey(e => e.EstadoId);
                entity.Property(e => e.Descripcion).HasMaxLength(45).IsRequired();
            });
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente");
                entity.HasKey(c => c.ClienteId);
                entity.Property(c => c.DNI).HasMaxLength(10).IsRequired();
                entity.Property(c => c.Nombre).HasMaxLength(45).IsRequired();
                entity.Property(c => c.Apellido).HasMaxLength(45).IsRequired();
                entity.Property(c => c.Email).HasMaxLength(45).IsRequired();
            });
            modelBuilder.Entity<Libro>(entity =>
            {
                entity.ToTable("Libros");
                entity.HasKey(l => l.ISBNId);
                entity.Property(l => l.Titulo).HasMaxLength(45).IsRequired();
                entity.Property(l => l.Autor).HasMaxLength(45).IsRequired();
                entity.Property(l => l.Editorial).HasMaxLength(45).IsRequired();
                entity.Property(l => l.Edicion).HasMaxLength(45).IsRequired();
                entity.Property(l => l.Stock).IsRequired();
                entity.Property(l => l.Imagen).HasMaxLength(255).IsRequired();
            });

            modelBuilder.Entity<EstadoDeAlquiler>().HasData(
                new EstadoDeAlquiler { EstadoId = 1, Descripcion = "Reservado" },
                new EstadoDeAlquiler { EstadoId = 2, Descripcion = "Alquilado" },
                new EstadoDeAlquiler { EstadoId = 3, Descripcion = "Cancelado" }
                );

            modelBuilder.Entity<Cliente>().HasData(
                new Cliente { ClienteId = 1, DNI = "123456", Apellido = "Dorrego", Nombre = "Braian", Email = "asd@hotmail.com"}
                );
            
            modelBuilder.Entity<Libro>().HasData(

                new Libro { ISBNId = "9788893672573", Titulo = "Harry Potter y la Piedra Filosofal", Autor = "J. K. Rowling", Editorial = "Magazzini Salani", Edicion = "Año de Edicion: 2017", Stock = 24, Imagen = "https://http2.mlstatic.com/D_NQ_NP_998415-MLA47726243958_102021-V.jpg" },
                new Libro { ISBNId = "9788426105134", Titulo = "Don Quijote De La Mancha", Autor = "Miguel de Cervantes", Editorial = "Mestas Ediciones", Edicion = "Año de Edicion: 1605", Stock = 17, Imagen = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSLAhuDxdCEH6S89mqvUI55Z5mD8e3W8E4UnQ&usqp=CAU" },
                new Libro { ISBNId = "9789875669062", Titulo = "Rebelión En La Granja - Debolsillo", Autor = "George Orwell", Editorial = "DEBOLS!LLO", Edicion = "Año de Edicion: 1945", Stock = 9, Imagen = "https://contentv2.tap-commerce.com/cover/large/9789875669062_1.jpg?id_com=1113" },
                new Libro { ISBNId = "9788416365166", Titulo = "Odisea", Autor = "Homero", Editorial = "Mestas Ediciones", Edicion = "Año de Edicion: 1906-1935", Stock = 5, Imagen = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR8zUlIPFtdj6g4-x5LEbsQRxOJ1htoQ5Fr9w&usqp=CAU" },
                new Libro { ISBNId = "9789974748637", Titulo = "Memorias De Los Andes", Autor = "José Luis Inciarte", Editorial = "SUDAMERICANA", Edicion = "Año de Edicion: 2017", Stock = 5, Imagen = "https://m.media-amazon.com/images/I/51gSPaWeIHL.jpg" },
                new Libro { ISBNId = "9789974903432", Titulo = "El Robo De La Historia", Autor = "Diego Fischer", Editorial = "SUDAMERICANA", Edicion = "Año de Edicion:2006", Stock = 13, Imagen = "https://www.planetadelibros.com.ar/usuaris/libros/fotos/338/m_libros/portada_el-robo-de-la-historia_diego-fischer_202107011404.jpg" },
                new Libro { ISBNId = "9789974881891", Titulo = "No Es Digno, Pero Es Legal", Autor = "Darwin Desbocatti", Editorial = "SUDAMERICANA", Edicion = "Año de Edicion: 2017", Stock = 3, Imagen = "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1514618090l/37666512._SY475_.jpg" },
                new Libro { ISBNId = "9789915404752", Titulo = "Cómo Jubilarte A Los 40", Autor = "Federico Lavagna", Editorial = "Edicion Del Autor", Edicion = "Año de Edicion: 2011", Stock = 1, Imagen = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSOv5FVmYrkoDfmKFsoiLRtrdj3dlf2WiWeTSKjXJ6OUZVDYaTApd5gj3-x76KpFP7Kfl4&usqp=CAU" },
                new Libro { ISBNId = "9789974907874", Titulo = "Herencia Maldita Historias De Los Años Duros", Autor = "Haberkorn Leonardo", Editorial = "Planeta", Edicion = "Año de Edicion:2020", Stock = 11, Imagen = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQW3N7TRmp4vVK597UgzUrvscYtuN1DnoDLFOyV5V5Ki1-wE-17Bztc3pDWBsW8AfO9XFs&usqp=CAU" },
                new Libro { ISBNId = "9789974718456", Titulo = "Los 90", Autor = "Diego Zas", Editorial = "EDICIONES B", Edicion = "Año de Edicion: 2021", Stock = 24, Imagen = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8SQ20DY4YDa0e9VzH7F7aOqLw3lBqWyu_nIa3CjsW5W8Qg9yK8i5SqR93h1eVVojAWwc&usqp=CAU" }

                );
        }

    }
}
