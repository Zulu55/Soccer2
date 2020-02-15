using Soccer.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckTeamsAsync();
            await CheckTournamentsAsync();
        }

        private async Task CheckTeamsAsync()
        {
            if (!_context.Teams.Any())
            {
                AddTeam("America");
                AddTeam("Argentina");
                AddTeam("Bolivia");
                AddTeam("Brasil");
                AddTeam("Bucaramanga");
                AddTeam("Canada");
                AddTeam("Chile");
                AddTeam("Colombia");
                AddTeam("Costa Rica");
                AddTeam("Ecuador");
                AddTeam("Honduras");
                AddTeam("Junior");
                AddTeam("Medellin");
                AddTeam("Mexico");
                AddTeam("Millonarios");
                AddTeam("Nacional");
                AddTeam("Once Caldas");
                AddTeam("Panama");
                AddTeam("Paraguay");
                AddTeam("Peru");
                AddTeam("Santa Fe");
                AddTeam("Uruguay");
                AddTeam("USA");
                AddTeam("Venezuela");
                await _context.SaveChangesAsync();
            }
        }

        private void AddTeam(string name)
        {
            _context.Teams.Add(new TeamEntity { Name = name, LogoPath = $"~/images/Teams/{name}.jpg" });
        }

        private async Task CheckTournamentsAsync()
        {
            if (!_context.Tournaments.Any())
            {
                var startDate = DateTime.Today.AddMonths(2).ToUniversalTime();
                var endDate = DateTime.Today.AddMonths(3).ToUniversalTime();

                _context.Tournaments.Add(new TournamentEntity
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    IsActive = true,
                    LogoPath = $"~/images/Tournaments/Copa America 2020.png",
                    Name = "Copa America 2020",
                    Groups = new List<GroupEntity>
                    {
                        new GroupEntity
                        {
                             Name = "A",
                             GroupDetails = new List<GroupDetailEntity>
                             {
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Colombia") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Ecuador") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Panama") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Canada") }
                             },
                             Matches = new List<MatchEntity>
                             {
                                 new MatchEntity
                                 {
                                     Date = startDate.AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Colombia"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Ecuador")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Panama"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Canada")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(4).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Colombia"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Panama")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(4).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Ecuador"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Canada")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(9).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Canada"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Colombia")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(9).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Ecuador"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Panama")
                                 }
                             }
                        },
                        new GroupEntity
                        {
                             Name = "B",
                             GroupDetails = new List<GroupDetailEntity>
                             {
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Argentina") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Paraguay") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Mexico") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Chile") }
                             },
                             Matches = new List<MatchEntity>
                             {
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(1).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Argentina"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Paraguay")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(1).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Mexico"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Chile")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(5).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Argentina"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Mexico")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(5).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Paraguay"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Chile")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(10).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Chile"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Argentina")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(10).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Paraguay"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Mexico")
                                 }
                             }
                        },
                        new GroupEntity
                        {
                             Name = "C",
                             GroupDetails = new List<GroupDetailEntity>
                             {
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Brasil") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Venezuela") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "USA") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Peru") }
                             },
                             Matches = new List<MatchEntity>
                             {
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(2).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Brasil"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Venezuela")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(2).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "USA"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Peru")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(6).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Brasil"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "USA")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(6).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Venezuela"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Peru")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(11).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Peru"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Brasil")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(11).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Venezuela"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "USA")
                                 }
                             }
                        },
                        new GroupEntity
                        {
                             Name = "D",
                             GroupDetails = new List<GroupDetailEntity>
                             {
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Uruguay") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Bolivia") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Costa Rica") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Honduras") }
                             },
                             Matches = new List<MatchEntity>
                             {
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(3).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Uruguay"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Bolivia")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(3).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Costa Rica"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Honduras")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(7).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Uruguay"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Costa Rica")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(7).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Bolivia"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Honduras")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(12).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Honduras"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Uruguay")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(12).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Bolivia"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Costa Rica")
                                 }
                             }
                        }
                    }
                });

                startDate = DateTime.Today.AddMonths(1).ToUniversalTime();
                endDate = DateTime.Today.AddMonths(4).ToUniversalTime();

                _context.Tournaments.Add(new TournamentEntity
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    IsActive = true,
                    LogoPath = $"~/images/Tournaments/Liga Aguila 2020-I.png",
                    Name = "Liga Aguila 2020-I",
                    Groups = new List<GroupEntity>
                    {
                        new GroupEntity
                        {
                             Name = "A",
                             GroupDetails = new List<GroupDetailEntity>
                             {
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "America") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Bucaramanga") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Junior") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Medellin") }
                             },
                             Matches = new List<MatchEntity>
                             {
                                 new MatchEntity
                                 {
                                     Date = startDate.AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "America"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Bucaramanga")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Junior"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Medellin")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(4).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "America"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Junior")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(4).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Bucaramanga"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Medellin")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(9).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Medellin"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "America")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(9).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Bucaramanga"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Junior")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(15).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Bucaramanga"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "America")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(15).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Medellin"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Junior")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(19).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Junior"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "America")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(19).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Medellin"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Bucaramanga")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(19).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "America"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Medellin")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(19).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Junior"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Bucaramanga")
                                 }
                             }
                        },
                        new GroupEntity
                        {
                             Name = "B",
                             GroupDetails = new List<GroupDetailEntity>
                             {
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Millonarios") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Nacional") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Once Caldas") },
                                 new GroupDetailEntity { Team = _context.Teams.FirstOrDefault(t => t.Name == "Santa Fe") }
                             },
                             Matches = new List<MatchEntity>
                             {
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(1).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Millonarios"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Nacional")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(1).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Once Caldas"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Santa Fe")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(5).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Millonarios"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Once Caldas")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(5).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Nacional"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Santa Fe")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(10).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Santa Fe"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Millonarios")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(10).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Nacional"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Once Caldas")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(16).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Nacional"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Millonarios")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(16).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Santa Fe"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Once Caldas")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(20).AddHours(14),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Once Caldas"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Millonarios")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(20).AddHours(17),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Santa Fe"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Nacional")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(35).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Millonarios"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Santa Fe")
                                 },
                                 new MatchEntity
                                 {
                                     Date = startDate.AddDays(35).AddHours(16),
                                     Local = _context.Teams.FirstOrDefault(t => t.Name == "Once Caldas"),
                                     Visitor = _context.Teams.FirstOrDefault(t => t.Name == "Nacional")
                                 }
                             }
                        }
                    }
                });

                await _context.SaveChangesAsync();
            }
        }

    }
}
