function altezzalayout() {
    var misprezzi = 0
    if (top.document.getElementById("frameprezzi") != null) {
        misprezzi = top.document.getElementById("frameprezzi").offsetHeight
    }
    var miscabine = 0;
    if (top.document.getElementById("framecabinecosta") != null) {
        miscabine = top.document.getElementById("framecabinecosta").offsetHeight
    }
    var misquota = 0;
    if (top.document.getElementById("divquota") != null) {
        misquota = top.document.getElementById("divquota").offsetHeight
    }
    var misdett = 0;
    if (top.document.getElementById("dettagliodiv") != null) {
        misdett = top.document.getElementById("dettagliodiv").offsetHeight
    }
    var misdatipax = 0;
    if (top.document.getElementById("datipax") != null) {
        misdatipax = top.document.getElementById("datipax").offsetHeight
    }
    var misdentro = 0;
    if (top.document.getElementById("dentrodati") != null) {
        misdentro = top.document.getElementById("dentrodati").offsetHeight
    }
    var mispreve = 0;
    if (top.document.getElementById("divpreventivo") != null) {
        mispreve = top.document.getElementById("divpreventivo").offsetHeight
    }
    var mispreventivo = 0;
    if (top.document.getElementById("framepreventivo")!=null){
        mispreventivo = top.document.getElementById("framepreventivo").offsetHeight
    }
    var misdettaglio = 0;
    if (top.document.getElementById("framedettaglio") != null) {
        misdettaglio = top.document.getElementById("framedettaglio").offsetHeight
    }
    var mismail = 0;
    if (top.document.getElementById("framemail") != null) {
        mismail = top.document.getElementById("framemail").offsetHeight
    }
    var misdati = 0;
    if (top.document.getElementById("framedati")!= null){
      misdati = top.document.getElementById("framedati").offsetHeight
  }
  var misccc = 0;
  if (top.document.getElementById("frameccc") != null) {
      misccc = top.document.getElementById("frameccc").offsetHeight
  }
  var misass = 0;
  if (top.document.getElementById("frameassegna") != null) {
      misass = top.document.getElementById("frameassegna").offsetHeight
  }
  var misitinerario = 0;
  if (top.document.getElementById("frameitinerario") != null) {
      misitinerario = top.document.getElementById("frameitinerario").offsetHeight
  }
  var misdatinave = 0;
  if (top.document.getElementById("framenave") != null) {
      misdatinave = top.document.getElementById("framenave").offsetHeight //lasciare il frame e non il div se no non funziona
  }
    var mislayout = top.document.getElementById("layout").offsetHeight

    var mis2 = parseInt(misprezzi) + parseInt(miscabine) + parseInt(misdettaglio) + parseInt(misdati) + parseInt(misitinerario) + parseInt(misdatinave) + parseInt(mispreventivo) + parseInt(misccc) + parseInt(mispreve) + parseInt(misdentro) + parseInt(misquota) + parseInt(misdatipax) + parseInt(misdett) + parseInt(misass) + parseInt(mismail) + parseInt(500);
   // alert(mis2);
   // alert( misprezzi + " " + miscabine + " " + misdettaglio + " " + misdati + " " + misitinerario + " " + misdatinave);
    top.document.getElementById("layout").style.height = mis2 + "px";
    top.document.getElementById("bottom").style.visibility = "visible";
}

function altezza2(px, divframe) {
    var mis = parseInt(px);
    if (mis < 760) { mis = 760; };
    top.document.getElementById(divframe).style.height = mis + "px";
    altezzalayout();
}

function altezza(px, divframe) {
    var mis = parseInt(px);
    top.document.getElementById(divframe).style.height = mis + "px";
    altezzalayout();
}