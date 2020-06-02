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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        public void loginButton_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(loginid.Text))
            {
                DisplayAlert("Oops!!Validation Error", "Login ID  required", "Re-try");
            }

            else if ((loginid.Text).ToUpper() == "TEST")
            {

                Navigation.PushAsync(new StartPage { Title = "Movable Bridges", BackgroundColor = Color.DarkOrange });
                Navigation.RemovePage(this);
            }
            else
            {
                DisplayAlert("Failed", "Invalid User", "Login Again");
            }
        }
    }
}