using Microsoft.AspNetCore.Identity;

namespace ProjectManagementApp.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public List<Project> OwnedProjects { get; set; }
        public List<Phase> OwnedPhases { get; set; }
        public List<Phase> AssignedPhases { get; set; }
        public List<PhaseSchedule> Schedules { get; set; }
        public List<ProjectRole> ProjectRoles { get; set; }
    }
}
