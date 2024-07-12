using ToDo.API.Enums;

namespace ToDo.API.Models;

public class ToDoTaskDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool Completed { get; set; }
    public PriorityLevel Priority { get; set; }
    public DateTime DueDate { get; set; }
}
