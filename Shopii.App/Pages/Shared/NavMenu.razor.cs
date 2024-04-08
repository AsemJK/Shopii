using Microsoft.AspNetCore.Components;
using Shopii.App.Application.Models;

namespace Shopii.App.Pages.Shared
{
    public partial class NavMenu
    {
        [CascadingParameter]
        public MainLayout? Layout { get; set; }
        private User? user => Layout?.User;
    }
}
