function ffiframe(elem) {
    document.getElementById(elem).style.visibility = "hidden";
    document.getElementById(elem).style.height = "0px";
}
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
function applicaroulette() {
    setInterval("animateroulette()", 5000);

}

function animateroulette() {
    for (var i = 0; i <= 21; i++) {
        if (document.getElementById(roulette[i]) != null) {
            cur2 = document.getElementById(roulette[i]);

        }
    }
}

var cc = 0;
var rr = 3;
var indietro = false;
function scrollDiv(righe) {
    if (document.getElementById('ultimescroll') != null) {
        if (rr == 1) {
            indietro = false;
        }
        if (rr < righe && indietro == false) {
            timerUp2 = setTimeout("scrollDivDown(" + righe + ")", 3000);
            rr++;
        } else {
            timerUp2 = setTimeout("scrollDivUp(" + righe + ")", 3000);
            rr--;
            indietro = true;
        }
    }
}

function scrollDivUp(righe) {
    document.getElementById('ultimescroll').scrollTop -= 1;
    cc++;
    if (cc == 57) {
        clearInterval(timerUp);
        cc = 0;
        scrollDiv(righe);
    } else {
        timerUp = setTimeout("scrollDivUp(" + righe + ")", 10);
    }
}

function scrollDivDown(righe) {
    document.getElementById('ultimescroll').scrollTop += 1;
    cc++;
    if (cc == 57) {
        clearInterval(timerUp);
        cc = 0;
        scrollDiv(righe);
    } else {
        timerUp = setTimeout("scrollDivDown(" + righe + ")", 10);
    }
}