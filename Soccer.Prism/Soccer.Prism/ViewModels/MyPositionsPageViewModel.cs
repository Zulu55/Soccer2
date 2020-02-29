using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Soccer.Prism.ViewModels
{
    public class MyPositionsPageViewModel : ViewModelBase
    {
        public MyPositionsPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "My Positons";
        }
    }
}
