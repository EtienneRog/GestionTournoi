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

            try
            {
                string resultMessage = EliminatoireManager.GenererEliminatoire(nbEquipes, tri);

                if (resultMessage != "ok")
                {
                    lblMessage.Text = resultMessage;
                    lblMessage.ForeColor = System.Drawing.Color.OrangeRed;
                }
                else
                {
                    lblMessage.Text = "✅ Phase générée avec succès.";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }

                BindPhases();
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Erreur inattendue : {ex.Message}";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void BindPhases()
        {
            var phases = JsonStorage.LoadEliminatoires();
            rptPhases.DataSource = phases;
            rptPhases.DataBind();
        }
    }
}