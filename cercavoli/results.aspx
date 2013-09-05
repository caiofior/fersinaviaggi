<% @ Page Language="VB" MasterPageFile="master\cercavoli.master" CodeFile="results.aspx.vb" Inherits="FlySearch" Title="Content Page 1" %>
<% @ Register TagPrefix="fc" TagName="Results" Src="control/results.ascx" %>
<asp:Content ID="Results" ContentPlaceHolderID="Results" Runat="Server">
<div id="fly_search_results">
<h2>Voli</h2>
<h3>La tua ricerca</h3>
<p>Andata:</p>
<p>Ritorno:</p>
<p>Passeggeri:</p>
<h2>Elenco dei voli</h2>
<fc:Results id="ResultsTable" runat="server" />
</div>
</asp:Content>