using Microsoft.AspNetCore.Mvc;
using Soccer.Web.Data;
using Soccer.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Soccer.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly DataContext _context;

        public TeamsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<TeamEntity> GetTeams()
        {
            return _context.Teams.OrderBy(t => t.Name);
        }
    }
}