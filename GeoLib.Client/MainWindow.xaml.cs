using GeoLib.Proxies;
using System.Diagnostics;
using System.Threading;
using System.Windows;

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
            var proxy = new GeoClient();
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
            if (txtState.Text != null)
            {

            }
        }

        private void btnMakeCall_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
