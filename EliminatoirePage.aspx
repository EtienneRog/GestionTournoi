<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EliminatoirePage.aspx.cs" Inherits="GestionTournoi.EliminatoirePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    Profondeur de la phase :
    <asp:DropDownList ID="ddlProfondeur" runat="server">
        <asp:ListItem Text="Finale" Value="2" />
        <asp:ListItem Text="Demi-finale" Value="4" />
        <asp:ListItem Text="Quart de finale" Value="8" />
        <asp:ListItem Text="Huitième de finale" Value="16" />
        <asp:ListItem Text="Seizième de finale" Value="32" />
    </asp:DropDownList>
    <br />
    Choisir les équipes :
    <asp:RadioButtonList ID="rblTriEquipes" runat="server" RepeatDirection="Horizontal">
        <asp:ListItem Text="Les plus fortes" Value="fortes" Selected="True" />
        <asp:ListItem Text="Les plus faibles" Value="faibles" />
    </asp:RadioButtonList>

    <br />
    <asp:Button ID="btnGenererPhase" runat="server" Text="Créer la phase" OnClick="btnGenererPhase_Click" />
    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />

    <hr />

    <h3>Phases Éliminatoires existantes</h3>

    <asp:Repeater ID="rptPhases" runat="server">
        <ItemTemplate>
            <div>
                <strong>Phase :</strong> <%# Eval("Name") %> — 
            <em>(<%# Eval("Teams.Count") %> équipes)</em>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
