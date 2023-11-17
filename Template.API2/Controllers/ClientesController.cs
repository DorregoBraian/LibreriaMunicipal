using Microsoft.AspNetCore.Mvc;
using Template.Application2.Interfaces;
using Template.Domain2.Dtos;

namespace Template.API2.Controllers
{
    [Route("api/clientes")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClientesService _service;

        public ClientesController(IClientesService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ClienteDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetClientes(string? nombre = null, string? apellido = null, string? dni = null)
        {
            try
            {
                var dataResponse = _service.GetCliente(nombre, apellido, dni);

                if (dataResponse != null)
                {
                    return new JsonResult(dataResponse) { StatusCode = 200 };
                }
                return StatusCode(404, new RespuestaDto("NotFound"));
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ClienteForCreationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RegistrarCliente([FromBody] ClienteForCreationDto cliente)
        {
            try
            {
                var clienteResponse = _service.RegistrarCliente(cliente);

                if (clienteResponse.StatusCode == 201)
                {
                    return new JsonResult(cliente) { StatusCode = 201 };
                }
                if (clienteResponse.StatusCode == 400)
                {
                    return StatusCode(400, new RespuestaDto("Campos Vacion, Campo con la Informacion Incorecta o Mail Invalido"));
                }
                return StatusCode(409, new RespuestaDto("El DNI Ingresado ya Existe"));

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
