using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MovableBridges.Model;
using MovableBridges.ViewModel;
using MovableBridges.DataService;

namespace MovableBridges.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Maintenance : ContentPage
    {
        List<Bridge> bList = new List<Bridge>();
        //List<District> dsList = new List<District>();
        List<Parish> pList = new List<Parish>();
        public Maintenance()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            this.DetailsGrid.IsVisible = false;

            var dsList = App.Database.GetDistrictcItemsAsync().Result;
            this.Districts.ItemsSource = dsList;
            this.Districts.ItemDisplayBinding = new Binding("District_Name");

            this.Parish.IsEnabled = false;
            this.Bridge.IsEnabled = false;
        }

        private void BridgeMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = sender as Picker;
            Bridge ds = (Bridge)picker.SelectedItem;
            if (ds != null)
            {
                this.Bridge.WidthRequest = 175;

                this.Tenders.Text = ds.Tenders.ToString();
                this.Recall_Numer.Text = ds.Recall_Number.ToString();
                this.lblTypeDraw.Text = ds.Type_Draw.ToString();
                this.lblStateRoute.Text = ds.State_Route.ToString();
                this.lblMilePoint.Text = ds.Mile_Point.ToString();
                this.DetailsGrid.IsVisible = true;
            }
            else
            {
                this.Bridge.WidthRequest = 100;
            }
        }

        private async void ParishMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = sender as Picker;
            Parish ds = new Parish();
            ds = (Parish)picker.SelectedItem;
            this.DetailsGrid.IsVisible = false;
            if (ds != null)
            {
                bList = await App.Database.GetBridgeItemsAsync();
                this.Bridge.ItemsSource = bList.Where(p => p.Parish_Id == ds.ID).ToList();
                this.Bridge.ItemDisplayBinding = new Binding("Bridge_Name");
                this.Bridge.IsEnabled = true;
            }
            else
            {
                this.Bridge.ItemDisplayBinding = null;
                this.Bridge.IsEnabled = false;
                this.Bridge.WidthRequest = 75;
            }
        }


        private void DistrictMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = sender as Picker;
            District ds = new District();
            ds = (District)picker.SelectedItem;
            this.DetailsGrid.IsVisible = false;
            if (ds != null)
            {
                pList = App.Database.GetParishItemsAsync().Result;
                this.Parish.ItemsSource = pList.Where(p => p.District_ID == ds.ID).ToList();
                this.Parish.ItemDisplayBinding = new Binding("Parish_Name");
                this.Parish.IsEnabled = true;
            }
            else
            {
                this.Parish.ItemDisplayBinding = null;
                this.Parish.IsEnabled = false;
            }
        }

        async void OnMainSaveClicked(object sender, EventArgs e)
        {
            var bridge = (Bridge)BindingContext;
            //var bridge = new Bridge();
            //if (detail.ID == 0)
            //{
            //    opening.Date_Created = DateTime.Now;
            //    opening.Date_Modified = DateTime.Now;
            //}
            //else
            //{
            //    opening.Date_Modified = DateTime.Now;
            //    opening.ID = detail.ID;
            //}
            //opening.User_Modified = "Nag";


            Bridge bds = (Bridge)this.Bridge.ItemsSource[0];
            //bridge.ID = bds.ID;
            //bridge.Bridge_Name = (from p in bList
            //                       where p.ID == bds.ID
            //                       select p.Bridge_Name).FirstOrDefault();
            //bridge.Parish_Id = bds.Parish_Id;
            //bridge.Recall_Number = bds.Recall_Number;
            //bridge.Type_Draw = bds.Type_Draw;
            bds.Tenders = bridge.Tenders;
            
            await App.Database.SaveBItemAsync(bds);
            await Navigation.PopAsync();
        }

        async void OnMainDeleteClicked(object sender, EventArgs e)
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

        async void OnMainCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }


        async void OnSendDatatoServer(object sender, EventArgs e)
        {
            var ds = await App.Database.GetItemsAsync();
            var navigationOpenings = (from p in ds
                                      select p).ToList();
            var result = await BridgeDataService.SaveNavigationOpenings(navigationOpenings);
            if(result)
            {
               await DisplayAlert("Success", "Data Successfully Updated to Server", "Ok");
            }
            else
            {
                await DisplayAlert("Failure", "There was some problem updating data to server. Try again later. If problem persists, please contact Reddy", "Ok");
            }
        }

    }
}