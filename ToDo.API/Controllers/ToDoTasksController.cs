using Microsoft.AspNetCore.JsonPatch;
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

    [HttpPut("{id}")]
    public ActionResult UpdateTask(int id, ToDoTaskUpdateDto toDoTask)
    {
        var taskToUpdate = ToDoTasksDataStore.Current.ToDoTasks.FirstOrDefault(t => t.Id == id);

        if (taskToUpdate == null) 
        { 
            return NotFound(); 
        }

        taskToUpdate.Title = toDoTask.Title;
        taskToUpdate.Description = toDoTask.Description;
        taskToUpdate.Completed = toDoTask.Completed;
        taskToUpdate.Priority = toDoTask.Priority;
        taskToUpdate.DueDate = toDoTask.DueDate;

        return NoContent();
    }

    [HttpPatch("{id}")]
    public ActionResult PartiallyUpdateTask(int id, JsonPatchDocument<ToDoTaskUpdateDto> patchDocument)
    {
        var taskToUpdate = ToDoTasksDataStore.Current.ToDoTasks.FirstOrDefault(t => t.Id == id);

        if (taskToUpdate == null)
        {
            return NotFound();
        }

        var taskToUpdateCopy = new ToDoTaskUpdateDto()
        {
            Title = taskToUpdate.Title,
            Description = taskToUpdate.Description,
            Completed = taskToUpdate.Completed,
            Priority = taskToUpdate.Priority,
            DueDate = taskToUpdate.DueDate
        };

        patchDocument.ApplyTo(taskToUpdateCopy, ModelState);

        if (!ModelState.IsValid || !TryValidateModel(taskToUpdateCopy))
        {
            return BadRequest(ModelState);
        }

        taskToUpdate.Title = taskToUpdateCopy.Title;
        taskToUpdate.Description = taskToUpdateCopy.Description;
        taskToUpdate.Completed = taskToUpdateCopy.Completed;
        taskToUpdate.Priority = taskToUpdateCopy.Priority;
        taskToUpdate.DueDate = taskToUpdateCopy.DueDate;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteTask(int id) 
    {
        var taskToDelete = ToDoTasksDataStore.Current.ToDoTasks.FirstOrDefault(t => t.Id == id);

        if (taskToDelete == null)
        {
            return NotFound();
        }

        ToDoTasksDataStore.Current.ToDoTasks.Remove(taskToDelete);

        return NoContent();
    }
}
