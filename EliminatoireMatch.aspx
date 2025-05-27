<%@ Page Title="Phase Éliminatoire" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EliminatoireMatch.aspx.cs" Inherits="GestionTournoi.EliminatoireMatch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Matchs de la Poule :
        <asp:Literal ID="litElimNom" runat="server" /></h2>

    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />

    <asp:Button ID="btnGenererSuivant" runat="server" Text="Générer la phase suivante"
        OnClick="btnGenererSuivant_Click" CssClass="btn btn-primary" />

    <br />
    <br />

    <asp:GridView ID="gvMatchs" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:TemplateField HeaderText="Phase">
                <ItemTemplate>
                    <asp:Label ID="lblPhase" runat="server" Text='<%# Eval("PhaseDtl") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
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
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Équipe B">
                <ItemTemplate>
                    <asp:Label ID="lblEquipeB" runat="server" Text='<%# Eval("EquipeB") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
