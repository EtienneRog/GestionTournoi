using Microsoft.Ajax.Utilities;
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
            var equipes = JsonStorage.LoadTeams()
                .OrderByDescending(t => t.Level)
                .ThenBy(t => t.Name)
                .ToList();

            var poules = new List<Poule>();
            for (int i = 0; i < nbPoules; i++)
            {
                poules.Add(new Poule($"Poule {decalage + 1}-{i + 1}"));
            }

            int index = 0;
            int tour = 0;
            foreach (var team in equipes)
            {
                poules[(index + tour) % nbPoules].Teams.Add(team);
                index = (index + 1) % nbPoules;
                if (index % nbPoules == 0)
                    tour = tour + decalage;
            }

            List<Poule> oldPoules = JsonStorage.LoadPoules();
            foreach (var p in oldPoules)
            {
                p.Visible = false;
            }

            // Enregistrement dans JSON
            JsonStorage.UpdatePoules(oldPoules);
            JsonStorage.SavePoules(poules);
            return poules;
        }

        public static List<Poule> GetAllPoules()
        {
            return JsonStorage.LoadPoules();
        }
    }
}