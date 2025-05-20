<%@ Page Title="Matchs de la Poule" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PouleMatch.aspx.cs" Inherits="GestionTournoi.PouleMatch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Matchs de la Poule :
        <asp:Literal ID="litPouleNom" runat="server" /></h2>
    <asp:GridView ID="gvMatchs" runat="server" AutoGenerateColumns="False">
        <Columns>
                    <asp:TemplateField HeaderText="Équipe A">
            <ItemTemplate>
                <asp:Label ID="lblEquipeA" runat="server" Text='<%# Eval("EquipeA") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
            <asp:TemplateField HeaderText="Score A">
                <ItemTemplate>
                    <asp:TextBox ID="txtScoreA" runat="server" Text='<%# Bind("ScoreA") %>' Width="50" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Score B">
                <ItemTemplate>
                    <asp:TextBox ID="txtScoreB" runat="server" Text='<%# Bind("ScoreB") %>' Width="50" />
                </ItemTemplate>
            </asp:TemplateField>        <asp:TemplateField HeaderText="Équipe B">
            <ItemTemplate>
                <asp:Label ID="lblEquipeB" runat="server" Text='<%# Eval("EquipeB") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <br />

    <asp:Button ID="btnSaveAll" runat="server" Text="Enregistrer tous les scores" OnClick="btnSaveAll_Click" />


    <asp:Label ID="lblMessage" runat="server" ForeColor="Green"></asp:Label>
</asp:Content>
