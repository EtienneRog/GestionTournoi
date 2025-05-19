using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GestionTournoi.App_Code;

namespace GestionTournoi
{
    public partial class PoulesPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var poules = PouleManager.GetAllPoules();  // ← depuis JSON
                rptPoules.DataSource = poules;
                rptPoules.DataBind();
            }
        }

        // Méthode appelée lors du clic sur le bouton "Générer les Poules"
        protected void GeneratePoules_Click(object sender, EventArgs e)
        {
            // Récupérer les valeurs des champs du formulaire
            int nombrePoules = int.Parse(numPoulesTextBox.Text);  // Nombre de poules
            int equipesParPoule = int.Parse(equipesParPouleTextBox.Text);  // Nombre d'équipes par poule
            int decalage = int.Parse(decalageTextBox.Text) - 1;  // Nombre d'équipes par poule

            // Appeler la méthode pour générer les poules avec les paramètres donnés
            var poules = TeamManager.GenererPoules(nombrePoules, equipesParPoule, decalage);
            MatchManager.LastGeneratedPoules = poules;

            // Lier les données au Repeater
            RepeaterPoules.DataSource = poules;
            RepeaterPoules.DataBind();
        }

        // Méthode pour obtenir la liste des équipes dans une poule avec leur score
        protected string GetTeamList(RepeaterItem container)
        {
            // Récupérer la poule à partir de l'élément actuel du Repeater
            var poule = (Poule)container.DataItem;

            var teamsList = new StringBuilder();

            foreach (var team in poule.Teams)
            {
                teamsList.AppendFormat("<li>{0} - {1} points</li>", team.Name, team.Points);
            }

            // Retourner la liste des équipes sous forme de chaîne HTML
            return teamsList.ToString();
        }
    }
}