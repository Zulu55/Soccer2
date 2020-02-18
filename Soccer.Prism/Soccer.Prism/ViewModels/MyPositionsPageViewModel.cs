using Prism.Navigation;

namespace Soccer.Prism.ViewModels
{
    public class MyPositionsPageViewModel : ViewModelBase
    {
        public MyPositionsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "My Positions";
        }
    }
}
