using System.ComponentModel;
using System.Windows;

namespace Randomizer.Tools
{
    internal interface ILoaderOwner : INotifyPropertyChanged
    {
        Visibility LoaderVisibility { get; set; }
        bool IsEnabled { get; set; }
        int PbCount { get; set; }
    }
}
