function vedidettaglio(nomediv, stringa, codiceperiodo, totale, persone, compagnia, pacchetti, categoria, passaporto) {
    var divattesa = '"cabattesa2"';
    var urlattesa;
    if (compagnia == 0) {
        urlattesa = "url(../images/attesa-msc.jpg)";
    }
    if (compagnia == 1) {
        urlattesa = "url(../images/attesa-costa2.jpg)";
    }
    top.document.getElementById(nomediv).style.visibility = "visible";
    top.document.getElementById(nomediv).innerHTML = "<div id=" + divattesa + " style='z-index:1001;background:" + urlattesa + " no-repeat; position:absolute; margin:7px; text-align:center; width:690px; height:326px;'><img src='../images/loading.gif' style='float:none;' alt='caricamento in corso' /></div><iframe id='framedettaglio'  onload='ffiframe(" + divattesa + ")'  style='height:250px;width:687px; border-bottom:1px solid #0171BB; border-left:1px solid #0171BB; border-right:1px solid #0171BB; margin-left:7px;' frameborder='0' scrolling='no' src='" + stringa + "?codiceperiodo=" + codiceperiodo + "&totale=" + totale + "&persone=" + persone + "&compagnia=" + compagnia +"&pacchetti=" + pacchetti + "&categoria="+categoria +"&passaporto="+passaporto+"'></iframe>";
}

function vedipacchetti(nomediv, stringa, codiceperiodo, persone, categoria, eta1, eta2, eta3, eta4, eta5, riga, cabina, nomeponte, volo, aeroporto, nomeclub, cartaclub, compagnia, frasepac, tipocabina, assi, passaporto) {    
    var divattesa = '"cabattesa5"';
    top.document.getElementById("layoutnascondi").style.visibility = "visible";
    var urlattesa;
    if (compagnia == 0) {
        urlattesa = "url(../images/attesa-msc.jpg)";
    }
    if (compagnia == 1) {
        urlattesa = "url(../images/attesa-costa2.jpg)";
    }
    top.document.getElementById(nomediv).style.visibility = "visible";
    top.document.getElementById(nomediv).innerHTML = "<div id=" + divattesa + " style='z-index:1001;background:" + urlattesa + " no-repeat; position:absolute; margin:7px; text-align:center; width:690px; height:326px;'><img src='../images/loading.gif' style='float:none;' alt='caricamento in corso' /></div><iframe id='framedettaglio'  onload='ffiframe(" + divattesa + ")'  style='height:250px;width:687px; border-bottom:1px solid #0171BB; border-left:1px solid #0171BB; border-right:1px solid #0171BB; margin-left:7px;' frameborder='0' scrolling='no' src='" + stringa + "?codiceperiodo=" + codiceperiodo + "&categoria=" + categoria + "&cabina= " + cabina + "&persone=" + persone + "&eta1=" + eta1 + "&eta2=" + eta2 + "&eta3=" + eta3 + "&eta4=" + eta4 + "&eta5=" + eta5 + "&nomeponte=" + nomeponte + "&volo=" + volo + "&aeroporto=" + aeroporto + "&nomeclub=" + nomeclub + "&cartaclub=" + cartaclub + "&compagnia=" + compagnia + "&frasepac=" + frasepac + "&tipologiacabina=" + tipocabina + "&assi="+assi+"&passaporto=" + passaporto + "'</iframe>";
}

function vedimail(nomediv, stringa, codiceperiodo, totale, persone, compagnia, pacchetti, categoria, tipocabina, eta1, eta2, eta3, eta4, eta5, aeroporto, frasepac) {
    var divattesa = '"cabattesa6"';
    top.document.getElementById("layoutnascondi").style.visibility = "visible";
    var urlattesa;
    if (compagnia == 0) {
        urlattesa = "url(../images/attesa-msc.jpg)";
    }
    if (compagnia == 1) {
        urlattesa = "url(../images/attesa-costa2.jpg)";
    }
    top.document.getElementById(nomediv).style.visibility = "visible";
    top.document.getElementById(nomediv).innerHTML = "<div id=" + divattesa + " style='z-index:1001;background:" + urlattesa + " no-repeat; position:absolute; margin:7px; text-align:center; width:690px; height:326px;'><img src='../images/loading.gif' style='float:none;' alt='caricamento in corso' /></div><iframe id='framemail'  onload='ffiframe(" + divattesa + ")'  style='height:250px;width:687px; border-bottom:1px solid #0171BB; border-left:1px solid #0171BB; border-right:1px solid #0171BB; margin-left:7px;' frameborder='0' scrolling='no' src='" + stringa + "?codiceperiodo=" + codiceperiodo + "&totale=" + totale + "&persone=" + persone + "&compagnia=" + compagnia + "&pacchetti=" + pacchetti + "&categoria=" + categoria + "&eta1=" + eta1 + "&eta2=" + eta2 + "&eta3=" + eta3 + "&eta4=" + eta4 + "&eta5=" + eta5 + "&aeroporto=" + aeroporto + "&frasepac=" + frasepac +"&tipologiacabina=" + tipocabina + "'></iframe>";
}

