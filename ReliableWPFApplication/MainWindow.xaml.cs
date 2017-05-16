using System;
using System.Net;
using System.Threading;
using System.Windows;

namespace UnreliableWPFApplication
{
    public partial class MainWindow : Window
    {
        private int count = 1;
        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            InitializeComponent();
        }

        private void RssButton_Click(object sender, RoutedEventArgs e)
        {
            BusyIndicator.Visibility = Visibility.Visible;

            RssButton.IsEnabled = false;
          
            var client = new WebClient();
            client.Proxy = new WebProxy("lm8isa03", 8080);
            client.Proxy.Credentials = new NetworkCredential(@"DLMT01\lb570247", @"!qaz1234512345");

            client.DownloadStringAsync(new Uri("http://www.filipekberg.se/rss/"));

            client.DownloadStringCompleted += Client_DownloadStringCompleted;
        }

        private void Client_DownloadStringCompleted(object sender, 
            DownloadStringCompletedEventArgs e)
        {
            RssText.Text = e.Result;

            BusyIndicator.Visibility = Visibility.Hidden;

            RssButton.IsEnabled = true;
        }

        private void CounterButton_Click(object sender, RoutedEventArgs e)
        {
            CounterText.Text = $"Counter: {count++}";
        }
    }
}
