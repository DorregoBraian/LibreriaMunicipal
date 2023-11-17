namespace Template.Domain2.Entities
{
    public class Libro
    {
        public string ISBNId { get; set; }  //PK
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Editorial { get; set; }
        public string Edicion { get; set; }
        public int? Stock { get; set; }
        public string Imagen { get; set; }

        public Libro()
        {
            Alquileres = new HashSet<Alquiler>();
        }
        //Ancla 
        public virtual ICollection<Alquiler> Alquileres { get; set; }
    }
}
