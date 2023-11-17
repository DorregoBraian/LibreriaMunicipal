using Template.Domain2.Entities;

namespace Template.Domain2.Commands
{
    public interface IClientesRepository
    {
        void AddCliente(Cliente cliente);
        void DeleteCliente(Cliente cliente);
        void DeleteById(int id);
        void UpdateCliente(Cliente cliente);
        Cliente GetClienteById(int id);
        Cliente GetClienteByEmailOrDni(string email, string dni);
        List<Cliente> GetAllClientes();
        List<Cliente> GetCliente(string? nombre = null, string? apellido = null, string? dni = null);

    }
}
