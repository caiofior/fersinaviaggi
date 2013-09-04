<%@ Control Language="VB" AutoEventWireup="false" CodeFile="results.ascx.vb" Inherits="FlySearch_Results" %>
<asp:Repeater id="ResultsData" runat="server">
<HeaderTemplate>
<div class="resultheader">
<span>Ora di partenza</span>
<span>Ora di arrivo</span>
<span>Aeroporto di partenza</span>
<span>Aeroporto di arrivo</span>
<span>prezzo</span>
</div>
</HeaderTemplate>
<ItemTemplate>
<div class="resultsrow">
<asp:Label ID="departure_datetime" runat="server" Text='<%#Eval("departure_datetime")%>' class="departure_datetime"></asp:Label>
<asp:Label ID="arrival_datetime" runat="server" Text='<%#Eval("arrival_datetime")%>' class="arrival_datetime"></asp:Label>
<asp:Label ID="departure_location_name" runat="server" Text='<%#Eval("departure_location_name")%>' class="departure_location_name"></asp:Label>
<asp:Label ID="arrival_location_name" runat="server" Text='<%#Eval("arrival_location_name")%>' class="arrival_location_name"></asp:Label>
<asp:Label ID="price" runat="server" Text='<%#Eval("price")%>' class="price"></asp:Label>
</div>
</ItemTemplate>
<FooterTemplate>
<div class="resultfooter">
<span>Ora di partenza</span>
<span>Ora di arrivo</span>
<span>Aeroporto di partenza</span>
<span>Aeroporto di arrivo</span>
<span>prezzo</span>
</div>
</FooterTemplate>
</asp:Repeater>

