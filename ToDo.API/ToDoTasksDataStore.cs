using ToDo.API.Enums;
using ToDo.API.Models;

namespace ToDo.API;

public class ToDoTasksDataStore
{
    public List<ToDoTaskDto> ToDoTasks { get; set; }
    public static ToDoTasksDataStore Current { get; } = new ToDoTasksDataStore();

    public ToDoTasksDataStore()
    {
        ToDoTasks = new List<ToDoTaskDto>()
        {
            new ToDoTaskDto()
            {
                Id = 1,
                Title = "Title1",
                Description = "Description11",
                Completed = true,
                Priority = PriorityLevel.Low,
                DueDate = DateTime.Now.AddDays(1),
            },
            new ToDoTaskDto()
            {
                Id = 2,
                Title = "Title2",
                Description = "Description22",
                Completed = false,
                Priority = PriorityLevel.Medium,
                DueDate = DateTime.Now.AddDays(2),
            },
            new ToDoTaskDto()
            {
                Id = 3,
                Title = "Title3",
                Description = "Description33",
                Completed = true,
                Priority = PriorityLevel.High,
                DueDate = DateTime.Now.AddDays(3),
            },
            new ToDoTaskDto()
            {
                Id = 4,
                Title = "Title4",
                Description = "Description44",
                Completed = false,
                Priority = PriorityLevel.Low,
                DueDate = DateTime.Now.AddDays(4),
            }
        };
    }
}
