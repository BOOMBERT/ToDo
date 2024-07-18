using Microsoft.EntityFrameworkCore;
using ToDo.API.DbContexts;
using ToDo.API.Entities;
using ToDo.API.Enums;
using ToDo.API.Validations;

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

        public async Task<IEnumerable<ToDoTask>> GetAllTasksAsync(
            bool? completed, PriorityLevel? priority, ushort? year, byte? month, byte? day)
        {
            if (completed == null && priority == null && year == null && month == null && day == null)
            {
                return await GetAllTasksAsync();
            }

            var collection = _context.ToDoTasks as IQueryable<ToDoTask>;

            if (completed != null)
            {
                collection = collection.Where(t => t.Completed == completed);
            }

            if (priority != null)
            {
                collection = collection.Where(t => t.Priority == priority);
            }

            if (year != null)
            {
                if (DateValidator.ValidateYear(year))
                {
                    collection = collection.Where(t => t.DueDate.Year == year);
                }
                else
                {
                    throw new ArgumentException($"Invalid year value.");
                }
            }

            if (month != null)
            {
                if (DateValidator.ValidateMonth(month))
                {
                    collection = collection.Where(t => t.DueDate.Month == month);
                }
                else
                {
                    throw new ArgumentException("Invalid month value. Month must be between 1 and 12.");
                }
            }

            if (day != null)
            {
                if (DateValidator.ValidateDay(day, month, year))
                {
                    collection = collection.Where(t => t.DueDate.Day == day);
                }
                else
                {
                    throw new ArgumentException("Invalid day value.");
                }
            }

            return await collection.ToListAsync();
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
