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
    public partial class BridgeDetailPage : ContentPage
    {
        public BridgeDetailPage()
        {
            InitializeComponent();
            //District ds = new District();
            List<District> dsList = new List<District>();
            dsList.Add(new District { ID = 1, District_Name = "District1" });
            dsList.Add(new District { ID = 2, District_Name = "District2" });
            dsList.Add(new District { ID = 3, District_Name = "District3" });
            dsList.Add(new District { ID = 4, District_Name = "District4" });
            dsList.Add(new District { ID = 5, District_Name = "District5" });
            
            this.Districts.ItemsSource = dsList;
            this.Districts.ItemDisplayBinding = new Binding("District_Name");

            this.HeightRequest = 400;
        }

        async void OnSaveClicked(object sender, EventArgs e)
        {
            //var todoItem = (Bridges)BindingContext;
            //await App.Database.SaveItemAsync(todoItem);
            await Navigation.PopAsync();
        }

        async void OnDeleteClicked(object sender, EventArgs e)
        {
            //var todoItem = (Bridges)BindingContext;
            //await App.Database.DeleteItemAsync(todoItem);
            await Navigation.PopAsync();
        }

        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var picker = sender as Picker;
            //District ds = new District();
            //ds = (District)picker.SelectedItem;
            //List<Parish> cList = new List<Parish>();
            //cList.Add(new Parish { ID = 1, Parish_Name = "City1", District_ID = 1 });
            //cList.Add(new Parish { ID = 2, Parish_Name = "City2", District_ID = 2 });
            //cList.Add(new Parish { ID = 3, Parish_Name = "City3", District_ID = 3 });
            //cList.Add(new Parish { ID = 4, Parish_Name = "City4", District_ID = 1 });
            //cList.Add(new Parish { ID = 5, Parish_Name = "City5", District_ID = 5 });
            //cList.Add(new Parish { ID = 6, Parish_Name = "City6", District_ID = 3 });
            //cList.Add(new Parish { ID = 7, Parish_Name = "City7", District_ID = 3 });
            //cList.Add(new Parish { ID = 8, Parish_Name = "City8", District_ID = 1 });
            //cList.Add(new Parish { ID = 9, Parish_Name = "City9", District_ID = 2 });

            //this.Cities.ItemsSource = cList.Where(p => p.District_ID == ds.ID).ToList();
            //this.Cities.ItemDisplayBinding = new Binding("City_Name");
        }
        private void CityPicker_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
            
    }
}