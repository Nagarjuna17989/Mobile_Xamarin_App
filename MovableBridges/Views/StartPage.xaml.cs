using MovableBridges.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovableBridges.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page1());
        }

        private void OnTapped(object sender, EventArgs e)
        {
            DisplayAlert("Test","Image Displayed","Close");
        }
        
        private async void OnOpeningsList(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BridgeListPage
            {
                BindingContext = new List<NavigationOpening>(),
                Title = "Navigation Opening List"
            });
        }

        private async void OnItemAdded(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NavigationDetailPage
            {
                BindingContext = new vmDetail(),
                Title = "Navigation Openings"
            });
        }

        private void OnContactTapped(object sender, EventArgs e)
        {
            DisplayAlert("Contact Info", "Contact Email: movable@outlook.com", "Close");
        }
        
    }
}