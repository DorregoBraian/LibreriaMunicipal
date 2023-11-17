namespace Template.Domain2.Entities
{
    public class Cliente
    {
        public Cliente()
        {
            Alquileres = new HashSet<Alquiler>();
        }
        //Ancla 
        public virtual ICollection<Alquiler> Alquileres { get; set; }

        public int ClienteId { get; set; }    //PK
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }




    }
}
