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
            gvTeams.DataSource = equipes.Select(t => new {
                t.Id,
                t.Name,
                LevelName = TournamentLevel.GetLevelName(t.Level),
                t.Points
            }).ToList();
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
            GridViewRow row = gvTeams.Rows[e.RowIndex];
            string nomEquipe = row.Cells[1].Text.Trim();

            TextBox txtPoints = (TextBox)row.FindControl("txtEditPoints");

            if (txtPoints != null && int.TryParse(txtPoints.Text, out int nouveauxPoints))
            {
                var equipes = JsonStorage.LoadTeams();
                var equipe = equipes.FirstOrDefault(t => t.Name.Equals(nomEquipe, StringComparison.OrdinalIgnoreCase));

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