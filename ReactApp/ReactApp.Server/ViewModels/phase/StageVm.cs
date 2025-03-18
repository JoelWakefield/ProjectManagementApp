namespace ReactApp.Server.ViewModels
{
    public class StageVm
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public int OrderId { get; set; }
    }
}
