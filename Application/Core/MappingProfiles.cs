using AutoMapper;
using Domain;

namespace Application.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        string currentUsername = null;
        CreateMap<ListTask, ListTask>();
        CreateMap<Task, Task>();
        CreateMap<Comment, Comment>();
    }
}