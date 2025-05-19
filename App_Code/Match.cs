using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionTournoi.App_Code
{
    public class Match
    {
        public static int CompteurId = 0;
        public int Id { get; set; }
        public string EquipeA { get; set; }
        public string EquipeB { get; set; }
        public int? ScoreA { get; set; }
        public int? ScoreB { get; set; }
        public string PouleNom { get; set; }  // Pour identifier la poule
        public Match(string equipeA, string equipeB, string pouleNom)
        {
            Id = CompteurId++;
            EquipeA = equipeA;
            EquipeB = equipeB;
            PouleNom = pouleNom;
        }
    }
}