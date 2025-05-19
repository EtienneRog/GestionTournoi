<%@ Page Title="Poules" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="PoulesPage.aspx.cs" Inherits="GestionTournoi.PoulesPage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-container">
        <h3>Paramétrer la génération des poules</h3>

        <!-- Nombre de poules -->
        <label for="numPoules">Nombre de Poules:</label>
        <asp:TextBox ID="numPoulesTextBox" runat="server" Text="4"></asp:TextBox>
        <br />

        <!-- Nombre d'équipes par poule -->
        <label for="equipesParPoule">Nombre d'Équipes par Poule:</label>
        <asp:TextBox ID="equipesParPouleTextBox" runat="server" Text="3"></asp:TextBox>
        <br />

        <!-- Numéro de la phase de poule -->
        <label for="decalage">Numéro de la phase de poule:</label>
        <asp:TextBox ID="decalageTextBox" runat="server" Text="1"></asp:TextBox>
        <br />

        <!-- Bouton pour générer les poules -->
        <asp:Button ID="generatePoulesButton" runat="server" Text="Générer les Poules" OnClick="GeneratePoules_Click" />
    </div>

    <div>
        <asp:Repeater ID="RepeaterPoules" runat="server">
            <HeaderTemplate>
                <h3>Poules du Tournoi</h3>
            </HeaderTemplate>
            <ItemTemplate>
                <div>
                    <h3>
                        <asp:HyperLink
                            ID="lnkPoule"
                            runat="server"
                            NavigateUrl='<%# Eval("Name", "PouleMatch.aspx?nom={0}") %>'
                            Text='<%# Eval("Name") %>' />
                    </h3>
                    <ul>
                        <%# GetTeamList(Container) %>
                    </ul>
                </div>
            </ItemTemplate>
            <FooterTemplate>
                <hr />
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
