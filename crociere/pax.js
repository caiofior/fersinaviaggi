var frmprezzi = "";

function caricaprezzi(nomediv, stringaframe, divattesa, imageback) {
    document.getElementById(nomediv).style.visibility = "visible";
    document.getElementById("cabinecosta").style.visibility = "hidden";
    document.getElementById("cabinecosta").innerHTML = "";
    document.getElementById("dettaglio").style.visibility = "hidden";
    document.getElementById("dettaglio").innerHTML = "";
    document.getElementById("dati").style.visibility = "hidden";
    document.getElementById("dati").innerHTML = "";
    divattesa = '"' + divattesa + '"';
   // document.getElementById("layoutnascondi").style.visibility = "visible";
    document.getElementById(nomediv).innerHTML = "<div id=" + divattesa + " style='position:absolute; margin:7px; text-align:center; width:690px; height:326px; background:"+ imageback+ "'><img src='../images/loading.gif' style='float:none;' alt='caricamento in corso' /></div><iframe id='frameprezzi' height='275' width='696'  onload='ffiframe(" + divattesa + ")' style='border:none;' frameborder='0' scrolling='no' src='" + stringaframe + "'></iframe>";
    //window.top.scrollTo(0, 300);
    //document.getElementById(mano).src = "../images/indicaverde.gif";
    //  ultimariga3 = riga;
}

function caricaitinerario(nomediv, stringaframe, imageback) {
    document.getElementById(nomediv).style.visibility = "visible";
    divattesa = '"caricaiti"';
    document.getElementById(nomediv).innerHTML = "<div id=" + divattesa + " style='position:absolute; margin:7px; text-align:center; width:690px; height:326px; background:" + imageback + "'><img src='../images/loading.gif' style='float:none;' alt='caricamento in corso' /></div><iframe id='frameitinerario' height='275' width='700'  onload='ffiframe(" + divattesa + ")' style='border:none; margin-top:10px;' frameborder='0' scrolling='no' src='" + stringaframe + "'></iframe>";
}

function caricadatinave(nomediv, stringaframe, imageback, idperiodo, idperiodo2, nave1, nave2, conf) {          
    var dentro = '<div id="titolo">';
    if (idperiodo2 != 0) {
        dentro = '<div id="titolo" style="height:75px;">';
        var cambiaHyperfotonave = "cambiaframe2('Hyperfotonave2', 'url(../images/sfondo-delimita5.gif) no-repeat', 'framenave', 'fotonave.aspx?id=" + idperiodo + "')";
        var cambiaHyperdatinave = "cambiaframe2('Hyperdatinave2', 'url(../images/sfondo-delimita5.gif) no-repeat', 'framenave', 'nave.aspx?id=" + idperiodo + "')";
        var cambiaHypercabine = "cambiaframe2('Hypercabine2', 'url(../images/sfondo-delimita6.gif) no-repeat', 'framenave', 'cabine.aspx?id=" + idperiodo + "')";    
        var cambiaHyperfotonave3 = "cambiaframe3('Hyperfotonave3', 'url(../images/sfondo-delimita5.gif) no-repeat', 'framenave', 'fotonave.aspx?id=" + idperiodo2 + "')";
        var cambiaHyperdatinave3 = "cambiaframe3('Hyperdatinave3', 'url(../images/sfondo-delimita5.gif) no-repeat', 'framenave', 'nave.aspx?id=" + idperiodo2 + "')";
        var cambiaHypercabine3 = "cambiaframe3('Hypercabine3', 'url(../images/sfondo-delimita6.gif) no-repeat', 'framenave', 'cabine.aspx?id=" + idperiodo2 + "')";
        dentro = dentro + '<a id="Hypercabine2" onclick="' + cambiaHypercabine + '" class="Cl7">CABINE ' + nave1 + '</a>';
        dentro = dentro + '<a id="Hyperfotonave2" onclick="' + cambiaHyperfotonave + '" class="Cl8">FOTO ' + nave1 + '</a>';
        dentro = dentro + '<a id="Hyperdatinave2" onclick="' + cambiaHyperdatinave + '" class="Cl9">DATI ' + nave1 + '</a>';
        if (conf == '1') {
            dentro = dentro + '<br />';
        }
        dentro = dentro + '<br /><br />';
        dentro = dentro + '<a id="Hypercabine3" onclick="' + cambiaHypercabine3 + '" class="Cl7">CABINE ' + nave2 + '</a>';
        dentro = dentro + '<a id="Hyperfotonave3" onclick="' + cambiaHyperfotonave3 + '" class="Cl8">FOTO ' + nave2 + '</a>';
        dentro = dentro + '<a id="Hyperdatinave3" onclick="' + cambiaHyperdatinave3 + '" class="Cl9">DATI ' + nave2 + '</a>';
    } else {        
        var cambiaHyperfotonave = "cambiaframe('Hyperfotonave', 'url(../images/sfondo-delimita5.gif) no-repeat', 'framenave', 'fotonave.aspx?id=" + idperiodo + "')";
        var cambiaHyperdatinave = "cambiaframe('Hyperdatinave', 'url(../images/sfondo-delimita5.gif) no-repeat', 'framenave', 'nave.aspx?id=" + idperiodo + "')";
        var cambiaHypercabine = "cambiaframe('Hypercabine', 'url(../images/sfondo-delimita6.gif) no-repeat', 'framenave', 'cabine.aspx?id=" + idperiodo + "')";    
        dentro = dentro + '<a id="Hypercabine" onclick="' + cambiaHypercabine + '" class="Cl7">CABINE</a>';
        dentro = dentro + '<a id="Hyperfotonave" onclick="' + cambiaHyperfotonave + '" class="Cl8">FOTO NAVE</a>';
        dentro = dentro + '<a id="Hyperdatinave" onclick="' + cambiaHyperdatinave + '" class="Cl9">DATI NAVE</a>';
    }    
    dentro = dentro + '</div>' 
    document.getElementById(nomediv).style.visibility = "visible";
    divattesa = '"caricadat"';
    document.getElementById(nomediv).innerHTML = dentro + "<div id=" + divattesa + " style='position:absolute; margin:7px; text-align:center; width:690px; height:326px; background:" + imageback + "'><img src='../images/loading.gif' style='float:none;' alt='caricamento in corso' /></div><iframe id='framenave' height='275' width='694'  onload='ffiframe(" + divattesa + ")' style='border:none; margin-top:10px;' frameborder='0' scrolling='no' src='" + stringaframe + "'></iframe>";
}

