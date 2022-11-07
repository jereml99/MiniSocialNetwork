namespace MiniSocialNetwork.Models
{
    public class User
    {
        public string Login { get; set; }
        public DateTime AccountCreationTime { get; set; }
        public List<string> Friends { get; set; }

        public User(string login, DateTime accountCreationTime, List<string> friends)
        {
            Login = login;
            AccountCreationTime = accountCreationTime;
            Friends = friends;
        }
    }
}
