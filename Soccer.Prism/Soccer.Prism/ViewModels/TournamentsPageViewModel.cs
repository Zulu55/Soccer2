using DryIoc;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Soccer.Common.Models;
using Soccer.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Soccer.Prism.ViewModels
{
    public class TournamentsPageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private List<TournamentResponse> _tournaments;

        public TournamentsPageViewModel(INavigationService navigationService, IApiService apiService)
            : base(navigationService)
        {
            _apiService = apiService;
            Title = "Soccer";
            LoadTournamentsAsync();
        }

        public List<TournamentResponse> Tournaments 
        {
            get => _tournaments;
            set => SetProperty(ref _tournaments, value);
        }

        private async void LoadTournamentsAsync()
        {
            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetListAsync<TournamentResponse>(
                url,
                "/api",
                "/Tournaments");

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            Tournaments = (List<TournamentResponse>)response.Result;
        }
    }
}





