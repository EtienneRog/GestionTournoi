<%@ Page Title="Equipes" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="TeamsPage.aspx.cs" Inherits="GestionTournoi.TeamsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Gestion des Équipes</h1>
    <h2>Ajouter une équipe</h2>
    <asp:TextBox ID="txtTeamName" runat="server" placeholder="Nom de l'équipe"></asp:TextBox>
    <asp:DropDownList ID="ddlLevel" runat="server"></asp:DropDownList>
    <br />
    <asp:Button ID="btnAddTeam" runat="server" Text="Ajouter" OnClick="BtnAddTeam_Click" />
    <br /><br />
    <h2>Importer depuis un fichier</h2>
    <asp:FileUpload ID="fileUpload" runat="server" />
    <br />
    <asp:Button ID="btnImport" runat="server" Text="Importer" OnClick="BtnImport_Click" />
    <br />
    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
    <br /><br /><br />

    <h3>Liste des équipes</h3>
    <asp:GridView ID="gvTeams" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="ID" />
            <asp:BoundField DataField="Name" HeaderText="Nom de l'équipe" />
            <asp:BoundField DataField="LevelName" HeaderText="Niveau" />
        </Columns>
    </asp:GridView>
</asp:Content>
