using Prism.Navigation;

namespace Soccer.Prism.ViewModels
{
    public class MyPredictionsPageViewModel : ViewModelBase
    {
        public MyPredictionsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "My Predictions";
        }
    }
}
