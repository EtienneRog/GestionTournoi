using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionTournoi.App_Code
{
    public class EliminatoireManager
    {

        public static void GenererEliminatoire(int nombreEquipes, string triType)
        {
            var phase = new Eliminatoire($"Eliminatoire {triType}-{nombreEquipes.ToString()}");
            List<Team> allTeams = JsonStorage.LoadTeams();
            List<Team> selection;
            // Vérification : pas assez d'équipes
            if (allTeams.Count <= nombreEquipes)
                throw new InvalidOperationException("Pas assez d'équipes pour générer cette phase.");

            if (triType.Equals("faible", StringComparison.OrdinalIgnoreCase))
            {
                selection = allTeams
                    .OrderBy(t => t.Points)
                    .Take(nombreEquipes)
                    .ToList();
            }
            else
            {
                // Valeur par défaut : on prend les plus forts
                selection = allTeams
                    .OrderByDescending(t => t.Points)
                    .Take(nombreEquipes)
                    .ToList();
            }


            // Sélection des équipes nécessaires
            phase.Teams = selection;
            phase.Etape = (EliminatoirePhase)nombreEquipes;

            // Appariement en matchs
            List<Matchs> matchs = new List<Matchs>();

            for (int i = 0; i < selection.Count / 2; i++)
            {
                var equipeA = selection[i];
                var equipeB = selection[selection.Count - 1 - i];

                matchs.Add(new Matchs(equipeA.Name, equipeB.Name, phase.Name, MatchsPhase.Elimination, phase.Etape));
            }

            JsonStorage.SaveEliminatoires(phase);
            JsonStorage.SaveMatchs(matchs);
        }


        public static void GenererPhaseSuivante(string nomEliminatoire)
        {
            var eliminatoires = JsonStorage.LoadEliminatoires();
            Eliminatoire eliminatoire= eliminatoires.FirstOrDefault(p =>
                p.Name.Equals(nomEliminatoire, StringComparison.OrdinalIgnoreCase));
            var tousLesMatchs = JsonStorage.LoadMatchs(); // méthode qui charge tous les matchs (éliminatoires inclus)
            var matchs = tousLesMatchs
                .Where(m => string.Equals(m.PhaseNom, eliminatoire.Name, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (eliminatoire.Etape == EliminatoirePhase.Finale)
                return; // Plus de phase à générer (finale déjà jouée)

            var matchsDernierePhase = matchs
                .Where(m => m.PhaseDtl == eliminatoire.Etape)
                .ToList();

            foreach (var match in matchsDernierePhase)
            {
                if (!match.ScoreA.HasValue || !match.ScoreB.HasValue || match.ScoreA == match.ScoreB)
                {
                    return;
                }
            }
            var vainqueurs = new List<string>();
            foreach (var match in matchsDernierePhase)
            {
                vainqueurs.Add(match.ScoreA > match.ScoreB ? match.EquipeA : match.EquipeB);
            }

            var nouveauxMatchs = new List<Matchs>();
            for (int i = 0; i < vainqueurs.Count; i += 2)
            {
                var m = new Matchs(vainqueurs[i], vainqueurs[i + 1], eliminatoire.Name, MatchsPhase.Elimination, (EliminatoirePhase)(vainqueurs.Count)); //Rajouter  la phase eliminatoire
                nouveauxMatchs.Add(m);
            }
            eliminatoire.Etape = (EliminatoirePhase)vainqueurs.Count;
            JsonStorage.UpdateEliminatoires(eliminatoires);
            JsonStorage.SaveMatchs(nouveauxMatchs);
        }

        public static List<Eliminatoire> GetAllEliminatoires()
        {
            return JsonStorage.LoadEliminatoires();
        }
    }

}
