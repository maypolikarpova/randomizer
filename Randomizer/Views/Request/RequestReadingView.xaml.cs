using Randomizer.ViewModels;

namespace Randomizer.Views.Request
{
    /// <summary>
    /// Interaction logic for RequestCreationView.xaml
    /// </summary>
    public partial class RequestReadingView
    {
        public RequestReadingView(Models.Request request)
        {
            InitializeComponent();
            var requestModel = new RequestReadingViewModel(request);
            DataContext = requestModel;
        }
    }
}
