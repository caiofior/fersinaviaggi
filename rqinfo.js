
function caricarichiesta(nomediv, nomedivtoggle) {
    if (document.getElementById(nomediv).style.visibility == "visible") {
        document.getElementById(nomediv).style.visibility = "hidden";
        document.getElementById("layoutnascondi").style.visibility = "hidden";
    } else {
        document.getElementById(nomediv).style.visibility = "visible";
        document.getElementById("layoutnascondi").style.visibility = "visible";
    }
   // toggleDisabled(document.getElementById(nomedivtoggle));
}

function scaricarichiesta(nomediv, nomedivtoggle) {
    document.getElementById(nomediv).style.visibility = "hidden";
    document.getElementById("layoutnascondi").style.visibility = "hidden";
   // toggleDisabled(document.getElementById(nomedivtoggle));
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
