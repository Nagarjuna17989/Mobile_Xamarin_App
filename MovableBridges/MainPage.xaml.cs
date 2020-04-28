using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using MovableBridges.Model;

namespace MovableBridges
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void button_click(object sender, EventArgs e)
        {
           
            Button btn = sender as Button;
            if (btn.Text == "Button1")
            {
                await DisplayAlert("Button 1 Clicked", "Info", "Bye");
            }
            else
            {
                await DisplayAlert("Button 2 Clicked", "Info", "Bye");
            }
        }
       
    }
}