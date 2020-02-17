using Prism;
using Prism.Ioc;
using Soccer.Common.Helpers;
using Soccer.Common.Services;
using Soccer.Prism.ViewModels;
using Soccer.Prism.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Soccer.Prism
{
    public partial class App
    {
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/TournamentsPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.Register<ITransformHelper, TransformHelper>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<TournamentsPage, TournamentsPageViewModel>();
            containerRegistry.RegisterForNavigation<GroupsPage, GroupsPageViewModel>();
            containerRegistry.RegisterForNavigation<MatchesPage, MatchesPageViewModel>();
            containerRegistry.RegisterForNavigation<ClosedMatchesPage, ClosedMatchesPageViewModel>();
            containerRegistry.RegisterForNavigation<TournamentTabbedPage, TournamentTabbedPageViewModel>();
            containerRegistry.RegisterForNavigation<TournamentTabbedPage, TournamentTabbedPageViewModel>();
        }
    }
}
