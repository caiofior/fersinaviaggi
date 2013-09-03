<%@ Page Language="VB" AutoEventWireup="false" CodeFile="crociere.aspx.vb" Inherits="crociere_crociere" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Offerte crociere MSC, COSTA crociere e ROYAL CARIBBEAN</title>
    <link rel="stylesheet" href="../fersina.css?id=1" type="text/css" />
    <link rel="stylesheet" href="crociere.css?id=6" type="text/css" />
    <meta name="Keywords" content="offerte crociere, crociere offerte, offerte MSC, Offerte costa, Offerte royal caribbean, msc offerte, costa offerte, viaggi crociera, crociera, crociere offerte, last minute crociere, crociere last minute" />		
    <meta name="Description" content="Prenota online crociere MSC, Costa Crociere e Royal Caribbean. Offerte in crociera da € 190 per persona." />
    <meta http-equiv="Content-Language" content="it" />
	<meta http-equiv="Content-type" content="text/html; charset=utf-8" />
	<meta content="global" name="distribution" />
	<meta content="IT" name="country" />
    <script language="javascript" type="text/javascript" src="crociere.js?id=2"></script>
    <meta name="verify-v1" content="+AZEjL+QIwNen7x2Pqdq60/urG/sJza5gqYagwptK7M=" />
            <script language="javascript" type="text/javascript">
                var offertachiama = new Array();
                var roulette = new Array();        
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
<div id="bordino"></div>
<form id="form1" runat="server"> 
<div id="layoutnascondi2" style="" ></div>
<div id="fb-root"></div>
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
                    <li><a href="../crociere/crociere.aspx" title="Crociere">Tutte le CROCIERE</a></li>                    
                    <li id="ocosta"><a href="../crociere/crociere.aspx?promo=1" title="Offerte Costa Crociere" id="lcosta" onmouseover="vedioff('offecosta', 'ocosta');" onmouseout="nascondo('offecosta');">Offerte Costa Crociere</a></li> 
                    <li id="omsc"><a href="../crociere/crociere.aspx?promo=2&promo2=3&promo3=4&promo4=5&promo5=6" id="lmsc" title="Offerte Msc Crociere" onmouseover="vedioff('offemsc', 'omsc');" onmouseout="nascondo('offemsc');">Offerte Msc Crociere</a></li> 
                    <li><a href="../crociere/crociere.aspx?spezzata=1" title="low Cost">Low Cost</a></li>
               </ul>
            </div>  
           <div id="ultime" runat="server" style="visibility:hidden">
                <div id="ultimescroll">
                    <ul>
                        <asp:Repeater ID="RepeaterUltime" runat="server">
                            <ItemTemplate>                            
                                <li>
                                    <asp:HyperLink ID="HyperTitolo" ForeColor="#AD1063" Font-Bold="true" NavigateUrl=''  runat="server" text=''></asp:HyperLink><br /> 
                                    <asp:Label ID="da" runat="server" Text=''></asp:Label><br /> 
                                    <asp:Label ID="tipocab" Font-Size="X-Small" runat="server" Text=''></asp:Label> 
                                    <asp:Label ID="adulti" Visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"adulti")%>'></asp:Label>
                                    <asp:Label ID="bambini" Visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"bambini")%>'></asp:Label>
                                    <asp:Label ID="id_periodo" Visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"id_periodo")%>'></asp:Label>
                                    <asp:Label ID="prezzocab" Visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"prezzocab")%>'></asp:Label>
                                    <asp:Label ID="titolo" Visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"titolo")%>'></asp:Label>
                                    <asp:Label ID="dal" Visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"dal")%>'></asp:Label>
                                    <asp:Label ID="imbarco" Visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"imbarco")%>'></asp:Label>
                                    <asp:Label ID="tipocabina" Visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"tipocabina")%>'></asp:Label>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
           </div>
            <div id="immagini">
                        <ul class="ppt">
	                        <li id="riga1" runat="server"><a href="crociere.aspx?promo=15" ><img src="../images/partycrociera.jpg" alt="Offerte Provaci Msc" style="border:none;" /></a></li>                            
                            <li id="riga2" runat="server"><a href="crociere.aspx?promo=14" ><img src="../images/prontivia.jpg" alt="Last Minute di Costa Crociere" style="border:none;" /></a></li>                              
                            <li id="riga3" runat="server"><a href="crociere.aspx?promo=8" ><img src="../images/roulettecosta.jpg" alt="Roulette Costa Crociere" style="border:none;" /></a></li>
	                        <li id="riga4" runat="server"><a href="crociere.aspx?promo=1" ><img src="../images/risparmiasubitocosta.jpg" alt="risparmia subito con Costa Crociere da € 169,00" style="border:none;" /></a></li>	                                                    
                        </ul>
                        
            </div>
             
            <div id="scontocosta" runat="server" style="position:absolute; visibility:hidden;"><a href="crociere.aspx?compagnia=1"><%--<img src="../images/sconto-costa.gif?id=7" style="border:none;"  alt="sconto costa crociere"/>--%></a></div>
            <div id="scontomsc" runat="server" style="position:absolute; visibility:hidden;"><a href="crociere.aspx?promo=4&promo2=5"><img src="../images/sconto-msc.gif" style="border:none;" alt="sconto msc crociere"/></a></div>
            <div id="ricerca">
                <div id='carica' style='position:absolute; text-align:center; width:265px; padding-top:50px;'><img src='../images/loading.gif' style='float:none;' alt="caricamento in corso" /></div>
                <iframe src="../ricerca.aspx" id="framecerca" runat="server" height="313" scrolling="no" width="265" frameborder="0" style="border:none" ></iframe>
            </div>
            <div id="offecosta" runat="server" onmouseover="vedimenu('offecosta', 'lcosta');" onmouseout="nascond('offecosta', 'lcosta');"  >                 
            </div>
            <div id="offemsc" runat="server" onmouseover="vedimenu('offemsc','lmsc');" onmouseout="nascond('offemsc','lmsc' );" >                
            </div>
            <div id="barra">
                <img src="../images/sfondo-barra-alto.gif" alt="le migliore compagnie di navi da crociera" />
            </div>
        </div>
        <div id="contenuto">
                
                
                <div id="pagine">
                    <div id="dentrop">
                        <asp:Label ID="Labelp1" runat="server" Text=""></asp:Label>
                    </div>
                    <ul>
                    <asp:Repeater id="RepeaterPagine" runat="server">
                        <ItemTemplate>
                            <li id="lipag" runat="server">
                                <a href='<%#databinder.eval(container.dataitem,"linkato")%>' id="linkato" runat="server"><asp:Label ID="Labellink" ForeColor="blue" runat="server" Text='<%#databinder.eval(container.dataitem,"pagina")%>'></asp:Label></a>
                            </li>                            
                        </ItemTemplate>
                    </asp:Repeater>
                    </ul>
                </div>
                <br />
                <div id="elenco">
                    <ul>
                    <asp:Repeater ID="Repeater1" runat="server">      
                        <ItemTemplate>
                        <li id="riga" runat="server" style="margin:0px 0 8px 0; height:156px; padding:10px 0 0 2px; width:960px;    background: url(../images/elencosopra.gif) no-repeat">                                                       
                            <div id="pbanda"> 
                                <asp:Label id="titolo" runat="server" CssClass="titolo" Text=''></asp:Label>
                                <asp:Label id="paesi" runat="server" CssClass="titolo" Text='<%#databinder.eval(container.dataitem,"paesi")%>'></asp:Label>
                                <asp:Label id="zona" visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"zona")%>'></asp:Label>
                                <asp:Label id="dal" visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"dal")%>'></asp:Label>
                                <asp:Label id="dal2" visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"dal2")%>'></asp:Label>
                                <asp:Label id="capodanno" visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"capodanno")%>'></asp:Label>
                                <asp:Label id="al" visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"al")%>'></asp:Label>
                                <asp:Label id="al2" visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"al2")%>'></asp:Label>
                                <asp:Label id="imbarco" visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"imbarco")%>'></asp:Label>
                                <asp:Label id="sbarco" visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"sbarco")%>'></asp:Label>
                                <asp:Label id="imbarco1" visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"imbarco1")%>'></asp:Label>
                                <asp:Label id="sbarco1" visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"sbarco1")%>'></asp:Label>
                                <asp:Label id="imbarco2" visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"imbarco2")%>'></asp:Label>
                                <asp:Label id="sbarco2" visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"sbarco2")%>'></asp:Label>
                                <asp:Label id="imbarco3" visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"imbarco3")%>'></asp:Label>
                                <asp:Label id="sbarco3" visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"sbarco3")%>'></asp:Label>
                                 <asp:Label id="id_periodo" visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"id_periodo")%>'></asp:Label>
                                 <asp:Label id="id_nave" visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"id_nave")%>'></asp:Label>
                                 <asp:Label id="id_nave2" visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"id_nave2")%>'></asp:Label>
                                 <asp:Label id="scadenza" visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"scadenza")%>'></asp:Label>
                                 <asp:Label id="tariffaunica" visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"tariffaunica")%>'></asp:Label>
                                  <asp:Label id="cabinapromo" visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"cabinapromo")%>'></asp:Label>
                                  <asp:Label id="tapromo" visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"tariffapromo")%>'></asp:Label>
                                  <asp:Label id="scadenzapromo" visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"scadenzapromo")%>'></asp:Label>
                                  <asp:Label id="durata" visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"durata")%>'></asp:Label>
                                  <asp:Label id="compagnia" visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"compagnia")%>'></asp:Label>   
                                  <asp:Label id="evidenzia" visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"evidenza")%>'></asp:Label>                                
                                  <asp:Label id="idpromo" visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"idpromo")%>'></asp:Label>                                
                                   <asp:Label id="chiama" visible="false" runat="server" Text='<%#databinder.eval(container.dataitem,"chiama")%>'></asp:Label>                                
                            </div>
                            

                            <div id="contenuto1" class="ClB">
                                <ul>
                                    <li>
                                        <asp:HyperLink ID="HyperNave" NavigateUrl=''  runat="server" text='<%#databinder.eval(container.dataitem,"titolo")%>'></asp:HyperLink>                                        
                                        
                                    </li>
                                    <li>
                                        <span class="ClD">Partenza:</span><asp:Label id="dala"  runat="server" Text=''></asp:Label>
                                    </li>
                                    <li>
                                        <span class="ClD">Rientro:</span><asp:Label id="ala"  runat="server" Text=''></asp:Label>
                                    </li>
                                    </ul>
                           </div>
                             <div id="contenuto2" class="ClB">       
                                    <ul>
                                    <li><span class="ClD">Durata:</span><asp:Label id="duratavedi"  runat="server" Text=''></asp:Label></li>
                                    <li>
                                        <span class="ClD">Imbarco:</span><asp:Label id="dalimbarco"  runat="server" Text=''></asp:Label>
                                    </li>
                                    <li>
                                        <span class="ClD">Sbarco:</span><asp:Label id="alsbarco"  runat="server" Text=''></asp:Label>
                                    </li>
                                    </ul>
                                    
                            </div>
                            <div id="vistaprezzi">
                                        <ul>
                                        <li>
                                            <asp:Label ID="lblinterna" CssClass="a1" runat="server" Text="INTERNA:"></asp:Label>
                                            <s><asp:Label id="interoi" CssClass="a2" runat="server" Text='<%#databinder.eval(container.dataitem,"interoi")%>'></asp:Label></s>
                                            <b><asp:Label id="prezzoi" CssClass="a3" runat="server" Text='<%#databinder.eval(container.dataitem,"prezzoi")%>'></asp:Label> </b>
                                        </li>
                                        <li>
                                            <asp:Label ID="lblfinestra" CssClass="a1"  runat="server" Text="ESTERNA:"></asp:Label> 
                                            <s><asp:Label id="interoe" CssClass="a2" runat="server" Text='<%#databinder.eval(container.dataitem,"interoe")%>'></asp:Label></s>
                                            <b><asp:Label id="prezzoe" CssClass="a3" runat="server" Text='<%#databinder.eval(container.dataitem,"prezzoe")%>'></asp:Label> </b>
                                        </li>              
                                        <li>
                                            <asp:Label ID="lblbalcony" CssClass="a1" runat="server" Text="BALCONE:"></asp:Label>                                          
                                            <s><asp:Label id="interob" CssClass="a2" runat="server" Text='<%#databinder.eval(container.dataitem,"interob")%>'></asp:Label></s>
                                            <b><asp:Label id="prezzob" CssClass="a3" runat="server" Text='<%#databinder.eval(container.dataitem,"prezzob")%>'></asp:Label> </b>                                            
                                        </li>        
                                        <li>
                                            <asp:Label ID="Label1" CssClass="a1" runat="server" Text="SUITE:"></asp:Label>                                          
                                            <s><asp:Label id="interos" CssClass="a2" runat="server" Text='<%#databinder.eval(container.dataitem,"interos")%>'></asp:Label></s>
                                            <b><asp:Label id="prezzos" CssClass="a3" runat="server" Text='<%#databinder.eval(container.dataitem,"prezzos")%>'></asp:Label> </b>                                            
                                        </li>                              
                                        </ul>
                                </div>
                            <div id="compa"><asp:Image ID="Logocompa" runat="server"  /></div>
                            <div class="ClA"><asp:Image id="fotop" imageurl='<%#databinder.eval(container.dataitem,"fotop")%>' Width="167px" Height="110px" runat="server"></asp:Image></div>
                            <div class="ClC"><asp:Image id="mappap" imageurl='<%#databinder.eval(container.dataitem,"mappa")%>' Width="167px" Height="110px"  runat="server"></asp:Image></div>
                            <div class="ClE" ><asp:HyperLink id="hyperpreno" ImageUrl="../images/vediofferta.gif" runat="server"></asp:HyperLink></div>
                            <asp:image id="promozione" Visible="false" style="position:absolute; margin: 5px 0 0 580px;" runat="server" imageurl="../images/risparmiasubito.gif"></asp:image>
                            <asp:image id="chiamaspeciale" Visible="false" style="position:absolute; margin: -5px 0 0 970px; width:0px;" runat="server" imageurl="../images/chiamaspeciale.gif"></asp:image>
                            <asp:image id="party" Visible="false" style="position:absolute; margin: -5px 0 0 970px; width:0px;" runat="server" imageurl="../images/allinclusive.gif"></asp:image>
                        </li>
                        </ItemTemplate>              
                    </asp:Repeater>
                    
                    </ul> 
                </div>   
               <div id="sotto">
               <div id="pagine2">
                    <div id="dentrop2">
                        <asp:Label ID="Labelp2" runat="server" Text=""></asp:Label>
                    </div>
                    <ul>
                    <asp:Repeater id="RepeaterPagine2" runat="server">
                        <ItemTemplate>
                            <li id="lipag" runat="server">
                                <a href='<%#databinder.eval(container.dataitem,"linkato")%>' id="linkato" runat="server"><asp:Label ID="Labellink" ForeColor="blue" runat="server" Text='<%#databinder.eval(container.dataitem,"pagina")%>'></asp:Label></a>
                            </li>                            
                        </ItemTemplate>
                    </asp:Repeater>
                    </ul>
                </div>                   
                </div>
                <br />
                <ul>
                <li style="text-align:center;" id="nopromo" runat="server" visible="false">
                        <br /><br />
                        <asp:Label Font-Bold="true" ID="Labelno" Visible="false" Font-Size="Medium"  runat="server" Text="Attualmente non ci sono promozioni per l'itinerario scelto"></asp:Label>
                        <br /><br />
                    </li>
                </ul>  
                 
            </div>           
      
    </div>
    <div id="centroprogramma" style="visibility:hidden; height:500px; text-align:right; z-index:1001;">
            <div id="face" runat="server">
             <div style="text-align:center; width:550px;">
               <img src="../images/logofacebook.gif" alt="logo facebook" />
	            <div style="color:#136697;margin-top:10px; margin-bottom:10px; text-align:center;">PAGINA RISERVATA AI FAN DEL GRUPPO<br /> FACEBOOK CROCIERE LAST MINUTE<br /><br />Clicca su Connetti<br />per accedere alle esclusive offerte</div>
	        <div id="login" style="text-align:center">	            
                <fb:login-button show-faces="true" width="200" max-rows="1"></fb:login-button>
	        </div>
            <div id="likediv" style="margin-top:10px;">
                <%--<div style="color:#136697; font-size:small; margin-bottom:5px;  text-align:center;">Non sei fan della nostra pagina facebook. <br />Clicca su Mi Piace per diventarlo e <br />accedere alle esclusive offerte!</div>--%>
	            <%--<fb:like id="like" href="http://www.facebook.com/pages/Webtutsinfo/202899166418298" send="false" width="50" show_faces="false"></fb:like>--%>
              <%--  <div class="fb-like" id="like" href="http://www.facebook.com/202899166418298" data-send="false" data-width="450" data-show-faces="false"></div>--%>

	        </div><br /><br />