function vedidati(nomediv, stringa, codiceperiodo, persone, categoria, eta1, eta2, eta3, eta4, eta5, riga, cabina, nomeponte, volo, aeroporto, nomeclub, cartaclub, compagnia, frasepac, prezzo, tipocabina, tipocab, assi, passaporto) {
    var divattesa = '"cabattesa4"';
    top.document.getElementById("layoutnascondi").style.visibility = "visible";
    if (top.document.getElementById("framedettaglio") != null) {
        top.document.getElementById("framedettaglio").style.visibility = "hidden";
        top.document.getElementById("framedettaglio").style.height = "1px";
    }
    if (top.document.getElementById("framemail") != null) {
        top.document.getElementById("framemail").style.visibility = "hidden";
        top.document.getElementById("framemail").style.height = "1px";
    }
    document.getElementById("menu").style.visibility = "hidden";
    document.getElementById("prosegui").style.visibility = "hidden";
    var urlattesa;
    if (compagnia == 0) {
        urlattesa = "url(../images/attesa-msc.jpg)";
    }
    if (compagnia == 1) {
        urlattesa = "url(../images/attesa-costa2.jpg)";
    }
    top.document.getElementById(nomediv).style.visibility = "visible";
    top.document.getElementById(nomediv).innerHTML = "<div id=" + divattesa + " style='z-index:1001;background:" + urlattesa + " no-repeat; position:absolute; margin:7px; text-align:center; width:690px; height:326px;'><img src='../images/loading.gif' style='float:none;' alt='caricamento in corso' /></div><iframe id='framedati'  onload='ffiframe(" + divattesa + ")'  style='height:250px;width:692px; margin-left:1px;' frameborder='0' scrolling='no' src='" + stringa + "?codiceperiodo=" + codiceperiodo + "&categoria=" + categoria + "&cabina= " + cabina + "&persone=" + persone + "&eta1=" + eta1 + "&eta2=" + eta2 + "&eta3=" + eta3 + "&eta4=" + eta4 + "&eta5=" + eta5 + "&nomeponte=" + nomeponte + "&volo=" + volo + "&aeroporto=" + aeroporto + "&nomeclub=" + nomeclub + "&cartaclub=" + cartaclub + "&compagnia=" + compagnia + "&frasepac=" + frasepac + "&prezzo=" + prezzo + "&tipologiacabina=" + tipocabina + "&tipocabina="+ tipocab + "&assi="+assi+"&passaporto=" + passaporto + "'></iframe>";
}


function caricaprezzi(nomediv, stringa, codiceperiodo, persone, categoria, eta1, eta2, eta3, eta4, eta5, riga, cabina, nomeponte, volo, aeroporto, nomeclub, cartaclub, compagnia, primo, tipocabina, assi, frasepac) {
    var divattesa = '"cabattesa3"';
    top.document.getElementById("layoutnascondi").style.visibility = "visible";
    //top.document.getElementById("framedettaglio").style.visibility = "hidden";
    //top.document.getElementById("framedettaglio").style.height = "1px";
    var urlattesa;
    if (compagnia == 0) {
        urlattesa = "url(../images/attesa-msc.jpg)";
    }
    if (compagnia == 1) {
        urlattesa = "url(../images/attesa-costa2.jpg)";
    }
    top.document.getElementById(nomediv).innerHTML = "<div id=" + divattesa + " style='z-index:1001;background:" + urlattesa + " no-repeat; position:absolute; margin:7px; text-align:center; width:690px; height:326px;'><img src='../images/loading.gif' style='float:none;' alt='caricamento in corso' /></div><iframe id='framepreventivo'  onload='ffiframe(" + divattesa + ")'  style='height:230px;width:700px; border:none; margin-left:3px;' frameborder='0' scrolling='no' src='" + stringa + "?codiceperiodo=" + codiceperiodo + "&categoria=" + categoria + "&cabina= " + cabina + "&persone=" + persone + "&eta1=" + eta1 + "&eta2=" + eta2 + "&eta3=" + eta3 + "&eta4=" + eta4 + "&eta5=" + eta5 + "&nomeponte=" + nomeponte + "&volo=" + volo + "&aeroporto=" + aeroporto + "&nomeclub=" + nomeclub + "&cartaclub=" + cartaclub + "&compagnia=" + compagnia + "&primo=" + primo + "&tipologiacabina=" + tipocabina + "&assi=" + assi + "&frasepac="+frasepac+"'></iframe>";
    window.top.scrollTo(0, 200);
}


function ffiframe(elem) {
    document.getElementById(elem).style.visibility = "hidden";
    document.getElementById(elem).style.height = "0px";
    document.getElementById("layoutnascondi").style.visibility = "hidden";
}

var rigona = new Array();

function premuto(riga) {
    for (var i = 0; i <= 6; i++) {
        if (document.getElementById(rigona[i]) != null) {
            document.getElementById(rigona[i]).style.height = "120px";
            document.getElementById(rigona[i]).style.borderBottom = "3px solid #0171BB";
            document.getElementById(rigona[i]).style.marginTop = "0px";
        }
    }
    if (document.getElementById(riga) != null) {
        document.getElementById(riga).style.height = "140px";
        document.getElementById(riga).style.borderBottom = "1px solid #ffffff";
        document.getElementById(riga).style.marginTop = "-10px";
    }
}

function assegnapreventivo(pre) {
    top.document.getElementById("salvapreventivo").value = pre;
}