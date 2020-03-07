using Prism.Navigation;
using Soccer.Common.Models;
using Soccer.Prism.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Soccer.Prism.ViewModels
{
    public class SoccerMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public SoccerMasterDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            LoadMenus();
        }

        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        private void LoadMenus()
        {
            List<Menu> menus = new List<Menu>
            {
                new Menu
                {
                    Icon = "tournament",
                    PageName = "TournamentsPage",
                    Title = Languages.Tournaments
                },
                new Menu
                {
                    Icon = "prediction",
                    PageName = "MyPredictionsPage",
                    Title = Languages.MyPredictions
                },
                new Menu
                {
                    Icon = "medal",
                    PageName = "MyPositionsPage",
                    Title = Languages.MyPositions
                },
                new Menu
                {
                    Icon = "user",
                    PageName = "ModifyUserPage",
                    Title = Languages.ModifyUser
                },
                new Menu
                {
                    Icon = "login",
                    PageName = "LoginPage",
                    Title = Languages.Login
                }
            };

            Menus = new ObservableCollection<MenuItemViewModel>(
                menus.Select(m => new MenuItemViewModel(_navigationService)
                {
                    Icon = m.Icon,
                    PageName = m.PageName,
                    Title = m.Title
                }).ToList());
        }
    }
}