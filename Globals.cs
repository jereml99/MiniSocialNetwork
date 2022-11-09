using MiniSocialNetwork.Models;

namespace MiniSocialNetwork
{
    public static class Globals
    {
        public static List<User> Users { get; set; } = new List<User>()
        {
            new User("a", DateTime.Now, new List<string>() { "ziom1", "ziom2" }),
            new User("greg", DateTime.Now, new List<string>() { "ziom1", "ziom2" })
        };
        public static User? CurrentlyLoggedUser = null;
    }
}
