


function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}


function isCharKey(evt) {

    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 65 || charCode > 90) && (charCode < 97 || charCode > 122)) {
        if (charCode == 32) {
            return true;
        } else {
            return false;
        }
    }
    return true;
}


function isCharNumberKey(evt) {

    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 44 || charCode > 90) && (charCode < 97 || charCode > 122)) {
        if (charCode == 32) {
            return true;
        } else {
            return false;
        }
    }
    return true;
}



function prendicabina(nomediv, stringa, idpreno, volo, aeroporto, nomeclub, cartaclub, compagnia, tipocabina, tipocab) {
    var divattesa = '"cabattesaassa2"';
    var urlattesa;
    if (compagnia == 0) {
        urlattesa = "url(../images/attesa-msc.jpg)";
    }
    if (compagnia == 1) {
        urlattesa = "url(../images/attesa-costa2.jpg)";
    }
    if (top.document.getElementById("framedati") != null) {
        top.document.getElementById("framedati").style.visibility = "hidden";
        top.document.getElementById("framedati").style.height = "0px";
    }
    if (top.document.getElementById("framepreventivo") != null) {
        top.document.getElementById("framepreventivo").style.visibility = "hidden";
        top.document.getElementById("framepreventivo").style.height = "0px";
    }
    if (top.document.getElementById("frameprezzi") != null) {
        top.document.getElementById("frameprezzi").style.visibility = "hidden";
        top.document.getElementById("frameprezzi").style.height = "0px";
    }
    if (top.document.getElementById("frameitinerario") != null) {
        top.document.getElementById("frameitinerario").style.visibility = "hidden";
        top.document.getElementById("frameitinerario").style.height = "0px";
    }
    if (top.document.getElementById("framenave") != null) {
        top.document.getElementById("framenave").style.visibility = "hidden";
        top.document.getElementById("framenave").style.height = "0px";
    }
    if (top.document.getElementById("datinave") != null) {
        top.document.getElementById("datinave").style.visibility = "hidden";
        top.document.getElementById("datinave").style.height = "0px";
    }
    //altezzalayout()
    //  window.location.href = stringa + "?idpreno=" + idpreno + "&volo=" + volo + "&aeroporto=" + aeroporto + "&nomeclub=" + nomeclub + "&cartaclub=" + cartaclub + "&tipologiacabina=" + tipocabina + "&tipocabina=" + tipocab;
    top.document.getElementById("layoutnascondi").style.visibility = "visible";
    top.document.getElementById("layout").style.visibility = "hidden";
    top.document.getElementById("layout").style.height = "300px";
    top.document.getElementById("bottom").style.visibility = "hidden";
    top.document.getElementById("bottom").style.height = "0px";

   //sleep_until(10);  
    top.document.getElementById(nomediv).style.visibility = "visible";
    top.document.getElementById(nomediv).innerHTML = "<div id=" + divattesa + " style='z-index:1001;background:" + urlattesa + " no-repeat; position:absolute; margin:7px; text-align:center; width:690px; height:326px;'><img src='../images/loading.gif' style='float:none;' alt='caricamento in corso' /></div><iframe id='frameassegna'  onload='ffiframe(" + divattesa + ")'  style='height:250px;width:687px; margin-left:2px;' frameborder='0' scrolling='no' src='" + stringa + "?idpreno=" + idpreno + "&volo=" + volo + "&aeroporto=" + aeroporto + "&nomeclub=" + nomeclub + "&cartaclub=" + cartaclub + "&tipologiacabina=" + tipocabina + "&tipocabina=" + tipocab + "'></iframe>";
    //setTimeout(function () { onLeftFramesLoad(loops + 1); }, 250);
    
window.top.scrollTo(0, 0);
}

function ffiframe(elem) {
    document.getElementById(elem).style.visibility = "hidden";
    document.getElementById(elem).style.height = "0px";
    top.document.getElementById("layoutnascondi").style.visibility = "hidden"; 
}

function parti() {
    document.getElementById("eccopreventivo").value = top.document.getElementById("salvapreventivo").value
}

function sleep_until(seconds) {
    var max_sec = new Date().getTime();
   while (new Date() < max_sec + seconds * 1000) { }
  return true;
}
