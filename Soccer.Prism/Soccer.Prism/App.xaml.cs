using Prism;
using Prism.Ioc;
using Soccer.Common.Helpers;
using Soccer.Common.Services;
using Soccer.Prism.ViewModels;
using Soccer.Prism.Views;
using Syncfusion.Licensing;
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
            SyncfusionLicenseProvider.RegisterLicense("MjExMTY0QDMxMzcyZTM0MmUzMEc1K2xKbkhrV2RmMHByRXF6YUJDQlQ3RkxLZ3hxOVlyMHY0T1RiSUFEZUk9");
            InitializeComponent();
            await NavigationService.NavigateAsync("/SoccerMasterDetailPage/NavigationPage/TournamentsPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.Register<IRegexHelper, RegexHelper>();
            containerRegistry.Register<ITransformHelper, TransformHelper>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<TournamentsPage, TournamentsPageViewModel>();
            containerRegistry.RegisterForNavigation<GroupsPage, GroupsPageViewModel>();
            containerRegistry.RegisterForNavigation<MatchesPage, MatchesPageViewModel>();
            containerRegistry.RegisterForNavigation<ClosedMatchesPage, ClosedMatchesPageViewModel>();
            containerRegistry.RegisterForNavigation<TournamentTabbedPage, TournamentTabbedPageViewModel>();
            containerRegistry.RegisterForNavigation<SoccerMasterDetailPage, SoccerMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<MyPredictionsPage, MyPredictionsPageViewModel>();
            containerRegistry.RegisterForNavigation<MyPositionsPage, MyPositionsPageViewModel>();
            containerRegistry.RegisterForNavigation<ModifyUserPage, ModifyUserPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();
        }
    }
}
