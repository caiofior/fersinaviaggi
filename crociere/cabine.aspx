<%@ Page Language="VB" AutoEventWireup="false" CodeFile="cabine.aspx.vb" Inherits="crociere_itinerario" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="cabine.css" type="text/css" />
    <script type="text/javascript" src="altezza.js"></script>
    <script language="javascript" type="text/javascript">
        var fotona = new Array();
        var rigona = new Array();
        var valoreriga = new Array();
        var altezzaframe = 0;              
        function cambiafoto(nomefoto, riga, dovefoto, calcolo, px) {
            for (var i = 0; i <= 40; i++) {
                if (document.getElementById(fotona[i]) != null) {
                    document.getElementById(fotona[i]).style.visibility = "hidden";
                    document.getElementById(rigona[i]).style.height = valoreriga[i];
                    document.getElementById(rigona[i]).style.background = "transparent";  
                }
            }            
            document.getElementById(dovefoto).src = nomefoto;
            document.getElementById(dovefoto).style.visibility = "visible";
            var misura = 300 + parseInt(calcolo)
            document.getElementById(riga).style.height = misura + "px";
            document.getElementById(riga).style.background = "#f3f3f3";             
            altezza(parseInt(altezzaframe) + 300, 'framenave');           
        }
    </script>
</head>
<body>
     <form id="form1" runat="server">
     <div id="descricabine">        
        <ul>
            <asp:Repeater ID="Repeatercabine" runat="server">
                <ItemTemplate> 
                    <li id="riga" runat="server">                        
                        <img id="mappacabinap" Class="Cl2" runat="server" src='<%#databinder.eval(container.dataitem,"mappacabinap")%>' alt='<%#databinder.eval(container.dataitem,"titolocabina")%>' />
                        <div>
                        <img id="fotocabinap" Class="Cl1" runat="server" src='<%#databinder.eval(container.dataitem,"fotocabinap")%>' alt='<%#databinder.eval(container.dataitem,"titolocabina")%>' />                        
                        <asp:Label ID="foto" Visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"fotocabina")%>'></asp:Label>                         
                        <asp:Label ID="mappa" Visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"mappacabina")%>'></asp:Label>                         
                        <div style="width:540px; height:auto;">
                            <asp:Label ID="Label1" Font-Bold="true" ForeColor="#067788"  runat="server" Text='<%#databinder.eval(container.dataitem,"titolocabina")%>'></asp:Label><br />                         
                            <asp:Label ID="descricabine"   runat="server" Text='<%#databinder.eval(container.dataitem,"descricabina")%>'></asp:Label>                         
                        </div>
                        <div style="text-align:center">
                            <img id="fotog" runat="server" style="visibility:hidden; padding-top:10px;"  alt='' />
                        </div>
                        </div>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>    
     </div>       
        
    </form>
</body>
</html>