function cambiaframe(btt, nomesfondo, elem, asrc) {    
    document.getElementById("Hyperfotonave").style.background = "url(../images/sfondo-delimita2.gif) no-repeat";
    document.getElementById("Hyperdatinave").style.background = "url(../images/sfondo-delimita2.gif) no-repeat";
    document.getElementById("Hypercabine").style.background = "url(../images/sfondo-delimita.gif) no-repeat";
    document.getElementById(btt).style.background = nomesfondo;
    document.getElementById(elem).src = asrc;
}

function cambiaframe2(btt, nomesfondo, elem, asrc) {
    document.getElementById("Hyperfotonave2").style.background = "url(../images/sfondo-delimita2.gif) no-repeat";
    document.getElementById("Hyperdatinave2").style.background = "url(../images/sfondo-delimita2.gif) no-repeat";
    document.getElementById("Hypercabine2").style.background = "url(../images/sfondo-delimita.gif) no-repeat";
    document.getElementById("Hyperfotonave3").style.background = "url(../images/sfondo-delimita2.gif) no-repeat";
    document.getElementById("Hyperdatinave3").style.background = "url(../images/sfondo-delimita2.gif) no-repeat";
    document.getElementById("Hypercabine3").style.background = "url(../images/sfondo-delimita.gif) no-repeat";
    document.getElementById(btt).style.background = nomesfondo;
    document.getElementById(elem).src = asrc;
}

function cambiaframe3(btt, nomesfondo, elem, asrc) {
    document.getElementById("Hyperfotonave2").style.background = "url(../images/sfondo-delimita2.gif) no-repeat";
    document.getElementById("Hyperdatinave2").style.background = "url(../images/sfondo-delimita2.gif) no-repeat";
    document.getElementById("Hypercabine2").style.background = "url(../images/sfondo-delimita.gif) no-repeat";
    document.getElementById("Hyperfotonave3").style.background = "url(../images/sfondo-delimita2.gif) no-repeat";
    document.getElementById("Hyperdatinave3").style.background = "url(../images/sfondo-delimita2.gif) no-repeat";
    document.getElementById("Hypercabine3").style.background = "url(../images/sfondo-delimita.gif) no-repeat";
    document.getElementById(btt).style.background = nomesfondo;
    document.getElementById(elem).src = asrc;
}


function cambiapax(nomediv, compagnia) {
        var numero;
        var numpax;
        var pax = document.getElementById("DropAd");
        for (var x = 0; x < pax.options.length; x++) {
            if (pax.options[x].selected == true) {
                numero = x + 2;
                numpax = x + 1;
                break
            }
        }
       
        for (var xx = 1; xx < numero; xx++) {
            document.getElementById("eta" + xx).style.visibility = "visible";
        }
        for (var xxx = numero; xxx <= 5; xxx++) {
            document.getElementById("eta" + xxx).style.visibility = "hidden";
        }       
    var imgapp="";
    if (compagnia == 0) {
        imgapp = "url(../images/attesa-msc.jpg) no-repeat";
    }
    if (compagnia == 1) {
        imgapp = "url(../images/attesa-costa.jpg) no-repeat";
    }
    if (controllaetaf() >= 18) {
        caricaprezzi('divframeprezzi', frmprezzi, 'carica', imgapp);
    } else {
        alert("E' obbligatorio che partecipi almeno una persona maggiorenne!");
        document.getElementById("divframeprezzi").style.visibility = "hidden";
    }
}

