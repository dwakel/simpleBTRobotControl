using System;
using System.Linq;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Networking.Sockets;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace RobotZ
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PairDevice : Page
    {
        public PairDevice()
        {          
        }

        

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            Pair();
            base.OnNavigatedTo(e);
        }


        public async void Pair()
        {
           

        }

      
    }
}
