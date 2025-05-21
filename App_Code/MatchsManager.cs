using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace GestionTournoi.App_Code
{
    public class MatchsManager
    {
        public static List<Matchs> GetMatchsByPoule(string nomPoule)
        {
            var allMatches = JsonStorage.LoadMatchs();

            var matchsExistants = allMatches
                .Where(m => m.PhaseNom.Equals(nomPoule, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (matchsExistants.Any())
                return matchsExistants;

            Poule poule = PouleManager.LastGeneratedPoules
                          .FirstOrDefault(p => p.Name.Equals(nomPoule, StringComparison.OrdinalIgnoreCase));
            var teams = poule.Teams
                .GroupBy(t => t.Name)
                .Select(g => g.First())
                .ToList();

            int id = allMatches.Any() ? allMatches.Max(m => m.Id) + 1 : 1;
            var nouveauxMatchs = new List<Matchs>();

            for (int i = 0; i < teams.Count; i++)
            {
                for (int j = i + 1; j < teams.Count; j++)
                {
                    nouveauxMatchs.Add(new Matchs(teams[i].Name, teams[j].Name, nomPoule, MatchsPhase.Poule));
                }
            }

            allMatches.AddRange(nouveauxMatchs);
            JsonStorage.SaveMatchs(allMatches);

            return nouveauxMatchs;
        }

        public static void UpdateScore(Matchs match, int scoreA, int scoreB)
        {

            if (match == null || (match.ScoreA.HasValue && match.ScoreB.HasValue))
                return;

            match.ScoreA = scoreA;
            match.ScoreB = scoreB;
            int ecart = Math.Abs(scoreA - scoreB);

            List<Team> allTeams = JsonStorage.LoadTeams();

            Team equipeA = allTeams
            .FirstOrDefault(m => m.Name.Equals(match.EquipeA, StringComparison.OrdinalIgnoreCase));
            Team equipeB = allTeams
            .FirstOrDefault(m => m.Name.Equals(match.EquipeB, StringComparison.OrdinalIgnoreCase));
            if (equipeA == null || equipeB == null)
                return;
            Team gagnant, perdant;

            if (scoreA > scoreB)
            {
                gagnant = equipeA;
                perdant = equipeB;
            }
            else if (scoreB > scoreA)
            {
                gagnant = equipeB;
                perdant = equipeA;
            }
            else
            {
                equipeA.Points += 3;
                equipeB.Points += 3;
                JsonStorage.SaveTeams(allTeams);
                return;
            }

            gagnant.Points += 5;

            if (ecart < 3)
                perdant.Points += 4;
            else if (ecart < 6)
                perdant.Points += 3;
            else if (ecart < 9)
                perdant.Points += 2;
            else
                perdant.Points += 1;

            JsonStorage.SaveTeams(allTeams);
        }


        public static List<Matchs> GetAllMatchs()
        {
            return JsonStorage.LoadMatchs();
        }

        public static List<Matchs> GetOrCreateMatchsForPoule(Poule poule)
        {
            var allMatchs = JsonStorage.LoadMatchs();

            var existants = allMatchs
                .Where(m => m.PhaseNom.Equals(poule.Name, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (existants.Count > 0)
                return existants;

            var nouveaux = GetMatchsByPoule(poule.Name);
            allMatchs.AddRange(nouveaux);
            JsonStorage.SaveMatchs(allMatchs);

            return nouveaux;
        }

    }
}