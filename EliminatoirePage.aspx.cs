using GestionTournoi.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestionTournoi
{
    public partial class EliminatoirePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPhases();
            }
        }

        protected void btnGenererPhase_Click(object sender, EventArgs e)
        {
            int nbEquipes = int.Parse(ddlProfondeur.SelectedValue);
            string tri = rblTriEquipes.SelectedValue;

            var equipes = JsonStorage.LoadTeams();

            if (tri == "fortes")
                equipes = equipes.OrderByDescending(t => t.Points).Take(nbEquipes).ToList();
            else
                equipes = equipes.OrderBy(t => t.Points).Take(nbEquipes).ToList();

            if (equipes.Count < nbEquipes)
            {
                lblMessage.Text = "Pas assez d'équipes pour cette phase.";
                return;
            }

            var phase = new Eliminatoire($"Eliminatoire {ddlProfondeur.SelectedItem.Text}({(tri == "fortes" ? "top" : "bas")})");
            phase.Teams = equipes;

            for (int i = 0; i < nbEquipes / 2; i++)
            {
                var match = new Matchs(equipes[i].Name, equipes[nbEquipes - 1 - i].Name, phase.Name, MatchsPhase.Elimination);
            }

            // Charger les phases existantes
            var allPhases = JsonStorage.LoadEliminatoires();
            allPhases.Add(phase);
            JsonStorage.SaveEliminatoires(allPhases);

            lblMessage.Text = "Phase créée avec succès.";
            BindPhases();
        }

        private void BindPhases()
        {
            var phases = JsonStorage.LoadEliminatoires();
            rptPhases.DataSource = phases;
            rptPhases.DataBind();
        }
    }
}