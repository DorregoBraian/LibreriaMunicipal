using Template.Domain2.Commands;
using Template.Domain2.Entities;

namespace Template.AccessData2.Commands
{
    public class ClientesRepository : IClientesRepository
    {
        private readonly LibreriaDbContext _context;

        public ClientesRepository(LibreriaDbContext context)
        {
            _context = context;
        }

        //Agrego Un Cliente
        public void AddCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            _context.SaveChanges();
        }

        //Actualizo Un Cliente
        public void UpdateCliente(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            _context.SaveChanges();
        }

        //Borro Un Cliente
        public void DeleteCliente(Cliente cliente)
        {
            _context.Clientes.Remove(cliente);
            _context.SaveChanges();
        }

        //Borro Un Cliente Por ID
        public void DeleteById(int id)
        {
            DeleteCliente(GetClienteById(id));
        }

        //Devuelve un Clientes por el Correo Electrónico o por DNI
        public Cliente GetClienteByEmailOrDni(string email, string dni)
        {
            return _context.Clientes.SingleOrDefault(c => c.Email == email || c.DNI == dni);
        }

        //Devuelve un Clientes por ID
        public Cliente GetClienteById(int id)
        {
            return _context.Clientes.Find(id);
        }

        //Devuelve Una Lista De Todos Las Clientes
        public List<Cliente> GetAllClientes()
        {
            return _context.Clientes.ToList();
        }

        //Devuelve una Lista De Todos Las Clientes Por Nombre, Apellodo y DNI
        public List<Cliente> GetCliente(string? nombre = null, string? apellido = null, string? dni = null)
        {
            return _context.Clientes.
                                    Where(cliente => (string.IsNullOrEmpty(nombre) || cliente.Nombre == nombre) &&
                                         (string.IsNullOrEmpty(apellido) || cliente.Apellido == apellido) &&
                                         (string.IsNullOrEmpty(dni) || cliente.DNI == dni)).ToList();
        }




    }
}
