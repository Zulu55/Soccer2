using Soccer.Web.Data.Entities;
using Soccer.Web.Models;

namespace Soccer.Web.Helpers
{
    public interface IConverterHelper
    {
        TeamEntity ToTeamEntity(TeamViewModel model, string path, bool isNew);

        TeamViewModel ToTeamViewModel(TeamEntity teamEntity);
    }
}
