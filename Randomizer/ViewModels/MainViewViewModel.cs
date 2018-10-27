using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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

        #endregion

        public ObservableCollection<Request> Requests
        {
            get { return _requests; }
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
            FillRequests();
            OnPropertyChanged(nameof(SelectedRequest));
            OnPropertyChanged(nameof(Requests));
        }

        private void AddRequestExecute(object o)
        {
            try
            {
                Request request = new Request(_startNumber, _endNumber, StationManager.CurrentUser);
                if (!request.IsValidRequest())
                {
                    MessageBox.Show(Resources.SignUp_FailedToValidateData);
                    return;
                }
                _requests.Add(request);
                _selectedRequest = request;
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format(Resources.SignIn_FailedToGetUser, Environment.NewLine,
                    ex.Message));
                return;
            }
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
