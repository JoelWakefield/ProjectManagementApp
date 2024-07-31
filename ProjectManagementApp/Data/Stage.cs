using System.ComponentModel.DataAnnotations;

namespace ProjectManagementApp.Data
{
    public class Stage
    {
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string Name { get; set; }
    }
}