function ffiframe(elem) {
    document.getElementById(elem).style.visibility = "hidden";
    document.getElementById(elem).style.height = "0px";
    document.getElementById("layoutnascondi").style.visibility = "hidden";
}


function controllaeta() {
    
    if (controllaetaf() >= 18) {
        document.getElementById("divframeprezzi").style.visibility = "visible";
    } else {
        alert("E' obbligatorio che partecipi almeno una persona maggiorenne!");
        document.getElementById("divframeprezzi").style.visibility = "hidden";        
    }
    document.getElementById("cabinecosta").style.visibility = "hidden";
    document.getElementById("cabinecosta").innerHTML = "";
    document.getElementById("dettaglio").style.visibility = "hidden";
    document.getElementById("dettaglio").innerHTML = "";
    document.getElementById("dati").style.visibility = "hidden";
    document.getElementById("dati").innerHTML = "";
}

function controllaetaf(){
    var numero;
    var pax = document.getElementById("DropAd") 
    for (var x = 0; x < pax.options.length; x++) {
        if (pax.options[x].selected == true) {
            numero = x + 2;    
            break
        }
    }
    maggiore = 0
    for (var xx = 1; xx < numero; xx++) {
        if (controllaetadrop(document.getElementById("eta" + xx)) >= maggiore) {
            maggiore = controllaetadrop(document.getElementById("eta" + xx))
        }
    }
    return maggiore;
}

function controllaetadrop(drop) {
    var numeroeta
    for (var x = 0; x < drop.options.length; x++) {
        if (drop.options[x].selected == true) {
            numeroeta = x + 1;
            break
        }
    }
    return numeroeta -1
}

function controllaetadp(drop, compagnia) {
    if (compagnia == 1) {
        var numeroeta
        for (var x = 0; x < drop.options.length; x++) {
            if (drop.options[x].selected == true) {
                numeroeta = x + 1;
                break
            }
        }
        if (numeroeta-1 > 0) {
            document.getElementById("divframeprezzi").style.visibility = "visible";
        } else {
            document.getElementById("divframeprezzi").style.visibility = "hidden";
            alert("Per i bambini da 6 mesi ad 1 anno preghiamo contattare i nostri uffici al 0461 914471");
        }
    }
}

function restituisci(frm, cerca, valore) {
    var sinistro = Left(frm, frm.indexOf(cerca));
    var appoggiodestro = Right(frm, frm.length - frmprezzi.indexOf(cerca));
    var misu = appoggiodestro.indexOf("&");
    var destro = Right(frm, frm.length - frm.indexOf(cerca) - misu);
    return sinistro + cerca + parseInt(valore) + destro;
}

function Left(str, n) {
    if (n <= 0)
        return "";
    else if (n > String(str).length)
        return str;
    else
        return String(str).substring(0, n);
}

function Right(str, n) {
    if (n <= 0)
        return "";
    else if (n > String(str).length)
        return str;
    else {
        var iLen = String(str).length;
        return String(str).substring(iLen, iLen - n);
    }
}



function nascondiresto(nomediv) {
    document.getElementById(nomediv).style.visibility = "hidden";
    document.getElementById(nomediv).style.height = "0px";
    document.getElementById(nomediv).style.width = "0px";
}

function caricarichiesta(nomediv, nomedivtoggle) {
    if (document.getElementById(nomediv).style.visibility == "visible") {
        document.getElementById(nomediv).style.visibility = "hidden";
        document.getElementById("layoutnascondi").style.visibility = "hidden";
    } else {
        document.getElementById(nomediv).style.visibility = "visible";
        document.getElementById("layoutnascondi").style.visibility = "visible";
    }
    toggleDisabled(document.getElementById(nomedivtoggle));
}

function scaricarichiesta(nomediv, nomedivtoggle) {
    document.getElementById(nomediv).style.visibility = "hidden";
    document.getElementById("layoutnascondi").style.visibility = "hidden";
    toggleDisabled(document.getElementById(nomedivtoggle));
}

function toggleDisabled(el) {
    try {
        el.disabled = el.disabled ? false : true;
    }
    catch (E) {
    }
    if (el.childNodes && el.childNodes.length > 0) {
        for (var x = 0; x < el.childNodes.length; x++) {
            toggleDisabled(el.childNodes[x]);
        }
    }
}


function cccframe(nomediv, stringaframe, imageback) {
    top.document.getElementById("layoutnascondi").style.visibility = "visible";
    document.getElementById(nomediv).style.visibility = "visible";
    divattesa = '"caricafff"';
    document.getElementById(nomediv).innerHTML = "<div id=" + divattesa + " style='z-index:1001;position:absolute; margin:1px; padding:5px; text-align:center; width:690px; height:326px; background:" + imageback + "'><img src='../images/loading.gif' style='float:none;' alt='caricamento in corso' /></div><iframe id='frameccc' height='275' width='694'  onload='ffiframe(" + divattesa + ")' style='border:none; margin-top:10px;' frameborder='0' scrolling='no' src='" + stringaframe + "'></iframe>";
}