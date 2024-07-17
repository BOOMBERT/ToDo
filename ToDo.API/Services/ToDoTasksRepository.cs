using Microsoft.EntityFrameworkCore;
using ToDo.API.DbContexts;
using ToDo.API.Entities;

namespace ToDo.API.Services
{
    public class ToDoTasksRepository : IToDoTasksRepository
    {
        private readonly ToDoTasksContext _context;

        public ToDoTasksRepository(ToDoTasksContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<ToDoTask>> GetAllTasksAsync()
        {
            return await _context.ToDoTasks.ToListAsync();
        }

        public async Task<ToDoTask?> GetTaskAsync(int id)
        {
            return await _context.ToDoTasks.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddTaskAsync(ToDoTask toDoTask)
        {
            await _context.ToDoTasks.AddAsync(toDoTask);
        }

        public void DeleteTask(ToDoTask toDoTask)
        {
            _context.ToDoTasks.Remove(toDoTask);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
