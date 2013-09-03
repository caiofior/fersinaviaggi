<%@ Page Language="VB" AutoEventWireup="false" CodeFile="voli.aspx.vb" Inherits="crociere_categorie" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link rel="stylesheet" href="cabinecosta.css" type="text/css" />
        <meta http-equiv="Pragma" content="no-cache" />
        <meta http-equiv="Cache-Control" content=" no-store, no-cache, must-revalidate, pre-check=0, post-check=0, max-age=0" />
        <meta http-equiv="Cache-Control" content="private" />
        <script type="text/javascript" src="cabinecosta.js"></script>
        <script type="text/javascript" src="altezza.js"></script>
</head>
<body style="margin:0px; background:none #ffffff; position:fixed;" >
    <form id="form1" runat="server">
      

    <div id="layoutcosta" style="">
        <asp:panel id="panelvolo" runat="server" visible ="false">
        <div id="volodiv">
            <h1><asp:Label ID="Label1" runat="server" Text="SELEZIONA AEROPORTO DI PARTENZA:"></asp:Label></h1> 
            <img  src="../images/volo.gif" alt="seleziona aeroporto" />
            <ul style="margin: -110px 0 0 200px; width:490px;">
            <li>
            <asp:label ID="lblvolo" CssClass="Cla1" Text=""  runat="server"></asp:label><br />
            </li>
            <li>
            <asp:label ID="lblapt" CssClass="Cla2" Text="Seleziona aeroporto:"  runat="server"></asp:label>
                                    <asp:DropDownList CssClass="Cla3" ID="DropVolo" runat="server" Width="200px" Height="30px">                                                     
                                                            <asp:ListItem  selected="True" value="NNN">Solo Crociera volo escluso</asp:ListItem>
															<asp:ListItem  value="AHO">Alghero</asp:ListItem>
															<asp:ListItem  value="AOI">Ancona</asp:ListItem>
															<asp:ListItem  value="BRI">Bari</asp:ListItem>
															<asp:ListItem  value="BLQ">Bologna</asp:ListItem>
															<asp:ListItem  value="BDS">Brindisi</asp:ListItem>
															<asp:ListItem  value="CAG">Cagliari</asp:ListItem>
															<asp:ListItem  value="CTA">Catania</asp:ListItem>
															<asp:ListItem  value="FLR">Firenze</asp:ListItem>
															<asp:ListItem  value="GOA">Genova</asp:ListItem>
															<asp:ListItem  value="SUF">Lamezia Terme</asp:ListItem>
															<asp:ListItem  value="MXP">Milano Malpensa</asp:ListItem>
															<asp:ListItem  value="NAP">Napoli</asp:ListItem>
															<asp:ListItem  value="OLB">Olbia</asp:ListItem>
															<asp:ListItem  value="PMO">Palermo</asp:ListItem>
															<asp:ListItem  value="PEG">Perugia</asp:ListItem>
															<asp:ListItem  value="PSA">Pisa</asp:ListItem>
															<asp:ListItem  value="REG">Reggio Calabria</asp:ListItem>
															<asp:ListItem  value="FCO">Roma Fiumicino</asp:ListItem>
															<asp:ListItem  value="TRN">Torino Caselle</asp:ListItem>
															<asp:ListItem  value="TRS">Trieste</asp:ListItem>
															<asp:ListItem  value="VCE">Venezia</asp:ListItem>
															<asp:ListItem  value="VRN">Verona</asp:ListItem>
                                    </asp:DropDownList>
                                </li>
                                <li>
                                   <asp:Button ID="volocontinua" CssClass="Cla4" runat="server" Text="CONTINUA" />
                                </li>
                                </ul>
            </div>
            <div id="Div1" runat="server" style="color: #ffffff; font-weight:bold; background:  #AD1063; width:693px;" >
                                    <img src="../images/perpreventivo.gif" style="margin:3px 0 0 5px;"  alt="Ora seleziona l'aeroporto desiderato"/> 
                                    <div style="margin:-30px 0 0 0px; padding:5px 0px 0 40px;  height:40px; "><asp:Label ID="Labelvolodesc" style="font-size:small" runat="server" Text="Ora seleziona l'aeroporto desiderato"></asp:Label></div>
                                </div>
        </asp:panel>
                       
      </div>
    </form>
           
</body>
</html>
