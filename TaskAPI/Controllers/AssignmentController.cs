using Microsoft.AspNetCore.Mvc;
using TaskAPI.Models;
using TaskAPI.DBAccess;
using Microsoft.AspNetCore.Cors;

namespace TaskAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentController : ControllerBase {
        private readonly ILogger<AssignmentController> _log;
        private readonly IDBAccess _dbAccess;
        public AssignmentController(ILogger<AssignmentController> log, IDBAccess dBAccess) {
            _log = log;
            _dbAccess = dBAccess;
        }
        // GET: api/<AssignmentController>
        [HttpGet]
        public ActionResult<Assignment> Get() {
            return Ok(_dbAccess.Get());
        }

        // GET api/<AssignmentController>/5
        [HttpGet("{id}")]
        public ActionResult<Assignment> Get(string id) {
            Assignment assignment;
            try {
                assignment = _dbAccess.Get(id);
            }
            catch (Exception e) {
                _log.LogWarning(e, "Unable to find assignment by id");
                return StatusCode(404);
            }
            return Ok(assignment);
        }

        // POST api/<AssignmentController>
        [HttpPost]
        public void Post([FromBody] Assignment assignment) {
            _dbAccess.Upsert(assignment);
        }

        // PUT api/<AssignmentController>/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] Assignment assignment) {
            _dbAccess.Upsert(assignment);
        }

        // DELETE api/<AssignmentController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id) {
            try {
                _dbAccess.Delete(id);
            }
            catch (Exception e) {
                _log.LogWarning(e, "Unable to find assignment for deletion");
                return StatusCode(404);
            }
            return Ok();
        }
    }
}
