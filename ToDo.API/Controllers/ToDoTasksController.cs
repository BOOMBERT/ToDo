using Microsoft.AspNetCore.Mvc;
using ToDo.API.Enums;
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

    [HttpGet("{id}", Name = "GetTask")]
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

    [HttpPost]
    public ActionResult<ToDoTaskDto> CreateTask(ToDoTaskCreationDto toDoTask)
    {
        var maxTaskId = ToDoTasksDataStore.Current.ToDoTasks.Max(i => i.Id);

        var finalTask = new ToDoTaskDto()
        {
            Id = ++maxTaskId,
            Title = toDoTask.Title,
            Description = toDoTask.Description,
            Completed = false,
            Priority = toDoTask.Priority,
            DueDate = toDoTask.DueDate
        };

        ToDoTasksDataStore.Current.ToDoTasks.Add(finalTask);

        return CreatedAtRoute("GetTask", new { finalTask.Id }, finalTask);
    }
}
