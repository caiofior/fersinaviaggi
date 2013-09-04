<% @ Page Language="VB" MasterPageFile="cercavoli\cercavoli.master" CodeFile="cercavoli.aspx.vb" Inherits="FlySearch" Title="Content Page 1" %>
<% @ Register TagPrefix="fc" TagName="Results" Src="cercavoli\results.ascx" %>
<asp:Content ID="Results" ContentPlaceHolderID="Results" Runat="Server">
    <fc:Results id="ResultsTable" runat="server" />
</asp:Content>