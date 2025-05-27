using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Security;

namespace GestionTournoi.App_Code
{
    public static class JsonStorage
    {
        private static string MatchsFile => HttpContext.Current.Server.MapPath("~/App_Data/matchs.json");
        private static string TeamsFile => HttpContext.Current.Server.MapPath("~/App_Data/teams.json");
        private static string PoulesFile => HttpContext.Current.Server.MapPath("~/App_Data/poules.json");
        private static string EliminatoiresFile => HttpContext.Current.Server.MapPath("~/App_Data/eliminatoires.json");

        // === MATCHS ===
        public static List<Matchs> LoadMatchs()
        {
            LoadPoules(); 
            LoadEliminatoires();
            if (!File.Exists(MatchsFile)) return new List<Matchs>();
            var json = File.ReadAllText(MatchsFile);
            return JsonConvert.DeserializeObject<List<Matchs>>(json) ?? new List<Matchs>();
        }

        public static void SaveMatchs(List<Matchs> matchs)
        {
            List<Matchs> storedMatchs = LoadMatchs();
            storedMatchs.AddRange(matchs);
            var json = JsonConvert.SerializeObject(storedMatchs, Formatting.Indented);
            File.WriteAllText(MatchsFile, json);
        }
        public static void UpdateMatchs(List<Matchs> matchs)
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
            List<Team> storedTeams = LoadTeams();
            storedTeams.AddRange(teams);
            var json = JsonConvert.SerializeObject(storedTeams, Formatting.Indented);
            File.WriteAllText(TeamsFile, json);
        }

        public static void SaveTeams(Team team)
        {
            List<Team> storedTeams = LoadTeams();
            storedTeams.Add(team);
            var json = JsonConvert.SerializeObject(storedTeams, Formatting.Indented);
            File.WriteAllText(TeamsFile, json);
        }

        public static void UpdateTeams(List<Team> teams)
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
            return listPoules;
        }

        public static void SavePoules(List<Poule> poules)
        {
            List<Poule> storedPoule = LoadPoules();
            storedPoule.AddRange(poules);
            var json = JsonConvert.SerializeObject(storedPoule, Formatting.Indented);
            File.WriteAllText(PoulesFile, json);
        }

        public static void UpdatePoules(List<Poule> poules)
        {
            var json = JsonConvert.SerializeObject(poules, Formatting.Indented);
            File.WriteAllText(PoulesFile, json);
        }

        // === ELIMINATOIRES ===
        public static List<Eliminatoire> LoadEliminatoires()
        {
            if (!File.Exists(EliminatoiresFile)) return new List<Eliminatoire>();
            var json = File.ReadAllText(EliminatoiresFile);
            List<Eliminatoire> listEliminatoires = JsonConvert.DeserializeObject<List<Eliminatoire>>(json) ?? new List<Eliminatoire>();
            return listEliminatoires;
        }

        public static void SaveEliminatoires(List<Eliminatoire> eliminatoires)
        {
            List<Eliminatoire> storedEliminatoire = LoadEliminatoires();
            storedEliminatoire.AddRange(eliminatoires);
            var json = JsonConvert.SerializeObject(storedEliminatoire, Formatting.Indented);
            File.WriteAllText(EliminatoiresFile, json);
        }

        public static void SaveEliminatoires(Eliminatoire eliminatoire)
        {
            List<Eliminatoire> storedEliminatoire = LoadEliminatoires();
            storedEliminatoire.Add(eliminatoire);
            var json = JsonConvert.SerializeObject(storedEliminatoire, Formatting.Indented);
            File.WriteAllText(EliminatoiresFile, json);
        }

        public static void UpdateEliminatoires(List<Eliminatoire> eliminatoires)
        {
            var json = JsonConvert.SerializeObject(eliminatoires, Formatting.Indented);
            File.WriteAllText(EliminatoiresFile, json);
        }
    }
}