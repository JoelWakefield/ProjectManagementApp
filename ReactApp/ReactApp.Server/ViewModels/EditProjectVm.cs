namespace ReactApp.Server.ViewModels
{
    public class EditProjectVm
    {
        public string? Id { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public DateTime ProjectedStart { get; set; }
        public DateTime ProjectedEnd { get; set; }
    }
}
