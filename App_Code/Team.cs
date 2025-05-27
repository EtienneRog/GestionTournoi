using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionTournoi.App_Code
{
    public class Team
    {
        public static int CompteurId = 0;
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int Points { get; set; }
        public int PointsDtl { get; set; }
        public Team(string name, int niveau, int points = 0, int pointsDtl = 0)
        {
            Id = CompteurId++;
            Name = name;
            Level = niveau;
            Points = points;
            PointsDtl = pointsDtl;
        }

        public string LevelName
        {
            get
            {
                return TournamentLevel.GetLevelName(Level);
            }
        }

    }
}