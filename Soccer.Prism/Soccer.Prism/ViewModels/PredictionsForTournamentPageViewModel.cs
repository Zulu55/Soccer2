using Newtonsoft.Json;
using Prism.Navigation;
using Soccer.Common.Helpers;
using Soccer.Common.Models;
using Soccer.Common.Services;
using Soccer.Prism.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Soccer.Prism.ViewModels
{
    public class PredictionsForTournamentPageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private TournamentResponse _tournament;
        private bool _isRunning;
        private List<PredictionItemViewModel> _predictions;

        public PredictionsForTournamentPageViewModel(INavigationService navigationService, IApiService apiService)
            : base(navigationService)
        {
            _apiService = apiService;
            Title = Languages.PredictionsFor;
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public List<PredictionItemViewModel> Predictions
        {
            get => _predictions;
            set => SetProperty(ref _predictions, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            _tournament = parameters.GetValue<TournamentResponse>("tournament");
            Title = $"{Languages.PredictionsFor}: {_tournament.Name}";
            LoadPredictionsAsync();
        }

        private async void LoadPredictionsAsync()
        {
            IsRunning = true;
            var url = App.Current.Resources["UrlAPI"].ToString();
            var connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }

            UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            var request = new PredictionsForUserRequest
            {
                TournamentId = _tournament.Id,
                UserId = new Guid(user.Id)
            };

            Response response = await _apiService.GetPredictionsForUserAsync(url, "/api", "/Predictions/GetPredictionsForUser", request, "bearer", token.Token);
            IsRunning = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            List<PredictionResponse> list = (List<PredictionResponse>)response.Result;
            Predictions = list.Select(p => new PredictionItemViewModel
            {
                GoalsLocal = p.GoalsLocal,
                GoalsVisitor = p.GoalsVisitor,
                Id = p.Id,
                Match = p.Match,
                Points = p.Points,
                User = p.User
            })
                .Where(p => !p.Match.IsClosed && p.Match.DateLocal > DateTime.Now)
                .OrderBy(p => p.Match.Date)
                .ToList();
        }
    }
}
