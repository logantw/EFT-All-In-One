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
using WpfApp1.Ammo;

namespace WpfApp1.MVVM.View
{
    /// <summary>
    /// Interaction logic for AmmoChartView.xaml
    /// </summary>
    public partial class AmmoChartView : UserControl
    {
        public AmmoChartView()
        {
            InitializeComponent();
        }




        private void QuickAmmo(object sender, RoutedEventArgs e)
        {
            QuickAmmo QAmmo = new QuickAmmo();
            QAmmo.Show();
        }

        private void AmmoHelp(object sender, RoutedEventArgs e)
        {
            AmmoHelp bulletHelp = new AmmoHelp();
                bulletHelp.Show();
        }
    }
}
