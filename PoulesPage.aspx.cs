using GestionTournoi.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestionTournoi
{
    public partial class PoulesPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ChargerPoules();
            }
        }

        private void ChargerPoules()
        {
            JsonStorage.LoadPoules();
            var poulesVisibles = JsonStorage.LoadPoules()
                //.Where(p => p.Visible)
                .ToList();

            rptPoules.DataSource = poulesVisibles;
            rptPoules.DataBind();
        }

        protected void btnGenererPoules_Click(object sender, EventArgs e)
        {
            int nbPoules = int.Parse(txtNbPoules.Text);
            int equipesParPoule = int.Parse(txtEquipesParPoule.Text);
            int decalage = int.Parse(txtDecalage.Text) - 1;

            PouleManager.GenererPoules(nbPoules, equipesParPoule, decalage);
            ChargerPoules();
        }

        protected void rptPoules_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var poule = (Poule)e.Item.DataItem;

                var gvEquipes = (GridView)e.Item.FindControl("gvEquipes");

                if (gvEquipes != null && poule.Teams != null)
                {
                    gvEquipes.DataSource = poule.Teams;
                    gvEquipes.DataBind();
                }
            }
        }
    }
}