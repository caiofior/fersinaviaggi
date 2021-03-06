﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="party-crociera.aspx.vb" Inherits="crociere_msc_crociere" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>fersinaviaggi.it: crociere MSC, COSTA, ROYAL CARIBBEAN e vacanze in barca a vela</title>    
    <link rel="stylesheet" href="crociere.css?id=1" type="text/css" />
    <link rel="stylesheet" href="../fersina.css?id=1" type="text/css" />
    <meta name="Keywords" content="Fersina Viaggi, viaggi Fersina, crociere, crociere MSC, crociere Costa, crociere Royal Caribbean, offerte crociere, vacanze barca vela, barca vela" />		
    <meta name="Description" content="Agenzia Fersina Viaggi di Trento, prenota online crociere MSC, Costa Crociere e Royal Caribbean o le vacanze in barca a vela." />
    <meta http-equiv="Content-Language" content="it" />
    <script type="text/javascript" src="pax.js?id=1"></script>
	<meta http-equiv="Content-type" content="text/html; charset=utf-8" />
	<meta content="global" name="distribution" />
	<meta content="IT" name="country" />
    <meta name="verify-v1" content="+AZEjL+QIwNen7x2Pqdq60/urG/sJza5gqYagwptK7M=" />
            <script language="javascript" type="text/javascript">
                function vedioff(elem, elem2) {
                    var pos = document.getElementById(elem2).offsetLeft;
                    var dim = document.getElementById(elem2).offsetWidth;
                    document.getElementById(elem).style.left = pos + "px";
                    document.getElementById(elem).style.width = dim + "px";
                    document.getElementById(elem).style.visibility = "visible";
                }
                function nascondo(elem) {
                    document.getElementById(elem).style.visibility = "hidden";
                }

                function nascond(elem, elem2) {
                    document.getElementById(elem).style.visibility = "hidden";
                    document.getElementById(elem2).style.background = "transparent"

                }
                function vedimenu(elem, elem2) {
                    document.getElementById(elem).style.visibility = "visible";
                    document.getElementById(elem2).style.background = "#ffffff";
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
<body style="background:url(../images/sfondoparty.jpg);">
    <div id="layoutnascondi" style="" ></div>
    <div id="bordino"></div>
    <div id="layout" style="background:#ffffff; border-left: 3px solid #ffffff; border-right: 3px solid #ffffff;">        
        <div id="logo">
            <div id="award"><img src="../images/msc-award.gif" alt="Vincitore 2 volte consecutive Msc award anno 2011 e anno 2012"  /></div>
            <div id="tel"><img src="../images/tel.gif" alt="chiamaci al numero 0461 914471" /></div>
            
            <img src="../images/fersinaviaggi.gif" alt="fersinaviaggi.it" />
        </div>
        <div id="contesto">
            <div id="menu">
                <ul>
                <li><a href="../index.html" title="Home page Fersina Viaggi">Home</a></li>
               </ul>
            </div>            
        </div>
        <div id="offecosta" runat="server" style="margin-top:-10px;" onmouseover="vedimenu('offecosta', 'lcosta');" onmouseout="nascond('offecosta', 'lcosta');"  >                 
            </div>
            <div id="offemsc" runat="server" style="margin-top:-10px;" onmouseover="vedimenu('offemsc','lmsc');" onmouseout="nascond('offemsc','lmsc' );" >                
            </div>
        <div id="contenutocrociera">
            <form id="form1" runat="server"> 
                <div id="linkcroce"><asp:Hyperlink ID="lnkcrocere" runat="server" Text="Crociere"></asp:Hyperlink></div>
                <div id="barraalta">
                    <div id="barraprezzo">
                        <asp:Label ID="lblda" style="font-size:small" runat="server" Text='da'></asp:Label>
                        <p style="margin:0px;padding:0px"><asp:Label ID="lblprezzo" runat="server" Text=''></asp:Label> </p>
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
                <div id="s3" >
                    <div id="domande" >
                        <img style="border:none; margin:-12px;" src="../images/p1.jpg" alt="party in crociera"  />
                    </div>
                </div>
                <div id="s2">
                    <div id="premio">
                        <img style="border:none; margin:-12px;" src="../images/p2.jpg" alt="party in crociera" />
                    </div>
                </div>
                <div id="s4">
                    <div id="facebook">
                            <img style="border:none; margin:-7px 0 0 -20px;" src="../images/p3.jpg" alt="party in crociera" />
                    </div>
                </div>

                <div id="datipax">
                        <img src="../images/indicaprimo.gif" style="position:absolute; margin: 11px 0 0 655px" />
                     <ul>
                        <li style="border-bottom:none">
                            <asp:Label ID="Label4" runat="server" CssClass="cle1" Text='Numero di passeggeri:'></asp:Label> 
                            <asp:Label ID="Label3" runat="server" CssClass="cle2" Text='Età passeggeri (alla data della partenza):'></asp:Label> 
                        </li>
                        <li>
                            
                            <asp:DropDownList ID="DropAd" style="font-size:medium" Width="45px"  CssClass="cle3" runat="server">                               
                                        <asp:ListItem Value="1">1</asp:ListItem>
                                        <asp:ListItem Value="2">2</asp:ListItem>
                                        <asp:ListItem Value="3">3</asp:ListItem>
                                        <asp:ListItem Value="4">4</asp:ListItem>
                                        <asp:ListItem Value="5">5</asp:ListItem>
                            </asp:DropDownList> 
                            <asp:Label ID="lblfamy" style="visibility:hidden"  CssClass="cle9" runat="server" Text="Per più famiglie inoltrare una<br />richiesta per ogni famiglia"></asp:Label>
                            <div class="cle4">
                                 <asp:DropDownList ID="eta1"  CssClass="cle5" Width="45px" runat="server">                                                                  
                                 </asp:DropDownList> 
                                 <asp:DropDownList ID="eta2"  CssClass="cle5" Width="45px" runat="server">                                                                  
                                 </asp:DropDownList> 
                                 <asp:DropDownList ID="eta3" style="visibility:hidden" CssClass="cle5" Width="45px" runat="server">                                                                  
                                 </asp:DropDownList> 
                                 <asp:DropDownList ID="eta4" style="visibility:hidden" CssClass="cle5" Width="45px" runat="server">                                                                  
                                 </asp:DropDownList> 
                                 <asp:DropDownList ID="eta5" style="visibility:hidden" CssClass="cle5" Width="45px" runat="server">                                                                  
                                 </asp:DropDownList> 
                             <asp:HyperLink ID="HyperLink1" style="visibility:hidden"  CssClass="cle12" runat="server">Vedi disponibilità</asp:HyperLink>
                             </div>
                         </li>
                         <li style="border-bottom:none">
                         <asp:Label ID="Label1" style="visibility:hidden" runat="server" CssClass="cle11" Text='Cognome:'></asp:Label> 
                            <asp:Label ID="Labelsocio" style="visibility:hidden" runat="server" CssClass="cle6" Text='N. carta socio '></asp:Label> 
                            <asp:textbox ID="socio" style="visibility:hidden" runat="server" CssClass="cle7" Width="70px"></asp:textbox>
                            <asp:DropDownList ID="dropsocio" style="visibility:hidden" runat="server" CssClass="cle8" Width="85px"></asp:DropDownList>
                            <asp:textbox ID="cognomeclub" Visible="false" style="visibility:hidden" runat="server" CssClass="cle10" Width="70px"></asp:textbox>
                         </li>
                     </ul>
                </div>
                 <div id="listino">
                    <div id="divframeprezzi" style="visibility:hidden"></div>                    
                    <div id="cabinecosta" style="visibility:hidden"></div>  
                    <div id="dettaglio" style="visibility:hidden"></div>   
                    <div id="dati" style="visibility:hidden"></div>  
                   <%-- <div id="divassegna" style="visibility:hidden"></div>  --%>  
                    <div id="itinerario" style="visibility:hidden"></div>      
                    <div id="datinave" style="visibility:hidden"></div>     
                    <input id="salvapreventivo" runat="server" type="text" style="visibility:hidden; width:1px;" />                                      
                 </div>
            </form> 
        </div>                 
    </div>    
    <div id="centroprogramma" style="visibility:hidden; text-align:right; z-index:1001;">
                    <div style="font-size:xx-large;  text-align:right" ><a onclick="javascript:scaricarichiesta('centroprogramma', 'layout')" style="cursor:pointer"><img style="border:none;" src="../images/close.png" alt="chiudi" /></a></div>
                    <iframe id="frame1" runat="server"  width="532px" height="390px" frameborder="0" style="border:none" scrolling="no"></iframe> 
    </div>
    <div id="layoutassegna">
        <div id="divassegna" style="visibility:hidden; "></div>
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
</html>
