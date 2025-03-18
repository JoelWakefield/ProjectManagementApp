using AutoMapper;
using ReactApp.Server.Models;
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
                )
                .ForMember(
                    dest => dest.ProjectedStart,
                    opt => opt.MapFrom(src => src.ProjectedStart.ToDateOnly())
                )
                .ForMember(
                    dest => dest.ProjectedEnd,
                    opt => opt.MapFrom(src => src.ProjectedEnd.ToDateOnly())
                )
                .ForMember(
                    dest => dest.ActualStart,
                    opt => opt.MapFrom(src => src.ActualStart.ToDateOnly())
                )
                .ForMember(
                    dest => dest.ActualEnd,
                    opt => opt.MapFrom(src => src.ActualEnd.ToDateOnly())
                );
            CreateMap<Project, EditProjectVm>()
                .ForMember(
                    dest => dest.ProjectedStart,
                    opt => opt.MapFrom(src => src.ProjectedStart.ToDateOnly())
                )
                .ForMember(
                    dest => dest.ProjectedEnd,
                    opt => opt.MapFrom(src => src.ProjectedEnd.ToDateOnly())
                );

            CreateMap<Phase, PhaseVm>();
            CreateMap<Phase, EditPhaseVm>();
        }
    }
}
