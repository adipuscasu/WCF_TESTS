using System.ServiceModel;
using System.Threading;
using System.Windows;
using GeoLib.Contracts;
using GeoLib.Services;

namespace GeoLib.WindowsHost
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            btnStart.IsEnabled = true;
            btnStop.IsEnabled = false;

            this.Title = "UI Running on Thread " + Thread.CurrentThread.ManagedThreadId;
        }

        private ServiceHost _HostGeoManager = null;
        
        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            _HostGeoManager = new ServiceHost(typeof(GeoManager));
            _HostGeoManager.Open();

            btnStart.IsEnabled = false;
            btnStop.IsEnabled = true;
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            _HostGeoManager.Close();

            btnStart.IsEnabled = true;
            btnStop.IsEnabled = false;
        }
    }
}
