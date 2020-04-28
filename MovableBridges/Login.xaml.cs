using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MovableBridges.Model;

namespace MovableBridges
{
    [DesignTimeVisible(false)]
    public partial class Login : ContentPage
    {
        public IList<Bridges> Bridges { get; private set; }
        public Login()
        {
            InitializeComponent();
            //Bridges b = new Bridges();
            //b.BridgeId = 1;
            //b.BridgeName = "Test 1";
            //App.DAUtil.Insert(b);
            //b.BridgeId = 2;
            //b.BridgeName = "Test 2";
            //App.DAUtil.Insert(b);
            //Bridges = GetBridgeList();
            //BindingContext = this;
        }

    }
}