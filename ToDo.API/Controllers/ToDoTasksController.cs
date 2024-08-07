﻿using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ToDo.API.Enums;
using ToDo.API.Models;
using ToDo.API.Services;

namespace ToDo.API.Controllers;

[ApiController]
[Route("api/tasks")]
public class ToDoTasksController : ControllerBase
{
    private readonly IToDoTasksRepository _toDoTasksRepository;
    private readonly IMapper _mapper;
    const int maxToDoTasksPageSize = 20;

    public ToDoTasksController(IToDoTasksRepository toDoTasksRepository, IMapper mapper)
    {
        _toDoTasksRepository = toDoTasksRepository ?? throw new ArgumentNullException(nameof(toDoTasksRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ToDoTaskDto>>> GetTasks(
        bool? completed, PriorityLevel? priority, ushort? year, byte? month, byte? day, 
        string? searchQuery,
        int pageNumber = 1, int pageSize = 10)
    {
        if (pageSize >  maxToDoTasksPageSize)
        {
            pageSize = maxToDoTasksPageSize;
        }

        try
        {
            var (toDoTaskEntities, paginationMetadata) = await _toDoTasksRepository.GetAllTasksAsync(
                completed, priority, year, month, day, searchQuery, pageNumber, pageSize);

            Response.Headers["X-Pagination"] = JsonSerializer.Serialize(paginationMetadata);

            return Ok(_mapper.Map<IEnumerable<ToDoTaskDto>>(toDoTaskEntities));
        }
        catch (ArgumentException ex) 
        {
            return BadRequest(new ProblemDetails
            { 
                Status = 400,
                Title = "The value is not valid.",
                Detail = ex.Message,
                Instance = HttpContext.Request.Path
            });
        }
    }

    [HttpGet("{id}", Name = "GetTask")]
    public async Task<IActionResult> GetTask(int id)
    {
        var toDoTaskEntity = await _toDoTasksRepository.GetTaskAsync(id);
        if (toDoTaskEntity == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<ToDoTaskDto>(toDoTaskEntity));
    }

    [HttpPost]
    public async Task<ActionResult<ToDoTaskDto>> CreateTask(ToDoTaskCreationDto toDoTask)
    {   
        var toDoTaskEntity = _mapper.Map<Entities.ToDoTask>(toDoTask);

        await _toDoTasksRepository.AddTaskAsync(toDoTaskEntity);
        await _toDoTasksRepository.SaveChangesAsync();

        var createdToDoTaskToReturn = _mapper.Map<ToDoTaskDto>(toDoTaskEntity);

        return CreatedAtRoute("GetTask", new { createdToDoTaskToReturn.Id }, createdToDoTaskToReturn);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateTask(int id, ToDoTaskUpdateDto toDoTask)
    {
        var toDoTaskEntity = await _toDoTasksRepository.GetTaskAsync(id);
        if (toDoTaskEntity == null)
        {
            return NotFound();
        }

        _mapper.Map(toDoTask, toDoTaskEntity);
        await _toDoTasksRepository.SaveChangesAsync();

        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> PartiallyUpdateTask(int id, JsonPatchDocument<ToDoTaskUpdateDto> patchDocument)
    {
        var toDoTaskEntity = await _toDoTasksRepository.GetTaskAsync(id);
        if (toDoTaskEntity == null)
        {
            return NotFound();
        }

        var toDoTaskToPatch = _mapper.Map<ToDoTaskUpdateDto>(toDoTaskEntity);

        patchDocument.ApplyTo(toDoTaskToPatch, ModelState);
        
        if (!ModelState.IsValid || !TryValidateModel(toDoTaskToPatch))
        {
            return BadRequest(ModelState);
        }

        _mapper.Map(toDoTaskToPatch, toDoTaskEntity);
        await _toDoTasksRepository.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTask(int id)
    {
        var toDoTaskEntity = await _toDoTasksRepository.GetTaskAsync(id);
        if (toDoTaskEntity == null)
        {
            return NotFound();
        }

        _toDoTasksRepository.DeleteTask(toDoTaskEntity);
        await _toDoTasksRepository.SaveChangesAsync();

        return NoContent();
    }
}
