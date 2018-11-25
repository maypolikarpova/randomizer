using System.Windows;
using System.Windows.Controls;
using Randomizer.ViewModels;
using Randomizer.Views.Request;

namespace Randomizer.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView
    {
        private MainViewViewModel _mainWindowViewModel;
        private RequestReadingView _currentRequestCreationView;

        public MainView()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            Visibility = Visibility.Visible;
            _mainWindowViewModel = new MainViewViewModel();
            _mainWindowViewModel.RequestChanged+= OnRequestChanged;
            DataContext = _mainWindowViewModel;
        }

        private void OnRequestChanged(DBModels.Request request)
        {
            if (request != null)
            {
                _currentRequestCreationView = new RequestReadingView(request);
                MainGrid.Children.Add(_currentRequestCreationView);
                Grid.SetRow(_currentRequestCreationView, 0);
                Grid.SetRowSpan(_currentRequestCreationView, 2);
                Grid.SetColumn(_currentRequestCreationView, 1);
            }
        }
        
    }
}
