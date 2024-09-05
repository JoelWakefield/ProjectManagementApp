using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Data;
using ProjectManagementApp.ViewModels;

namespace ProjectManagementApp.Services
{
    public interface IProjectRoleService
    {
        IEnumerable<ProjectRoleVm> GetAllRoles();
        Dictionary<ProjectRoleVm, bool> GetAllRolesAndUserAssignment(ApplicationUserVm user);
        Task<IEnumerable<ApplicationUserVm?>> GetUsersWithRoleAsync(string roleName);
        IEnumerable<UserWithRolesVm> GetAllUsersWithAllRoles();
        Task CreateRoleAsync(string name);
        Task UpdateRoleForUserAsync(ApplicationUserVm user, ProjectRoleVm role);
	}

    public class ProjectRoleService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, IMapper mapper) : IProjectRoleService
    {
        private ApplicationDbContext dbContext { get; set; } = dbContext;
        private UserManager<ApplicationUser> userManager { get; set; } = userManager;
        private IMapper mapper { get; set; } = mapper;

        public IEnumerable<ProjectRoleVm> GetAllRoles() => mapper.Map<IEnumerable<ProjectRole>, IEnumerable<ProjectRoleVm>>(dbContext.ProjectRoles);

        public IEnumerable<UserWithRolesVm> GetAllUsersWithAllRoles()
        {
            IEnumerable<ApplicationUser> users = userManager.Users;

            foreach (var user in users)
            {
                yield return mapper.Map<UserWithRolesVm>(user);
            }
        }

		public Dictionary<ProjectRoleVm, bool> GetAllRolesAndUserAssignment(ApplicationUserVm user) => GetAllRoles()
            .ToDictionary(r => r,user.ProjectRoles.Contains);

        public async Task<IEnumerable<ApplicationUserVm?>> GetUsersWithRoleAsync(string roleName)
        {
            ProjectRole? projectRole = await dbContext.ProjectRoles.FirstOrDefaultAsync(r => r.Name == roleName);

            if (projectRole == null)
                return Enumerable.Empty<ApplicationUserVm>();
            else
                return mapper.Map<IEnumerable<ApplicationUser>,IEnumerable<ApplicationUserVm>>(projectRole.Users);
        }

        public async Task CreateRoleAsync(string name)
        {
            await dbContext.ProjectRoles.AddAsync(new ProjectRole() { Name = name });
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateRoleForUserAsync(ApplicationUserVm userVm, ProjectRoleVm roleVm)
        {
            var user = mapper.Map<ApplicationUser>(userVm);
            var role = mapper.Map<ProjectRole>(roleVm);

            if (user.ProjectRoles.Contains(role))
                user.ProjectRoles.Remove(role);
            else
                user.ProjectRoles.Add(role);

            await dbContext.SaveChangesAsync();
        }
	}
}
