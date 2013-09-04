<%@ Control Language="VB" AutoEventWireup="false" CodeFile="results.ascx.vb" Inherits="FlySearch_Results" %>
<asp:Repeater id="ResultsData" runat="server">
<ItemTemplate>
<asp:Label ID="departure_datetime" runat="server" Text='<%#Eval("departure_datetime")%>'></asp:Label>
</ItemTemplate>
</asp:Repeater>

