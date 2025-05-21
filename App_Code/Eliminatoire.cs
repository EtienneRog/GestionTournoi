using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionTournoi.App_Code
{
    public class Eliminatoire
    {
        public string Name { get; set; }
        public List<Team> Teams { get; set; }
    
        public Eliminatoire(string name)
        {
            Name = name;
            Teams = new List<Team>();
        }
        public static Eliminatoire GetByName(string nomEliminatoire)
        {
            var eliminatoires = JsonStorage.LoadEliminatoires();
            return eliminatoires.FirstOrDefault(p =>
                p.Name.Equals(nomEliminatoire, StringComparison.OrdinalIgnoreCase));
        }
    }
}