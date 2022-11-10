namespace MiniSocialNetwork.Models
{
    public class User
    {
        public string Login { get; set; }
        public DateTime CreationTime { get; set; }
        public List<string> Friends { get; set; }

        public User(string login, DateTime creationTime, List<string> friends)
        {
            Login = login;
            CreationTime = creationTime;
            Friends = friends;
        }
    }
}
