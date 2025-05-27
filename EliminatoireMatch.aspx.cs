using GestionTournoi.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestionTournoi
{
    public partial class EliminatoireMatch : System.Web.UI.Page
    {
        private string EliminatoireNom => Request.QueryString["phase"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ChargerMatchsEliminatoires();
            }
        }

        private void ChargerMatchsEliminatoires()
        {
            if (string.IsNullOrEmpty(EliminatoireNom))
            {
                lblMessage.Text = "Phase non spécifiée dans l'URL.";
                return;
            }

            var tousLesMatchs = JsonStorage.LoadMatchs(); // Charge tous les matchs
            var matchsDePhase = tousLesMatchs
                .Where(m => string.Equals(m.PhaseNom, EliminatoireNom, StringComparison.OrdinalIgnoreCase))
                .ToList();

            var affichage = matchsDePhase.Select(m => new
            {
                Id = m.Id,
                PhaseDtl = m.PhaseDtl,
                EquipeA = m.EquipeA,
                ScoreA = m.ScoreA.HasValue ? m.ScoreA.Value.ToString() : "",
                ScoreB = m.ScoreB.HasValue ? m.ScoreB.Value.ToString() : "",
                EquipeB = m.EquipeB
            }).ToList();

            gvMatchs.DataSource = affichage;
            gvMatchs.DataBind();
        }

        protected void btnGenererSuivant_Click(object sender, EventArgs e)
        {
            var matchs = JsonStorage.LoadMatchs();
            foreach (GridViewRow row in gvMatchs.Rows)
            {
                var eliminatoire = Eliminatoire.GetByName(EliminatoireNom);
                var lblPhase = (Label)row.FindControl("lblPhase");
                string phaseDtl = lblPhase.Text.Trim();
                if (phaseDtl != eliminatoire.Etape.ToString())
                    continue;
                var lblEquipeA = (Label)row.FindControl("lblEquipeA");
                string equipeA = lblEquipeA.Text.Trim();
                var lblEquipeB = (Label)row.FindControl("lblEquipeB");
                string equipeB = lblEquipeB.Text.Trim();
                string strScoreA = ((TextBox)row.FindControl("txtScoreA")).Text;
                string strScoreB = ((TextBox)row.FindControl("txtScoreB")).Text;

                if (int.TryParse(strScoreA, out int scoreA) && int.TryParse(strScoreB, out int scoreB))
                {
                    var match = matchs.FirstOrDefault(m =>
                        m.EquipeA == equipeA && m.EquipeB == equipeB && m.Phase == MatchsPhase.Elimination && m.PhaseDtl == eliminatoire.Etape);
                    if (match != null)
                    {
                        MatchsManager.UpdateScore(match, scoreA, scoreB);
                    }
                }
            }

            JsonStorage.UpdateMatchs(matchs);
            EliminatoireManager.GenererPhaseSuivante(EliminatoireNom);
            ChargerMatchsEliminatoires();
        }
    }
}