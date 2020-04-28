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
    public partial class BridgeListPage : ContentPage
    {
        public BridgeListPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var ds = await App.Database.GetItemsAsync();

            listView.ItemsSource = (from p in ds
                                    orderby p.Date_Modified descending
                                    select p).ToList();

        }
        async void OnItemAdded(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NavigationDetailPage
            {
                BindingContext = new NavigationOpening()
            });
        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var opening = (NavigationOpening)e.SelectedItem;

                var detail = new vmDetail();

                detail.ID = opening.ID;
                var dsList = App.Database.DistrictList();
                var pList = App.Database.ParishList();
                var bList = App.Database.BridgeList();

                var bridge = new Bridge();
                bridge = (from p in bList
                          where p.ID == opening.Bridge_ID
                          select p).FirstOrDefault();
                detail.bridge = bridge;

                var parish = new Parish();
                parish = (from p in pList
                          where p.ID == bridge.Parish_Id
                          select p).FirstOrDefault();
                detail.parish = parish;

                detail.district = (from p in dsList
                                   where p.ID == parish.District_ID
                                   select p).FirstOrDefault();
                detail.Entry_Date = opening.Entry_Date;
                detail.Opening_Time = opening.Opening_Time;
                detail.Closing_Time = opening.Closing_Time;

                await Navigation.PushAsync(new NavigationDetailPage
                {
                    BindingContext = detail,
                    Title = "Navigation Openings"
                });
                ;
            }
        }
    }
}