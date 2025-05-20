using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Newtonsoft.Json;

namespace GestionTournoi.App_Code
{
    public static class JsonStorage
    {
        private static string MatchsFile => HttpContext.Current.Server.MapPath("~/App_Data/matchs.json");
        private static string TeamsFile => HttpContext.Current.Server.MapPath("~/App_Data/teams.json");
        private static string PoulesFile => HttpContext.Current.Server.MapPath("~/App_Data/poules.json");

        // === MATCHS ===
        public static List<Matchs> LoadMatchs()
        {
            LoadPoules();
            if (!File.Exists(MatchsFile)) return new List<Matchs>();
            var json = File.ReadAllText(MatchsFile);
            return JsonConvert.DeserializeObject<List<Matchs>>(json) ?? new List<Matchs>();
        }

        public static void SaveMatchs(List<Matchs> matchs)
        {
            var json = JsonConvert.SerializeObject(matchs, Formatting.Indented);
            File.WriteAllText(MatchsFile, json);
        }

        // === TEAMS ===
        public static List<Team> LoadTeams()
        {
            if (!File.Exists(TeamsFile)) return new List<Team>();
            var json = File.ReadAllText(TeamsFile);
            return JsonConvert.DeserializeObject<List<Team>>(json) ?? new List<Team>();
        }

        public static void SaveTeams(List<Team> teams)
        {
            var json = JsonConvert.SerializeObject(teams, Formatting.Indented);
            File.WriteAllText(TeamsFile, json);
        }

        // === POULES ===
        public static List<Poule> LoadPoules()
        {
            if (!File.Exists(PoulesFile)) return new List<Poule>();
            var json = File.ReadAllText(PoulesFile);
            List<Poule> listPoules = JsonConvert.DeserializeObject<List<Poule>>(json) ?? new List<Poule>();
            PouleManager.LastGeneratedPoules = listPoules;
            return listPoules;
        }

        public static void SavePoules(List<Poule> poules)
        {
            var json = JsonConvert.SerializeObject(poules, Formatting.Indented);
            File.WriteAllText(PoulesFile, json);
        }
    }
}