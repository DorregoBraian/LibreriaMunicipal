using Template.Domain2.Entities;

namespace Template.Domain2.Commands
{
    public interface ILibrosRepository
    {
        void AddLibro(Libro libro);
        void DeleteLibro(Libro libro);
        void DeleteByISBN(string ISBN);
        void UpdateLibro(Libro libro);
        int? GetLibroStock(string isbn);
        bool HasStock(Libro libro);
        Libro GetLibroByISBN(string ISBN);
        List<Libro> GetAllLibros();
        public List<Libro> GetInfoLibros(string? titulo = null, string? autor = null, bool? stock = null);
        void RestarStock(Libro libro);
        void SumarStock(Libro libro);
    }
}
