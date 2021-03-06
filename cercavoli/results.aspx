﻿<% @ Page Language="VB" MasterPageFile="master\cercavoli.master" CodeFile="results.aspx.vb" Inherits="Results" Title="Content Page 1" %>
<% @ Register TagPrefix="fc" TagName="Results" Src="control/results.ascx" %>
<asp:Content ID="Results" ContentPlaceHolderID="Results" Runat="Server">
<div id="fly_search_results">
<%  If (passengers.Text > 0) Then%>
<h2>Voli
<asp:label id="departure_location_name" runat="server"></asp:label> - <asp:label id="arrival_location_name" runat="server"></asp:label>
</h2>
<div id="request_summary">
<h3>La tua ricerca</h3>
<p>Andata:<asp:label id="departure_datetime" runat="server"></asp:label></p>
<p>Ritorno:<asp:label id="arrival_datetime" runat="server"></asp:label></p>
<p>Passeggeri:<asp:label id="passengers" runat="server"></asp:label></p>
</div>
<%  End If %>
<h2>Elenco dei voli</h2>
<fc:Results id="ResultsTable" runat="server" />
</div>
<script type="text/javascript"> var request_id = parseInt("<%=getReqestId()%>");</script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Scripts" Runat="Server">
<script type="text/javascript" src="js/results.js?id=1"></script>
</asp:Content>