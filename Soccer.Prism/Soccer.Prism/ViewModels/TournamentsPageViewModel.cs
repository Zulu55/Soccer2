using Prism.Navigation;
using Soccer.Common.Models;
using Soccer.Common.Services;
using Soccer.Prism.Helpers;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Soccer.Prism.ViewModels
{
    public class TournamentsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private List<TournamentItemViewModel> _tournaments;
        private bool _isRunning;
        private static TournamentsPageViewModel _instance;

        public TournamentsPageViewModel(INavigationService navigationService, IApiService apiService) 
            : base(navigationService)
        {
            _instance = this;
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Soccer";
            LoadTournamentsAsync();
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public List<TournamentItemViewModel> Tournaments
        {
            get => _tournaments;
            set => SetProperty(ref _tournaments, value);
        }

        public static TournamentsPageViewModel GetInstance()
        {
            return _instance;
        }

        public void AddMessage(string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                App.Current.MainPage.DisplayAlert(Languages.Ok, message, Languages.Accept);
            });
        }

        private async void LoadTournamentsAsync()
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

            Response response = await _apiService.GetListAsync<TournamentResponse>(url, "/api", "/Tournaments");
            IsRunning = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            List<TournamentResponse> list = (List<TournamentResponse>)response.Result;
            Tournaments = list.Select(t => new TournamentItemViewModel(_navigationService)
            {
                EndDate = t.EndDate,
                Groups = t.Groups,
                Id = t.Id,
                IsActive = t.IsActive,
                LogoPath = t.LogoPath,
                Name = t.Name,
                StartDate = t.StartDate
            }).ToList();
        }
    }
}
