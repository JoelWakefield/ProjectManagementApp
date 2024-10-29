using ReactApp.Server.Data;

namespace ReactApp.Server
{
    /// <summary>
    /// Project roles allow the team to make adjustments without having access to AspNet Identity roles, which should be managed outside the app.
    /// </summary>
    public class ProjectRole
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }

        //public ICollection<AppUser> Users { get; set; }
    }
}
