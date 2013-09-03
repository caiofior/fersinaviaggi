var tab = new Array();

function cambia(elem) {
    for (var i = 0; i <= 40; i++) {
        if (document.getElementById(tab[i]) != null) {
            if (tab[i] == elem) {
                document.getElementById(tab[i]).setAttribute("class", "selected");
            } else {
                document.getElementById(tab[i]).setAttribute("class", "");
            }
        }
    }
}

