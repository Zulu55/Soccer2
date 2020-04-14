﻿using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Soccer.Common.Helpers;
using Soccer.Common.Models;
using Soccer.Prism.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Soccer.Prism.ViewModels
{
    public class TournamentItemViewModel : TournamentResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectTournamentCommand;
        private DelegateCommand _selectTournament2Command;

        public TournamentItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectTournamentCommand => _selectTournamentCommand ?? 
            (_selectTournamentCommand = new DelegateCommand(SelectTournamentAsync));

        public DelegateCommand SelectTournament2Command => _selectTournament2Command ?? 
            (_selectTournament2Command = new DelegateCommand(SelectTournamentForPredictionAsync));

        private async void SelectTournamentAsync()
        {
            var parameters = new NavigationParameters
            {
                { "tournament", this }
            };

            await _navigationService.NavigateAsync(nameof(TournamentTabbedPage), parameters);
        }

        private async void SelectTournamentForPredictionAsync()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "tournament", this }
            };

            await _navigationService.NavigateAsync(nameof(PredictionsTabbedPage), parameters);
        }
    }
}
