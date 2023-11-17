using Template.Domain2.Dtos;


namespace Template.Application2.Interfaces
{
    public interface IAlquilerService
    {
        ReservaDto DeReservaAAlquiler(ReservaDto reservaDto);
        List<InfoAlquilerDto> InfoDeLibrosAlquilados(InfoDeEstadoAlquileresDto estadoDto);
        AlquilerDtoForCreation RegistrarAlquiler(AlquilerDtoForCreation alquiler);
        List<LibroDto> InfoDeLibrosByClienteId(int clienteId);
    }
}
