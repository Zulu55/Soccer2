using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Soccer.Common.Helpers;
using Soccer.Common.Models;
using Soccer.Common.Services;
using Soccer.Prism.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Soccer.Prism.ViewModels
{
    public class MyPositionsPageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private bool _isRunning;
        private ObservableCollection<PositionResponse> _positions;
        private List<PositionResponse> _myPositions;
        private string _search;
        private DelegateCommand _searchCommand;

        public MyPositionsPageViewModel(INavigationService navigationService, IApiService apiService)
            : base(navigationService)
        {
            _apiService = apiService;
            Title = Languages.Positions;
            LoadPositionsAsync();
        }

        public DelegateCommand SearchCommand => _searchCommand ?? (_searchCommand = new DelegateCommand(ShowPositions));

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public string Search
        {
            get => _search;
            set
            {
                SetProperty(ref _search, value);
                ShowPositions();
            }
        }

        public ObservableCollection<PositionResponse> Positions
        {
            get => _positions;
            set => SetProperty(ref _positions, value);
        }

        private async void LoadPositionsAsync()
        {
            IsRunning = true;
            string url = App.Current.Resources["UrlAPI"].ToString();
            bool connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }

            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            Response response = await _apiService.GetListAsync<PositionResponse>(url, "/api", $"/Predictions", "bearer", token.Token);
            IsRunning = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            _myPositions = (List<PositionResponse>)response.Result;
            ShowPositions();
        }

        private void ShowPositions()
        {
            if (string.IsNullOrEmpty(Search))
            {
                Positions = new ObservableCollection<PositionResponse>(_myPositions);
            }
            else
            {
                Positions = new ObservableCollection<PositionResponse>(
                    _myPositions.Where(p => p.UserResponse.FirstName.ToUpper().Contains(Search.ToUpper()) ||
                                            p.UserResponse.LastName.ToUpper().Contains(Search.ToUpper())));
            }
        }
    }
}
