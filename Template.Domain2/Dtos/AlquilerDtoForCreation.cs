namespace Template.Domain2.Dtos
{
    public class AlquilerDtoForCreation
    {

        public int Cliente_idx { get; set; }
        public string ISBN_idx { get; set; }
        public DateTime? FechaAlquiler { get; set; }
        public DateTime? FechaReserva { get; set; }
    }
}
