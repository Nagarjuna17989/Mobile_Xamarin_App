using MovableBridges.Model;
using MovableBridges.ViewModel;
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
    public partial class NavigationDetailPage : ContentPage
    {
        List<Bridge> bList = new List<Bridge>();
        List<District> dsList = new List<District>();
        List<Parish> pList = new List<Parish>();

        public NavigationDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var detail = (vmDetail)BindingContext;

            dsList = App.Database.GetDistrictcItemsAsync().Result;
            
            this.Districts.ItemsSource = dsList;
            this.Districts.ItemDisplayBinding = new Binding("District_Name");

            this.Parish.IsEnabled = false;
            this.Bridge.IsEnabled = false;

            if (detail.ID == 0)
            {
                this.DtSelectedDate.Date = DateTime.Now;
            }
            if (detail.district != null)
            {
                this.Districts.SelectedItem = (from p in dsList
                                               where p.ID == detail.district.ID
                                               select p).FirstOrDefault();
            }
            this.HeightRequest = 400;
        }


        async void OnSaveClicked(object sender, EventArgs e)
        {
            var detail = (vmDetail)BindingContext;
            var opening = new NavigationOpening();


            if (detail.ID == 0)
            {
                opening.Date_Created = DateTime.Now;
                opening.Date_Modified = DateTime.Now;
            }   
            else
            {
                opening.Date_Modified = DateTime.Now;
                opening.ID = detail.ID;
            }
            opening.User_Modified = "Nag";
            

            Bridge bds = new Bridge();
            bds = (Bridge)this.Bridge.ItemsSource[0];
            opening.Bridge_ID = bds.ID;
            opening.Bridge_Name = (from p in bList
                                     where p.ID == bds.ID
                                     select p.Bridge_Name).FirstOrDefault();
            opening.Opening_Time = detail.Opening_Time;
            opening.Closing_Time = detail.Closing_Time;

            opening.Entry_Date = detail.Entry_Date;

            await App.Database.SaveItemAsync(opening);
            await Navigation.PopAsync();
        }

        async void OnDeleteClicked(object sender, EventArgs e)
        {
            var detail = (vmDetail)BindingContext;
            var opening = new NavigationOpening();
            if (detail.ID == 0)
            {
                opening.Date_Created = DateTime.Now;
                opening.Date_Modified = DateTime.Now;
            }
            else
            {
                opening.Date_Modified = DateTime.Now;
                opening.ID = detail.ID;
            }
            opening.User_Modified = "Nag";


            Bridge bds = new Bridge();
            bds = (Bridge)this.Bridge.ItemsSource[0];
            opening.Bridge_ID = bds.ID;
            opening.Bridge_Name = (from p in bList
                                     where p.ID == bds.ID
                                     select p.Bridge_Name).FirstOrDefault();
            opening.Opening_Time = detail.Opening_Time;
            opening.Closing_Time = detail.Closing_Time;

            opening.Entry_Date = detail.Entry_Date;
            await App.Database.DeleteItemAsync(opening);
            await Navigation.PopAsync();
        }

        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void Bridge_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = sender as Picker;
            Bridge ds = (Bridge)picker.SelectedItem;
            if (ds != null)
            {
                this.Bridge.WidthRequest = 150;
            }
            else
            {
                this.Bridge.WidthRequest = 100;
            }
        }

        private void Parish_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = sender as Picker;
            Parish ds = new Parish();
            ds = (Parish)picker.SelectedItem;
            if (ds != null)
            {
                bList = App.Database.GetBridgeItemsAsync().Result;
                this.Bridge.ItemsSource = bList.Where(p => p.Parish_Id == ds.ID).ToList();
                this.Bridge.ItemDisplayBinding = new Binding("Bridge_Name");
                this.Bridge.IsEnabled = true;

                var detail = (vmDetail)BindingContext;

                if (detail.bridge != null)
                {
                    this.Bridge.SelectedItem = (from p in bList
                                                where p.ID == detail.bridge.ID
                                                select p).FirstOrDefault();
                }

            }
            else
            {
                this.Bridge.ItemDisplayBinding = null;
                this.Bridge.IsEnabled = false;
                this.Bridge.WidthRequest = 75;
            }
        }

        
        private void Districts_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = sender as Picker;
            District ds = new District();
            ds = (District)picker.SelectedItem;
            if (ds != null)
            {
                pList = App.Database.GetParishItemsAsync().Result;
                this.Parish.ItemsSource = pList.Where(p => p.District_ID == ds.ID).ToList();
                this.Parish.ItemDisplayBinding = new Binding("Parish_Name");
                this.Parish.IsEnabled = true;

                var detail = (vmDetail)BindingContext;

                if (detail.parish != null)
                {
                    this.Parish.SelectedItem = (from p in pList
                                                where p.ID == detail.parish.ID
                                                select p).FirstOrDefault();
                }
            }
            else
            {
                this.Parish.ItemDisplayBinding = null;
                this.Parish.IsEnabled = false;
            }
        }
    }
}