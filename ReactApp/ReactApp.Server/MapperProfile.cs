using AutoMapper;
using ReactApp.Server.Data;
using ReactApp.Server.ViewModels;

namespace ReactApp.Server
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AppUser, UserVm>()
                .ForMember(
                    dest => dest.ProjectRoles, 
                    opt => opt.MapFrom(src => String.Join(", ", src.ProjectRoles.Select(r => r.Name)))
                );

            CreateMap<AppUser, EditUserVm>()
                .ForMember(
                    dest => dest.ProjectRoles,
                    opt => opt.MapFrom(src => src.ProjectRoles.Select(
                        role => new EditUserProjectRole { Name = role.Name, Value = true }
                    ))
                );

            CreateMap<Project, ProjectVm>()
                .ForMember(
                    dest => dest.OwnerName,
                    opt => opt.MapFrom(src => src.Owner.Name)
                );
            CreateMap<Project, EditProjectVm>()
                .ForMember(
                    dest => dest.ProjectedStart,
                    opt => opt.MapFrom(src => src.ProjectedStart)
                )
                .ForMember(
                    dest => dest.ProjectedEnd,
                    opt => opt.MapFrom(src => src.ProjectedEnd)
                );
        }
    }
}
