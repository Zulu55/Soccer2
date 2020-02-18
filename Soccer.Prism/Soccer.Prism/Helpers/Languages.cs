using Soccer.Prism.Interfaces;
using Soccer.Prism.Resources;
using Xamarin.Forms;

namespace Soccer.Prism.Helpers
{
    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Accept => Resource.Accept;

        public static string ConnectionError => Resource.ConnectionError;

        public static string Error => Resource.Error;

        public static string Name => Resource.Name;

        public static string MP => Resource.MP;

        public static string MW => Resource.MW;

        public static string MT => Resource.MT;

        public static string ML => Resource.ML;

        public static string PO => Resource.PO;

        public static string GD => Resource.GD;

        public static string Loading => Resource.Loading;

        public static string Tournaments => Resource.Tournaments;

        public static string MyPredictions => Resource.MyPredictions;

        public static string MyPositions => Resource.MyPositions;

        public static string ModifyUser => Resource.ModifyUser;

        public static string Login => Resource.Login;

        public static string Groups => Resource.Groups;

        public static string Pending => Resource.Pending;

        public static string Closed => Resource.Closed;
    }
}
