using Microsoft.AspNetCore.Identity;

namespace ProjectManagementApp.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public List<Project> OwnedProjects { get; set; } = new List<Project>();
        public List<Phase> OwnedPhases { get; set; } = new List<Phase>();
        public List<Phase> AssignedPhases { get; set; } = new List<Phase>();
        public List<PhaseSchedule> Schedules { get; set; } = new List<PhaseSchedule>();
        public List<ProjectRole> ProjectRoles { get; set; } = new List<ProjectRole>();
    }
}
