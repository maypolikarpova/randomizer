using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Controls;
using Randomizer.Managers;
using Randomizer.Tools;
using Randomizer.ViewModels;

namespace Randomizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IContentWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            var navigationModel = new NavigationModel(this);
            NavigationManager.Instance.Initialize(navigationModel);
            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
            DataContext = mainWindowViewModel;
            mainWindowViewModel.StartApplication();
        }

        public ContentControl ContentControl
        {
            get { return _contentControl; }
        }
    }
}
