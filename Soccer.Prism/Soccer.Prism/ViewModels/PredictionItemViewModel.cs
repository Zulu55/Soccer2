using Newtonsoft.Json;
using Prism.Commands;
using Soccer.Common.Helpers;
using Soccer.Common.Models;
using Soccer.Common.Services;
using Soccer.Prism.Helpers;
using System;
using System.Threading.Tasks;

namespace Soccer.Prism.ViewModels
{
    public class PredictionItemViewModel : PredictionResponse
    {
        private readonly IApiService _apiService;
        private DelegateCommand _updatePredictionCommand;

        public PredictionItemViewModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        public DelegateCommand UpdatePredictionCommand => _updatePredictionCommand ?? (_updatePredictionCommand = new DelegateCommand(UpdatePredictionAsync));

        private async void UpdatePredictionAsync()
        {
            bool isValid = await ValidateDataAsync();
            if (!isValid)
            {
                return;
            }

            string url = App.Current.Resources["UrlAPI"].ToString();
            bool connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }

            UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            PredictionRequest request = new PredictionRequest
            {
                GoalsLocal = GoalsLocal.Value,
                GoalsVisitor = GoalsVisitor.Value,
                MatchId = Match.Id,
                UserId = new Guid(user.Id),
                CultureInfo = Languages.Culture
            };

            Response response = await _apiService.MakePredictionAsync(url, "/api", "/Predictions", request, "bearer", token.Token);

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
            }
        }

        private async Task<bool> ValidateDataAsync()
        {
            if (GoalsLocal == null)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.LocalGoalsError, Languages.Accept);
                return false;
            }

            if (GoalsVisitor == null)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.VisitorGoalsError, Languages.Accept);
                return false;
            }

            if (Match.DateLocal <= DateTime.Now)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.MatchAlreadyStarts, Languages.Accept);
                return false;
            }

            return true;
        }
    }
}
