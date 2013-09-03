<%@ Page Language="VB" AutoEventWireup="false" CodeFile="conferma-crociere.aspx.vb" Inherits="crociere_conferma_crociere" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>fersinaviaggi.it: crociere MSC, COSTA, ROYAL CARIBBEAN e vacanze in barca a vela</title>
    <link rel="stylesheet" href="../fersina.css?id=2" type="text/css" />
    <link rel="stylesheet" href="crociere.css?id=1" type="text/css" />
        <link rel="stylesheet" href="conferma-crociere.css" type="text/css" />
    <meta name="Keywords" content="Fersina Viaggi, viaggi Fersina, crociere, crociere MSC, crociere Costa, crociere Royal Caribbean, offerte crociere, vacanze barca vela, barca vela" />		
    <meta name="Description" content="Agenzia Fersina Viaggi di Trento, prenota online crociere MSC, Costa Crociere e Royal Caribbean o le vacanze in barca a vela." />
    <meta http-equiv="Content-Language" content="it" />
    <script type="text/javascript" src="pax.js?id=4"></script>
	<meta http-equiv="Content-type" content="text/html; charset=utf-8" />
	<meta content="global" name="distribution" />
	<meta content="IT" name="country" />
    <meta name="verify-v1" content="+AZEjL+QIwNen7x2Pqdq60/urG/sJza5gqYagwptK7M=" />
    <script type="text/javascript" src="pax.js"></script>
    <script type="text/javascript" src="conferma-crociere.js"></script>
        <script language="javascript" type="text/javascript">
            function chiudi() {
                top.document.getElementById("layoutnascondi").style.visibility = "hidden";
                top.document.getElementById("centroponte").style.visibility = "hidden";
            }
        </script>
        <!--[if lt IE 9]> 
        <style type="text/css"> 
            #ricerca{margin:-352px 0 0 0px; position:absolute; height:292px;}
            #barra{margin-top:-62px;}
            #contenuto{margin-top:-63px;}
        </style> 
        <![endif]-->
        <!--[if lt IE 8]> 
        <style type="text/css">
            #contenuto{margin-top:-83px;}
            #dentrop, #dentrop2{margin-left:-730px;}
        </style> 
        <![endif]-->
