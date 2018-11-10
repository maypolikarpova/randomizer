using Randomizer.Managers;
using Randomizer.Properties;
using Randomizer.Tools;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Randomizer.ViewModels
{
    public class MainWindowViewModel : ILoaderOwner
    {
        private Visibility _visibility = Visibility.Hidden;
        private int _pbCount = 0;
        private bool _isEnabled = true;

        public Visibility LoaderVisibility
        {
            get { return _visibility; }
            set
            {
                _visibility = value;
                OnPropertyChanged();
            }
        }

        public int PbCount
        {
            get { return _pbCount; }
            set
            {
                _pbCount = value;
                OnPropertyChanged();
            }
        }
       
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            LoaderManager.Instance.Initialize(this);
        }

        internal void StartApplication()
        {
            NavigationManager.Instance.Navigate(StationManager.CurrentUser != null ? ModesEnum.Main : ModesEnum.SignIn);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
