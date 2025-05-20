using System;
using System.Collections.Generic;
using System.Linq;
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
        public string PouleNom { get; set; }  // Pour identifier la poule
        public MatchsPhase Phase { get; set; } // <- nouvelle propriété

        public Matchs(string equipeA, string equipeB, string pouleNom, MatchsPhase phase)
        {
            Id = CompteurId++;
            EquipeA = equipeA;
            EquipeB = equipeB;
            PouleNom = pouleNom;
            Phase = phase;
        }
    }
}