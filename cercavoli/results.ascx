<%@ Control Language="VB" AutoEventWireup="false" CodeFile="results.ascx.vb" Inherits="FlySearch_Results" %>
<asp:Repeater id="ResultsData" runat="server">
<HeaderTemplate>
<div class="resultheader">
<span class="odd">Ora di partenza</span>
<span class="even">Ora di arrivo</span>
<span class="odd">Aeroporto di partenza</span>
<span class="even">Aeroporto di arrivo</span>
<span class="odd">Prezzo</span>
</div>
</HeaderTemplate>
<ItemTemplate>
<div class="resultsrow">
<asp:Label ID="departure_datetime" runat="server" Text='<%#Eval("departure_datetime")%>' class="odd departure_datetime"></asp:Label>
<asp:Label ID="arrival_datetime" runat="server" Text='<%#Eval("arrival_datetime")%>' class="even arrival_datetime"></asp:Label>
<asp:Label ID="departure_location_name" runat="server" Text='<%#Eval("departure_location_name")%>' class="odd departure_location_name"></asp:Label>
<asp:Label ID="arrival_location_name" runat="server" Text='<%#Eval("arrival_location_name")%>' class="even arrival_location_name"></asp:Label>
<asp:Label ID="price" runat="server" Text='<%#Eval("price")%>' class="odd price"></asp:Label>
</div>
</ItemTemplate>
<FooterTemplate>
<div class="resultfooter">
<span class="odd">Ora di partenza</span>
<span class="even">Ora di arrivo</span>
<span class="odd">Aeroporto di partenza</span>
<span class="even">Aeroporto di arrivo</span>
<span class="odd">Prezzo</span>
</div>
</FooterTemplate>
</asp:Repeater>

