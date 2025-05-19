<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PouleMatch.aspx.cs" Inherits="GestionTournoi.PouleMatch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><asp:Label ID="lblPoule" runat="server" /></h2>

    <asp:GridView ID="gvMatchs" runat="server" AutoGenerateColumns="false" OnRowCommand="gvMatchs_RowCommand">
        <Columns>
            <asp:BoundField DataField="EquipeA" HeaderText="Équipe A" />
            <asp:BoundField DataField="EquipeB" HeaderText="Équipe B" />
            <asp:TemplateField HeaderText="Score A">
                <ItemTemplate>
                    <asp:TextBox ID="txtScoreA" runat="server" Text='<%# Bind("ScoreA") %>' Width="40px" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Score B">
                <ItemTemplate>
                    <asp:TextBox ID="txtScoreB" runat="server" Text='<%# Bind("ScoreB") %>' Width="40px" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btnSave" runat="server" Text="Enregistrer" CommandName="SaveScore" CommandArgument='<%# Eval("Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
