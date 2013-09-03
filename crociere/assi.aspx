<%@ Page Language="VB" AutoEventWireup="false" CodeFile="assi.aspx.vb" Inherits="assi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="biglietti.css" type="text/css" />
    <script type="text/javascript" src="altezza.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="centroitx" style="text-align:center">
          <asp:HyperLink runat="server" ID="visualizza" Font-Size="Medium" Font-Bold="true" NavigateUrl="CondizioniassicurazioneAMITRAVEL.pdf" Target="_blank">>>> Visualizza o stampa le condizioni assicurative <<<</asp:HyperLink><br /><br />
          <iframe id="frameassi" runat="server" src="CondizioniassicurazioneAMITRAVEL.pdf" width="678px" height="600px" frameborder="0" style="border:none" scrolling="auto"></iframe><br /><br />
          <asp:Label id="lbl" style="font-size:small; font-weight:bold;" Text="Consigliamo sempre stampare e portare durante il viaggio le condizioni assicurative. Nel caso di necessità preghiamo leggere attentamente tutto quanto riportato e di eseguire correttamente la procedura necessaria al fine di non incorrere in successive problematiche relative ad assistenza sanitaria o rimborso. <br /><br />L'agenzia Fersina Viaggi è esonerata da qualsiasi responsabilità nel caso la procedura non venga eseguita in maniera corretta o fuori dai termini riportati nelle condizioni assicurative." runat="server"></asp:Label>
            
      </div>                       
    </form>
</body>
</html>

