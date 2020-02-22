using Soccer.Common.Models;
using Soccer.Web.Data;
using Soccer.Web.Data.Entities;
using Soccer.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;

        public ConverterHelper(DataContext context, ICombosHelper combosHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
        }

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
                EndDate = model.EndDate.ToUniversalTime(),
                Groups = model.Groups,
                Id = isNew ? 0 : model.Id,
                IsActive = model.IsActive,
                LogoPath = path,
                Name = model.Name,
                StartDate = model.StartDate.ToUniversalTime()
            };
        }

        public async Task<GroupEntity> ToGroupEntityAsync(GroupViewModel model, bool isNew)
        {
            return new GroupEntity
            {
                GroupDetails = model.GroupDetails,
                Id = isNew ? 0 : model.Id,
                Matches = model.Matches,
                Name = model.Name,
                Tournament = await _context.Tournaments.FindAsync(model.TournamentId)
            };
        }

        public GroupViewModel ToGroupViewModel(GroupEntity groupEntity)
        {
            return new GroupViewModel
            {
                GroupDetails = groupEntity.GroupDetails,
                Id = groupEntity.Id,
                Matches = groupEntity.Matches,
                Name = groupEntity.Name,
                Tournament = groupEntity.Tournament,
                TournamentId = groupEntity.Tournament.Id
            };
        }

        public async Task<GroupDetailEntity> ToGroupDetailEntityAsync(GroupDetailViewModel model, bool isNew)
        {
            return new GroupDetailEntity
            {
                GoalsAgainst = model.GoalsAgainst,
                GoalsFor = model.GoalsFor,
                Group = await _context.Groups.FindAsync(model.GroupId),
                Id = isNew ? 0 : model.Id,
                MatchesLost = model.MatchesLost,
                MatchesPlayed = model.MatchesPlayed,
                MatchesTied = model.MatchesTied,
                MatchesWon = model.MatchesWon,
                Team = await _context.Teams.FindAsync(model.TeamId)
            };
        }

        public GroupDetailViewModel ToGroupDetailViewModel(GroupDetailEntity groupDetailEntity)
        {
            return new GroupDetailViewModel
            {
                GoalsAgainst = groupDetailEntity.GoalsAgainst,
                GoalsFor = groupDetailEntity.GoalsFor,
                Group = groupDetailEntity.Group,
                GroupId = groupDetailEntity.Group.Id,
                Id = groupDetailEntity.Id,
                MatchesLost = groupDetailEntity.MatchesLost,
                MatchesPlayed = groupDetailEntity.MatchesPlayed,
                MatchesTied = groupDetailEntity.MatchesTied,
                MatchesWon = groupDetailEntity.MatchesWon,
                Team = groupDetailEntity.Team,
                TeamId = groupDetailEntity.Team.Id,
                Teams = _combosHelper.GetComboTeams()
            };
        }

        public async Task<MatchEntity> ToMatchEntityAsync(MatchViewModel model, bool isNew)
        {
            return new MatchEntity
            {
                Date = model.Date.ToUniversalTime(),
                GoalsLocal = model.GoalsLocal,
                GoalsVisitor = model.GoalsVisitor,
                Group = await _context.Groups.FindAsync(model.GroupId),
                Id = isNew ? 0 : model.Id,
                IsClosed = model.IsClosed,
                Local = await _context.Teams.FindAsync(model.LocalId),
                Visitor = await _context.Teams.FindAsync(model.VisitorId)
            };
        }

        public MatchViewModel ToMatchViewModel(MatchEntity matchEntity)
        {
            return new MatchViewModel
            {
                Date = matchEntity.Date.ToLocalTime(),
                GoalsLocal = matchEntity.GoalsLocal,
                GoalsVisitor = matchEntity.GoalsVisitor,
                Group = matchEntity.Group,
                GroupId = matchEntity.Group.Id,
                Id = matchEntity.Id,
                IsClosed = matchEntity.IsClosed,
                Local = matchEntity.Local,
                LocalId = matchEntity.Local.Id,
                Teams = _combosHelper.GetComboTeams(matchEntity.Group.Id),
                Visitor = matchEntity.Visitor,
                VisitorId = matchEntity.Visitor.Id
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

        public TournamentResponse ToTournamentResponse(TournamentEntity tournamentEntity)
        {
            return new TournamentResponse
            {
                EndDate = tournamentEntity.EndDate,
                Id = tournamentEntity.Id,
                IsActive = tournamentEntity.IsActive,
                LogoPath = tournamentEntity.LogoPath,
                Name = tournamentEntity.Name,
                StartDate = tournamentEntity.StartDate,
                Groups = tournamentEntity.Groups?.Select(g => new GroupResponse
                {
                    Id = g.Id,
                    Name = g.Name,
                    GroupDetails = g.GroupDetails?.Select(gd => new GroupDetailResponse
                    {
                        GoalsAgainst = gd.GoalsAgainst,
                        GoalsFor = gd.GoalsFor,
                        Id = gd.Id,
                        MatchesLost = gd.MatchesLost,
                        MatchesPlayed = gd.MatchesPlayed,
                        MatchesTied = gd.MatchesTied,
                        MatchesWon = gd.MatchesWon,
                        Team = ToTeamResponse(gd.Team)
                    }).ToList(),
                    Matches = g.Matches?.Select(m => new MatchResponse
                    {
                        Date = m.Date,
                        GoalsLocal = m.GoalsLocal,
                        GoalsVisitor = m.GoalsVisitor,
                        Id = m.Id,
                        IsClosed = m.IsClosed,
                        Local = ToTeamResponse(m.Local),
                        Visitor = ToTeamResponse(m.Visitor),
                        Predictions = m.Predictions?.Select(p => new PredictionResponse
                        {
                            GoalsLocal = p.GoalsLocal,
                            GoalsVisitor = p.GoalsVisitor,
                            Id = p.Id,
                            Points = p.Points,
                            User = ToUserResponse(p.User)
                        }).ToList()
                    }).ToList()
                }).ToList()
            };
        }

        public List<TournamentResponse> ToTournamentResponse(List<TournamentEntity> tournamentEntities)
        {
            List<TournamentResponse> list = new List<TournamentResponse>();
            foreach (TournamentEntity tournamentEntity in tournamentEntities)
            {
                list.Add(ToTournamentResponse(tournamentEntity));
            }

            return list;
        }

        private UserResponse ToUserResponse(UserEntity user)
        {
            if (user == null)
            {
                return null;
            }

            return new UserResponse
            {
                Address = user.Address,
                Document = user.Document,
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                PicturePath = user.PicturePath,
                Team = ToTeamResponse(user?.Team),
                UserType = user.UserType
            };
        }

        private TeamResponse ToTeamResponse(TeamEntity team)
        {
            if (team == null)
            {
                return null;
            }

            return new TeamResponse
            {
                Id = team.Id,
                LogoPath = team.LogoPath,
                Name = team.Name
            };
        }
    }
}
