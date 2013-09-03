var rigona1 = new Array();
var divone1 = new Array();
var rigona2 = new Array();
var divone2 = new Array();
var rigona3 = new Array();
var divone3 = new Array();
var altezzadiv;

function iniaz() {
    for (var i = 0; i <= 40; i++) {
        if (document.getElementById(rigona1[i]) != null) {
            document.getElementById(rigona1[i]).style.border = "none";
            document.getElementById(rigona1[i]).style.borderBottom = "1px solid #dcdcdc";
            document.getElementById(rigona1[i]).style.height = "25px";
        }
        if (document.getElementById(divone1[i]) != null) {
            document.getElementById(divone1[i]).style.visibility = "hidden";
        }
        if (document.getElementById(rigona2[i]) != null) {
            document.getElementById(rigona2[i]).style.border = "none";
            document.getElementById(rigona2[i]).style.borderBottom = "1px solid #dcdcdc";
            document.getElementById(rigona2[i]).style.height = "25px";
        }
        if (document.getElementById(divone2[i]) != null) {
            document.getElementById(divone2[i]).style.visibility = "hidden";
        }
        if (document.getElementById(rigona3[i]) != null) {
            document.getElementById(rigona3[i]).style.border = "none";
            document.getElementById(rigona3[i]).style.borderBottom = "1px solid #dcdcdc";
            document.getElementById(rigona3[i]).style.height = "25px";
        }
        if (document.getElementById(divone3[i]) != null) {
            document.getElementById(divone3[i]).style.visibility = "hidden";
        }
    }
}

function vedisotto(divo, ria, px, cl, frm) {
    if (document.getElementById(divo) != null) {

        if (document.getElementById(divo).style.visibility == "hidden") {
            for (var i = 0; i <= 40; i++) {
                if (document.getElementById(divone1[i]) != null) {
                    document.getElementById(divone1[i]).style.visibility = "hidden";
                }
                if (document.getElementById(rigona1[i]) != null) {
                    document.getElementById(rigona1[i]).style.border = "none";
                    document.getElementById(rigona1[i]).style.borderBottom = "1px solid #dcdcdc";
                    document.getElementById(rigona1[i]).style.height = "25px";
                }
                if (document.getElementById(divone2[i]) != null) {
                    document.getElementById(divone2[i]).style.visibility = "hidden";
                }
                if (document.getElementById(rigona2[i]) != null) {
                    document.getElementById(rigona2[i]).style.border = "none";
                    document.getElementById(rigona2[i]).style.borderBottom = "1px solid #dcdcdc";
                    document.getElementById(rigona2[i]).style.height = "25px";
                }
                if (document.getElementById(divone3[i]) != null) {
                    document.getElementById(divone3[i]).style.visibility = "hidden";
                }
                if (document.getElementById(rigona3[i]) != null) {
                    document.getElementById(rigona3[i]).style.border = "none";
                    document.getElementById(rigona3[i]).style.borderBottom = "1px solid #dcdcdc";
                    document.getElementById(rigona3[i]).style.height = "25px";
                }
            }
            altezza(altezzadiv+parseInt(px), frm);
            document.getElementById(ria).style.height = px;
            document.getElementById(ria).style.background = "#ffffff";
            //document.getElementById(ria).style.border = "1px dotted #dcdcdc";
            document.getElementById(divo).style.visibility = "visible";
            document.getElementById(divo).style.width = "300px";
        } else {
            altezza(altezzadiv, frm);
            document.getElementById(ria).style.border = "none";
            document.getElementById(ria).style.borderBottom = "1px solid #dcdcdc";
            document.getElementById(ria).style.height = "25px";
            document.getElementById(ria).style.background = cl;
            document.getElementById(divo).style.visibility = "hidden";
        }
    }
}




var frasepac =";";

function segna(idpac, valore, nomedrop) {
    if (frasepac.indexOf(";" + idpac + "-") == -1) {
        frasepac = frasepac + idpac + "-" + valore + ";";
    } else {
        var appindexfrase = frasepac.indexOf(";" + idpac + "-");
        var prima = Left(frasepac, appindexfrase) + ";";
        var dopo = Right(frasepac, frasepac.length - appindexfrase - 1);
        var indexapp = dopo.indexOf(";");
        var dopoapp = Right(dopo, dopo.length - indexapp -1);
        if (valore > 0) {
            frasepac = prima + idpac + "-" + valore + ";" + dopoapp;
        } else {
            frasepac = prima + dopoapp;
        }
    }
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


function caricaprezzi(nomediv, stringa, codiceperiodo, persone, categoria, eta1, eta2, eta3, eta4, eta5, riga, cabina, nomeponte, volo, aeroporto, nomeclub, cartaclub, compagnia, primo, tipocabina, assi, passaporto) {
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
    top.document.getElementById(nomediv).innerHTML = "<div id=" + divattesa + " style='z-index:1001;background:" + urlattesa + " no-repeat; position:absolute; margin:7px; text-align:center; width:690px; height:326px;'><img src='../images/loading.gif' style='float:none;' alt='caricamento in corso' /></div><iframe id='framepreventivo'  onload='ffiframe(" + divattesa + ")'  style='height:230px;width:700px; border:none; margin-left:3px;' frameborder='0' scrolling='no' src='" + stringa + "?codiceperiodo=" + codiceperiodo + "&categoria=" + categoria + "&cabina= " + cabina + "&persone=" + persone + "&eta1=" + eta1 + "&eta2=" + eta2 + "&eta3=" + eta3 + "&eta4=" + eta4 + "&eta5=" + eta5 + "&nomeponte=" + nomeponte + "&volo=" + volo + "&aeroporto=" + aeroporto + "&nomeclub=" + nomeclub + "&cartaclub=" + cartaclub + "&compagnia=" + compagnia + "&frasepac=" + frasepac + "&primo="+ primo +"&tipologiacabina="+ tipocabina + "&assi="+assi+"&passaporto=" + passaporto + "'></iframe>";
    window.top.scrollTo(0, 200);
}

function ffiframe(elem) {
    document.getElementById(elem).style.visibility = "hidden";
    document.getElementById(elem).style.height = "0px";
    document.getElementById("layoutnascondi").style.visibility = "hidden";
}
