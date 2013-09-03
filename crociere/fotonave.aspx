<%@ Page Language="VB" AutoEventWireup="false" CodeFile="fotonave.aspx.vb" Inherits="crociere_itinerario" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="nave.css" type="text/css" />
    <script type="text/javascript" src="altezza.js"></script>
    <script language="javascript" type="text/javascript">       
        function cambiafoto(nomefoto) {            
            document.getElementById("fotog").src = nomefoto;
        }
    </script>
</head>
<body>
     <form id="form1" runat="server">
     <div id="fotonave">
        <div id="divimagegrande">
            <img id="fotog" runat="server" src="" />  
        </div>    
        <ul>
            <asp:Repeater ID="Repeaterfotonave" runat="server">
                <ItemTemplate> 
                    <li id="riga" runat="server">
                        <img id="fotop" runat="server" src='<%#databinder.eval(container.dataitem,"fotop")%>' alt='<%#databinder.eval(container.dataitem,"descrifotonave")%>' />
                        <asp:Label ID="foto" Visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"foto")%>'></asp:Label>                         
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>    
     </div>       
        
    </form>
</body>
</html>
