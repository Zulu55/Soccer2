using Microsoft.EntityFrameworkCore;
using Soccer.Common.Enums;
using Soccer.Web.Data;
using Soccer.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer.Web.Helpers
{
    public class MatchHelper : IMatchHelper
    {
        private readonly DataContext _context;
        private MatchEntity _matchEntity;
        private MatchStatus _matchStatus;

        public MatchHelper(DataContext context)
        {
            _context = context;
        }

        public async Task CloseMatchAsync(int matchId, int goalsLocal, int goalsVisitor)
        {
            _matchEntity = await _context.Matches
                .Include(m => m.Local)
                .Include(m => m.Visitor)
                .Include(m => m.Predictions)
                .Include(m => m.Group)
                .ThenInclude(g => g.GroupDetails)
                .ThenInclude(gd => gd.Team)
                .FirstOrDefaultAsync(m => m.Id == matchId);

            _matchEntity.GoalsLocal = goalsLocal;
            _matchEntity.GoalsVisitor = goalsVisitor;
            _matchEntity.IsClosed = true;
            _matchStatus = GetMatchStaus(_matchEntity.GoalsLocal.Value, _matchEntity.GoalsVisitor.Value);

            UpdatePointsInpredictions();
            UpdatePositions();

            await _context.SaveChangesAsync();
        }

        private void UpdatePointsInpredictions()
        {
            foreach (PredictionEntity predictionEntity in _matchEntity.Predictions)
            {
                predictionEntity.Points = GetPoints(predictionEntity);
            }
        }

        private int GetPoints(PredictionEntity predictionEntity)
        {
            int points = 0;
            if (predictionEntity.GoalsLocal == _matchEntity.GoalsLocal)
            {
                points += 2;
            }

            if (predictionEntity.GoalsVisitor == _matchEntity.GoalsVisitor)
            {
                points += 2;
            }

            if (_matchStatus == GetMatchStaus(predictionEntity.GoalsLocal.Value, predictionEntity.GoalsVisitor.Value))
            {
                points++;
            }

            return points;
        }

        private MatchStatus GetMatchStaus(int goalsLocal, int goalsVisitor)
        {
            if (goalsLocal > goalsVisitor)
            {
                return MatchStatus.LocalWin;
            }

            if (goalsVisitor > goalsLocal)
            {
                return MatchStatus.VisitorWin;
            }

            return MatchStatus.Tie;
        }

        private void UpdatePositions()
        {
            GroupDetailEntity local = _matchEntity.Group.GroupDetails.FirstOrDefault(gd => gd.Team == _matchEntity.Local);
            GroupDetailEntity visitor = _matchEntity.Group.GroupDetails.FirstOrDefault(gd => gd.Team == _matchEntity.Visitor);

            local.MatchesPlayed++;
            visitor.MatchesPlayed++;

            local.GoalsFor += _matchEntity.GoalsLocal.Value;
            local.GoalsAgainst += _matchEntity.GoalsVisitor.Value;
            visitor.GoalsFor += _matchEntity.GoalsVisitor.Value;
            visitor.GoalsAgainst += _matchEntity.GoalsLocal.Value;

            if (_matchStatus == MatchStatus.LocalWin)
            {
                local.MatchesWon++;
                visitor.MatchesLost++;
            }
            else if (_matchStatus == MatchStatus.VisitorWin)
            {
                visitor.MatchesWon++;
                local.MatchesLost++;
            }
            else
            {
                local.MatchesTied++;
                visitor.MatchesTied++;
            }
        }
    }
}
