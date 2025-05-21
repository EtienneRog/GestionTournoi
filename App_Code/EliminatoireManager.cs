using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionTournoi.App_Code
{
    public class EliminatoireManager
    {
        public static List<Eliminatoire> LastGeneratedEliminatoires { get; set; } = new List<Eliminatoire>();

        public static List<Eliminatoire> GenererEliminatoires(List<Eliminatoire> a)
        {
            return a;
        }

        public static List<Eliminatoire> GetAllEliminatoires()
        {
            return JsonStorage.LoadEliminatoires();
        }
    }

}
