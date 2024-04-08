using Coravel;
using Coravel.Events.Interfaces;
using Shopii.App.Application.Events;
using Shopii.App.Application.Events.Listeners;

namespace Shopii.App.Application.Startup
{
    public static class Events
    {
        public static IServiceProvider RegisterEvents(this IServiceProvider services)
        {
            IEventRegistration registration = services.ConfigureEvents();

            // add events and listeners here
            registration
                .Register<UserCreated>()
                .Subscribe<EmailNewUser>();

            return services;
        }
    }
}
