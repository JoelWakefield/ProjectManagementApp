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
    }

    public class UserService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, IMapper mapper) : IUserService
    {
        private ApplicationDbContext dbContext = dbContext;
        private UserManager<ApplicationUser> userManager = userManager;
        private IMapper mapper = mapper;

        public async Task<ApplicationUserVm> GetUser(string userId) => mapper.Map<ApplicationUserVm>(
            await userManager.Users.FirstOrDefaultAsync(u => u.Id == userId));
    }
}
