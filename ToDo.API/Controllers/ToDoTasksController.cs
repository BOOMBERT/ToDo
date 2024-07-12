using Microsoft.AspNetCore.Mvc;
using ToDo.API.Models;

namespace ToDo.API.Controllers;

[ApiController]
[Route("api/tasks")]
public class ToDoTasksController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<ToDoTaskDto>> GetTasks()
    {
        return Ok(ToDoTasksDataStore.Current.ToDoTasks);
    }

    [HttpGet("{id}")]
    public ActionResult<ToDoTaskDto> GetTask(int id)
    {
        var taskToReturn = ToDoTasksDataStore.Current.ToDoTasks
            .FirstOrDefault(t => t.Id == id);

        if (taskToReturn == null)
        {
            return NotFound();
        }

        return Ok(taskToReturn);
    }
}
