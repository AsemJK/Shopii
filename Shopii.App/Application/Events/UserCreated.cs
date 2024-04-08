using Coravel.Events.Interfaces;
using Shopii.App.Application.Models;

namespace Shopii.App.Application.Events
{
    public class UserCreated : IEvent
    {
        public User User { get; set; }

        public UserCreated(User user)
        {
            this.User = user;
        }
    }
}
