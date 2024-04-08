using Microsoft.AspNetCore.Components;
using Shopii.App.Application.Models;
using Shopii.App.Pages.Shared;

namespace Shopii.App.Pages
{
    public partial class Dashboard
    {
        [CascadingParameter]
        public MainLayout? Layout { get; set; }
        private User? user => Layout.User;
    }
}
