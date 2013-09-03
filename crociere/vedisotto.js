var rigona = new Array();
var divone = new Array();
var imapiu = new Array();
var testone = new Array();
var prezzone = new Array();
var prezzone2 = new Array();
var prezzone3 = new Array();
var prezzocolonna1 = new Array();
var prezzocolonna2 = new Array();
var prezzocolonna3 = new Array();
var manina1 = new Array();
var manina2 = new Array();
var manina3 = new Array();
var appoggio1 = new Array();
var appoggio2 = new Array();
var appoggio3 = new Array();

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;

    return true;
}

var colore = new Array();  //controlla se pari o dispari
function IsNotUneven(numero) {
    if (isNaN(numero) == false) {
        return (numero % 2 == 1 ? true : false);
    }
    else {
        return null;
    }
}


function iniaz(cl1, cl2){
    for (var i = 0; i <= 40; i++) {
        if (IsNotUneven(i) == true) {
            colore[i] = cl1;
        } else {
            colore[i] = cl2;
        }
        if (document.getElementById(rigona[i])!= null) {
            document.getElementById(rigona[i]).style.border = "none";  
            document.getElementById(rigona[i]).style.borderBottom = "1px solid #dcdcdc";
            document.getElementById(rigona[i]).style.height = "33px";
        }
        if (document.getElementById(divone[i]) != null) {
            document.getElementById(divone[i]).style.visibility = "hidden";
        }
    }
}

function addListener(element, eventName, handler) {
    if (element.addEventListener) {
        element.addEventListener(eventName, handler, false);
    }
    else if (element.attachEvent) {
        element.attachEvent('on' + eventName, handler);
    }
    else {
        element['on' + eventName] = handler;
    }
}

var colonnaprima = 0;

