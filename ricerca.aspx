<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ricerca.aspx.vb" Inherits="ricerca" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="ricerca.js"></script>
<link href="ricerca.css" rel="stylesheet" type="text/css" />
</head>
<body onload="">
<form id="form1" runat="server">
    <div id="ricerca">
    <br />
        <ul>
            <li>
                <asp:Label ID="Label5" CssClass="R1" runat="server" Text="Destinazione:"></asp:Label>
                <asp:DropDownList ID="DropZona" CssClass="R2"  runat="server" >
                </asp:DropDownList>
            </li>
            <li>
                <asp:Label ID="Label1" CssClass="R1" runat="server" Text="Mese:"></asp:Label>
                <asp:DropDownList ID="DropMese" CssClass="R2"  runat="server" >
                </asp:DropDownList>
            </li>
            <li>
            <asp:Label ID="Label4" CssClass="R1" runat="server" Text="Compagnia:"></asp:Label>
                <asp:DropDownList ID="DropCompagnia" CssClass="R2"  runat="server">
                </asp:DropDownList>
            </li>
            <li>
                <asp:Label ID="Label2" CssClass="R1" runat="server" Text="Nave:"></asp:Label>
                <asp:DropDownList ID="DropNave" CssClass="R2"  runat="server">
                </asp:DropDownList>     
            </li>
            <li>
                <asp:Label ID="Label3" CssClass="R1" runat="server" Text="Porto:"></asp:Label>
                <asp:DropDownList ID="DropPorto" CssClass="R2"  runat="server">
                </asp:DropDownList>   
            </li>
            <li>
             <input type="button" runat="server" name="ButtonAnnulla" value="Annulla filtro" id="ButtonAnnulla" class="R0" style="font-size:X-Small;" /> 
             <input type="button" runat="server"  name="ButtonCerca" value="Cerca" id="ButtonCerca" class="R3" style="font-size:Large;font-weight:bold;" /> 
            </li>
        </ul>        
    </div>
    </form>
</body>
</html>
