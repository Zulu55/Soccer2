using Prism.Commands;
using Soccer.Common.Models;
using Soccer.Prism.Helpers;
using System.Threading.Tasks;

namespace Soccer.Prism.ViewModels
{
    public class PredictionItemViewModel : PredictionResponse
    {
        private DelegateCommand _updatePredictionCommand;

        public PredictionItemViewModel()
        {
        }

        public DelegateCommand UpdatePredictionCommand => _updatePredictionCommand ?? (_updatePredictionCommand = new DelegateCommand(UpdatePredictionAsync));

        public bool IsUpdated { get; set; }

        private async void UpdatePredictionAsync()
        {
            bool isValid = await ValidateDataAsync();
            if (!isValid)
            {
                return;
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

            return true;
        }
    }
}