function vedisotto(divo, ria, px, cl, ima, descri,pre, colonna, altezzar) {
    if (document.getElementById(divo) != null)   {
        if ((document.getElementById(divo).style.visibility == "hidden") || (colonnaprima != colonna)) {
            colonnaprima = colonna;
            for (var i = 0; i <= 40; i++) {
                if (document.getElementById(divone[i]) != null) {
                    document.getElementById(divone[i]).style.visibility = "hidden";
                }
                if (document.getElementById(rigona[i]) != null) {
                    document.getElementById(rigona[i]).style.border = "none";
                    document.getElementById(rigona[i]).style.borderBottom = "1px solid #dcdcdc";
                    document.getElementById(rigona[i]).style.height = "33px";
                    document.getElementById(rigona[i]).style.background = colore[i];
                    document.getElementById(imapiu[i]).src = "../images/piu.gif";
                    document.getElementById(testone[i]).style.fontWeight = "normal";
                    document.getElementById(prezzone[i]).style.fontWeight = "normal";
                    document.getElementById(prezzone2[i]).style.fontWeight = "normal";
                    document.getElementById(prezzone3[i]).style.fontWeight = "normal";
                }
            }
                    for (var i = 0; i <= 40; i++) {
                        if (document.getElementById(prezzocolonna1[i]) != null) {
                            document.getElementById(prezzocolonna1[i]).style.fontWeight = "normal";
                            document.getElementById(prezzocolonna1[i]).style.border = "none";
                            document.getElementById(prezzocolonna1[i]).style.cursor = "auto";
                            document.getElementById(prezzocolonna1[i]).onmouseover = function () {}
                            document.getElementById(prezzocolonna1[i]).setAttribute("onClick", "");
                        }
                        if (document.getElementById(prezzocolonna2[i]) != null) {
                            document.getElementById(prezzocolonna2[i]).style.fontWeight = "normal";
                            document.getElementById(prezzocolonna2[i]).style.border = "none";
                            document.getElementById(prezzocolonna2[i]).style.cursor = "auto";
                           document.getElementById(prezzocolonna2[i]).onmouseover = function () {}
                           document.getElementById(prezzocolonna2[i]).setAttribute("onClick", "");
                        }
                        if (document.getElementById(prezzocolonna3[i]) != null) {
                            document.getElementById(prezzocolonna3[i]).style.fontWeight = "normal";
                            document.getElementById(prezzocolonna3[i]).style.border = "none";
                            document.getElementById(prezzocolonna3[i]).style.cursor = "auto";
                            document.getElementById(prezzocolonna3[i]).onmouseover = function () {}
                            document.getElementById(prezzocolonna3[i]).setAttribute("onClick", "");    
                        }
                        if (document.getElementById(manina1[i]) != null) {
                            document.getElementById(manina1[i]).src= "../images/nulla.gif";
                        }
                        if (document.getElementById(manina2[i]) != null) {
                            document.getElementById(manina2[i]).src = "../images/nulla.gif";
                        }
                        if (document.getElementById(manina3[i]) != null) {
                            document.getElementById(manina3[i]).src = "../images/nulla.gif";
                        } 
                    }
                    for (var i = 0; i <= 40; i++) {
                        if (colonna == 1) {
                            if (document.getElementById(prezzocolonna1[i]) != null) {
                                document.getElementById(prezzocolonna1[i]).style.fontWeight = "bold";
                                if ((document.getElementById(prezzocolonna1[i]).style.color == "lightgrey") || (document.getElementById(prezzocolonna1[i]).style.color == "LightGrey") || (document.getElementById(prezzocolonna1[i]).style.color == "rgb(211, 211, 211)")) {
                                    document.getElementById(prezzocolonna1[i]).style.border = "3px solid #EEC6DA";
                                    document.getElementById(prezzocolonna1[i]).style.paddingLeft = "2px";                            
                                } else {
                                    document.getElementById(prezzocolonna1[i]).style.border = "3px solid #1ECDAE";
                                    document.getElementById(prezzocolonna1[i]).style.paddingLeft = "2px";
                                    document.getElementById(prezzocolonna1[i]).style.cursor = "pointer";
                                    document.getElementById(manina1[i]).src = "../images/indica.gif";                                    
                                    document.getElementById(prezzocolonna1[i]).setAttribute("onClick", "javascript:caricacab(" + appoggio1[i] + ")");                                    
                                    document.getElementById(prezzocolonna1[i]).onmouseover = function () {
                                        this.style.backgroundColor = "#7FFFE7";                                        
                                    }
                                    document.getElementById(prezzocolonna1[i]).onmouseout = function () {
                                        this.style.backgroundColor = "#ffffff";
                                    }
                                }
                            }
                            if (document.getElementById(prezzocolonna3[i]) != null) {
                                document.getElementById(prezzocolonna3[i]).style.paddingLeft = "5px";
                            }
                            if (document.getElementById(prezzocolonna2[i]) != null) {
                                document.getElementById(prezzocolonna2[i]).style.paddingLeft = "5px";
                            }
                        }
                        if (colonna == 2) {
                            if (document.getElementById(prezzocolonna2[i]) != null) {
                                document.getElementById(prezzocolonna2[i]).style.fontWeight = "bold";
                                if ((document.getElementById(prezzocolonna2[i]).style.color == "lightgrey") || (document.getElementById(prezzocolonna2[i]).style.color == "LightGrey") || (document.getElementById(prezzocolonna2[i]).style.color == "rgb(211, 211, 211)")) {
                                    document.getElementById(prezzocolonna2[i]).style.border = "3px solid #EEC6DA";
                                    document.getElementById(prezzocolonna2[i]).style.paddingLeft = "2px";                                                           
                                } else {
                                    document.getElementById(prezzocolonna2[i]).style.border = "3px solid #1ECDAE";
                                    document.getElementById(prezzocolonna2[i]).style.paddingLeft = "2px";
                                    document.getElementById(manina2[i]).src = "../images/indica.gif";
                                    document.getElementById(prezzocolonna2[i]).style.cursor = "pointer";
                                    document.getElementById(prezzocolonna2[i]).setAttribute("onClick", "javascript:caricacab(" + appoggio2[i] + ")");    
                                    document.getElementById(prezzocolonna2[i]).onmouseover = function () {
                                        this.style.backgroundColor = "#7FFFE7";
                                    }
                                    document.getElementById(prezzocolonna2[i]).onmouseout = function () {
                                        this.style.backgroundColor = "#ffffff";
                                    }
                                }
                            }
                            if (document.getElementById(prezzocolonna3[i]) != null) {
                                document.getElementById(prezzocolonna3[i]).style.paddingLeft = "5px";
                            }
                            if (document.getElementById(prezzocolonna1[i]) != null) {
                                document.getElementById(prezzocolonna1[i]).style.paddingLeft = "5px";
                            }
                        }
                        if (colonna == 3) {
                            if (document.getElementById(prezzocolonna3[i]) != null) {
                                document.getElementById(prezzocolonna3[i]).style.fontWeight = "bold";
                               //alert(document.getElementById(prezzocolonna3[i]).style.color);
                                if ((document.getElementById(prezzocolonna3[i]).style.color == "lightgrey") || (document.getElementById(prezzocolonna3[i]).style.color == "LightGrey") || (document.getElementById(prezzocolonna3[i]).style.color == "rgb(211, 211, 211)")) {
                                    document.getElementById(prezzocolonna3[i]).style.border = "3px solid #EEC6DA";
                                    document.getElementById(prezzocolonna3[i]).style.paddingLeft = "2px";
                                } else {
                                    document.getElementById(prezzocolonna3[i]).style.border = "3px solid #1ECDAE";
                                    document.getElementById(prezzocolonna3[i]).style.paddingLeft = "2px";
                                    document.getElementById(manina3[i]).src = "../images/indica.gif";
                                    document.getElementById(prezzocolonna3[i]).style.cursor = "pointer";                                   
                                    document.getElementById(prezzocolonna3[i]).setAttribute( "onClick", "javascript:caricacab("+appoggio3[i]+")");                                    
                                    document.getElementById(prezzocolonna3[i]).onmouseover = function () {
                                        this.style.backgroundColor = "#7FFFE7";
                                    }
                                    document.getElementById(prezzocolonna3[i]).onmouseout = function () {
                                        this.style.backgroundColor = "#ffffff";
                                    }
                                }
                            }
                            if (document.getElementById(prezzocolonna2[i]) != null) {
                                document.getElementById(prezzocolonna2[i]).style.paddingLeft = "5px";
                            }
                            if (document.getElementById(prezzocolonna1[i]) != null) {
                                document.getElementById(prezzocolonna1[i]).style.paddingLeft = "5px";
                            }
                        }
                    }
            document.getElementById(ria).style.height = px + 'px';
            var mis = parseInt(altezzar) + parseInt(px);
            top.document.getElementById("frameprezzi").style.height =  mis + "px";
            document.getElementById(ria).style.background = "#ffffff";
            //document.getElementById(ria).style.border = "1px dotted #dcdcdc";
            document.getElementById(divo).style.visibility = "visible";
            document.getElementById(divo).style.width = "300px";
            document.getElementById(ima).src = "../images/meno.gif";
            document.getElementById(descri).style.fontWeight = "bold";
            document.getElementById(pre).style.fontWeight = "bold";
        } else {
            colonnaprima = 0;
            document.getElementById(ria).style.border = "none";
            document.getElementById(ria).style.borderBottom = "1px solid #dcdcdc";
            document.getElementById(ria).style.height = "33px";
            top.document.getElementById("frameprezzi").style.height = parseInt(altezzar) + "px";
           document.getElementById(ria).style.background = cl;
           document.getElementById(divo).style.visibility = "hidden";
           document.getElementById(ima).src = "../images/piu.gif";
           document.getElementById(descri).style.fontWeight = "normal";
           document.getElementById(pre).style.fontWeight = "normal";
        }
   }
   altezzalayout();
}


