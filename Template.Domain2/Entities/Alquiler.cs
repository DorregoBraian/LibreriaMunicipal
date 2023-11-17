namespace Template.Domain2.Entities
{
    public class Alquiler
    {
        public int AlquileresId { get; set; } //PK
        public int Cliente_idx { get; set; }
        public string ISBN_idx { get; set; }
        public int Estado_idx { get; set; }
        public DateTime? FechaAlquiler { get; set; }
        public DateTime? FechaReserva { get; set; }
        public DateTime? FechaDevolucion { get; set; }

        public EstadoDeAlquiler Estado { get; set; }//Fk
        public Libro ISBN { get; set; } //FK
        public Cliente Cliente { get; set; }//FK

    }
}
