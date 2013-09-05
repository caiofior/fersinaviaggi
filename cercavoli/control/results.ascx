<%@ Control Language="VB" AutoEventWireup="false" CodeFile="results.ascx.vb" Inherits="FlySearch_Results" %>
<asp:Repeater id="ResultsData" runat="server">
<ItemTemplate>
<div>
<asp:Label ID="total_price" runat="server" Text='<%#"Prezzo da "+String.Format(Eval("price")+Eval("return_price"))+ " €"%>' class="total_price"></asp:Label>
</div>
<div class="resultheader">
<span class="odd">Partenza</span>
<span class="even">Arrivo</span>
<span class="odd">Origine</span>
<span class="even">Destinazione</span>
<span class="separator odd">Partenza</span>
<span class="even">Arrivo</span>
<span class="odd">Origine</span>
<span class="even">Destinazione</span>
</div>
<div class="pricerow">
<asp:Label ID="price" runat="server" Text='<%#"Andata - Prezzo: "+String.Format(Eval("price"))+ " €"%>' class="price"></asp:Label>
<asp:Label ID="return_price" runat="server" Text='<%#"Ritorno - Prezzo: "+String.Format(Eval("return_price"))+ " €"%>' class="separator price"></asp:Label>
</div>
<div class="resultsrow">
<asp:Label ID="departure_datetime" runat="server" Text='<%#Eval("departure_datetime","{0:g}")%>' class="odd departure_datetime"></asp:Label>
<asp:Label ID="arrival_datetime" runat="server" Text='<%#Eval("arrival_datetime","{0:g}")%>' class="even arrival_datetime"></asp:Label>
<asp:Label ID="departure_location_name" runat="server" Text='<%#Eval("departure_location_name")%>' class="odd departure_location_name"></asp:Label>
<asp:Label ID="arrival_location_name" runat="server" Text='<%#Eval("arrival_location_name")%>' class="even arrival_location_name"></asp:Label>
<asp:Label ID="return_departure_datetime" runat="server" Text='<%#Eval("return_departure_datetime","{0:g}")%>' class="separator odd departure_datetime"></asp:Label>
<asp:Label ID="return_arrival_datetime" runat="server" Text='<%#Eval("return_arrival_datetime","{0:g}")%>' class="even arrival_datetime"></asp:Label>
<asp:Label ID="return_departure_location_name" runat="server" Text='<%#Eval("return_departure_location_name")%>' class="odd departure_location_name"></asp:Label>
<asp:Label ID="return_arrival_location_name" runat="server" Text='<%#Eval("return_arrival_location_name")%>' class="even arrival_location_name"></asp:Label>
</div>
</ItemTemplate>
</asp:Repeater>

