using Microsoft.AspNetCore.Mvc;
using Template.Application2.Interfaces;
using Template.Domain2.Dtos;

namespace Template.API2.Controllers
{
    [Route("api/libros")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly ILibrosService _service;

        public LibrosController(ILibrosService service)
        {
            _service = service;
        }


        [Route("/api/libros/{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult InfoDeLibros([FromQuery] LibroDtoStock stockDto, string id)
        {
            try
            {
                var libroResponse = _service.StockDisponible(stockDto.Stock, id);

                if (libroResponse != null)
                {
                    return new JsonResult(libroResponse) { StatusCode = 200 };
                }
                return StatusCode(404, new RespuestaDto("NotFound"));
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }

        }


        [HttpGet]
        [ProducesResponseType(typeof(List<LibroDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult InfoDeLibrosByFiltros(string? titulo = null, string? autor = null, bool? stock = null)
        {
            try
            {
                return new JsonResult(_service.InfoDeLibro(titulo, autor, stock)) { StatusCode = 200 };
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
