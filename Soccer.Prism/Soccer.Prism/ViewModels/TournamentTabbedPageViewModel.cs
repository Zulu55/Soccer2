using Prism.Navigation;
using Soccer.Common.Models;

namespace Soccer.Prism.ViewModels
{
    public class TournamentTabbedPageViewModel : ViewModelBase
    {
        private TournamentResponse _tournament;

        public TournamentTabbedPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Tournament";
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("tournament"))
            {
                _tournament = parameters.GetValue<TournamentResponse>("tournament");
                Title = _tournament.Name;
            }
        }
    }
}
