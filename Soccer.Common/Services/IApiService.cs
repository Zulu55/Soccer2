using Soccer.Common.Models;
using Soccer.Common.Models.Soccer.Common.Models;
using System.Threading.Tasks;

namespace Soccer.Common.Services
{
    public interface IApiService
    {
        Task<Response> GetListAsync<T>(string urlBase, string servicePrefix, string controller);

        Task<Response> GetListAsync<T>(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken);

        Task<bool> CheckConnectionAsync(string url);

        Task<Response> GetTokenAsync(string urlBase, string servicePrefix, string controller, TokenRequest request);

        Task<Response> GetTokenAsync(string urlBase, string servicePrefix, string controller, FacebookProfile request);

        Task<Response> GetUserByEmail(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, EmailRequest request);

        Task<Response> RegisterUserAsync(string urlBase, string servicePrefix, string controller, UserRequest userRequest);

        Task<Response> RecoverPasswordAsync(string urlBase, string servicePrefix, string controller, EmailRequest emailRequest);

        Task<Response> PutAsync<T>(string urlBase, string servicePrefix, string controller, T model, string tokenType, string accessToken);

        Task<Response> ChangePasswordAsync(string urlBase, string servicePrefix, string controller, ChangePasswordRequest changePasswordRequest, string tokenType, string accessToken);

        Task<Response> GetPredictionsForUserAsync(string urlBase, string servicePrefix, string controller, PredictionsForUserRequest predictionsForUserRequest, string tokenType, string accessToken);

        Task<Response> MakePredictionAsync(string urlBase, string servicePrefix, string controller, PredictionRequest predictionRequest, string tokenType, string accessToken);
    }
}