function controllaetadrop(drop) {
    var numeroeta
    for (var x = 0; x < drop.options.length; x++) {
        if (drop.options[x].selected == true) {
            numeroeta = x + 1;
            break
        }
    }
    return numeroeta - 1
}

function controllapaxdrop(drop) {
    var pax;
    for (var x = 0; x < drop.options.length; x++) {
        if (drop.options[x].selected == true) {
            pax = x + 1;
            break
        }
    }
    return pax
}


function controllanomeclub(drop) {
    var nome;
    for (var x = 0; x < drop.options.length; x++) {
        if (drop.options[x].selected == true) {
            nome = drop.options[x].text;
            break
        }
    }
    return nome;
}


function caricacab(dal, nomecab, tipocab, nomediv, stringa, codiceperiodo, categoria, mano, volo, voloobb, aeroporto, compagnia, tipocabina) {
    top.document.getElementById("dettaglio").style.visibility = "hidden";
    top.document.getElementById("dettaglio").innerHTML = "";
    top.document.getElementById("dati").style.visibility = "hidden";
    top.document.getElementById("dati").innerHTML = "";
    var eta1 = controllaetadrop(top.document.getElementById("eta1"));
    var eta2 = controllaetadrop(top.document.getElementById("eta2"));
    var eta3 = controllaetadrop(top.document.getElementById("eta3"));
    var eta4 = controllaetadrop(top.document.getElementById("eta4"));
    var eta5 = controllaetadrop(top.document.getElementById("eta5"));
    var persone = controllapaxdrop(top.document.getElementById("DropAd"));
    var cartaclub = top.document.getElementById("socio").value;
    var nomeclub = '';
    document.getElementById("barrainfo").style.visibility = "hidden";
    top.document.getElementById(nomediv).style.visibility = "visible";
    var divattesa = '"cabattesa"';
    var urlattesa;
    if (compagnia == 0) {
        urlattesa = "url(../images/attesa-msc.jpg)";
        nomeclub = controllanomeclub(top.document.getElementById("dropsocio"));
    }
    if (compagnia == 1) {
        urlattesa = "url(../images/attesa-costa2.jpg)";
        nomeclub = top.document.getElementById("cognomeclub").value;
    }
   top.document.getElementById("layoutnascondi").style.visibility = "visible";
   top.document.getElementById(nomediv).innerHTML = "<div id=" + divattesa + " style='z-index:1001;background:" + urlattesa + " no-repeat; position:absolute; margin:7px; text-align:center; width:690px; height:326px;'><img src='../images/loading.gif' style='float:none;' alt='caricamento in corso' /></div><iframe id='framecabinecosta'  onload='ffiframe(" + divattesa + ")' style='margin-left:3px; padding-top:3px; width:700px; height:230px; border:none;' frameborder='no' scrolling='no' src='" + stringa + "?dal="+dal+"&nomecabina=" + nomecab + "&tipocabina=" + tipocab + "&codiceperiodo=" + codiceperiodo + "&categoria=" + categoria + "&persone=" + persone + "&eta1=" + eta1 + "&eta2=" + eta2 + "&eta3=" + eta3 + "&eta4=" + eta4 + "&eta5=" + eta5 + "&nomeclub=" + nomeclub + "&cartaclub=" + cartaclub + "&volo=" + volo + "&voloobb=" + voloobb + "&aeroporto=" + aeroporto +"&compagnia=" + compagnia + "&tipologiacabina=" + tipocabina + "'></iframe>";
   window.top.scrollTo(0, 300);
    document.getElementById(mano).src = "../images/indicaverde.gif";
  //  ultimariga3 = riga;
}