<%--            <a href="javascript:loginWithFacebook()">Accedi con Facebook</a>
	        </div>--%>
            </div>
            </div>
            <div id="ferragosto" runat="server" style="visibility:hidden; margin:-3px;">
                <img src="../images/chiusiferragosto.jpg" alt="Chiusi a Ferragosto" />
                <div style="font-size:xx-large;  text-align:right; margin: -510px 0 0 30px; " ><a onclick="javascript:scaricarichiesta('centroprogramma', 'layout')" style="cursor:pointer">&nbsp;&nbsp;</a> </div>
            </div>
     </div>         
                    
</form> 
    <div id="bottom" style="width:100%; margin-top:150px; height:60px; background:#AD1063; border-top:#EA7DB6 solid 5px;">
            <div id="menusotto">
                <ul>
                    <li><a href="../chi-siamo.html" title="Chi siamo fersinaviaggi.it">CHI SIAMO</a></li>
                    <li><a href="../dove-siamo.html" title="Dove siamo">DOVE SIAMO</a></li>
                    <li><a href="../contatti.html" title="Contatti Fersina Viaggi">CONTATTI</a></li>
                    <li><a href="../legale.html" title="Avviso legale Fersina Viaggi">AVVISO LEGALE</a></li>
                    <li><a href="../privacy.html" title="Condizioni privacy Fersina Viaggi">PRIVACY</a></li>
               </ul>
            </div> 
    </div>
</body>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.3.2/jquery.min.js"></script>
<script language="javascript" type="text/javascript" src="../slide.js?id=2"></script>
<script language="javascript" type="text/javascript" src="chiama.js?id=1"></script>
</html>
