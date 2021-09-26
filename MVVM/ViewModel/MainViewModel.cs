using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Core;

namespace WpfApp1.MVVM.ViewModel
{
    internal class MainViewModel : ObersvableObject
    {

        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand GamesViewCommand { get; set; }
        public RelayCommand QuestsViewCommand { get; set; }
        public RelayCommand AmmoChartViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public GamesViewModel GamesVM { get; set; }
        public QuestsViewModel QuestsVM { get; set; }
        public AmmoChartViewModel AmmoChartVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            GamesVM = new GamesViewModel();
            QuestsVM = new QuestsViewModel();
            AmmoChartVM = new AmmoChartViewModel();
            CurrentView = HomeVM;
            
            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            GamesViewCommand = new RelayCommand(o =>
            {
                CurrentView = GamesVM;
            });

            QuestsViewCommand = new RelayCommand(o =>
            {
                CurrentView = QuestsVM;
            });

            AmmoChartViewCommand = new RelayCommand(o =>
            {
                CurrentView = AmmoChartVM;
            });
        }
    }
}
