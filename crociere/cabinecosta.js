var rigona = new Array();
var fremone = new Array();

var altezzaframe = 0;

function vedisottocabina(ria, px, frame, frm) {

    if (document.getElementById(frame).style.visibility == "hidden") {
        for (var i = 0; i <= 40; i++) {
            if (document.getElementById(rigona[i]) != null) {
                document.getElementById(rigona[i]).style.border = "none";
                document.getElementById(rigona[i]).style.borderBottom = "1px solid #dcdcdc";
                document.getElementById(rigona[i]).style.height = "30px";
            }
            if (document.getElementById(fremone[i]) != null) {
                document.getElementById(fremone[i]).style.visibility = 'hidden';
            }
        }
        document.getElementById(ria).style.height = px + 'px';
        document.getElementById(frame).style.visibility = 'visible';
        var mis = 330 + parseInt(altezzaframe);
        altezza(mis, frm);
    } else {
        document.getElementById(ria).style.height = '30px';
        document.getElementById(frame).style.visibility = 'hidden';
        altezza(altezzaframe, frm);
    }
}



function caricacab(dal, nomecab, tipocab, nomediv, stringa, codiceperiodo, categoria, mano, volo, voloobb, aeroporto, compagnia, tipocabina) {
    var eta1 = controllaetadrop(top.document.getElementById("eta1"));
    var eta2 = controllaetadrop(top.document.getElementById("eta2"));
    var eta3 = controllaetadrop(top.document.getElementById("eta3"));
    var eta4 = controllaetadrop(top.document.getElementById("eta4"));
    var eta5 = controllaetadrop(top.document.getElementById("eta5"));
    var persone = controllapaxdrop(top.document.getElementById("DropAd"));
    aeroporto = ricavaaeroporto(document.getElementById("DropVolo"));
    var cartaclub = top.document.getElementById("socio").value;
    //document.getElementById("barrainfo").style.visibility = "hidden";
    top.document.getElementById(nomediv).style.visibility = "visible";
    var divattesa = '"cabattesa"';
    var urlattesa;
    var nomeclub = "";
    if (compagnia == 0) {
        urlattesa = "url(../images/attesa-msc.jpg)";
    }
    if (compagnia == 1) {
        urlattesa = "url(../images/attesa-costa2.jpg)";
        nomeclub = top.document.getElementById("cognomeclub").value;
    }
    top.document.getElementById("layoutnascondi").style.visibility = "visible";
    top.document.getElementById(nomediv).innerHTML = "<div id=" + divattesa + " style='z-index:1001;background:" + urlattesa + " no-repeat; position:absolute; margin:7px; text-align:center; width:690px; height:326px;'><img src='../images/loading.gif' style='float:none;' alt='caricamento in corso' /></div><iframe id='framecabinecosta'  onload='ffiframe(" + divattesa + ")' style='margin-left:3px; padding-top:3px; width:700px; height:230px; border:none;' frameborder='0' scrolling='no' src='" + stringa + "?dal=" + dal + "&nomecabina=" + nomecab + "&tipocabina=" + tipocab + "&codiceperiodo=" + codiceperiodo + "&categoria=" + categoria + "&persone=" + persone + "&eta1=" + eta1 + "&eta2=" + eta2 + "&eta3=" + eta3 + "&eta4=" + eta4 + "&eta5=" + eta5 + "&cartaclub=" + cartaclub + "&nomeclub=" + nomeclub + "&volo=" + volo + "&voloobb=" + voloobb + "&aeroporto=" + aeroporto + "&compagnia=" + compagnia + "&tipologiacabina=" + tipocabina + "'></iframe>";
    //window.top.scrollTo(0, 300);
   // document.getElementById(mano).src = "../images/indicaverde.gif";
    //  ultimariga3 = riga;
}

function ricavaaeroporto(drop) {
    var aero;
    for (var x = 0; x < drop.options.length; x++) {
        if (drop.options[x].selected == true) {
            aero = drop.options[x].value;
            break
        }
    }
    return aero;
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

function assacabina(nomediv, stringa, codiceperiodo, persone, categoria, eta1, eta2, eta3, eta4, eta5, riga, cabina, nomeponte, volo, aeroporto, nomeclub, cartaclub, compagnia, frasepac, tipocabina, tipocab, passaporto) {
    var divattesa = '"cabattesa"';
    top.document.getElementById("layoutnascondi").style.visibility = "visible";
    var urlattesa;
    if (compagnia == 0) {
        urlattesa = "url(../images/attesa-msc.jpg)";
    }
    if (compagnia == 1) {
        urlattesa = "url(../images/attesa-costa2.jpg)";
    }
    top.document.getElementById(nomediv).innerHTML = "<div id=" + divattesa + " style='z-index:1001;background:" + urlattesa +" no-repeat; position:absolute; margin:7px; text-align:center; width:690px; height:326px;'><img src='../images/loading.gif' style='float:none;' alt='caricamento in corso' /></div><iframe id='framepreventivo'  onload='ffiframe(" + divattesa + ")'  style='height:230px;width:700px; border:none; margin-left:3px;' frameborder='0' scrolling='no' src='" + stringa + "?codiceperiodo=" + codiceperiodo + "&categoria=" + categoria + "&cabina= " + cabina + "&persone=" + persone + "&eta1=" + eta1 + "&eta2=" + eta2 + "&eta3=" + eta3 + "&eta4=" + eta4 + "&eta5=" + eta5 + "&nomeponte=" + nomeponte + "&volo=" + volo + "&aeroporto=" + aeroporto + "&nomeclub=" + nomeclub + "&cartaclub=" + cartaclub + "&compagnia=" + compagnia + "&frasepac=" + frasepac +  "&tipologiacabina=" + tipocabina + "&tipocabina=" + tipocab + "&passaporto=" + passaporto + "'></iframe>";
}

function vedidati(nomediv, stringa, codiceperiodo, categoria, compagnia, prezzo, tipocabina, tipocab, passaporto) {
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
    top.document.getElementById(nomediv).innerHTML = "<div id=" + divattesa + " style='z-index:1001;background:" + urlattesa + " no-repeat; position:absolute; margin:7px; text-align:center; width:690px; height:326px;'><img src='../images/loading.gif' style='float:none;' alt='caricamento in corso' /></div><iframe id='framedati'  onload='ffiframe(" + divattesa + ")'  style='height:250px;width:692px; margin-left:1px;' frameborder='0' scrolling='no' src='" + stringa + "?codiceperiodo=" + codiceperiodo + "&categoria=" + categoria + "&persone=" + persone + "&eta1=" + eta1 + "&eta2=" + eta2 + "&eta3=" + eta3 + "&eta4=" + eta4 + "&eta5=" + eta5 + "&nomeclub=" + nomeclub + "&cartaclub=" + cartaclub + "&compagnia=" + compagnia + "&prezzo=" + prezzo + "&tipologiacabina=" + tipocabina + "&rhq=1&passaporto="+ passaporto+ "'></iframe>";
}