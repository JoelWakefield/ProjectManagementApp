using AutoMapper;
using ProjectManagementApp.Data;
using ProjectManagementApp.ViewModels;

namespace ProjectManagementApp
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProjectRole, ProjectRoleVm>().ReverseMap();
            CreateMap<Stage, StageVm>().ReverseMap();

            CreateMap<ApplicationUser, ApplicationUserVm>().ReverseMap();
            CreateMap<ApplicationUser, UserWithRolesVm>()
                .ForMember(
                    dest => dest.User,
                    opt => opt.MapFrom(src => src)
                )
                .ForMember(
                    dest => dest.Roles,
                    opt => opt.MapFrom(
                        src => src.ProjectRoles.Any() == true
                            ? string.Join(", ", src.ProjectRoles.Select(r => r.Name))
                            : string.Empty
                ));

            CreateMap<Phase, PhaseVm>().ReverseMap();
            CreateMap<Phase, CreatePhaseVm>().ReverseMap();
            CreateMap<Project, ProjectVm>().ReverseMap();
            CreateMap<Project, CreateProjectVm>().ReverseMap();
        }
    }
}
