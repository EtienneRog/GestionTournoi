using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionTournoi.App_Code
{
    public enum EliminatoirePhase
    {
        TrenteDeuxieme = 64,
        Seizieme = 32,
        Huitieme = 16,
        Quart = 8,
        Demi = 4,
        Finale = 2,
        Terminee = 1,
        Autre = 99
    }
    public class Eliminatoire
    {
        public string Name { get; set; }
        public List<Team> Teams { get; set; }
        public EliminatoirePhase Etape { get; set; }  

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