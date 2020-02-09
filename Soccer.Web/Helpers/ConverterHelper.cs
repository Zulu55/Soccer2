using Soccer.Web.Data.Entities;
using Soccer.Web.Models;

namespace Soccer.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        public TeamEntity ToTeamEntity(TeamViewModel model, string path, bool isNew)
        {
            return new TeamEntity
            {
                Id = isNew ? 0 : model.Id,
                LogoPath = path,
                Name = model.Name
            };
        }

        public TeamViewModel ToTeamViewModel(TeamEntity teamEntity)
        {
            return new TeamViewModel
            {
                Id = teamEntity.Id,
                LogoPath = teamEntity.LogoPath,
                Name = teamEntity.Name
            };
        }

        public TournamentEntity ToTournamentEntity(TournamentViewModel model, string path, bool isNew)
        {
            return new TournamentEntity
            {
                    EndDate = model.EndDate,
                    Groups = model.Groups,
                    Id = isNew ? 0 : model.Id,
                    IsActive = model.IsActive,
                    LogoPath = path,
                    Name = model.Name,
                    StartDate = model.StartDate
            };
        }

        public TournamentViewModel ToTournamentViewModel(TournamentEntity tournamentEntity)
        {
            return new TournamentViewModel
            {
                    EndDate = tournamentEntity.EndDate,
                    Groups = tournamentEntity.Groups,
                    Id = tournamentEntity.Id,
                    IsActive = tournamentEntity.IsActive,
                    LogoPath = tournamentEntity.LogoPath,
                    Name = tournamentEntity.Name,
                    StartDate = tournamentEntity.StartDate
            };
        }
    }
}
