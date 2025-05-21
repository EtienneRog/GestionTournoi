using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionTournoi.App_Code
{
    public static class PouleManager
    {
        public static List<Poule> LastGeneratedPoules { get; set; } = new List<Poule>();

        public static List<Poule> GenererPoules(int nbPoules, int nbEquipesParPoule, int decalage)
        {
            var equipes = TeamManager.GetAllEquipes()
                .OrderByDescending(t => t.Level)
                .ThenBy(t => t.Name)
                .ToList();

            var poules = new List<Poule>();
            for (int i = 0; i < nbPoules; i++)
            {
                poules.Add(new Poule($"Poule {decalage + 1}-{i + 1}"));
            }

            int index = 0;
            foreach (var team in equipes)
            {
                poules[(index + decalage) % nbPoules].Teams.Add(team);
                index = (index + 1) % nbPoules;
                if (index % nbPoules == 0)
                    decalage = (decalage + 1) * decalage;
            }
            foreach (var p in LastGeneratedPoules)
            {
                p.Visible = false;
            }

            LastGeneratedPoules.AddRange(poules);
            // Enregistrement dans JSON
            JsonStorage.SavePoules(LastGeneratedPoules);
            return poules;
        }

        public static List<Poule> GetAllPoules()
        {
            return JsonStorage.LoadPoules();
        }
    }
}