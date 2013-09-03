<%@ Page Language="VB" AutoEventWireup="false" CodeFile="dettaglio.aspx.vb" Inherits="crociere_categorie" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link rel="stylesheet" href="dettaglio.css" type="text/css" />
        <meta http-equiv="Pragma" content="no-cache" />
        <meta http-equiv="Cache-Control" content=" no-store, no-cache, must-revalidate, pre-check=0, post-check=0, max-age=0" />
        <meta http-equiv="Cache-Control" content="private" />
        <script type="text/javascript" src="altezza.js"></script>
</head>
<body >
    <form id="form1" runat="server">   
    <div id="layoutcosta" style="">
                <img src="../images/quota-comprende.gif" style="position:absolute; margin: 30px 0 0 590px;" alt="quota comprende" />
                <div id="divcomprende">
                    <ul>
                        <li style="background:url(../images/sfondocomprende.gif); margin-left:-20px; width:580px"></li>
                        <li>Sistemazione nella cabina scelta per tutta la durata della crociera</li>
                        <li>Prima colazione - pranzo - cena e buffet di mezzanotte</li>
                        <li>Cocktail di benvenuto del Comandante</li>
                        <li>Attività di animazione a bordo</li>
                        <li>Utilizzo di tutte le attrezzature della Nave</li>
                        <li>Serata di Gala e serate a tema</li>
                        <li>Facchinaggio bagagli nei porti di inizio e termine crociera</li>
                        <li>Mezzi di imbarco e sbarco nei porti dove la nave non attraccherà la banchina</li>
                        <li>Tasse portuali, quote d'iscrizione e assicurazioni</li>
                        <li id="Libevande2" runat="server" visible="false">Bevande alcoliche e analcoliche illimitate durante tutta la crociera</li>
                        <li id="Liquote2" runat="server" visible="false">Quote di servizio (mance)</li>
                        <li id="Lidj" runat="server" visible="false">DJ SET a bordo nave in piscina e discoteca con i migliori dj </li>
                    </ul>
                </div>
                <img src="../images/quota-non-comprende.gif" style="position:absolute; margin: 30px 0 0 590px;" alt="quota non comprende" />
                <div id="divnoncomprende">
                    <ul>
                        <li style="background:url(../images/sfondononcomprende.gif); margin-left:-20px; width:580px"></li>
                        <li id="bevande" runat="server">Bevande</li>
                        <li>Escursioni guidate a terra, facoltative e in vendita direttamente sulla nave</li>
                        <li id="quote" runat="server">Quote di servizio obbligatorie (il prezzo varia a seconda della durata dell'itinerario)</li>
                        <li>Tutto quanto non indicato nella quota comprende</li>                        
                    </ul>
                </div>
                <img src="../images/pagamenti.gif" style="position:absolute; margin: 30px 0 0 590px;" alt="pagamenti" />
                <div id="divpagamenti">                    
                    <ul>
                        <li style="background:url(../images/sfondopagamenti.gif); margin-left:0px; width:580px; height:15px"></li>
                        <asp:Repeater ID="RepeaterPaga" runat="server">
                            <ItemTemplate>                                 
                                <li>                                                         
                                    <asp:Label ID="descripaga" CssClass="PP1" runat="server" Text='<%#databinder.eval(container.dataitem,"descripaga")%>'></asp:Label> 
                                    <asp:Label ID="prezzo" CssClass="PP2" runat="server" Text='<%#databinder.eval(container.dataitem,"prezzo", "{0:c}")%>'></asp:Label>  
                                    <asp:Label ID="Label12" runat="server" CssClass="PP3" Text="Entro:"></asp:Label>
                                    <asp:Label ID="scadenza" CssClass="PP4" runat="server" Text='<%#databinder.eval(container.dataitem,"scadenza", "{0:dd/MM/yyyy}")%>'></asp:Label>                                                                                                      
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>  
                    </ul>
                    <br /><br />
                    <img src="../images/modalitapagamento.jpg"  alt="Modalità di pagamento" title="Modalità di pagamento" />
                </div>
                 <img src="../images/documenti.gif" style="position:absolute; margin: 30px 0 0 590px;" alt="pagamenti" />
                <div id="divdocumenti">
                    <ul>
                         <li style="background:url(../images/sfondodocumenti.gif); margin-left:0px; width:580px; height:15px"></li>
                         <li style="height:auto; text-align:justify;"><asp:label runat="server" id="lbldocumenti"></asp:label></li>
                    </ul>    
                </div>       
      </div>   
       
    </form>           
</body>
</html>
