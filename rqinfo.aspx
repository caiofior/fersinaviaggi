<%@ Page Language="VB" AutoEventWireup="false" CodeFile="rqinfo.aspx.vb" Inherits="crociere_rqinfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="crociere/rqinfo.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="rqinfo" style="height:355px">
            <asp:Panel ID="Panel1" runat="server">
                
                                <ul>
                                     <li><asp:Label ID="Label27" CssClass="AT75" runat="server" Text="e-mail:"></asp:Label><asp:TextBox CssClass="AT76" ID="TextEmail" runat="server"></asp:TextBox><asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server"  ValidationGroup="validarq" ControlToValidate="TextEmail" Display="Static" ErrorMessage="e-mail non corretta!" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Font-Size="Small" Font-Names="Arial" CssClass="AT78"></asp:RegularExpressionValidator>
                                         <asp:RequiredFieldValidator id="RequiredFieldValidator5" runat="server" ValidationGroup="validarq" ControlToValidate="TextEmail" Display="Static" ErrorMessage="e-mail obbligatoria!"  CssClass="AT78"></asp:RequiredFieldValidator></li>
                                     <li><asp:Label ID="Label28" CssClass="AT75" runat="server" Text="Telefono <span style='font-size:xx-small;'>(opzionale)</span>:"></asp:Label><asp:TextBox CssClass="AT76" ID="TextTelefono" runat="server"></asp:TextBox></li>
                                     <li style="border-bottom:none;  height:135px;"><asp:Label ID="Label29" CssClass="AT75" runat="server" Text="Richiesta:"></asp:Label><asp:TextBox ID="TextRichiesta" CssClass="AT76" Height="100px" Width="300px" TextMode="MultiLine" runat="server"></asp:TextBox>
                                         <asp:Button ID="Button1" class="AT77" runat="server" Text="Invia" ValidationGroup="validarq" /></li>
                                    <li><asp:Label ID="LabelNo" ForeColor="Red" runat="server" Text=""></asp:Label></li>
                                     <li style="font-size:small; text-align:justify"><asp:CheckBox ID="CheckBox1" runat="server" /><asp:Label ID="lblconprivacy" runat="server" Text="dichiaro di avere ricevuto le informazioni di cui all’art. 13 del D.lgs. 196/2003 in particolare riguardo ai diritti da me riconosciuti dalla legge ex art. 7 D.lgs. 196/2003, acconsento al trattamento dei miei dati con le modalità e per le finalità indicate nella informativa stessa, comunque strettamente connesse e strumentali alla gestione del rapporto contrattuale." ></asp:Label> 
                                     &nbsp;Per leggere l'informativa della privacy <a href="privacy.html" target="_blank">premi qui</a></li>
                                </ul>
            
            </asp:Panel> 
            <asp:Panel ID="Panel2" runat="server" Visible="false" style="text-align:center">
                <br /><asp:Label ID="risposta" runat="server" Text="Label"></asp:Label>
            </asp:Panel>           
            </div>
        </form>
</body>
</html>
