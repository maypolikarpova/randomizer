using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Randomizer.Managers;
using Randomizer.Models;
using Randomizer.Properties;
using Randomizer.Tools;

namespace Randomizer.ViewModels
{
    class MainViewViewModel : INotifyPropertyChanged
    {
        #region Fields
        private Request _selectedRequest;
        private ObservableCollection<Request> _requests;
        private int _startNumber = 1;
        private int _endNumber = 10;
        #region Commands
        private ICommand _addRequestCommand;
        private ICommand _deleteRequestCommand;
        private ICommand _logOutCommand;
        #endregion
        #endregion

        #region Properties
        #region Commands

        public ICommand AddRequestCommand
        {
            get
            {
                return _addRequestCommand ?? (_addRequestCommand = new RelayCommand<object>(AddRequestExecute));
            }
        }

        public ICommand DeleteRequestCommand
        {
            get
            {
                return _deleteRequestCommand ?? (_deleteRequestCommand = new RelayCommand<KeyEventArgs>(DeleteRequestExecute));
            }
        }

        public ICommand LogOutCommand
        {
            get
            {
                return _logOutCommand ?? (_logOutCommand = new RelayCommand<KeyEventArgs>(LogOutExecute));
            }
        }
        #endregion

        public ObservableCollection<Request> Requests
        {
            get { return _requests; }
            set
            {
                _requests = value;
                OnPropertyChanged();
            }
        }

        public Request SelectedRequest
        {
            get { return _selectedRequest; }
            set
            {
                _selectedRequest = value;
                OnPropertyChanged();
            }
        }

        public int StartNumber
        {
            get { return _startNumber; }
            set
            {
                _startNumber = value;
                OnPropertyChanged();
            }
        }

        public int EndNumber
        {
            get { return _endNumber; }
            set
            {
                _endNumber = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Constructor
        public MainViewViewModel()
        {
            FillRequests();
            PropertyChanged += OnPropertyChanged;
        }
        #endregion

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "SelectedRequest")
                OnRequestChanged(_selectedRequest);
        }

        private void FillRequests()
        {
            _requests = new ObservableCollection<Request>();
            foreach (var request in StationManager.CurrentUser.Requests)
            {
                _requests.Add(request);
            }
            if (_requests.Count > 0)
            {
                _selectedRequest = Requests[0];
            }
        }

        private void DeleteRequestExecute(KeyEventArgs args)
        {
            if (args.Key != Key.Delete) return;

            if (SelectedRequest == null) return;

            StationManager.CurrentUser.Requests.RemoveAll(uwr => uwr.Guid == SelectedRequest.Guid);
            DBManager.UpdateUser(StationManager.CurrentUser);
            FillRequests();
            OnPropertyChanged(nameof(SelectedRequest));
            OnPropertyChanged(nameof(Requests));
        }

        private async void AddRequestExecute(object o)
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() =>
            {
                try
                {
                        Request request = new Request(_startNumber, _endNumber, StationManager.CurrentUser);
                        if (!request.IsValidRequest())
                        {
                            MessageBox.Show(String.Format("Wrongs numbers! Start number should be greater or equal 0, end number should be greater than 0 and start number"));
                            LoaderManager.Instance.HideLoader();
                            return;
                        }
                    Application.Current.Dispatcher.Invoke(delegate
                    {
                        DBManager.AddRequest(request);
                        _requests.Add(request);
                        _selectedRequest = request;
                        return;
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            });
            LoaderManager.Instance.HideLoader();
        }

        private void LogOutExecute(KeyEventArgs args)
        {
            _requests.Clear();
            NavigationManager.Instance.Navigate(ModesEnum.SignIn);
        }

        #region EventsAndHandlers
            #region Loader
        internal event RequestChangedHandler RequestChanged;
        internal delegate void RequestChangedHandler(Request request);

        internal virtual void OnRequestChanged(Request request)
        {
            RequestChanged?.Invoke(request);
        }
        #endregion
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }  
        #endregion
        #endregion


    }
}
