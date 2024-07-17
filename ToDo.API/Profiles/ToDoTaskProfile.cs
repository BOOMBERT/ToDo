using AutoMapper;

namespace ToDo.API.Profiles
{
    public class ToDoTaskProfile : Profile
    {
        public ToDoTaskProfile()
        {
            CreateMap<Entities.ToDoTask, Models.ToDoTaskDto>();
            CreateMap<Models.ToDoTaskCreationDto, Entities.ToDoTask>();
            CreateMap<Models.ToDoTaskUpdateDto, Entities.ToDoTask>();
            CreateMap<Entities.ToDoTask, Models.ToDoTaskUpdateDto>();
        }
    }
}
