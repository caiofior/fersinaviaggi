<%@ Page Language="VB" AutoEventWireup="false" CodeFile="biglietti.aspx.vb" Inherits="crociere_iti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="biglietti.css" type="text/css" />
    <script type="text/javascript" src="altezza.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="centroitx">
            <div id="dettagliodoc">
            <div id="cc"><img src="../images/biglietti.gif" alt="biglietti" /></div>
                            <h1><asp:Label ID="Label16" runat="server" Text="BIGLIETTI DI VIAGGIO:" Width="300px"></asp:Label></h1>
                            <ul><li style="height:160px">
                            <asp:Label ID="Label17" Font-Size="Small" runat="server" Text="Ricevuto il pagamento sarà nostra cura inviare i documenti di viaggio via mail prima della partenza. I biglietti elettronici si potranno anche scaricare dal nostro sito in questa scheda, vanno stampati e consegnati a bordo della nave. Ricordiamo che per tutti i passeggeri (anche minorenni) è obbligatorio presentare il documento d'identità in corso di validità e valida ai fini dell'espatrio, le informazioni sono elencate nella scheda DOCUMENTI. <br /><br />Il numero di cabina può essere soggetto a variazione fino al giorno dell'imbarco. La tipologia della cabina (interna, esterna con finestra o esterna con balcone) rimane garantita. "></asp:Label>
                            </li> </ul>
                                <asp:HyperLink ID="HyperTicket" Visible="false" runat="server" Font-Size="Large" Target="_blank">Stampa biglietto di viaggio - premi qui</asp:HyperLink><br />
                                <asp:HyperLink ID="HyperTicket2" Visible="false" runat="server" Font-Size="Large" Target="_blank">Stampa biglietto di viaggio - premi qui</asp:HyperLink><br />
                                <asp:HyperLink ID="HyperTicket3" Visible="false" runat="server" Font-Size="Large" Target="_blank">Stampa biglietto di viaggio - premi qui</asp:HyperLink><br />
                                <asp:HyperLink ID="HyperTicket4" Visible="false" runat="server" Font-Size="Large" Target="_blank">Stampa biglietto di viaggio - premi qui</asp:HyperLink><br />
                                <br />
                               
                                
                                     

            </div>
      </div>                       
    </form>
</body>
</html>
