using Template.Domain2.Commands;
using Template.Domain2.Entities;

namespace Template.AccessData2.Commands
{
    public class LibrosRepository : ILibrosRepository
    {
        private readonly LibreriaDbContext _context;

        public LibrosRepository(LibreriaDbContext context)
        {
            _context = context;
        }

        //Agrego un Libro
        public void AddLibro(Libro libro)
        {
            _context.Libros.Add(libro);
            _context.SaveChanges();
        }

        //Borro un Libro
        public void DeleteLibro(Libro libro)
        {
            _context.Libros.Remove(libro);
            _context.SaveChanges();
        }

        //Actualizo un Libro
        public void UpdateLibro(Libro libro)
        {
            _context.Libros.Update(libro);
            _context.SaveChanges();
        }


        //Borro un Libro por el ISBN 
        public void DeleteByISBN(string ISBN)
        {
            DeleteLibro(GetLibroByISBN(ISBN));
        }

        //Debuelvo una Lista de todos los Libros
        public List<Libro> GetAllLibros()
        {
            return _context.Libros.ToList();
        }

        //Devuelvo el Libro por el ISBN 
        public Libro GetLibroByISBN(string ISBN)
        {
            return _context.Libros.SingleOrDefault(libro => libro.ISBNId == ISBN);
        }

        //Devuelvo el Stock del Libro que concuerde con el ISBN
        public int? GetLibroStock(string isbn)
        {
            return GetLibroByISBN(isbn).Stock;
        }

        //Devuelve una Lista de Libros por Titulo y/o Autor y/o Stock
        public List<Libro> GetInfoLibros(string? titulo = null, string? autor = null, bool? stock = null)
        {

            if (stock == true)
            {
                return _context.Libros.
                                    Where(libro => (string.IsNullOrEmpty(titulo) || libro.Titulo == titulo) &&
                                         (string.IsNullOrEmpty(autor) || libro.Autor == autor) &&
                                         (stock == null || (libro.Stock > 0))).ToList();
            }
            if (stock == false)
            {
                return _context.Libros.
                                    Where(libro => (string.IsNullOrEmpty(titulo) || libro.Titulo == titulo) &&
                                         (string.IsNullOrEmpty(autor) || libro.Autor == autor) &&
                                         (stock == null || !(libro.Stock <= 0) == stock)).ToList();
            }
            return _context.Libros.
                                    Where(libro => (string.IsNullOrEmpty(titulo) || libro.Titulo == titulo) &&
                                         (string.IsNullOrEmpty(autor) || libro.Autor == autor)).ToList();
        }

        public bool HasStock(Libro libro)
        {
            if (libro.Stock <= 0)
            {
                return false;
            }
            else
                return true;
        }
        public void RestarStock(Libro libro)
        {
            var l = GetLibroByISBN(libro.ISBNId);
            l.Stock--;
            _context.SaveChanges();
        }

        public void SumarStock(Libro libro)
        {
            var l = GetLibroByISBN(libro.ISBNId);
            l.Stock++;
            _context.SaveChanges();
        }

    }
}
