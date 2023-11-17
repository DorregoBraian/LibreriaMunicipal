namespace Template.Domain2.Dtos
{
    public class InfoAlquilerDto
    {


        public int Cliente_idx { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public DateTime? FechaAlquiler { get; set; }
        public DateTime? FechaReserva { get; set; }
        public DateTime? FechaDevolucion { get; set; }

    }
}
