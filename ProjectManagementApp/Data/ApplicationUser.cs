using Microsoft.AspNetCore.Identity;

namespace ProjectManagementApp.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public IEnumerable<Project> OwnedProjects { get; set; }
        public IEnumerable<Phase> OwnedPhases { get; set; }
        public IEnumerable<Phase> AssignedPhases { get; set; }
        public IEnumerable<PhaseSchedule> Schedules { get; set; }
        public IEnumerable<ProjectRole> ProjectRoles { get; set; }
    }
}
