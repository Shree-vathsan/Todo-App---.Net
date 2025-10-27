using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private static List<TodoItem> items = new();
        private static int nextId = 1;

        [HttpGet]
        public IActionResult GetAll() => Ok(items);

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = items.FirstOrDefault(x => x.Id == id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public IActionResult Create(TodoItem todo)
        {
            todo.Id = nextId++;
            items.Add(todo);
            return CreatedAtAction(nameof(Get), new { id = todo.Id }, todo);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, TodoItem updated)
        {
            var item = items.FirstOrDefault(x => x.Id == id);
            if (item == null) return NotFound();

            item.Title = updated.Title;
            item.IsComplete = updated.IsComplete;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = items.FirstOrDefault(x => x.Id == id);
            if (item == null) return NotFound();

            items.Remove(item);
            return NoContent();
        }
    }
}
