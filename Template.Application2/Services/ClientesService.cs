using AutoMapper;
using Template.Application2.Interfaces;
using Template.Domain2.Commands;
using Template.Domain2.Dtos;
using Template.Domain2.Entities;

namespace Template.Application2.Services
{
    public class ClientesService : IClientesService
    {
        private readonly IClientesRepository _clientesRepository;
        private readonly IMapper _mapper;

        public ClientesService(IClientesRepository repository, IMapper mapper)
        {
            _clientesRepository = repository;
            _mapper = mapper;
        }


        //Devuelve una Lista de ClienteDto por DNI, Nombre y Apellodo
        public List<ClienteDto> GetCliente(string? nombre = null, string? apellido = null, string? dni = null)
        {
            var clienteEntity = _clientesRepository.GetCliente(nombre, apellido, dni);

            if (clienteEntity != null)
            {

                return _mapper.Map<List<ClienteDto>>(clienteEntity);

            }

            return null;
        }

        //Crea un Cliente y lo Agrega a la DB
        public ClienteRespuestaDto RegistrarCliente(ClienteForCreationDto cliente)
        {
            ClienteRespuestaDto respuesta = new ClienteRespuestaDto()
            {
                Apellido = cliente.Apellido,
                Nombre = cliente.Nombre,
                DNI = cliente.Dni,
                Email = cliente.Email
            };

            if (ValidarCamposCliente(cliente))
            {
                var clienteEntity = _clientesRepository.GetClienteByEmailOrDni(cliente.Email, cliente.Dni);

                if (clienteEntity == null)
                {
                    _clientesRepository.AddCliente(_mapper.Map<Cliente>(cliente));
                    respuesta.StatusCode = 201;
                    return respuesta;
                }
                respuesta.StatusCode = 409;
                return respuesta;
            }
            respuesta.StatusCode = 400;
            return respuesta;
        }

        public bool ValidarCamposCliente(ClienteForCreationDto cliente)
        {
            if ((cliente.Nombre == "") || (cliente.Apellido == "") || (cliente.Dni == "") || (cliente.Email == "")) return false;
            if (!cliente.Email.Contains("@") || !cliente.Email.Contains(".com")) return false;
            return true;
        }

    }
}
