using ToDo.API.Entities;

namespace ToDo.API.Services
{
    public interface IToDoTasksRepository
    {
        Task<IEnumerable<ToDoTask>> GetAllTasksAsync();
        Task<ToDoTask?> GetTaskAsync(int id);
        Task AddTaskAsync(ToDoTask toDoTask);
        void DeleteTask(ToDoTask toDoTask);
        Task<bool> SaveChangesAsync();
    }
}
