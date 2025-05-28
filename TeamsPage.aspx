<%@ Page Title="Equipes" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="TeamsPage.aspx.cs" Inherits="GestionTournoi.TeamsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Gestion des Équipes</h1>
    <h2>Ajouter une équipe</h2>
    <asp:TextBox ID="txtTeamName" runat="server" placeholder="Nom de l'équipe"></asp:TextBox>
    <asp:DropDownList ID="ddlLevel" runat="server"></asp:DropDownList>
    <br />
    <asp:Button ID="btnAddTeam" runat="server" Text="Ajouter" OnClick="BtnAddTeam_Click" />
    <br />
    <br />
    <h2>Importer depuis un fichier</h2>
    <asp:FileUpload ID="fileUpload" runat="server" />
    <br />
    <asp:Button ID="btnImport" runat="server" Text="Importer" OnClick="BtnImport_Click" />
    <br />
    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
    <br />
    <br />
    <br />
<hr />
<h3>Réinitialisation des données</h3>

<asp:CheckBoxList ID="chkResetOptions" runat="server">
    <asp:ListItem Text="Équipes" Value="teams" />
    <asp:ListItem Text="Poules" Value="poules" />
    <asp:ListItem Text="Matchs" Value="matchs" />
    <asp:ListItem Text="Éliminatoires" Value="eliminatoires" />
</asp:CheckBoxList>

<br />
<asp:Button ID="btnResetSelected" runat="server" Text="Réinitialiser la sélection" OnClick="btnResetSelected_Click" CssClass="btn btn-danger" />
<asp:Label ID="lblResetMessage" runat="server" ForeColor="Green" />
    <br />

    <h3>Liste des équipes</h3>
    <asp:GridView ID="gvTeams" runat="server" AutoGenerateColumns="false" DataKeyNames="Id" AllowSorting="True"
        OnSorting="gvTeams_Sorting" OnRowEditing="gvTeams_RowEditing" OnRowUpdating="gvTeams_RowUpdating" OnRowCancelingEdit="gvTeams_RowCancelingEdit">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id" />
            <asp:BoundField DataField="Name" HeaderText="Nom de l'équipe" SortExpression="Name" />
            <asp:BoundField DataField="LevelName" HeaderText="Niveau" SortExpression="Level" />
            <asp:TemplateField HeaderText="Points" SortExpression="Points">
                <ItemTemplate>
                    <asp:Label ID="lblPoints" runat="server" Text='<%# Bind("Points") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEditPoints" runat="server" Text='<%# Bind("Points") %>' Width="60"></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="PointsDtl" HeaderText="Pts Ecart" SortExpression="Ecart" />
            <asp:CommandField ShowEditButton="True" />
        </Columns>
    </asp:GridView>
</asp:Content>
