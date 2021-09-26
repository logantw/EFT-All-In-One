using System;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var client = new Client(IPAddress.Parse("127.0.0.1"), 1212);
            client.MessageRecieved += ClientOnMessageReceived;
            var task = new Task(() =>
            {
                client.StartClient();
            });
            task.Start();
        }

        private void ClientOnMessageReceived(object sender, Client.MessageRecievedEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => this.onlineUsersCountLabel.Content = e.Message));
        }

        private void GamesViewClick(object sender, RoutedEventArgs e)
        {
        }

        private void QuestsViewClick(object sender, RoutedEventArgs e)
        {
        }

        private void AmmoChartViewClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
