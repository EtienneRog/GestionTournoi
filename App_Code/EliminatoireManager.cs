﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionTournoi.App_Code
{
    public class EliminatoireManager
    {

        public static string GenererEliminatoire(int nombreEquipes, string triType)
        {
            string phaseName = $"Eliminatoire {triType}-{nombreEquipes}";

            // Vérifie si la phase existe déjà
            var phasesExistantes = JsonStorage.LoadEliminatoires();
            if (phasesExistantes.Any(p => p.Name.Equals(phaseName, StringComparison.OrdinalIgnoreCase)))
                return "⚠️ Une phase éliminatoire avec ce nom existe déjà.";

            var phase = new Eliminatoire(phaseName);
            List<Team> allTeams = JsonStorage.LoadTeams();
            List<Team> selection;
            // Vérification : pas assez d'équipes
            if (allTeams.Count < nombreEquipes)
                return "❌ Pas assez d'équipes pour générer cette phase.";

            if (triType.Equals("Consolante", StringComparison.OrdinalIgnoreCase))
            {
                selection = allTeams
                    .OrderBy(t => t.Points)
                    .ThenBy(t => t.PointsDtl)
                    .Take(nombreEquipes)
                    .ToList();
            }
            else
            {
                // Valeur par défaut : on prend les plus forts
                selection = allTeams
                    .OrderByDescending(t => t.Points)
                    .ThenByDescending(t => t.PointsDtl)
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
            return "ok";
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
            int n = vainqueurs.Count;
            EliminatoirePhase nouvellePhase = (EliminatoirePhase)(n);
            for (int i = 0; i < n / 2; i++)
            {
                var equipeA = vainqueurs[i];
                var equipeB = vainqueurs[n - 1 - i];

                var nouveauMatch = new Matchs(equipeA, equipeB, eliminatoire.Name, MatchsPhase.Elimination, nouvellePhase);
                nouveauxMatchs.Add(nouveauMatch);
            }
            eliminatoire.Etape = nouvellePhase;
            JsonStorage.UpdateEliminatoires(eliminatoires);
            JsonStorage.SaveMatchs(nouveauxMatchs);
        }

        public static List<Eliminatoire> GetAllEliminatoires()
        {
            return JsonStorage.LoadEliminatoires();
        }
    }

}
