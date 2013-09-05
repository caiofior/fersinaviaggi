<% @ Page Language="VB" MasterPageFile="master\cercavoli.master" CodeFile="results.aspx.vb" Inherits="Results" Title="Content Page 1" %>
<% @ Register TagPrefix="fc" TagName="Results" Src="control/results.ascx" %>
<asp:Content ID="Results" ContentPlaceHolderID="Results" Runat="Server">
<div id="fly_search_results">
<%  If (passengers.Text <> "") Then%>
<h2>Voli
<asp:label id="departure_location_name" runat="server"></asp:label> - <asp:label id="arrival_location_name" runat="server"></asp:label>
</h2>
<h3>La tua ricerca</h3>
<p>Andata:<asp:label id="departure_datetime" runat="server"></asp:label></p>
<p>Ritorno:<asp:label id="arrival_datetime" runat="server"></asp:label></p>
<p>Passeggeri:<asp:label id="passengers" runat="server"></asp:label></p>
<%  End If %>
<h2>Elenco dei voli</h2>
<fc:Results id="ResultsTable" runat="server" />
</div>
</asp:Content>