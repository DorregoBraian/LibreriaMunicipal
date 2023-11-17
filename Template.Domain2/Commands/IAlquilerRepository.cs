using Template.Domain2.Entities;

namespace Template.Domain2.Commands
{
    public interface IAlquilerRepository
    {

        void AddAlquiler(Alquiler alquiler);
        void DeleteAlquiler(Alquiler alquiler);
        void UpdateAlquiler(Alquiler alquiler);
        List<Alquiler> GetAlquilerByClienteId(int clienteId);
        Alquiler GetAlquilerByISBN(string isbn);
        List<Alquiler> GetAllAlquileres();
        List<Alquiler> GetAlquilerByClienteIdAndISBNid(int clienteId, string isbn);
        List<Alquiler> GetAlquilerByEstado(int estado);

    }
}