function vedidati(nomediv, stringa, codiceperiodo, categoria, compagnia, prezzo, tipocabina, tipocab) {
    var divattesa = '"cabattesa4"';
    var eta1 = controllaetadrop(top.document.getElementById("eta1"));
    var eta2 = controllaetadrop(top.document.getElementById("eta2"));
    var eta3 = controllaetadrop(top.document.getElementById("eta3"));
    var eta4 = controllaetadrop(top.document.getElementById("eta4"));
    var eta5 = controllaetadrop(top.document.getElementById("eta5"));
    var nomeclub = '';
    var persone = controllapaxdrop(top.document.getElementById("DropAd"));
    var cartaclub = top.document.getElementById("socio").value;
    top.document.getElementById("layoutnascondi").style.visibility = "visible";
//    document.getElementById("menu").style.visibility = "hidden";
//    document.getElementById("prosegui").style.visibility = "hidden";
    var urlattesa;
    if (compagnia == 0) {
        urlattesa = "url(../images/attesa-msc.jpg)";
        nomeclub = controllanomeclub(top.document.getElementById("dropsocio"));
    }
    if (compagnia == 1) {
        urlattesa = "url(../images/attesa-costa2.jpg)";
        nomeclub = top.document.getElementById("cognomeclub").value;
    }
    top.document.getElementById(nomediv).style.visibility = "visible";
    top.document.getElementById(nomediv).innerHTML = "<div id=" + divattesa + " style='z-index:1001;background:" + urlattesa + " no-repeat; position:absolute; margin:7px; text-align:center; width:690px; height:326px;'><img src='../images/loading.gif' style='float:none;' alt='caricamento in corso' /></div><iframe id='framedati'  onload='ffiframe(" + divattesa + ")'  style='height:250px;width:692px; margin-left:1px;' frameborder='0' scrolling='no' src='" + stringa + "?codiceperiodo=" + codiceperiodo + "&categoria=" + categoria + "&persone=" + persone + "&eta1=" + eta1 + "&eta2=" + eta2 + "&eta3=" + eta3 + "&eta4=" + eta4 + "&eta5=" + eta5 + "&nomeclub=" + nomeclub +"&cartaclub=" + cartaclub + "&compagnia=" + compagnia  + "&prezzo=" + prezzo + "&tipologiacabina=" + tipocabina + "'></iframe>";
}