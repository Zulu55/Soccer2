using Soccer.Common.Models;
using Soccer.Web.Data.Entities;
using Soccer.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Soccer.Web.Helpers
{
    public interface IConverterHelper
    {
        TeamEntity ToTeamEntity(TeamViewModel model, string path, bool isNew);

        TeamViewModel ToTeamViewModel(TeamEntity teamEntity);

        TournamentEntity ToTournamentEntity(TournamentViewModel model, string path, bool isNew);

        TournamentViewModel ToTournamentViewModel(TournamentEntity tournamentEntity);

        Task<GroupEntity> ToGroupEntityAsync(GroupViewModel model, bool isNew);

        GroupViewModel ToGroupViewModel(GroupEntity groupEntity);

        Task<GroupDetailEntity> ToGroupDetailEntityAsync(GroupDetailViewModel model, bool isNew);

        GroupDetailViewModel ToGroupDetailViewModel(GroupDetailEntity groupDetailEntity);

        Task<MatchEntity> ToMatchEntityAsync(MatchViewModel model, bool isNew);

        MatchViewModel ToMatchViewModel(MatchEntity matchEntity);

        TournamentResponse ToTournamentResponse(TournamentEntity tournamentEntity);

        List<TournamentResponse> ToTournamentResponse(List<TournamentEntity> tournamentEntities);
    }
}
