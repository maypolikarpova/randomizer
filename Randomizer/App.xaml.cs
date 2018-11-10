using System.Windows;
using Randomizer.Managers;

namespace Randomizer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            StationManager.CloseApp();
        }
    }
}
