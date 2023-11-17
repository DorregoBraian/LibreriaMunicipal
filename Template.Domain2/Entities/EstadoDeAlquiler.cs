namespace Template.Domain2.Entities
{
    public class EstadoDeAlquiler
    {
        public int EstadoId { get; set; }   //Pk
        public string Descripcion { get; set; }

        public EstadoDeAlquiler()
        {
            Alquileres = new HashSet<Alquiler>();
        }
        //Ancla 
        public virtual ICollection<Alquiler> Alquileres { get; set; }
    }
}
