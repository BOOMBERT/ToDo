using Microsoft.EntityFrameworkCore;
using ToDo.API.Entities;
using ToDo.API.Enums;

namespace ToDo.API.DbContexts
{
    public class ToDoTasksContext : DbContext
    {
        public ToDoTasksContext(DbContextOptions<ToDoTasksContext> options) : base(options) { }

        public DbSet<ToDoTask> ToDoTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoTask>()
                .HasData(
                new ToDoTask()
                {
                    Id = 1,
                    Title = "Title1",
                    Description = "Description11",
                    Completed = true,
                    Priority = PriorityLevel.Low,
                    DueDate = DateTime.Now.AddDays(1),
                },
                new ToDoTask()
                {
                    Id = 2,
                    Title = "Title2",
                    Description = "Description22",
                    Completed = false,
                    Priority = PriorityLevel.Medium,
                    DueDate = DateTime.Now.AddDays(2),
                },
                new ToDoTask()
                {
                    Id = 3,
                    Title = "Title3",
                    Description = "Description33",
                    Completed = true,
                    Priority = PriorityLevel.High,
                    DueDate = DateTime.Now.AddDays(3),
                },
                new ToDoTask()
                {
                    Id = 4,
                    Title = "Title4",
                    Description = "Description44",
                    Completed = false,
                    Priority = PriorityLevel.Low,
                    DueDate = DateTime.Now.AddDays(4),
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
