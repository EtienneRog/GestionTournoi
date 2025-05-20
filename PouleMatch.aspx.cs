using GestionTournoi.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace GestionTournoi
{
    public partial class PouleMatch : System.Web.UI.Page
    {
        private string PouleNom => Request.QueryString["poule"];
        private List<Matchs> Matchs
        {
            get => ViewState["Matchs"] as List<Matchs>;
            set => ViewState["Matchs"] = value;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                litPouleNom.Text = PouleNom;
                Matchs = JsonStorage.LoadMatchs();
                ChargerMatchs();
            }
        }

        private void ChargerMatchs()
        {
           var pouleMatchs= MatchsManager.GetMatchsByPoule(PouleNom);
            gvMatchs.DataSource = pouleMatchs;
            gvMatchs.DataBind();
        }

        protected void btnSaveAll_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvMatchs.Rows)
            {
                var lblEquipeA = (Label)row.FindControl("lblEquipeA");
                string equipeA = lblEquipeA.Text.Trim();
                var lblEquipeB = (Label)row.FindControl("lblEquipeB");
                string equipeB = lblEquipeB.Text.Trim();
                string strScoreA = ((TextBox)row.FindControl("txtScoreA")).Text;
                string strScoreB = ((TextBox)row.FindControl("txtScoreB")).Text;

                if (int.TryParse(strScoreA, out int scoreA) && int.TryParse(strScoreB, out int scoreB))
                {
                    var match = Matchs.FirstOrDefault(m =>
                        m.EquipeA == equipeA && m.EquipeB == equipeB);
                    if (match != null)
                    {
                        MatchsManager.UpdateScore(match, scoreA, scoreB);
                    }
                }
            }

            JsonStorage.SaveMatchs(Matchs);
        }
    }
}