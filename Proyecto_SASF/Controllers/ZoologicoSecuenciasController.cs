using AGE.Utils.Paginacion;
using AGE.Utils.WebLink;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto_SASF.Entities.DAO.ZoologicoSecuenciasDAO;
using Proyecto_SASF.Middleware.Exceptions.BadRequest;
using Proyecto_SASF.Services.ZoologicoSecuenciasService;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_SASF.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ZoologicoSecuenciasController : ControllerBase
    {
        private readonly IZoologicoSecuenciasService _service;

        public ZoologicoSecuenciasController(IZoologicoSecuenciasService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Page<ZoologicoSecuenciasPrimariaDAO>), 200)]

        public async Task<IActionResult> ConsultarTodos(
            [FromQuery] Pageable pageable)
        {
            Page<ZoologicoSecuenciasPrimariaDAO> ageSecuenciasPrimariaDAOList =
                await _service.ConsultarTodos(pageable);

            return Ok(ageSecuenciasPrimariaDAOList);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ZoologicoSecuenciasPrimariaDAO), 200)]

        public async Task<IActionResult> ConsultarPorId(
            [FromRoute, Required] int id)
        {
            ZoologicoSecuenciasPrimariaDAO zooSecuenciasPrimariaDAO = await _service.ConsultarPorId(id);
            return Ok(zooSecuenciasPrimariaDAO);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Resource<ZoologicoSecuenciasPrimariaDAO>), 200)]

        public async Task<IActionResult> Insertar(
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromBody] ZoologicoSecuenciasSaveDAO zooSecuenciasPrimariasSaveDAO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            Resource<ZoologicoSecuenciasPrimariaDAO> zooSecuenciasPrimariaDAO =
                await _service.Insertar(httpContextAccessor, zooSecuenciasPrimariasSaveDAO);

            return Ok(zooSecuenciasPrimariaDAO);
        }


        [HttpPost]
        [Route("varios")]
        [ProducesResponseType(typeof(List<Resource<ZoologicoSecuenciasPrimariaDAO>>), 200)]

        public async Task<IActionResult> InsertarVarios(
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromBody] List<ZoologicoSecuenciasSaveDAO> zooSecuenciasPrimariasSaveDAO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<Resource<ZoologicoSecuenciasPrimariaDAO>> ageSecuenciasPrimariaDAOList =
                await _service.InsertarVarios(httpContextAccessor, zooSecuenciasPrimariasSaveDAO);

            return Ok(ageSecuenciasPrimariaDAOList);
        }


        [HttpPut]
        [ProducesResponseType(typeof(ZoologicoSecuenciasPrimariaDAO), 200)]

        public async Task<IActionResult> Actualizar(
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromBody] ZoologicoSecuenciasSaveDAO zooSecuenciasPrimariasSaveDAO)
        {
            ZoologicoSecuenciasPrimariaDAO zooSecuenciasPrimariaDAO =
                await _service.Actualizar(httpContextAccessor, zooSecuenciasPrimariasSaveDAO);

            return Ok(zooSecuenciasPrimariaDAO);
        }


        [HttpPut]
        [Route("varios")]
        [ProducesResponseType(typeof(List<ZoologicoSecuenciasPrimariaDAO>), 200)]

        public async Task<IActionResult> ActualizarVarios(
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromBody] List<ZoologicoSecuenciasSaveDAO> zooSecuenciasPrimariasSaveDAO)
        {
            List<ZoologicoSecuenciasPrimariaDAO> zooSecuenciasPrimariaDAOList =
                await _service.ActualizarVarios(httpContextAccessor, zooSecuenciasPrimariasSaveDAO);

            return Ok(zooSecuenciasPrimariaDAOList);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 200)]
        public async Task<IActionResult> EliminarPorId([FromRoute, Required] int id, [FromQuery] Pageable pageable)
        {
            await _service.EliminarPorId(id);

            Page<ZoologicoSecuenciasPrimariaDAO> ageSecuenciasPrimariaDAOList =await _service.ConsultarTodos(pageable);

            return Ok(ageSecuenciasPrimariaDAOList);
        }

    }
}
