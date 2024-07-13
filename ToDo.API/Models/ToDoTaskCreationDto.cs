using System.ComponentModel.DataAnnotations;
using ToDo.API.Enums;

namespace ToDo.API.Models
{
    public class ToDoTaskCreationDto
    {
        [StringLength(50, MinimumLength = 1)]
        public required string Title { get; set; }

        [MaxLength(200)]
        public string Description { get; set; } = string.Empty;

        [EnumDataType(typeof(PriorityLevel))]
        public required PriorityLevel Priority { get; set; }
        
        [DataType(DataType.DateTime, ErrorMessage = "Due date must be a valid date and time")]
        public required DateTime DueDate { get; set; } 
    }
}
