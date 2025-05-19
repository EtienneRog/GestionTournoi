using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionTournoi.App_Code
{

    public class TournamentLevel
    {
        public string Name { get; set; }
        public int Value { get; set; }

        public static List<TournamentLevel> GetAllLevels()
        {
            return new List<TournamentLevel>
            {
                new TournamentLevel { Name = "Départementale", Value = 1 },
                new TournamentLevel { Name = "Régionale", Value = 2 },
                new TournamentLevel { Name = "Nationale", Value = 3 }
            };
        }

        public static TournamentLevel GetByValue(int value)
        {
            return GetAllLevels().Find(l => l.Value == value);
        }
    }
}