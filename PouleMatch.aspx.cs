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
    public partial class PouleMatch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string nomPoule = Request.QueryString["poule"];
                if (!string.IsNullOrEmpty(nomPoule))
                {
                    var poule = Poule.GetByName(nomPoule);
                    if (poule != null)
                    {
                        // ⚠ Génère les matchs SEULEMENT s’ils n’existent pas déjà
                        var matchs = MatchManager.GetOrCreateMatchsForPoule(poule);
                        rptMatchs.DataSource = matchs;
                        rptMatchs.DataBind();
                    }
                }
            }
        }

        protected void LoadMatchs(string nomPoule)
        {
            var matchs = MatchManager.GetMatchsByPoule(nomPoule);
            gvMatchs.DataSource = matchs;
            gvMatchs.DataBind();
        }

        protected void gvMatchs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SaveScore")
            {
                int rowIndex = Convert.ToInt32(((GridViewRow)((Button)e.CommandSource).NamingContainer).RowIndex);
                GridViewRow row = gvMatchs.Rows[rowIndex];

                int matchId = Convert.ToInt32(e.CommandArgument);
                int scoreA = int.Parse(((TextBox)row.FindControl("txtScoreA")).Text);
                int scoreB = int.Parse(((TextBox)row.FindControl("txtScoreB")).Text);

                MatchManager.UpdateScore(matchId, scoreA, scoreB);
                lblPoule.Text += $"- Score {matchId} sauvegardé";
            }
        }
    }
}