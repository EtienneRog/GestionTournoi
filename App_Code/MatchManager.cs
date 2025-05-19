using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionTournoi.App_Code
{
    public class MatchManager
    {
        public static List<Match> GetMatchsByPoule(Poule poule)
        {
            var allMatches = JsonStorage.LoadMatchs();

            var matchsExistants = allMatches
                .Where(m => m.PouleNom.Equals(poule.Name, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (matchsExistants.Any())
                return matchsExistants;

            var teams = poule.Teams
                .GroupBy(t => t.Name)
                .Select(g => g.First())
                .ToList();

            int id = allMatches.Any() ? allMatches.Max(m => m.Id) + 1 : 1;
            var nouveauxMatchs = new List<Match>();

            for (int i = 0; i < teams.Count; i++)
            {
                for (int j = i + 1; j < teams.Count; j++)
                {
                    nouveauxMatchs.Add(new Match(id++, teams[i].Name, teams[j].Name, poule.Name));
                }
            }

            allMatches.AddRange(nouveauxMatchs);
            JsonStorage.SaveMatchs(allMatches);

            return nouveauxMatchs;
        }

        public static void UpdateScore(int matchId, int scoreA, int scoreB)
        {
            var allMatches = JsonStorage.LoadMatchs();
            var match = allMatches.FirstOrDefault(m => m.Id == matchId);

            if (match != null)
            {
                match.ScoreA = scoreA;
                match.ScoreB = scoreB;
                JsonStorage.SaveMatchs(allMatches);
            }
        }

        public static List<Match> GetAllMatchs()
        {
            return JsonStorage.LoadMatchs();
        }

        public static List<Match> GetOrCreateMatchsForPoule(Poule poule)
        {
            var allMatchs = JsonStorage.LoadMatchs();

            var existants = allMatchs
                .Where(m => m.PouleNom.Equals(poule.Name, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (existants.Count > 0)
                return existants;

            var nouveaux = GetMatchsByPoule(poule);
            allMatchs.AddRange(nouveaux);
            JsonStorage.SaveMatchs(allMatchs);

            return nouveaux;
        }
    }
}