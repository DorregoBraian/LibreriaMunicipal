using AutoMapper;
using Template.Application2.Interfaces;
using Template.Domain2.Commands;
using Template.Domain2.Dtos;
using Template.Domain2.Entities;

namespace Template.Application2.Services
{
    public class AlquilerService : IAlquilerService
    {

        private readonly IAlquilerRepository _alquilerRepository;
        private readonly IClientesRepository _clientesRepository;
        private readonly ILibrosRepository _librosRepository;
        private readonly IMapper _mapper;

        public AlquilerService(IAlquilerRepository alquilerRepository, IClientesRepository clientesRepository, ILibrosRepository librosRepository, IMapper mapper)
        {
            _alquilerRepository = alquilerRepository;
            _clientesRepository = clientesRepository;
            _librosRepository = librosRepository;
            _mapper = mapper;
        }

        //Crea un DTO de Alquiler y lo Agrega a la DB
        public AlquilerDtoForCreation RegistrarAlquiler(AlquilerDtoForCreation alquilerDto)
        {
            //Validamos que no tengamos un cliente o libro en la DB 
            var clienteEntity = _clientesRepository.GetClienteById(alquilerDto.Cliente_idx);
            var libroEntity = _librosRepository.GetLibroByISBN(alquilerDto.ISBN_idx);

            if (clienteEntity != null && libroEntity != null)
            {
                //Chequeo si hay Stock Disponivle del Libro
                if (libroEntity.Stock <= 0) return null;

                //Verifico si es un Alquiler o una Reserva segun la Fechas
                if (alquilerDto.FechaAlquiler != null)
                {
                    var alquiler = _mapper.Map<Alquiler>(alquilerDto);
                    alquiler.Estado_idx = 2;
                    alquiler.FechaDevolucion = alquiler.FechaAlquiler.Value.AddDays(7);
                    _alquilerRepository.AddAlquiler(alquiler);
                    _librosRepository.RestarStock(libroEntity);
                    return alquilerDto;
                }
                else
                {
                    var alquiler = _mapper.Map<Alquiler>(alquilerDto);
                    alquiler.Estado_idx = 1;
                    _alquilerRepository.AddAlquiler(alquiler);
                    _librosRepository.RestarStock(libroEntity);
                    return alquilerDto;
                }
            }
            return null;

        }

        //Convierte un recerva en una alquiler
        public ReservaDto DeReservaAAlquiler(ReservaDto reservaDto)
        {
            //Traigo una lista de Alquileres por el cliente y el isbn
            var listAlquilerEntity = _alquilerRepository.GetAlquilerByClienteIdAndISBNid(reservaDto.Cliente_idx, reservaDto.ISBN_idx);

            if (listAlquilerEntity.Count > 0)
            {
                //Recorro la lista y actualizo los campos nesesarion para convertirla una reserva en un alquiler
                foreach (var i in listAlquilerEntity)
                {
                    i.Estado_idx = 2;
                    i.FechaAlquiler = DateTime.Now;
                    i.FechaReserva = null;
                    i.FechaDevolucion = DateTime.Now.AddDays(7);
                    _alquilerRepository.UpdateAlquiler(i);
                }
            }
            return reservaDto;
        }

        //Da una Informacion completa de los Alquileres y los Libros segun si estan alquilados o reservados
        public List<InfoAlquilerDto> InfoDeLibrosAlquilados(InfoDeEstadoAlquileresDto estadoDto)
        {
            //Traigo una lista de Alquiler o una lista de Reserva segun el Estado (1- Reserva, 2-Alquilado)
            var listAlquiler = _alquilerRepository.GetAlquilerByEstado(estadoDto.Estado_idx);
            List<InfoAlquilerDto> list = new List<InfoAlquilerDto>();

            if (listAlquiler != null)
            {
                foreach (Alquiler i in listAlquiler)
                {
                    var libroEntity = _librosRepository.GetLibroByISBN(i.ISBN_idx);

                    var AlquilerDto = new InfoAlquilerDto()
                    {
                        Cliente_idx = i.Cliente_idx,
                        Titulo = libroEntity.Titulo,
                        Autor = libroEntity.Autor,
                        FechaAlquiler = i.FechaAlquiler,
                        FechaReserva = i.FechaReserva,
                        FechaDevolucion = i.FechaDevolucion
                    };

                    list.Add(AlquilerDto);

                }
                return list;
            }
            return null;
        }

        //Devuelve los Libros Reservado o Alquilados de un cliente
        public List<LibroDto> InfoDeLibrosByClienteId(int clienteId)
        {
            var listAlquiler = _alquilerRepository.GetAlquilerByClienteId(clienteId);
            List<LibroDto> list = new List<LibroDto>();

            if (listAlquiler != null)
            {
                foreach (Alquiler i in listAlquiler)
                {
                    var libroEntity = _librosRepository.GetLibroByISBN(i.ISBN_idx);
                    var libroDto = new LibroDto()
                    {
                        ISBNId = i.ISBN_idx,
                        Autor = libroEntity.Autor,
                        Titulo = libroEntity.Titulo,
                        Editorial = libroEntity.Editorial,
                        Edicion = libroEntity.Edicion,
                        Imagen = libroEntity.Imagen,
                        Estado_Libro = i.Estado_idx

                    };
                    list.Add(libroDto);
                }
                return list;

            }
            return null;
        }

        public bool ValidarCamposAlquilere(InfoDeEstadoAlquileresDto estado)
        {
            if (estado.Estado_idx >= 3)
            {
                return true;
            }
            return false;
        }
    }
}
