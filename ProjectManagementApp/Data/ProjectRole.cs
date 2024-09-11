using System.ComponentModel.DataAnnotations;

namespace ProjectManagementApp.Data
{
    /// <summary>
    /// Project roles allow the team to make adjustments without having access to 
    /// AspNet Identity roles, which should be managed outside the app.
    /// </summary>
    public class ProjectRole
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public required string Name { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; } = Enumerable.Empty<ApplicationUser>();
    }

    public class ProjectRoleArchive
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string Name { get; set; }
    }
}
