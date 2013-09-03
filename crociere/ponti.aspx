<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ponti.aspx.vb" Inherits="crociere_iti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="ponti.css" type="text/css" />
    <script type="text/javascript" src="altezza.js"></script>
       <script type="text/javascript" src="pax.js"></script>
     <script type="text/javascript">
         function ruota(elemento) {
             top.document.getElementById("layoutnascondi").style.visibility = "visible";
             top.document.getElementById("centroponte").style.visibility = "visible";
             top.document.getElementById("frame1").src = elemento;
         }

     </script>
</head>

<body>
    <form id="form1" runat="server">
    <div id="centroitx">
 
            <div id="dettaglionave">
                            <h1><asp:Label ID="Labeltitolo" runat="server" Width="300px"></asp:Label></h1>
            <ul>               
                <li id="rigap" runat="server"><asp:Image ID="imagepiani" runat="server" Width="680px"  /></li>

            
                <asp:Repeater ID="Repeaterponti" runat="server">
                    <ItemTemplate>                                 
                        <li> 
                            <b><asp:Label ID="Label5" Font-Size="Small" Text='Ponte ' runat="server"></asp:Label><asp:Label ID="descriponte" Font-Size="Small" Text='<%#databinder.eval(container.dataitem,"numero")%>' runat="server"></asp:Label> - <asp:Label ID="Label4" Font-Size="Small" Text='<%#databinder.eval(container.dataitem,"nome")%>' runat="server"></asp:Label>:</b>                                                        
                            <asp:image ID="imageponte" style="cursor:pointer" BorderStyle="None" Width="680px" runat="server" Imageurl='<%#databinder.eval(container.dataitem,"imgponte")%>' />
                        </li>
                    </ItemTemplate>
                </asp:Repeater>                            
            </ul>
            </div>
      </div>                       
    </form>   

</body>
</html>
