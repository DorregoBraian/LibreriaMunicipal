using Template.Domain2.Dtos;

namespace Template.Application2.Interfaces
{
    public interface IClientesService
    {
        List<ClienteDto> GetCliente(string nombre = null, string apellido = null, string dni = null);
        ClienteRespuestaDto RegistrarCliente(ClienteForCreationDto cliente);

    }
}
