using System;
using System.Collections.Generic;
using System.Web;

namespace GestionTournoi.App_Code
{
    public enum MatchsPhase
    {
        Poule,
        Elimination
    }

    [Serializable]
    public class Matchs
    {
        public static int CompteurId = 1;
        public int Id { get; set; }
        public string EquipeA { get; set; }
        public string EquipeB { get; set; }
        public int? ScoreA { get; set; }
        public int? ScoreB { get; set; }
        public string PhaseNom { get; set; }  // Pour identifier la poule
        public MatchsPhase Phase { get; set; }
        public EliminatoirePhase PhaseDtl { get; set; }

        public Matchs(string equipeA, string equipeB, string phaseNom, MatchsPhase phase, EliminatoirePhase phaseDtl = EliminatoirePhase.Autre)
        {
            Id = CompteurId++;
            EquipeA = equipeA;
            EquipeB = equipeB;
            PhaseNom = phaseNom;
            Phase = phase;
            PhaseDtl = phaseDtl;
        }
    }
}