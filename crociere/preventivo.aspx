<%@ Page Language="VB" AutoEventWireup="false" CodeFile="preventivo.aspx.vb" Inherits="crociere_categorie" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link rel="stylesheet" href="preventivo.css" type="text/css" />
        <meta http-equiv="Pragma" content="no-cache" />
        <meta http-equiv="Cache-Control" content=" no-store, no-cache, must-revalidate, pre-check=0, post-check=0, max-age=0" />
        <meta http-equiv="Cache-Control" content="private" />
        <script type="text/javascript" src="preventivo.js?id=9"></script>
         <script type="text/javascript" src="altezza.js?id=2"></script>
         <script type="text/javascript">
             function conta() {
                 document.getElementById("ptotale").value = document.getElementById("pimporto").value * document.getElementById("ppax").value
             }
         </script>
</head>
<body >
    <form id="form1" runat="server">   
    <div id="layoutcosta" style="">
        <h1><asp:Label ID="labeltitolo" runat="server"></asp:Label></h1>
        <asp:Panel ID="PanelPreventivo" runat="server" >     
                <%--<img src="../images/preventivo.gif" style="position:absolute; margin: 4px 0 0 600px;" alt="preventivo" />--%>
                <div id="divpreventivo">
                    <ul>
                        <%--<li style="background:url(../images/sfondopreventivo.gif); margin-left:0px; width:683px; height:25px"></li>--%>
                        <asp:Repeater ID="RepeaterPreventivo" runat="server">
                            <ItemTemplate> 
                                 <li runat="server" id="riga">
                                    <asp:Label ID="descrizione" CssClass="Cl1" runat="server" Text='<%#databinder.eval(container.dataitem,"descrizione")%>'></asp:Label>
                                    <asp:Label ID="persone" CssClass="Cl2" runat="server" Text='<%#databinder.eval(container.dataitem,"persone")%>'></asp:Label>
                                    <asp:Label ID="prezzo" CssClass="Cl3" runat="server" Text='<%#databinder.eval(container.dataitem,"prezzo", "{0:c}")%>'></asp:Label>
                                    <asp:Label ID="totale" CssClass="Cl4" runat="server" Text='<%#databinder.eval(container.dataitem,"totale", "{0:c}")%>'></asp:Label>
                                    <asp:Label ID="prezzop" visible = "false" runat="server" Text='<%#databinder.eval(container.dataitem,"prezzo" )%>'></asp:Label>
                                    <asp:Label ID="totalep" visible = "false" runat="server" Text='<%#databinder.eval(container.dataitem,"totale")%>'></asp:Label>
                                    <asp:Label ID="pacc" visible = "false" runat="server" Text='<%#databinder.eval(container.dataitem,"pacc")%>'></asp:Label>
                                 </li>                                                          
                            </ItemTemplate>
                        </asp:Repeater>
                        <li id="noi" runat="server">
                            <asp:TextBox ID="pdescrizione" Width="460px" style="" runat="server"></asp:TextBox>
                            <asp:TextBox ID="ppax"  Width="30px" style="text-align:center; " runat="server"></asp:TextBox>
                            <asp:TextBox ID="pimporto"   Width="60px" style="text-align:right; " runat="server"></asp:TextBox>
                            <asp:TextBox ID="ptotale"  Width="60px" style="text-align:right; " runat="server"></asp:TextBox>
                            <asp:Button ID="Button4" runat="server" Font-Bold="true" Text="+" />
                        </li>
                    </ul>
                </div>
                <br />
                    <div id="asstogli" visible="false" runat="server" style="background: #f6f6f6; border: 1px solid #dcdcdc; padding:2px;">
                <asp:CheckBox ID="CheckTogli" runat="server"  /><asp:Label ID="Labeltogli" runat="server"
                    Text="Rinuncio all'assicurazione esonerando Fersina Viaggi da ogni responsabilità che ne consegue" Font-Bold="true" ></asp:Label>
                    </div>
                <br />
                
                <div  style="text-align:center;"> 
                    <img src="../images/prosegui-rosa.gif" id="prosegui" runat="server"  style="cursor:pointer" alt="prosegui" title="prosegui" />
                </div>
                <div id="menu" runat="server">
                    <ul>
                        <li id="vdett" runat="server" style="margin-top:-10px; height:140px; border-bottom:1px solid #ffffff;"><img src="../images/vedidettaglio.gif" alt="vedi dettaglio" title="vedi dettaglio" /></li>
                        <li id="vpacc" runat="server"><img src="../images/vedipacchetti.gif" alt="vedi dettaglio" title="aggiungi pacchetti" /></li>
                        <li id="voffe" runat="server"><img src="../images/vediriceviofferta.gif" alt="vedi dettaglio" title="ricevi offerta via mail" /></li>
                        <li id="vprez" runat="server"><img src="../images/vediseguiilprezzo.gif" alt="vedi dettaglio" title="segui il prezzo migliore" /></li>
                    </ul>
                </div>
                <iframe id="framedettaglio" runat="server" frameborder="0" style="border:none"></iframe>
                <%--<img src="../images/quota-comprende.gif" style="position:absolute; margin: 30px 0 0 600px;" alt="quota comprende" />
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
                    </ul>
                </div>
                <img src="../images/quota-non-comprende.gif" style="position:absolute; margin: 30px 0 0 600px;" alt="quota non comprende" />
                <div id="divnoncomprende">
                    <ul>
                        <li style="background:url(../images/sfondononcomprende.gif); margin-left:-20px; width:580px"></li>
                        <li>Bevande</li>
                        <li>Escursioni guidate a terra, facoltative e in vendita direttamente sulla nave</li>
                        <li>Quote di servizio obbligatorie (il prezzo varia a seconda della durata dell'itinerario)</li>
                        <li>Tutto quanto non indicato nella quota comprende</li>                        
                    </ul>
                </div>
                <img src="../images/pagamenti.gif" style="position:absolute; margin: 30px 0 0 600px;" alt="pagamenti" />
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
                 <img src="../images/documenti.gif" style="position:absolute; margin: 30px 0 0 600px;" alt="pagamenti" />
                <div id="divdocumenti">
                    <ul>
                         <li style="background:url(../images/sfondodocumenti.gif); margin-left:0px; width:580px; height:15px"></li>
                         <li style="height:auto; text-align:justify;"><asp:label runat="server" id="lbldocumenti"></asp:label></li>
                    </ul>    
                </div>--%>
            </asp:Panel>
        <asp:panel ID="paneleerrore" runat="server" Visible="false">                         
            <asp:Label ID="labelerrore" runat="server" style="font-size:large; font-weight: bold;" Visible="false"></asp:Label>
        </asp:panel>                        
      </div>   
       <asp:GridView ID="GridPrezzi" runat="server" AutoGenerateColumns="false"  Visible="false">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                           <asp:Label ID="Code"   runat="server" Text='<%#databinder.eval(container.dataitem,"Code")%>'></asp:Label>
                                                           <asp:Label ID="Description"   runat="server" Text='<%#databinder.eval(container.dataitem,"Description")%>'></asp:Label>
                                                           <asp:Label ID="Amount"   runat="server" Text='<%#databinder.eval(container.dataitem,"Amount")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                            </asp:GridView>   
                                            <asp:Label ID="prezzo" Visible="false" runat="server" CssClass="ClPPP" Font-Bold="True"></asp:Label>
    </form>           
</body>
</html>
