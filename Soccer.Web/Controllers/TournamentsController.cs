using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Soccer.Web.Data;
using Soccer.Web.Helpers;
using Soccer.Web.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer.Web.Controllers
{
    public class TournamentsController : Controller
    {
        private readonly DataContext _context;
        private readonly IImageHelper _imageHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly ICombosHelper _combosHelper;

        public TournamentsController(
            DataContext context,
            IImageHelper imageHelper,
            IConverterHelper converterHelper,
            ICombosHelper combosHelper)
        {
            _context = context;
            _imageHelper = imageHelper;
            _converterHelper = converterHelper;
            _combosHelper = combosHelper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context
                .Tournaments
                .Include(t => t.Groups)
                .OrderBy(t => t.StartDate)
                .ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TournamentViewModel model)
        {
            if (ModelState.IsValid)
            {
                string path = string.Empty;

                if (model.LogoFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.LogoFile, "Tournaments");
                }

                var tournament = _converterHelper.ToTournamentEntity(model, path, true);
                _context.Add(tournament);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournamentEntity = await _context.Tournaments.FindAsync(id);
            if (tournamentEntity == null)
            {
                return NotFound();
            }

            var model = _converterHelper.ToTournamentViewModel(tournamentEntity);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TournamentViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    var path = model.LogoPath;

                    if (model.LogoFile != null)
                    {
                        path = await _imageHelper.UploadImageAsync(model.LogoFile, "Tournaments");
                    }

                    var tournamentEntity = _converterHelper.ToTournamentEntity(model, path, false);
                    _context.Update(tournamentEntity);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournamentEntity = await _context.Tournaments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tournamentEntity == null)
            {
                return NotFound();
            }

            _context.Tournaments.Remove(tournamentEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournamentEntity = await _context.Tournaments
                .Include(t => t.Groups)
                .ThenInclude(t => t.Matches)
                .ThenInclude(t => t.Local)
                .Include(t => t.Groups)
                .ThenInclude(t => t.Matches)
                .ThenInclude(t => t.Visitor)
                .Include(t => t.Groups)
                .ThenInclude(t => t.GroupDetails)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tournamentEntity == null)
            {
                return NotFound();
            }

            return View(tournamentEntity);
        }

        public async Task<IActionResult> AddGroup(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournamentEntity = await _context.Tournaments.FindAsync(id);
            if (tournamentEntity == null)
            {
                return NotFound();
            }

            var model = new GroupViewModel 
            { 
                Tournament = tournamentEntity,
                TournamentId = tournamentEntity.Id 
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddGroup(GroupViewModel model)
        {
            if (ModelState.IsValid)
            {
                var groupEntity = await _converterHelper.ToGroupEntityAsync(model, true);
                _context.Add(groupEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction($"{nameof(Details)}/{model.TournamentId}");
            }

            return View(model);
        }

        public async Task<IActionResult> EditGroup(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupEntity = await _context.Groups
                .Include(g => g.Tournament)
                .FirstOrDefaultAsync(g => g.Id == id);
            if (groupEntity == null)
            {
                return NotFound();
            }

            var model = _converterHelper.ToGroupViewModel(groupEntity);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGroup(GroupViewModel model)
        {
            if (ModelState.IsValid)
            {
                var groupEntity = await _converterHelper.ToGroupEntityAsync(model, false);
                _context.Update(groupEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction($"{nameof(Details)}/{model.TournamentId}");
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteGroup(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupEntity = await _context.Groups
                .Include(g => g.Tournament)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groupEntity == null)
            {
                return NotFound();
            }

            _context.Groups.Remove(groupEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction($"{nameof(Details)}/{groupEntity.Tournament.Id}");
        }

        public async Task<IActionResult> DetailsGroup(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupEntity = await _context.Groups
                .Include(g => g.Matches)
                .ThenInclude(g => g.Local)
                .Include(g => g.Matches)
                .ThenInclude(g => g.Visitor)
                .Include(g => g.Tournament)
                .Include(g => g.GroupDetails)
                .ThenInclude(gd => gd.Team)
                .FirstOrDefaultAsync(g =>g.Id == id);
            if (groupEntity == null)
            {
                return NotFound();
            }

            return View(groupEntity);
        }

        public async Task<IActionResult> AddGroupDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupEntity = await _context.Groups.FindAsync(id);
            if (groupEntity == null)
            {
                return NotFound();
            }

            var model = new GroupDetailViewModel
            {
                Group = groupEntity,
                GroupId = groupEntity.Id,
                Teams = _combosHelper.GetComboTeams()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddGroupDetail(GroupDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var groupDetailEntity = await _converterHelper.ToGroupDetailEntityAsync(model, true);
                _context.Add(groupDetailEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction($"{nameof(DetailsGroup)}/{model.GroupId}");
            }

            model.Teams = _combosHelper.GetComboTeams();
            return View(model);
        }

        public async Task<IActionResult> AddMatch(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupEntity = await _context.Groups.FindAsync(id);
            if (groupEntity == null)
            {
                return NotFound();
            }

            var model = new MatchViewModel
            {
                Date = DateTime.Now,
                Group = groupEntity,
                GroupId = groupEntity.Id,
                Teams = _combosHelper.GetComboTeams(groupEntity.Id)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMatch(MatchViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.LocalId != model.VisitorId)
                {
                    var matchEntity = await _converterHelper.ToMatchEntityAsync(model, true);
                    _context.Add(matchEntity);
                    await _context.SaveChangesAsync();
                    return RedirectToAction($"{nameof(DetailsGroup)}/{model.GroupId}");
                }

                ModelState.AddModelError(string.Empty, "The local and vistitor must be differents teams.");
            }

            model.Group = await _context.Groups.FindAsync(model.GroupId);
            model.Teams = _combosHelper.GetComboTeams(model.GroupId);
            return View(model);
        }
    }
}
