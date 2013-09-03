<%@ Page Language="VB" AutoEventWireup="false" CodeFile="riceviemail.aspx.vb" Inherits="crociere_riceviemail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body{font-family:Arial, Times New Roman;}
        .cl0{margin:7px 0 0 0; position:absolute;}
        .cl1{margin:0 0 0 90px; position:absolute;}
    </style>
    <script type="text/javascript" src="altezza.js?id=1"></script>
    <script type="text/javascript">
        function parti() {
            document.getElementById("eccopreventivo").value = top.document.getElementById("salvapreventivo").value
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <input id="eccopreventivo" style="visibility:hidden; width:1px" runat="server" type="text" />
    <div>
        <img src="../images/ricevi-mail.gif" style="position: absolute; margin: 0px 0 0 20px;" alt="ricevi via mail"  />
        <div style="position:absolute; margin: 35px 0 0 200px;">
            <asp:Button ID="confmail" ValidationGroup="valida" style="position: absolute; margin: 0 0 0 300px; width:90px; height:107px;" Text="Invia Mail" runat="server" />            
            <asp:Label ID="lblmail" CssClass="cl0" runat="server" Text="Email:"></asp:Label> <asp:TextBox style="border:1px solid #518E30; width:200px; height:30px;" CssClass="cl1" runat="server" ID="email" ></asp:TextBox><br />
            <asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server" Font-Size="X-Small" Font-Bold="true" style="text-align:right; position:absolute; margin: 0px 0 0 90px; width:200px" ValidationGroup="valida" ControlToValidate="email" Display="Static" ErrorMessage="e-mail non corretta!" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Font-Names="Arial"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator id="RequiredFieldValidator5" runat="server" font-Size="X-Small" Font-Bold="true" style="text-align:right; position:absolute; margin: 0px 0 0 190px; width:100px;" ValidationGroup="valida" ControlToValidate="email" Display="Static" ErrorMessage="obbligatorio!"></asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="avvmail" CssClass="cl0" style="width:500px; color:Green; font-size:xx-large;" runat="server" Font-Bold="true"  Visible = "false" Text="Email inviata correttamente!"></asp:Label> 
            <asp:Label ID="lblcognome" CssClass="cl0" runat="server" Text="Cognome:"></asp:Label> <asp:TextBox style="border:1px solid #518E30; width:200px; height:30px;" CssClass="cl1" runat="server" ID="cognome" ></asp:TextBox><br />
            <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" font-Size="X-Small" Font-Bold="true" style="text-align:right; position:absolute; margin: 0px 0 0 190px; width:100px;" ValidationGroup="valida" ControlToValidate="cognome" Display="Static" ErrorMessage="obbligatorio!"></asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="Labeltel" CssClass="cl0" runat="server" Text="Telefono:"></asp:Label> <asp:TextBox style="border:1px solid #518E30; width:200px; height:30px;" CssClass="cl1" runat="server" ID="telefono" ></asp:TextBox><br />                                              
            <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" font-Size="X-Small" Font-Bold="true" style="text-align:right; position:absolute; margin: 0px 0 0 190px; width:100px;" ValidationGroup="valida" ControlToValidate="telefono" Display="Static" ErrorMessage="obbligatorio!"></asp:RequiredFieldValidator>
            <asp:Label ID="Labelacc" Visible="false" runat="server" style="color:Red; font-weight:bold; position:absolute; font-size:small; width:400px; margin-top:40px;" Text="Per proseguire si prega accettare Legge sulla privacy "></asp:Label>                                    
            <br />
            <asp:CheckBox ID="ckconta" Font-Size="Small" Font-Bold="true" Text="Desidero essere contatatto telefonicamente riguardo a quest'offerta" runat="server" />
        </div>
        <div style="position:absolute; margin: 185px 0 0 0px; width:650px; text-align:justify; font-size:small">
            <asp:Checkbox ID="Checkboxprivacy" style="margin:5px; padding-top:5px;" runat="server" />
            <asp:Label ID="Label32" style="position:absolute;" runat="server" Text="Dichiaro di avere ricevuto le informazioni di cui all’art. 13 del D.lgs. 196/2003 in particolare riguardo ai diritti da me riconosciuti dalla legge ex art. 7 D.lgs. 196/2003, acconsento al trattamento dei miei dati con le modalità e per le finalità indicate nella informativa stessa, comunque strettamente connesse e strumentali alla gestione del rapporto contrattuale. Per leggere le condizioni relative  all'informativa della privacy <a href='../privacy.html' target='_blank'>premi qui.</a> "></asp:Label>
        </div>
        <div style="position:absolute; margin: 285px 0 0 0px; color:Green; width:660px; text-align:justify; font-size:medium">
            <asp:Label ID="Label3" style="position:absolute;" runat="server" Text="Trattasi di solo preventivo non vincolante e soggetto a verifica disponibilità in fase di riconferma. <b>Se si desidera bloccare il prezzo e opzionare la cabina preghiamo premere il tasto prosegui (tasto fucsia situato sotto il totale) ed inserire tutti i dati per inviare la richiesta.</b> Tutte le rischieste pervenute non sono impegnative o vincolanti fino alla reale accettazione della pratica che si completa con il pagamento. "></asp:Label>
        </div>
    </div>

    </form>
</body>
</html>
