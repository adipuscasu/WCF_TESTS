using GeoLib.Proxies;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;
using System.Windows;
using GeoLib.Client.Contracts;

namespace GeoLib.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Title = "UI Running on Thread " + Thread.CurrentThread.ManagedThreadId +
                         " | Process " + Process.GetCurrentProcess().Id.ToString();
        }

        private void btnGetInfo_Click(object sender, RoutedEventArgs e)
        {
            if (txtZipCode.Text == "") return;
            var proxy = new GeoClient("webEP");
            var data = proxy.GetZipInfo(txtZipCode.Text);
            if (data != null)
            {
                lblCity.Content = data.City;
                lblState.Content = data.State;
            }
            proxy.Close();
        }

        private void btnGetZipCodes_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtState.Text)) return;
            var endpointAddress = new EndpointAddress("net.tcp://localhost:8009/GeoService");
            Binding binding = new NetTcpBinding();

            var proxy = new GeoClient(binding, endpointAddress);
            var data = proxy.GetZips(txtState.Text);
            if (data != null) lstZips.ItemsSource = data;
            proxy.Close();
        }

        private void btnMakeCall_Click(object sender, RoutedEventArgs e)
        {
                ChannelFactory<IMessageService> factory = new ChannelFactory<IMessageService>("");
                IMessageService proxy = factory.CreateChannel();
                proxy.ShowMessage(txtMessage.Text);

                factory.Close();
        }
    }
}
