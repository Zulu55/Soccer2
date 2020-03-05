using Newtonsoft.Json;
using Prism.Navigation;
using Soccer.Common.Helpers;
using Soccer.Common.Models;
using Soccer.Common.Services;
using Soccer.Prism.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Soccer.Prism.ViewModels
{
    public class PositionsInTournamentPageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private TournamentResponse _tournament;
        private bool _isRunning;
        private ObservableCollection<PositionResponse> _positions;

        public PositionsInTournamentPageViewModel(INavigationService navigationService, IApiService apiService)
            : base(navigationService)
        {
            _apiService = apiService;
            Title = Languages.Positions;
            LoadPositionsAsync();
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public ObservableCollection<PositionResponse> Positions
        {
            get => _positions;
            set => SetProperty(ref _positions, value);
        }

        private async void LoadPositionsAsync()
        {
            IsRunning = true;
            _tournament = JsonConvert.DeserializeObject<TournamentResponse>(Settings.Tournament);
            string url = App.Current.Resources["UrlAPI"].ToString();
            bool connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }

            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            Response response = await _apiService.GetListAsync<PositionResponse>(url, "/api", $"/Predictions/{_tournament.Id}", "bearer", token.Token);
            IsRunning = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            List<PositionResponse> list = (List<PositionResponse>)response.Result;
            Positions = new ObservableCollection<PositionResponse>(list);
        }
    }
}
