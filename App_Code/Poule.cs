using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionTournoi.App_Code
{
    public class Poule
    {
        public string Name { get; set; }
        public List<Team> Teams { get; set; }
        public bool Visible { get; set; } = true;

        public Poule(string name)
        {
            Name = name;
            Teams = new List<Team>();
            Visible = true;
        }

        public static Poule GetByName(string nomPoule)
        {
            var poules = JsonStorage.LoadPoules();
            return poules.FirstOrDefault(p =>
                p.Name.Equals(nomPoule, StringComparison.OrdinalIgnoreCase));
        }
    }
}