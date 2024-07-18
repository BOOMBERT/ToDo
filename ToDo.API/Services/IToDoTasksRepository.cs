using ToDo.API.Entities;
using ToDo.API.Enums;

namespace ToDo.API.Services
{
    public interface IToDoTasksRepository
    {
        Task<IEnumerable<ToDoTask>> GetAllTasksAsync();
        Task<IEnumerable<ToDoTask>> GetAllTasksAsync(
            bool? completed, PriorityLevel? priority, ushort? year, byte? month, byte? day
            );
        Task<ToDoTask?> GetTaskAsync(int id);
        Task AddTaskAsync(ToDoTask toDoTask);
        void DeleteTask(ToDoTask toDoTask);
        Task<bool> SaveChangesAsync();
    }
}
