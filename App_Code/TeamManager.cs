using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionTournoi.App_Code
{
    public class TeamManager
    {
        public static void AddTeam(Team team)
        {
            var teams = JsonStorage.LoadTeams();
            teams.Add(team);
            JsonStorage.SaveTeams(teams);
        }

        public static List<Team> GetAllEquipes()
        {
            return JsonStorage.LoadTeams();
        }

        public static void ClearTeams()
        {
            JsonStorage.SaveTeams(new List<Team>());
        }

    }
}