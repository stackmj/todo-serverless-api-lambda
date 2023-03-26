using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Mvc;

namespace ToDoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly IDynamoDBContext _context;

        public ToDoController(IDynamoDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ToDo todo)
        {
            var existingTodo = await _context.LoadAsync<ToDo>(todo.id);
            if (existingTodo != null)
            {
                return BadRequest("Id is already exists");

            }
            else
            {
                await _context.SaveAsync<ToDo>(todo);
                return Ok(todo);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(ToDo todo)
        {
            var existingTodo = await _context.LoadAsync<ToDo>(todo.id);
            if (existingTodo != null)
            {
                await _context.SaveAsync<ToDo>(todo);
                return Ok(todo);
            }
            else
            {
                return NotFound("Id is not valid");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var existingTodos = await _context.ScanAsync<ToDo>(default).GetRemainingAsync();
            if (existingTodos != null)
            {
                return Ok(existingTodos);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var existingTodo = await _context.LoadAsync<ToDo>(id);
            if (existingTodo != null)
            {
                return Ok(existingTodo);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingTodo = await _context.LoadAsync<ToDo>(id);
            if (existingTodo != null)
            {
                await _context.DeleteAsync(existingTodo);
                return Ok();
            }
            return NotFound();
        }
    }
}
