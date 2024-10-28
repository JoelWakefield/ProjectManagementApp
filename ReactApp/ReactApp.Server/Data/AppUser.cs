using System.ComponentModel.DataAnnotations.Schema;

namespace ReactApp.Server.Data
{
    [Table("AspNetUsers")]
    public class AppUser
    {
        public string Id { get; set; }
        [Column("UserName")]
        public string Name { get; set; }
    }
}
