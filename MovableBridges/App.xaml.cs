using MovableBridges.DataLayer;
using MovableBridges.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovableBridges
{
    public partial class App : Application
    {
        static DBWrapper database;
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            ///MainPage = new NavigationPage(new StartPage { Title = "Movable Bridges", BackgroundColor= Color.DarkOrange });
            //var nav = new NavigationPage(new BridgeListPage());
            //nav.BarBackgroundColor = (Color)App.Current.Resources["primaryGreen"];
            //nav.BarTextColor = Color.White;
            MainPage = new NavigationPage(new LoginPage { Title = "Movable Bridges" });
            //MainPage = nav;
        }

        public static DBWrapper Database
        {
            get
            {
                if (database == null)
                {
                    database = new DBWrapper();
                }
                return database;
            }
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
