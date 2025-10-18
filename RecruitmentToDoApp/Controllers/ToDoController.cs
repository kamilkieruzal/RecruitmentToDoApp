using Microsoft.AspNetCore.Mvc;
using RecruitmentToDoApp.Models;
using RecruitmentToDoApp.Services;

namespace RecruitmentToDoApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly ILogger<ToDoController> logger;
        private readonly IToDoService toDoService;

        public ToDoController(IToDoService toDoService, ILogger<ToDoController> logger)
        {
            this.logger = logger;
            this.toDoService = toDoService;
        }

        // Endpoint for all todos or filtered by date (with pagination)
        [HttpGet]
        public async Task<ActionResult<IList<ToDo>>> GetToDosAsync([FromQuery]ToDoParams filter)
        {
            var result = await toDoService.GetToDosAsync(filter);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ToDo>> GetToDoAsync(int id)
        {
            var result = await toDoService.GetToDoByIdAsync(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> CreateToDoAsync(ToDo toDo)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }

            var result = await toDoService.CreateToDoAsync(toDo);

            return result ? Ok() : BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateToDoAsync([FromQuery] int id, ToDo toDo)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }

            var result = await toDoService.UpdateToDoAsync(id, toDo);
            return result ? Ok() : BadRequest();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteToDoAsync(int id)
        {
            if (id < 1)
                return StatusCode(StatusCodes.Status400BadRequest, "Provide correct Id");

            var result = await toDoService.DeleteToDoAsync(id);
            return result ? Ok() : BadRequest();
        }
    }
}
