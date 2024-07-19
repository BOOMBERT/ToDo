using ToDo.API.Entities;
using ToDo.API.Enums;

namespace ToDo.API.Services
{
    public interface IToDoTasksRepository
    {
        Task<(IEnumerable<ToDoTask>, PaginationMetadata)> GetAllTasksAsync(
            bool? completed, PriorityLevel? priority, ushort? year, byte? month, byte? day, 
            string? searchQuery,
            int pageNumber, int pageSize
            );
        Task<ToDoTask?> GetTaskAsync(int id);
        Task AddTaskAsync(ToDoTask toDoTask);
        void DeleteTask(ToDoTask toDoTask);
        Task<bool> SaveChangesAsync();
    }
}
