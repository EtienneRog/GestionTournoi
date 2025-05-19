using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionTournoi.App_Code
{
    public static class PouleManager
    {
        public static List<Poule> GenererPoules(int nbPoules, int nbEquipesParPoule, int decalage)
        {
            var equipes = TeamManager.GetAllEquipes()
                .OrderByDescending(t => t.Level)
                .ThenBy(t => t.Name)
                .ToList();

            var poules = new List<Poule>();
            for (int i = 0; i < nbPoules; i++)
                poules.Add(new Poule("Poule " + (char)('A' + i)));

            int index = 0;
            foreach (var team in equipes)
            {
                poules[index].Teams.Add(team);
                index = (index + decalage) % nbPoules;
            }

            // Enregistrement dans JSON
            JsonStorage.SavePoules(poules);
            return poules;
        }

        public static List<Poule> GetAllPoules()
        {
            return JsonStorage.LoadPoules();
        }
    }
}