using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using GestionTournoi.App_Code;

namespace GestionTournoi
{
    public partial class TeamsPage : System.Web.UI.Page
    {
        // Stockage temporaire en session
        private List<Team> TeamsList
        {
            get
            {
                if (Session["Teams"] == null)
                    Session["Teams"] = new List<Team>();
                return (List<Team>)Session["Teams"];
            }
            set
            {
                Session["Teams"] = value;
            }
        }

        private string SortColumn
        {
            get => ViewState["SortColumn"] as string ?? "Id";
            set => ViewState["SortColumn"] = value;
        }

        private SortDirection SortDirection
        {
            get => (SortDirection)(ViewState["SortDirection"] ?? SortDirection.Descending);
            set => ViewState["SortDirection"] = value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindLevels();      // charge les niveaux dans la dropdown
                BindGrid();        // charge les équipes depuis le fichier
            }
        }

        private void BindGrid()
        {
            var equipes = JsonStorage.LoadTeams();

            // Tri
            switch (SortColumn)
            {
                case "Name":
                    equipes = SortDirection == SortDirection.Ascending
                        ? equipes.OrderBy(t => t.Name).ToList()
                        : equipes.OrderByDescending(t => t.Name).ToList();
                    break;
                case "Level":
                    equipes = SortDirection == SortDirection.Ascending
                        ? equipes.OrderBy(t => t.Level).ToList()
                        : equipes.OrderByDescending(t => t.Level).ToList();
                    break;
                case "Points":
                    equipes = SortDirection == SortDirection.Ascending
                        ? equipes.OrderBy(t => t.Points).ToList()
                        : equipes.OrderByDescending(t => t.Points).ToList();
                    break;
                default:
                    equipes = SortDirection == SortDirection.Ascending
                        ? equipes.OrderBy(t => t.Id).ToList()
                        : equipes.OrderByDescending(t => t.Id).ToList();
                    break;
            }

            // Ajout des flèches dans l'entête
            foreach (DataControlField col in gvTeams.Columns)
            {
                if (!string.IsNullOrEmpty(col.SortExpression))
                {
                    string headerText = col.SortExpression;

                    switch (col.SortExpression)
                    {
                        case "Id": headerText = "ID"; break;
                        case "Name": headerText = "Nom de l'équipe"; break;
                        case "Level": headerText = "Niveau"; break;
                        case "Points": headerText = "Points"; break;
                    }

                    if (col.SortExpression == SortColumn)
                    {
                        string arrow = SortDirection == SortDirection.Ascending ? " ▲" : " ▼";
                        col.HeaderText = headerText + arrow;
                    }
                    else
                    {
                        col.HeaderText = headerText;
                    }
                }
            }

            gvTeams.DataSource = equipes;
            gvTeams.DataBind();
        }

        private void BindLevels()
        {
            ddlLevel.DataSource = TournamentLevel.GetAllLevels();
            ddlLevel.DataTextField = "Name";
            ddlLevel.DataValueField = "Value";
            ddlLevel.DataBind();
        }
        protected void BtnAddTeam_Click(object sender, EventArgs e)
        {
            string name = txtTeamName.Text.Trim();
            if (!string.IsNullOrEmpty(name))
            {
                var team = new Team(name, int.Parse(ddlLevel.SelectedValue));
                //TeamsList.Add(team);
                TeamManager.AddTeam(team);
                BindGrid();
                txtTeamName.Text = "";
            }
        }
        protected void BtnImport_Click(object sender, EventArgs e)
        {
            if (fileUpload.HasFile)
            {
                string ext = System.IO.Path.GetExtension(fileUpload.FileName).ToLower();
                try
                {
                    if (ext == ".csv")
                        ImportFromCsv(fileUpload.FileContent);
                    else if (ext == ".xlsx")
                        ImportFromExcel(fileUpload.FileContent);
                    else
                        throw new Exception("Format de fichier non supporté. Veuillez utiliser .csv ou .xlsx");

                    BindGrid();
                    lblMessage.Text = "Importation réussie.";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Erreur : " + ex.Message;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void gvTeams_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (e.SortExpression == SortColumn)
            {
                // Inverser le sens de tri
                SortDirection = SortDirection == SortDirection.Ascending ? SortDirection.Descending : SortDirection.Ascending;
            }
            else
            {
                SortColumn = e.SortExpression;
                SortDirection = SortDirection.Ascending;
            }

            BindGrid();
        }

        protected void gvTeams_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvTeams.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void gvTeams_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvTeams.EditIndex = -1;
            BindGrid();
        }

        protected void gvTeams_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int equipeId = (int)gvTeams.DataKeys[e.RowIndex].Value;
            GridViewRow row = gvTeams.Rows[e.RowIndex];
            TextBox txtPoints = (TextBox)row.FindControl("txtEditPoints");

            if (txtPoints != null && int.TryParse(txtPoints.Text, out int nouveauxPoints))
            {
                var equipes = JsonStorage.LoadTeams();
                var equipe = equipes.FirstOrDefault(t => t.Id == equipeId);

                if (equipe != null)
                {
                    equipe.Points = nouveauxPoints;
                    JsonStorage.SaveTeams(equipes);
                }
            }

            gvTeams.EditIndex = -1;
            BindGrid();
        }

        private void ImportFromExcel(System.IO.Stream stream)
        {
            using (var package = new ExcelPackage(stream))
            {
                ExcelPackage.License.SetNonCommercialPersonal("<RogEti>");
                var worksheet = package.Workbook.Worksheets[0]; // première feuille
                int rowCount = worksheet.Dimension.End.Row;

                for (int row = 2; row <= rowCount; row++) // en supposant une ligne d'entête
                {
                    string name = worksheet.Cells[row, 1].Text.Trim();
                    string levelText = worksheet.Cells[row, 2].Text.Trim();
                    int level = int.TryParse(levelText, out int result) ? result : 1;

                    if (!string.IsNullOrEmpty(name))
                    {
                        var team = new Team(name, level);
                        //TeamsList.Add(team); 
                        TeamManager.AddTeam(team);
                    }
                }
            }
        }

        private void ImportFromCsv(System.IO.Stream stream)
        {
            using (var reader = new System.IO.StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        var parts = line.Split(';'); // ou ',' si besoin
                        string name = parts[0].Trim();
                        int level = parts.Length > 1 ? int.Parse(parts[1]) : 1;
                        
                        var team = new Team(name, level);
                        //TeamsList.Add(team);
                        TeamManager.AddTeam(team);
                    }
                }
            }
        }

    }

}