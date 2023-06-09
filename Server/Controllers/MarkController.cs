using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dziennik.Server.Controllers
{
    [ApiController]
    [Route("api/mark")]
    public class MarkController : ControllerBase
    {
        private readonly IMarkService _service;

        public MarkController(IMarkService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<MarkResponse>>> GetMarks()
        {
            var serviceResponse = _service.GetMarks();
            if (serviceResponse == null)
            {
                return BadRequest(serviceResponse);
            }

            return Ok(serviceResponse);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<MarkResponse>> GetMark(int id)
        {
            var serviceResponse = _service.GetMark(id);
            if (serviceResponse == null)
            {
                return NotFound(serviceResponse);
            }

            return Ok(serviceResponse);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<bool>> PostMark(MarkRequest mark)
        {
            var serviceResponse = _service.PostMark(mark);
            if (serviceResponse == false)
            {
                return BadRequest(serviceResponse);
            }

            return Ok(serviceResponse);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> PutMark(int id, MarkRequest mark)
        {
            var serviceResponse = _service.PutMark(id, mark);
            if (serviceResponse == false)
            {
                return BadRequest(serviceResponse);
            }

            return Ok(serviceResponse);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteMark(int id)
        {
            var serviceResponse = _service.DeleteMark(id);
            if (serviceResponse == false)
            {
                return BadRequest(serviceResponse);
            }

            return Ok(serviceResponse);
        }
    }
}
