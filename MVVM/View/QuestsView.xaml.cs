using EFTAllInOne.Quest;
using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Quest.Prapor;


namespace WpfApp1.MVVM.View
{
    /// <summary>
    /// Interaction logic for QuestsView.xaml
    /// </summary>
    public partial class QuestsView : UserControl
    {
        public QuestsView()
        {
            InitializeComponent();
        }

        private void PraporQuest(object sender, RoutedEventArgs e)
        {
            PraporQuest Prapor = new PraporQuest();
            Prapor.Show();
        }
        private void SkierQuestClick(object sender, RoutedEventArgs e)
        {
            SkierQuest Skier = new SkierQuest();
            Skier.Show();
        }
    }
}
