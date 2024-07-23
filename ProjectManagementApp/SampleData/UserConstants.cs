using ProjectManagementApp.Data;

namespace ProjectManagementApp.Models
{
    public static class Users
    {
        public static ApplicationUser Bert { get; set; } = new ApplicationUser()
        {
            UserName = "Bert",
            Email = "bert@fake.email",
            EmailConfirmed = true,
        };
        public static ApplicationUser Mylo { get; set; } = new ApplicationUser()
        {
            UserName = "Mylo",
            Email = "mylo@fake.email",
            EmailConfirmed = true,
        };
        public static ApplicationUser Alayah { get; set; } = new ApplicationUser()
        {
            UserName = "Alayah",
            Email = "alayah@fake.email",
            EmailConfirmed = true,
        };
        public static ApplicationUser Zahir { get; set; } = new ApplicationUser()
        {
            UserName = "Zahir",
            Email = "zahir@fake.email",
            EmailConfirmed = true,
        };
    }
}
