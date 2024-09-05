using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Data;
using ProjectManagementApp.ViewModels;

namespace ProjectManagementApp.Services
{
    public interface IUserService
    {
        Task<ApplicationUserVm> GetUser(string userId);
        IEnumerable<ApplicationUserVm> GetUsers();
    }

    public class UserService(UserManager<ApplicationUser> userManager, IMapper mapper) : IUserService
    {
        private UserManager<ApplicationUser> userManager = userManager;
        private IMapper mapper = mapper;

        public async Task<ApplicationUserVm> GetUser(string userId) => mapper.Map<ApplicationUserVm>(
            await userManager.Users.FirstOrDefaultAsync(u => u.Id == userId));

        public IEnumerable<ApplicationUserVm> GetUsers() => userManager.Users
            .Select(mapper.Map<ApplicationUserVm>);
    }
}
