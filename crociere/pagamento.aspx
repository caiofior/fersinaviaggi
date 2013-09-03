<%@ Page Language="VB" AutoEventWireup="false" CodeFile="pagamento.aspx.vb" Inherits="crociere_pagamento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="pagamento.css" type="text/css" />
    <script type="text/javascript" src="altezza.js"></script>
    <script type="text/javascript">
        function controllapag(data, tasto) {            
            var dataora = new Date();
            var dataprima = new Date(Date.parse(data));
            var ua = navigator.userAgent.toLowerCase();
            if (ua.indexOf('safari') != -1) {
                if (ua.indexOf('chrome') > -1) {
                     dataprima = new Date(data.replace(/T/g, " ")); //crome
                 } else {
                     data = data.split(/[-T.]/); //safari
                     dataprima = new Date(data.slice(0, 3).join('/') + ' ' + data[3]);                   
                }
            }
            var datediff = dataora.getTime() - dataprima.getTime()
            if (parseInt(datediff) > 0) {
                alert("Tempo per il pagamento scaduto!    Contattare i nostri uffici al numero 0461 914471");
            } else {
              document.getElementById(tasto).submit();
            }
        }
    </script>
</head>
<body>
   
    <div>
    <asp:Label visible="false" ID="lblcodicepreno" Font-Bold="true" runat="server"></asp:Label>
    <asp:Label ID="mailx" Visible="false" runat="server"></asp:Label>

                                
                                <div id="divimporto">
                                    <h1><asp:Label ID="Label18" runat="server" Text="PAGAMENTI:" Width="200px"></asp:Label></h1> 
                                        <ul>
                                            <asp:Repeater ID="RepeaterPaga" runat="server">
                                                <ItemTemplate>                                 
                                                    <li>                                                         
                                                        <asp:Label ID="idpaga" visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"id_pagamento")%>'></asp:Label> 
                                                        <asp:Label ID="ricevuto" visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"ricevuto")%>'></asp:Label> 
                                                        <asp:Label ID="descripaga" CssClass="PP1" runat="server" Text='<%#databinder.eval(container.dataitem,"descripaga")%>'></asp:Label> 
                                                        <asp:Label ID="prezzo" CssClass="PP2" runat="server" Text='<%#databinder.eval(container.dataitem,"prezzo")%>'></asp:Label>  
                                                        <asp:Label ID="Label12" runat="server" CssClass="PP3" Text="Entro:"></asp:Label>
                                                        <asp:Label ID="scadenza" CssClass="PP4" runat="server" Text='<%#databinder.eval(container.dataitem,"scadenza")%>'></asp:Label>                                                                                                      
                                                        <asp:Label ID="labelpaga" CssClass="PP8" runat="server" Text=""></asp:Label>
                                                        <asp:Label ID="labelpaga2" style="visibility:hidden" runat="server" Text=""></asp:Label>
                                                        <asp:Label ID="labelricevuto" CssClass="PP8" Visible="false" runat="server" Text="<b>Stato:</b> Ricevuto"></asp:Label>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>                                
                                        </ul>                                                                                          
                                    <h1><asp:Label ID="Labelatt" Visible="false" runat="server" Text="PRATICA IN ATTESA DEL PAGAMENTO:" Width="400px"></asp:Label></h1> 
                                    <asp:Label ID="lblbonifico" Font-Size="Small" runat="server" Text=""></asp:Label><br /><br />                                
                                    <img src="../images/bonifico.gif" alt="bonifico bancario" style="margin-left:10px" />
                                    <img src="../images/mastercard.gif" alt="mastercard" style="margin-left:10px" />     
                                    <img src="../images/visa.gif" alt="visa" style="margin-left:10px" /> 
                                    <img src="../images/postepay.gif" alt="postepay" style="margin-left:10px" />
                                 </div>
                            </div>  
   <br /><br />
   <form id="formapp" runat="server"></form>
</body>
</html>
