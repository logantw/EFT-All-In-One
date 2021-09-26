using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Maps;

namespace WpfApp1.MVVM.View
{
    /// <summary>
    /// Interaction logic for GamesView.xaml
    /// </summary>
    public partial class GamesView : UserControl
    {
        public GamesView()
        {
            InitializeComponent();
        }

        private void WoodsMap(object sender, RoutedEventArgs e)
        {
            WoodsMap Woods = new WoodsMap();
            Woods.Show();
        }

        private void CustomsMap(object sender, RoutedEventArgs e)
        {
            CustomsMap Customs = new CustomsMap();
            Customs.Show();
        }

        private void ShorelineMap(object sender, RoutedEventArgs e)
        {
            ShorelineMap Shoreline = new ShorelineMap();
            Shoreline.Show();
        }
        private void ReserveMap(object sender, RoutedEventArgs e)
        {
            ReserveMap Reserve = new ReserveMap();
            Reserve.Show();
        }

        private void FactoryMap(object sender, RoutedEventArgs e)
        {
            FactoryMap Factory = new FactoryMap();
            Factory.Show();
        }

        private void InterchangeMap(object sender, RoutedEventArgs e)
        {
            InterchangeMap Interchange = new InterchangeMap();
            Interchange.Show();
        }

        private void TheLabMap(object sender, RoutedEventArgs e)
        {
            TheLabMap TheLab = new TheLabMap();
            TheLab.Show();
        }
    }
}
