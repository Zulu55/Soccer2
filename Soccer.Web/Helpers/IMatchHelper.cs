using System.Threading.Tasks;

namespace Soccer.Web.Helpers
{
    public interface IMatchHelper
    {
        Task CloseMatchAsync(int matchId, int goalsLocal, int goalsVisitor);
    }
}
