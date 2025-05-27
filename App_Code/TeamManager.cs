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
            JsonStorage.SaveTeams(team);
        }

        public static List<Team> GetAllEquipes()
        {
            return JsonStorage.LoadTeams();
        }

        public static void ClearTeams()
        {
            JsonStorage.UpdateTeams(new List<Team>());
        }
        public static void SynchroniserEquipes(Team equipeMaj)
        {
            // Mise à jour des poules
            var poules = JsonStorage.LoadPoules();

            foreach (var poule in poules)
            {
                for (int i = 0; i < poule.Teams.Count; i++)
                {
                    if (poule.Teams[i].Id == equipeMaj.Id)
                    {
                        poule.Teams[i] = equipeMaj;
                    }
                }
            }

            JsonStorage.UpdatePoules(poules);

            // Mise à jour des phases éliminatoires
            var eliminatoires = JsonStorage.LoadEliminatoires();

            foreach (var eliminatoire in eliminatoires)
            {
                for (int i = 0; i < eliminatoire.Teams.Count; i++)
                {
                    if (eliminatoire.Teams[i].Id == equipeMaj.Id)
                    {
                        eliminatoire.Teams[i] = equipeMaj;
                    }
                }
            }
            JsonStorage.UpdateEliminatoires(eliminatoires);
        }
    }
}