using AutoMapper;
using ProjectManagementApp.Data;
using ProjectManagementApp.ViewModels;

namespace ProjectManagementApp
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<ProjectRole, ProjectRoleVm>();
            CreateMap<ProjectRoleVm, ProjectRole>();
            CreateMap<Stage, StageVm>();
            CreateMap<StageVm,Stage>();

            CreateMap<ApplicationUser, ApplicationUserVm>();
            CreateMap<ApplicationUserVm, ApplicationUser>();
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
        }
    }
}
