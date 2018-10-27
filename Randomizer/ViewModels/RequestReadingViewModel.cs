using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Randomizer.Models;
using Randomizer.Properties;

namespace Randomizer.ViewModels
{
    internal class RequestReadingViewModel : INotifyPropertyChanged
    {
        #region Fields
        private readonly Request _currentRequest;
        #endregion

        #region Properties

        public DateTime Date
        {
            get { return _currentRequest.Date; }
            set
            {
                _currentRequest.Date = value;
                OnPropertyChanged();
            }
        }
        public int StartNumber
        {
            get { return _currentRequest.StartNumber; }
            set
            {
                _currentRequest.StartNumber = value;
                OnPropertyChanged();
            }
        }
        public int EndNumber
        {
            get { return _currentRequest.EndNumber; }
            set
            {
                _currentRequest.EndNumber = value;
                OnPropertyChanged();
            }
        }
        public int GeneratedAmount
        {
            get { return _currentRequest.GeneratedAmount; }
            set
            {
                _currentRequest.GeneratedAmount = value;
                OnPropertyChanged();
            }
        }
        public ISet<int> Sequence
        {
            get { return _currentRequest.Sequence; }
            set
            {
                _currentRequest.Sequence = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructor
        public RequestReadingViewModel(Request request)
        {
            _currentRequest = request;
        }
        #endregion
        #region EventsAndHandlers
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        internal virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #endregion


    }
}
