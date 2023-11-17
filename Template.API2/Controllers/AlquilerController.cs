using Microsoft.AspNetCore.Mvc;
using Template.Application2.Interfaces;
using Template.Domain2.Dtos;


namespace Template.API2.Controllers
{
    [Route("api/alquiler")]
    [ApiController]
    public class AlquilerController : Controller
    {
        private readonly IAlquilerService _service;

        public AlquilerController(IAlquilerService service)
        {
            _service = service;
        }


        [HttpPost]
        [ProducesResponseType(typeof(AlquilerDtoForCreation), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult RegistrarAlquiler([FromBody] AlquilerDtoForCreation alquiler)
        {

            try
            {
                var alquilerResponse = _service.RegistrarAlquiler(alquiler);

                if (alquilerResponse != null)
                {
                    return new JsonResult(alquiler) { StatusCode = 201 };
                }

                return StatusCode(409, new RespuestaDto("Conflict"));
            }
            catch (Exception)
            {

                return StatusCode(500, "Internal Server Error");
            }

        }


        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult ReservaAAlquiler(ReservaDto reserva)
        {
            try
            {
                var resevaResponse = _service.DeReservaAAlquiler(reserva);

                if (resevaResponse != null)
                {
                    return new JsonResult(resevaResponse) { StatusCode = 201 };
                }

                return StatusCode(404, new RespuestaDto("NotFound"));
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<InfoAlquilerDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult InfoDeLibros([FromQuery] InfoDeEstadoAlquileresDto estadoDto)
        {
            try
            {
                var estadoDtoResponse = _service.InfoDeLibrosAlquilados(estadoDto);

                if (estadoDtoResponse != null)
                {
                    return new JsonResult(estadoDtoResponse) { StatusCode = 200 };
                }

                return StatusCode(404, new RespuestaDto("NotFound"));
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }

        }


        [Route("/api/alquiler/cliente/{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(List<LibroDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult InfoLibrosAlquiladosPorCliente(int id)
        {
            try
            {
                var idResponse = _service.InfoDeLibrosByClienteId(id);

                if (idResponse != null)
                {
                    return new JsonResult(idResponse) { StatusCode = 200 };
                }

                return StatusCode(404, new RespuestaDto("NotFound"));
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }

        }
    }
}
