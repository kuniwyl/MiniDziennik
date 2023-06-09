using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dziennik.Server.Controllers
{
    [Route("api/subject")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<SubjectResponse>>> GetSubjects()
        {
            var result = _subjectService.GetSubjects();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<SubjectResponse>> GetSubject(int id)
        {
            var result = _subjectService.GetSubject(id);
            if (result == null)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<bool>> PostSubject(SubjectRequest subject)
        {
            var result = _subjectService.PostSubject(subject);
            if (result == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<bool>> PutSubject(int id, SubjectRequest subject)
        {
            var result = _subjectService.PutSubject(id, subject);
            if (result == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<bool>> DeleteSubject(int id)
        {
            var result = _subjectService.DeleteSubject(id);
            if (result == false)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpPut("{id}/teacher/{teacherId}")]
        [Authorize]
        public async Task<ActionResult<bool>> SetTeacher(int id, int teacherId)
        {
            var result = _subjectService.SetTeacher(id, teacherId);
            if (result == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("{id}/students")]
        [Authorize]
        public async Task<ActionResult<bool>> SetStudents(int id, List<int> studentsList)
        {
            var result = _subjectService.SetStudents(id, studentsList);
            if (result == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
