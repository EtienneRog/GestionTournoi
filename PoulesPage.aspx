<%@ Page Title="Poules" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PoulesPage.aspx.cs" Inherits="GestionTournoi.PoulesPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Liste des Poules</h1>

    <asp:Panel ID="pnlParametres" runat="server">
        <h2>Paramètres de génération</h2>
        <label for="nbPoules">Nombre de Poules:</label>
        <asp:TextBox ID="txtNbPoules" runat="server" Text="4" Width="50px" />
        <br />
        <label for="equipesParPoule">Nombre d'Équipes par Poule:</label>
        <asp:TextBox ID="txtEquipesParPoule" runat="server" Text="3" Width="50px" />
        <br />
        <label for="decalage">Numéro de la phase de poule:</label>
        <asp:TextBox ID="txtDecalage" runat="server" Text="1" Width="50px"/>
        <br />
        <asp:Button ID="btnGenererPoules" runat="server" Text="Générer les poules" OnClick="btnGenererPoules_Click" />
        <br /><br />
    </asp:Panel>

    <asp:Repeater ID="rptPoules" runat="server"  OnItemDataBound="rptPoules_ItemDataBound">
        <ItemTemplate>
            <div style="border:1px solid #ccc; padding:10px; margin-bottom:15px;">
                <h3>
                    <asp:HyperLink ID="lnkPoule" runat="server" 
                        NavigateUrl='<%# "PouleMatch.aspx?poule=" + Eval("Name") %>' 
                        Text='<%# Eval("Name") %>'></asp:HyperLink>
                </h3>
                <asp:GridView ID="gvEquipes" runat="server" AutoGenerateColumns="false" CssClass="table">
                    <Columns>
                        <asp:BoundField HeaderText="Équipe" DataField="Name" />
                        <asp:BoundField HeaderText="Niveau" DataField="Level" />
                        <asp:BoundField HeaderText="Points" DataField="Points" />
                    </Columns>
                </asp:GridView>
            </div>
        </ItemTemplate>
    </asp:Repeater>

</asp:Content>
