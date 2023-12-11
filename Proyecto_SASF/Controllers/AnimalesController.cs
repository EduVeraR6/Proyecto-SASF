using AGE.Utils.Paginacion;
using AGE.Utils.WebLink;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto_SASF.Entities.DAO.AnimalesDAO;
using Proyecto_SASF.Middleware.Exceptions.BadRequest;
using Proyecto_SASF.Services.AnimalesService;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_SASF.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class AnimalesController : ControllerBase
    {
        private readonly IAnimalesService _service;

        public AnimalesController(IAnimalesService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Page<AnimalesDAO>), 200)]
        public async Task<IActionResult> ConsultarTodos([FromQuery] Pageable pageable)
        {
            Page<AnimalesDAO> animalesDAOList =
                await _service.ConsultarTodos(pageable);

            return Ok(animalesDAOList);
        }


        [HttpGet("filtro")]
        [ProducesResponseType(typeof(Page<AnimalesDAO>), 200)]
        public async Task<IActionResult> ConsultarListaFiltro(
            [FromQuery, Required] string filtro,
            [FromQuery] Pageable pageable)
        {
            Page<AnimalesDAO> animalesDAOList =
                await _service.ConsultarListaFiltro(filtro, pageable);

            return Ok(animalesDAOList);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AnimalesDAO), 200)]
        public async Task<IActionResult> ConsultarPorId(
            [FromRoute, Required] int id)
        {
            AnimalesDAO animalDAO = await _service.ConsultarPorId(id);
            return Ok(animalDAO);
        }



        [HttpPost]
        [ProducesResponseType(typeof(Resource<AnimalesDAO>), 200)]
        public async Task<IActionResult> Insertar(
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromBody] AnimalesSaveDAO animalSaveDAO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Resource<AnimalesDAO> animalDAO = await _service.Insertar(httpContextAccessor, animalSaveDAO);
            return Ok(animalDAO);
        }


        [HttpPost("varios")]
        [ProducesResponseType(typeof(List<Resource<AnimalesDAO>>), 200)]
        public async Task<IActionResult> InsertarVarios(
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromBody] List<AnimalesSaveDAO> animalesDAOList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<Resource<AnimalesDAO>> agePaisesDAOList = await _service.InsertarVarios(httpContextAccessor, animalesDAOList);
            return Ok(agePaisesDAOList);
        }


        [HttpPut]
        [ProducesResponseType(typeof(AnimalesDAO), 200)]
        public async Task<IActionResult> Actualizar(
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromBody] AnimalesUpdateDAO animalSaveDAO)
        {
            AnimalesDAO animalDAO = await _service.Actualizar(httpContextAccessor, animalSaveDAO);
            return Ok(animalDAO);
        }


        [HttpPut("varios")]
        [ProducesResponseType(typeof(List<AnimalesDAO>), 200)]
        public async Task<IActionResult> ActualizarVarios(
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromBody] List<AnimalesUpdateDAO> animalesSaveDAOList)
        {
            List<AnimalesDAO> agePaisesDAOList = await _service.ActualizarVarios(httpContextAccessor, animalesSaveDAOList);
            return Ok(agePaisesDAOList);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 200)]
        public async Task<IActionResult> EliminarPorId([FromRoute, Required] int id , [FromQuery] Pageable pageable)
        {
            await _service.EliminarPorId(id);

            Page<AnimalesDAO> animalesDAOList = await _service.ConsultarTodos(pageable);

            return Ok(animalesDAOList);
        }

        [HttpGet("estadisticas")]
        [ProducesResponseType(typeof(List<AnimalesEstadisticasDAO>), 200)]
        public AnimalesEstadisticasDAO ObtenerEstadisticas()
        {
            return _service.ObtenerEstadisticas();
        }



    }
}
