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

            EliminatoireManager.GenererEliminatoire(nbEquipes, tri);

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