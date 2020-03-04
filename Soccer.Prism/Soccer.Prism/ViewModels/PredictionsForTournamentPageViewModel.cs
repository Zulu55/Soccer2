using Prism.Navigation;

namespace Soccer.Prism.ViewModels
{
    public class PredictionsForTournamentPageViewModel : ViewModelBase
    {
        public PredictionsForTournamentPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Predictions for...";
        }
    }
}
