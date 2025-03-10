using ReactApp.Server.Models;

namespace ReactApp.Server.ViewModels
{
    public class PhaseVm
    {
        public string? Id { get; set; }
        public string Name { get; set; } = "";
        public string? ProjectId { get; set; }
        public string? Description { get; set; }
        public Priority Priority { get; set; } = Priority.Medium;
    }
}
