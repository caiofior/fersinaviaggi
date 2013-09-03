<%@ Page Language="VB" AutoEventWireup="false" CodeFile="cercavoli.aspx.vb" Inherits="FlySearch" EnableEventValidation="false" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>fersinaviaggi.it: crociere MSC, COSTA, ROYAL CARIBBEAN e vacanze in barca a vela</title>
    <link rel="stylesheet" href="fersina.css?id=1" type="text/css" />
    <link rel="stylesheet" href="cercavoli.css?id=1" type="text/css" />
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" type="text/css" />
    <meta name="Keywords" content="Fersina Viaggi, viaggi Fersina, crociere, crociere MSC, crociere Costa, crociere Royal Caribbean, offerte crociere, vacanze barca vela, barca vela" />		
    <meta name="Description" content="Agenzia Fersina Viaggi di Trento, prenota online crociere MSC, Costa Crociere e Royal Caribbean o le vacanze in barca a vela." />
    <meta http-equiv="Content-Language" content="it" />
	<meta http-equiv="Content-type" content="text/html; charset=utf-8" />
	<meta content="global" name="distribution" />
	<meta content="IT" name="country" />
    <meta name="verify-v1" content="+AZEjL+QIwNen7x2Pqdq60/urG/sJza5gqYagwptK7M=" />
    <!--[if lt IE 9]> 
        <style type="text/css"> 
            #ricerca{margin:-352px 0 0 0px; position:absolute; height:292px;}
            #barra{margin-top:-62px;}
            #offerte{margin-top:0px; padding-top:31px}
        </style> 
        <![endif]-->
            <!--[if lt IE 8]> 
        <style type="text/css"> 
            #ricerca{margin:-352px 0 0 0px; position:absolute; height:292px;}
            #barra{margin-top:-62px;}
            #offerte{padding-top:31px;}
        </style> 
        <![endif]-->
        
</head>
<body>
<div id="bordino"></div>
    <div id="layout">
        <div id="logo">
            <div id="award"><img src="images/msc-award.gif" alt="Vincitore 2 volte consecutive Msc award anno 2011 e anno 2012"  /></div>
            <div id="tel"><img src="images/tel.gif" alt="chiamaci al numero 0461 914471" /></div>
            
            <img src="images/fersinaviaggi.gif" alt="fersinaviaggi.it" />
        </div>
        <div id="contesto">
            <div id="menu">
                <ul>
                    <li><a href="index.html" title="Home page Fersina Viaggi">Home</a></li>
                    <li><a href="crociere/crociere.aspx" title="Crociere">Crociere</a></li>                    
                    <li><a href="barcavela/" title="Vacanze in barca a vela">Barca a Vela</a></li>
                    <li><a href="#" title="Vacanze in barca a vela">Oktoberfest</a></li>
                    <li style="border-right:none"><a href="#" title="Viaggi di capodanno">Capodanno</a></li>
               </ul>
            </div> 
            <div id="immagini">
                        <ul class="ppt">
	                        <li id="riga1" runat="server"><a href="crociere/crociere.aspx?promo=15" ><img src="images/partycrociera.jpg" alt="Party in Crociera" style="border:none;" /></a></li>                            
                            <li id="riga2" runat="server"><a href="crociere/crociere.aspx?promo=14" ><img src="images/prontivia.jpg" alt="Last Minute di Costa Crociere" style="border:none;" /></a></li>                            
                            
                            <li id="riga3" runat="server"><a href="crociere/crociere.aspx?promo=8" ><img src="images/roulettecosta.jpg" alt="Roulette Costa Crociere" style="border:none;" /></a></li>
	                        <li id="riga4" runat="server"><a href="crociere/crociere.aspx?promo=1" ><img src="images/risparmiasubitocosta.jpg" alt="risparmia subito con Costa Crociere da € 169,00" style="border:none;" /></a></li>
                        </ul>                        
            </div>            
            <div id="ricerca">
            <form>
               <div class="ui-widget">
                 <label for="fly_from">Da: </label>
                 <input id="fly_from" name="fly_from" />
                 <input type="hidden" id="fly_from_code" name="fly_from_code" />
               </div>
            </form>
            </div>
            <div id="barra">
                <img src="images/sfondo-barra.gif" alt="le migliore compagnie di navi da crociera" />
            </div>

        </div>
    </div>
    <div id="bottom" style="width:100%; margin-top:140px; height:60px; background:#AD1063; border-top:#EA7DB6 solid 5px;">
            <div id="menusotto">
                <ul>
                    <li><a href="chi-siamo.html" title="Chi siamo fersinaviaggi.it">CHI SIAMO</a></li>
                    <li><a href="dove-siamo.html" title="Dove siamo">DOVE SIAMO</a></li>
                    <li><a href="contatti.html" title="Contatti Fersina Viaggi">CONTATTI</a></li>
                    <li><a href="legale.html" target="_blank" title="Avviso legale Fersina Viaggi">AVVISO LEGALE</a></li>
                    <li><a href="privacy.html" target="_blank" title="Condizioni privacy Fersina Viaggi">PRIVACY</a></li>
               </ul>
            </div> 
    </div>
</body>
<script src="http://code.jquery.com/jquery-1.9.1.js"></script>
<script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script> 
<script type="text/javascript" src="cercavoli.js?id=1"></script>
<script type="text/javascript" src="slide.js?id=1"></script>
</html>
