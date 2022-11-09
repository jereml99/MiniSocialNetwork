using MiniSocialNetwork.Controllers;

namespace MiniSocialNetwork.Models
{
    public class HomeViewModel
    {
        public User? CurrentylLoggedUser = UserController.CurrentlyLoggedUser ?? null;
    }
}
