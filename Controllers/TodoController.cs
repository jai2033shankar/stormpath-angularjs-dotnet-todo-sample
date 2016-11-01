using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAngularStorm.Models;

namespace WebApiAngularStorm.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class TodosController : Controller
    {
        private readonly ApiContext _context;

        public TodosController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {

            return Ok(_context.Todos.Where(t => t.User == User.Identity.Name));
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(int id)
        {
            var todo = _context.Todos.SingleOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound($"No todo with an Id of {id} was found.");
            }
            return Ok(todo);
        }
        public IActionResult Post([FromBody] Todo todo)
        {
            if (string.IsNullOrEmpty(todo.Description))
            {
                return BadRequest("There must be a description in the todo.");
            }
            if (todo.Id > 0)
            {
                _context.Entry(todo).State = EntityState.Modified;
            }
            else
            {
                todo.User = User.Identity.Name;
                _context.Entry(todo).State = EntityState.Added;
            }
            _context.SaveChanges();

            return CreatedAtRoute("GetTodo", new { id = todo.Id }, todo);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = _context.Todos.FirstOrDefault(t => t.User == User.Identity.Name && t.Id == id);
            if (todo == null)
            {
                return NotFound($"No todo with an Id of {id} was found to this user.");
            }

            _context.Todos.Remove(todo);
            _context.SaveChanges();
            return Ok();
        }

    }
}