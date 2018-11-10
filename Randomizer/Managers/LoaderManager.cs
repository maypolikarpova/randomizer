using Randomizer.Tools;
using System.Threading;
using System.Windows;

namespace Randomizer.Managers
{
    internal class LoaderManager
    {
        #region static
        private static readonly object Lock = new object();
        private static LoaderManager _instance;

        internal static LoaderManager Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;
                lock (Lock)
                {
                    return _instance = new LoaderManager();
                }
            }
        }
        #endregion
        private ILoaderOwner _loaderOwner;

        internal void Initialize(ILoaderOwner loaderOwner)
        {
            _loaderOwner = loaderOwner;
        }

        internal void ShowLoader()
        {
            _loaderOwner.LoaderVisibility = Visibility.Visible;
            _loaderOwner.IsEnabled = false;
            for (int i = 0; i < 100; i++)
            {
                _loaderOwner.PbCount++;
            }

        }

        internal void HideLoader()
        {
            _loaderOwner.LoaderVisibility = Visibility.Hidden;
            _loaderOwner.IsEnabled = true;
            _loaderOwner.PbCount = 0;
        }
    }
}
