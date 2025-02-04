using System.ComponentModel.DataAnnotations.Schema;

namespace ReactApp.Server.Data
{
    /// <summary>
    /// The Project entity keeps track of broad data which isn't likely to change and whose changes aren't worth tracking.
    /// Possible change: https://learn.microsoft.com/en-us/dotnet/standard/datetime/choosing-between-datetime#the-datetimeoffset-structure
    /// </summary>
    [Table("Projects")]
    public class Project
    {
        public string? Id { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public DateTime ProjectedStart { get; set; }
        public DateTime ProjectedEnd { get; set; }
        public DateTime ActualStart { get; set; }
        public DateTime ActualEnd { get; set; }
        public float TotalWorkHours { get; set; }

        public string? OwnerId { get; set; }
        public AppUser? Owner { get; set; }
    }
}
