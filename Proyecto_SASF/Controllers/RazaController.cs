using Proyecto_SASF.Utils.Paginacion;
using Proyecto_SASF.Utils.WebLink;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto_SASF.Entities.DAO.RazaDAO;
using Proyecto_SASF.Middleware.Exceptions.BadRequest;
using Proyecto_SASF.Services.RazasService;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_SASF.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize]
    public class RazaController : ControllerBase
    {

        private readonly IRazasService _service;

        public RazaController(IRazasService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Page<RazaDAO>), 200)]
        public async Task<IActionResult> ConsultarTodos([FromQuery] Pageable pageable)
        {
            Page<RazaDAO> razasDAOList =
                await _service.ConsultarTodos(pageable);

            return Ok(razasDAOList);
        }


        [HttpGet("filtro")]
        [ProducesResponseType(typeof(Page<RazaDAO>), 200)]
        public async Task<IActionResult> ConsultarListaFiltro(
            [FromQuery, Required] string filtro,
            [FromQuery] Pageable pageable)
        {
            Page<RazaDAO> razasDAOList =
                await _service.ConsultarListaFiltro(filtro, pageable);

            return Ok(razasDAOList);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RazaDAO), 200)]
        public async Task<IActionResult> ConsultarPorId(
            [FromRoute, Required] int id)
        {
            RazaDAO razaDAO = await _service.ConsultarPorId(id);
            return Ok(razaDAO);
        }



        [HttpPost]
        [ProducesResponseType(typeof(Resource<RazaDAO>), 200)]
        public async Task<IActionResult> Insertar(
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromBody] RazaSaveDAO razaSaveDAO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Resource<RazaDAO> razaDAO = await _service.Insertar(httpContextAccessor, razaSaveDAO);
            return Ok(razaDAO);
        }


        [HttpPost("varios")]
        [ProducesResponseType(typeof(List<Resource<RazaDAO>>), 200)]
        public async Task<IActionResult> InsertarVarios(
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromBody] List<RazaSaveDAO> razasSaveDAOList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<Resource<RazaDAO>> razasDAOList = await _service.InsertarVarios(httpContextAccessor, razasSaveDAOList);
            return Ok(razasDAOList);
        }


        [HttpPut]
        [ProducesResponseType(typeof(RazaDAO), 200)]
        public async Task<IActionResult> Actualizar(
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromBody] RazaSaveDAO razaSaveDAO)
        {
            RazaDAO razaDAO = await _service.Actualizar(httpContextAccessor, razaSaveDAO);
            return Ok(razaDAO);
        }


        [HttpPut("varios")]
        [ProducesResponseType(typeof(List<RazaDAO>), 200)]
        public async Task<IActionResult> ActualizarVarios(
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromBody] List<RazaSaveDAO> razasSaveDAOList)
        {
            List<RazaDAO> razaDAOList = await _service.ActualizarVarios(httpContextAccessor, razasSaveDAOList);
            return Ok(razaDAOList);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 200)]
        public async Task<IActionResult> EliminarPorId([FromRoute, Required] int id , [FromQuery] Pageable pageable)
        {
            await _service.EliminarPorId(id);

            Page<RazaDAO> razasDAOList = await _service.ConsultarTodos(pageable);


            return Ok(razasDAOList);
        }

    }
}
