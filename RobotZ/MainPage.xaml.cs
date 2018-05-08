using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using System.ComponentModel;
using Windows.Devices.Enumeration;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Windows.Gaming.Input;
using Windows.UI.Core;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RobotZ
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        public MainPage()
        {
            this.InitializeComponent();
            PairAsync();
           // gamepad = Gamepad.Gamepads.First();
            
        }
       // private Gamepad gamepad { get; set; }

        
        private StreamSocket _socket;
        private RfcommDeviceService _service;

        private void Game_KeyDow(CoreWindow sender, KeyEventArgs args)
        {

        }
        
        private async void PairAsync()
        {
            try
            {
                var devices =
                    await DeviceInformation.FindAllAsync(RfcommDeviceService.GetDeviceSelector(RfcommServiceId.SerialPort));

                var device = devices.Single(x => x.Name == "Dev B");
                //var device = devices.Where(x => x.Name == "HC-05");
                _service = await RfcommDeviceService.FromIdAsync(device.Id);

                _socket = new StreamSocket();

                await _socket.ConnectAsync(
                    _service.ConnectionHostName,
                    _service.ConnectionServiceName,
                    SocketProtectionLevel.BluetoothEncryptionWithAuthentication);
            }
            catch (Exception)
            {
                PairAsync();
            }
        }
        public DeviceInformation device { get; set; }
        private string _movementDirection;
        public string MovementDirection {
            get { return this._movementDirection; }
            set {
                this._movementDirection = value;
                OnPropertyChanged();
            }
        }
       
       public Windows.Media.SpeechRecognition.SpeechRecognizer speechRecognizer = new Windows.Media.SpeechRecognition.SpeechRecognizer();

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName]String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        public async void SpeechAsync()
        {
            var constraint = await speechRecognizer.CompileConstraintsAsync();
            Windows.Media.SpeechRecognition.SpeechRecognitionResult speechRecognitionResult = await speechRecognizer.RecognizeAsync();
            switch (speechRecognitionResult.Text.ToLower())
            {
                case "move forward":
                    Forward_Click(Forward, null);
                    break;
                case "moves forward":
                    Forward_Click(Forward, null);
                    break;
                case "movies forward":
                    Forward_Click(Forward, null);
                    break;
                case "moves straight":
                    Forward_Click(Forward, null);
                    break;
                case "movies straight":
                    Forward_Click(Forward, null);
                    break;
                case "move backward":
                    Backward_Click(Backward, null);
                    break;
                case "movies backward":
                    Backward_Click(Backward, null);
                    break;
                case "move backwards":
                    Backward_Click(Backward, null);
                    break;
                case "moves backwards":
                    Backward_Click(Backward, null);
                    break;
                case "move back":
                    Backward_Click(Backward, null);
                    break;
                case "reverse":
                    Backward_Click(Backward, null);
                    break;
                case "move left":
                    Left_Click(Left, null);
                    break;
                case "movies left":
                    Left_Click(Left, null);
                    break;
                case "move right":
                    Right_Click(Right, null);
                    break;
                case "moves right":
                    Right_Click(Right, null);
                    break;
                case "movies right":
                    Right_Click(Right, null);
                    break;
                case "light on":
                    On_Click(On, null);
                    break;
                case "lights on":
                    On_Click(On, null);
                    break;
                case "stop":
                    Stop_Click(Stop, null);
                    break;

                default:
                    break;
            }
        }

        public void Move(string command)
        {
            try
            {
                var writer = new DataWriter(_socket.OutputStream);
                writer.WriteString(command);

                var store = writer.StoreAsync().AsTask();
            }
            catch (Exception ex)
            {
                DirectionLabel.Text = ex.Message;  
            }

        }

        private void Forward_Click(object sender, RoutedEventArgs e)
        {
            //MovementDirection = "Moving Forward";
            DirectionLabel.Text = "Moving Forward";
            Move("3");  
        }

        private void Backward_Click(object sender, RoutedEventArgs e)
        {
            //MovementDirection = "Moving Backward";
            DirectionLabel.Text = "Moving Backward";
            Move("2");
            
        }

        private async void On_Click(object sender, RoutedEventArgs e)
        {
            Move("1");
            await System.Threading.Tasks.Task.Delay(5000);
            Move("0");
        }

        private void Left_Click(object sender, RoutedEventArgs e)
        {
            DirectionLabel.Text = "Moving Left";
            Move("5");
        }

        private void Right_Click(object sender, RoutedEventArgs e)
        {
            DirectionLabel.Text = "Moving Right";
            Move("6");
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            Move("4");
        }

        private void btn_Speech_Click(object sender, RoutedEventArgs e)
        {
            SpeechAsync();
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Move("4");
            base.OnNavigatedFrom(e);
        }
        private void Key_KeyDown(object sender, KeyRoutedEventArgs e)
        {
        //    var key = sender as KeyboardAccelerator;
        //    switch (key.Key)
        //    {
        //        case Windows.System.VirtualKey.Up:
        //            Forward_Click(Forward, null);
        //            break;
        //        case Windows.System.VirtualKey.Down:
        //            Backward_Click(Backward, null);
        //            break;
        //        case Windows.System.VirtualKey.Left:
        //            Left_Click(Left, null);
        //            break;
        //        case Windows.System.VirtualKey.Right:
        //            Right_Click(Right, null);
        //            break;
                
        //    }
        }

        private void SelfDriven_Click(object sender, RoutedEventArgs e)
        {
            Move("7");
        }
    }
}
