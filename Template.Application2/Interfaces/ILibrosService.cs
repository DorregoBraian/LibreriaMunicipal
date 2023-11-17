using Template.Domain2.Dtos;

namespace Template.Application2.Interfaces
{
    public interface ILibrosService
    {
        public string StockDisponible(int Stock, string isbn);
        public List<LibroDto> InfoDeLibro(string? titulo = null, string? autor = null, bool? stock = null);

    }
}