</head>
<body>
<div id="layoutnascondi" style="" ></div>
<div id="bordino"></div>
    <div id="layout">        
        <div id="logo">
            <div id="award"><img src="../images/msc-award.gif" alt="Vincitore 2 volte consecutive Msc award anno 2011 e anno 2012"  /></div>
            <div id="tel"><img src="../images/tel.gif" alt="chiamaci al numero 0461 914471" /></div>
            
            <img src="../images/fersinaviaggi.gif" alt="fersinaviaggi.it" />
        </div>
        <div id="contesto">
            <div id="menu">
                <ul>
                    <li><a href="../index.html" title="Home page Fersina Viaggi">Home</a></li>
                    <li><a href="../crociere/crociere.aspx" title="Crociere">Crociere</a></li>                    
                </ul>
            </div>            
        </div>
        <div id="contenutocrociera">

                <div id="linkcroce" style="background:none;"><asp:Hyperlink ID="lnkcrocere" ForeColor="Black" Font-Bold="false" runat="server" Text="Crociere"></asp:Hyperlink></div>
                <div id="barraalta">
                    <div id="barraprezzo">
                        
                    </div>
                    <div id="barratitolo">
                        <asp:Label ID="lbltitolo" runat="server" Text=''></asp:Label> 
                    </div>
                </div>
                <div id="s1">
                    <div id="fotoiti">
                        <asp:Image ID="Image1" ImageUrl="../images/lgmsc.gif"  runat="server" /><br />
                        <asp:Image ID="Image2" ImageUrl="../images/fascinosap.jpg" Width="205px"  runat="server" /><br />
                        <asp:Image ID="Image3" ImageUrl="../images/msc/01MA.jpg" Width="205px"  runat="server" /><br />
                        <div id="testoiti">
                            <asp:Label ID="Labelnave" runat="server" style="font-size:x-large;" Text='Costa Mediterranea'></asp:Label> <br />
                            <asp:Label ID="Labeldescri" runat="server" Text='7 giorni a bordo di Costa Mediterranea da Venezia il 28.03.2012'></asp:Label> 
                        </div>
                    </div>
                </div>
                <div id="s3">
                    <div id="domande" style="padding-left:18px; width:235px;">
                        <img src="../images/distributore-msc.jpg" style="cursor:default" alt="distributore ufficiale msc crociere" runat="server" id="distributore" />
                    </div>
                </div>
                <div id="s2">
                    <div id="premio">
                        <img src="../images/miglior-web.jpg" alt="premiata miglior agenzia web msc crociere" />
                    </div>
                </div>
                <div id="s4">
                    <div id="facebook">
                        <div id="facebookmargin">
                            <iframe src="http://www.facebook.com/plugins/likebox.php?id=202899166418298&amp;width=235&amp;connections=8&amp;stream=false&amp;header=false&amp;height=247" scrolling="no" frameborder="0" style="border:none; overflow:hidden; width:265px; height:247px; left:-5px; overflow:hidden; position:relative; top:-1px;" allowTransparency="true" ></iframe>
                        </div>
                    </div>
                </div>

                <div id="datipax">
                     <ul>
                        <li style="border-bottom:none">
                        
                        </li>
                        <li>           
                           <asp:Label id="lblcod" CssClass="Cl1" runat="server" Text="Codice Prenotazione:"></asp:Label>                                          
                           <asp:Label CssClass="Cl2" ID="lblcodicepreno" Font-Bold="true" runat="server"></asp:Label>
                         </li>
                         <li style="border-bottom:none">
                            <asp:Label id="lbldata" CssClass="Cl1" runat="server" Text="Data Prenotazione:"></asp:Label>
                            <asp:Label CssClass="Cl2" ID="lbldatapreno" runat="server"></asp:Label>
                         </li>
                     </ul>
                </div>
                 <div id="listino" style="font-size:small">
                    <br />
                    <div id="dentrodati">
                        <ul>
                            <li>
                                <asp:Label CssClass="Cl1" ID="Label2" runat="server" Text="Nave:"></asp:Label>
                                <asp:Label CssClass="Cl3" ID="lblnave" runat="server"></asp:Label>
                            </li>
                            <li>
                                <asp:Label CssClass="Cl1" ID="Label3" runat="server" Text="Itinerario:"></asp:Label>
                                <asp:Label CssClass="Cl3" ID="lblitinerario" runat="server"></asp:Label>
                            </li>
                            <li>
                                <asp:Label CssClass="Cl1" ID="Label4" runat="server" Text="Durata:"></asp:Label>
                                <asp:Label CssClass="Cl3" ID="lbldurata" runat="server"></asp:Label>
                            </li>
                            <li>
                                <asp:Label CssClass="Cl1" ID="Label5" runat="server" Text="Imbarco:"></asp:Label>
                                <asp:Label CssClass="Cl3" ID="lblimbarco" runat="server"></asp:Label>
                            </li>
                            <li>
                                <asp:Label CssClass="Cl1" ID="Label6" runat="server" Text="Sbarco:"></asp:Label>
                                <asp:Label CssClass="Cl3" ID="lblsbarco" runat="server"></asp:Label>
                            </li>
                            <li id="livoloda" runat="server" visible="false">
                                <asp:Label CssClass="Cl1" ID="Label16" runat="server" Text="Partenza da:"></asp:Label>
                                <asp:Label CssClass="Cl3" ID="voloda" runat="server"></asp:Label>
                            </li>
                            <li id="livoloa" runat="server" visible="false">
                                <asp:Label CssClass="Cl1" ID="Label17" runat="server" Text="Ritorno a:"></asp:Label>
                                <asp:Label CssClass="Cl3" ID="voloa" runat="server"></asp:Label>
                            </li>
                        </ul>
                        <br /><br />
                    </div>
                    
                    <div id="divpreventivo" style="font-size:small">
                    <h1><asp:Label ID="labeltitolo" runat="server"></asp:Label></h1>
                        <ul>
                            <asp:Repeater ID="RepeaterPreventivo" runat="server">
                                <ItemTemplate> 
                                    <li runat="server" id="riga">
                                        <asp:Label ID="descrizione" CssClass="Clp1" runat="server" Text='<%#databinder.eval(container.dataitem,"descrizione")%>'></asp:Label>
                                        <asp:Label ID="persone" CssClass="Clp2" runat="server" Text='<%#databinder.eval(container.dataitem,"pax")%>'></asp:Label>
                                        <asp:Label ID="prezzo" CssClass="Clp3" runat="server" Text='<%#databinder.eval(container.dataitem,"importo", "{0:c}")%>'></asp:Label>
                                        <asp:Label ID="totale" CssClass="Clp4" runat="server" Text='<%#databinder.eval(container.dataitem,"totale", "{0:c}")%>'></asp:Label>
                                     </li>                                                          
                                </ItemTemplate>
                            </asp:Repeater>
                            <li style="height:35px; border-bottom: 2px solid #10AAC6;">
                                <asp:Label ID="lbltot"  runat="server" Text='Totale:' Font-Size="x-Large"></asp:Label>
                                <asp:Label ID="tot"  runat="server" Text='Totale:' Width="200px" style="margin-left:480px;" Font-Size="x-Large" Font-Bold="true"></asp:Label>
                            </li>
                        </ul>
                         <br /><br />
                    </div>                   
                    <div id="divquota">                                
                        <asp:Panel ID="PanelAttesa" runat="server" >    
                            <h1><asp:Label ID="Label27" runat="server" Text="PRATICA IN ATTESA DI CONFERMA:" Width="300px"></asp:Label></h1>             
                            <ul>
                                <li>Preghiamo gentilmente attendere la riconferma da parte nostra della prenotazione</li>
                                <li>Appena la prenotazione sarà riconfermata sarà visualizzata nella conferma il numero di cabina e di pratica MSC</li>        
                                <li>Saranno visualizzati inoltre i dati per procedere al pagamento tramite bonifico o carta di credito</li>                                                                        
                                <li>Il pagamento dovrà avvenire entro il termine della scadenza indicato nei dati prenotazione</li>
                                <li>NB:Il pagamento tramite bonifico è accettato solo fino a 7 giorni prima della partenza entro il termine indicato</li>
                            </ul>
                        </asp:Panel>
                    </div>
                    <div id="dettagliodiv">
                        <asp:Panel ID="Paneldettaglio" runat="server"  Visible="false" >
                            <img src="../images/pratica-conf.gif" style="position:absolute; margin:0 0 0 530px;" alt="Dettaglio pratica"/>
                             <ul>
                                <li id="licab" runat="server">
                                    <asp:Label CssClass="Cl11" ID="Label9" runat="server" Text="Cabina:"></asp:Label>
                                    <asp:Label CssClass="Cl33" ID="lblcabina" runat="server"></asp:Label>
                                </li>
                                <li id="ricat" visible="false" runat="server">
                                    <asp:Label CssClass="Cl11" ID="Label14" runat="server" Text="Categoria:"></asp:Label>
                                    <asp:Label CssClass="Cl33" ID="lblcat" runat="server"></asp:Label>
                                    <asp:Label CssClass="Cl22" ID="lponte" Visible="false" runat="server" Text="Ponte:"></asp:Label>
                                    <asp:Label CssClass="Cl44" ID="lblponte" Visible="false" runat="server"></asp:Label>
                                </li>
                                <li>
                                    <asp:Label CssClass="Cl11" ID="Label15" runat="server" Text="Passeggeri:"></asp:Label>
                                    <asp:Label CssClass="Cl33" ID="Labelpasseggeri" runat="server"></asp:Label>
                                </li>
                                <li>
                                    <asp:Label CssClass="Cl11" ID="Label10" runat="server" Text="N°pratica MSC:"></asp:Label>
                                    <asp:Label CssClass="Cl33" ID="lblopzione" runat="server"></asp:Label>
                                </li>
                                <li runat="server" id="liscade">
                                    <asp:Image ID="imagesotto" runat="server" ImageUrl="../images/frecciasotto.gif" Visible="false" style="position:absolute; margin: 85px 0 0 360px" />
                                    <asp:Label CssClass="Cl11" ID="lblscade" runat="server" Text="Scadenza:"></asp:Label>
                                    <asp:Label CssClass="Cl33" ID="lblscadenza" Font-Bold="true" forecolor="red" runat="server"></asp:Label><br />
                                    <asp:Label CssClass="Cl11" ID="Labelscd" style="margin-top:20px" runat="server" Visible="false" Text=""></asp:Label> 

                                </li>
                            </ul>
                        </asp:Panel>
                        <br /><br /><br />
                    </div>                    
                    <asp:Panel ID="PanelOk" runat="server" Visible="false"  Width="690px" >  
                        <div id="divmenu">                            
                            <ul id="countrytabs" class="shadetabs" >
                                <li><a id="tabpag" runat="server" class="selected">PAGAMENTI</a></li>
                                <li><a id="tabpac" runat="server" >PACCHETTI</a></li>                                
                                <li><a id="tabcon" runat="server" >CONTRATTO</a></li>                              
                                <li><a id="tabquo" runat="server" >QUOTE</a></li>  
                                <li><a id="tabdoc" runat="server" >DOCUMENTI</a></li>                                                              
                                <li><a id="tabbig" runat="server" >BIGLIETTI</a></li>                             
                                <li><a id="tabpon" runat="server" >PONTI</a></li>    
                                <li><a id="tabassi" runat="server" >ASSICURA</a></li>                            
                            </ul>
                            <div id="divapriframe" style="visibility:hidden"></div>                                                                                                                                                                                              
                       </div>    
                     </asp:Panel>    
                    <div id="itinerario" style="visibility:hidden"></div>      
                    <div id="datinave" style="visibility:hidden"></div>                                        
                 </div>
        </div>                 
    </div>
    <div id="centroponte" style="visibility:hidden; text-align:right; z-index:1001;">
                    <div style="font-size:xx-large;  text-align:right" ><a onclick="javascript:chiudi()" style="cursor:pointer"><img style="border:none;" src="../images/close.png" alt="chiudi" /></a></div>
                    <iframe id="frame1" runat="server"  width="230px" height="600px" frameborder="0" style="border:none" scrolling="auto"></iframe> 
    </div>
    <div id="bottom" style="visibility:hidden; width:100%; margin-top:0px; height:130px; background:#AD1063; border-top:#EA7DB6 solid 5px;">
         <div id="menusotto">
                <ul>
                    <li><a href="../chi-siamo.html" title="Chi siamo fersinaviaggi.it">CHI SIAMO</a></li>
                    <li><a href="../dove-siamo.html" title="Dove siamo">DOVE SIAMO</a></li>
                    <li><a href="../contatti.html" title="Contatti Fersina Viaggi">CONTATTI</a></li>
                    <li><a href="../legale.html" target="_blank" title="Avviso legale Fersina Viaggi">AVVISO LEGALE</a></li>
                    <li><a href="../privacy.html" target="_blank" title="Condizioni privacy Fersina Viaggi">PRIVACY</a></li>
               </ul>
            </div> 
    </div>
</body>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.3.2/jquery.min.js"></script>
<script language="javascript" type="text/javascript" src="../slide.js"></script>
<form id="form2" runat="server"></form>
</html>