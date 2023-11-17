using Template.Domain2.Commands;
using Template.Domain2.Entities;

namespace Template.AccessData2.Commands
{
    public class AlquilerRepository : IAlquilerRepository
    {
        private readonly LibreriaDbContext _context;

        public AlquilerRepository(LibreriaDbContext context)
        {
            _context = context;
        }




        //Agrego Un Alquiler
        public void AddAlquiler(Alquiler alquiler)
        {
            _context.Alquileres.Add(alquiler);
            _context.SaveChanges();
        }

        //Actualizo Un Alquiler
        public void UpdateAlquiler(Alquiler alquiler)
        {
            _context.Alquileres.Update(alquiler);
            _context.SaveChanges();
        }

        //Borro Un Alquiler
        public void DeleteAlquiler(Alquiler alquiler)
        {
            _context.Alquileres.Remove(alquiler);
            _context.SaveChanges();
        }

        //Devuelve un Alquiler por el Id del CLiente
        public List<Alquiler> GetAlquilerByClienteId(int clienteId)
        {

            return _context.Alquileres.Where(a => a.Cliente_idx == clienteId).ToList();

        }

        //Devuelve un Alquiler por ISBN del Libre
        public Alquiler GetAlquilerByISBN(string isbn)
        {

            return _context.Alquileres.SingleOrDefault(a => a.ISBN_idx == isbn);

        }

        //Devuelve una Lista de todos los alquileres
        public List<Alquiler> GetAllAlquileres()
        {
            return _context.Alquileres.ToList();
        }

        //Devuelve una lista de Alquileres que tiene el Cliente de ese Libro
        public List<Alquiler> GetAlquilerByClienteIdAndISBNid(int clienteId, string isbn)
        {
            return _context.Alquileres.Where(a => a.Cliente_idx == clienteId && a.ISBN_idx == isbn).ToList();
        }

        //Devuelvo una Lista de Reserva (Alquile) o de Alquileres segun en Estado (1- Reserva, 2-Alquilado)
        public List<Alquiler> GetAlquilerByEstado(int estado)
        {
            return _context.Alquileres.Where(a => a.Estado_idx == estado).ToList();
        }




    }
}
