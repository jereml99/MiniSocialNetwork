using MiniSocialNetwork.Models;

namespace MiniSocialNetwork
{
    public static class Globals
    {
        public static string AdminLogin = "admin";
        public static List<User> Users { get; set; } = new List<User>()
        {
            new User(AdminLogin, DateTime.Now, new List<string>() {}),
            new User("jereml", DateTime.Now, new List<string>() { AdminLogin })
        };
        public static User? CurrentlyLoggedUser = Users.FirstOrDefault(u => u.Login == AdminLogin);
    }
}
