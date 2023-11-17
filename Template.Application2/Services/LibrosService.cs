using AutoMapper;
using Template.Application2.Interfaces;
using Template.Domain2.Commands;
using Template.Domain2.Dtos;

namespace Template.Application2.Services
{
    public class LibrosService : ILibrosService
    {
        private readonly ILibrosRepository _librosRepository;
        private readonly IMapper _mapper;

        public LibrosService(ILibrosRepository repository, IMapper mapper)
        {
            _librosRepository = repository;
            _mapper = mapper;
        }

        //Devuelve true si hay Stock del Libro con el ISBN pasado por parametro 
        public string StockDisponible(int Stock, string isbn)
        {
            var libroEntity = _librosRepository.GetLibroByISBN(isbn);

            if (libroEntity != null)
            {
                if (libroEntity.Stock >= Stock)
                {
                    return "Si hay Stock de " + libroEntity.Titulo + " de " + libroEntity.Autor;
                }
                else
                {
                    return "Lo sentimos no hay suficiente Stock de " + libroEntity.Titulo + " de " + libroEntity.Autor;
                }
            }
            return null;

        }

        //Devuelve la informacion de un Libro segun el Stock,Autor o Titulo
        public List<LibroDto> InfoDeLibro(string? titulo = null, string? autor = null, bool? stock = null)
        {
            var listLibros = _librosRepository.GetInfoLibros(titulo, autor, stock);

            if (listLibros != null)
            {
                return _mapper.Map<List<LibroDto>>(listLibros);
            }
            return null;
        }
    }
}
