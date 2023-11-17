namespace Template.Domain2.Dtos
{
    public class RespuestaDto
    {
        public RespuestaDto(string mensaje)
        {
            this.mensaje = mensaje;
        }

        public string mensaje { get; set; }
    }
}

