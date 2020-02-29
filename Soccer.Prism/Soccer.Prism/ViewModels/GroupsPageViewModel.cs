using Prism.Common;
using Prism.Navigation;
using Soccer.Common.Helpers;
using Soccer.Common.Models;
using Soccer.Prism.Views;
using System.Collections.Generic;

namespace Soccer.Prism.ViewModels
{
    public class GroupsPageViewModel : ViewModelBase
    {
        private readonly ITransformHelper _transformHelper;
        private TournamentResponse _tournament;
        private List<Group> _groups;

        public GroupsPageViewModel(INavigationService navigationService, ITransformHelper transformHelper)
            : base(navigationService)
        {
            _transformHelper = transformHelper;
            Title = "Groups";
        }

        public List<Group> Groups
        {
            get => _groups;
            set => SetProperty(ref _groups, value);
        }

        public TournamentResponse Tournament 
        {
            get => _tournament;
            set => SetProperty(ref _tournament, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            Tournament = parameters.GetValue<TournamentResponse>("tournament");
            Title = Tournament.Name;
            Groups = _transformHelper.ToGroups(Tournament.Groups);
        }
    }
}
