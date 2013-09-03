
var varCounter3 = 0;
var varName3 = function () {
    if (varCounter3 < 199) {
        varCounter3++;
        for (var i = 0; i <= 21; i++) {
            if (document.getElementById(offertachiama[i]) != null) {
                document.getElementById(offertachiama[i]).style.width = varCounter3 + "px";
            }
        }
        //alert("pippo");
    } else {
        clearInterval(varName3);
    }
};

function isiPhone() {
    return (
    //Detect iPhone
        (navigator.platform.indexOf("iPhone") != -1) ||
    //Detect iPod
        (navigator.platform.indexOf("iPad") != -1)
    );
}

$(function () {
    //setTimeout(setInterval(varName3, 1), 300000);
    if (isiPhone() == false) {
        setTimeout(function () { setInterval("varName3()", 1) }, 1000);
    } else {
        for (var i = 0; i <= 21; i++) {
            if (document.getElementById(offertachiama[i]) != null) {
                document.getElementById(offertachiama[i]).style.width = "199px";
            }
        }
    }
});

var varCounter5 = 0;
var varcounter6 = 100;
var varName5 = function () {

    if (varCounter5 < 100) {
        varCounter5++;
        varcounter6--;
            if (document.getElementById("inte") != null) {
                document.getElementById("inte").style.visibility = "visible";
            }
            if (document.getElementById("inte") != null) {
                document.getElementById("inte").style.width = varCounter5 + "%";
            }
            if (document.getElementById("interessare") != null) {
                document.getElementById("interessare").style.right = varcounter6 + "%";
            }
    } else {
        clearInterval(varName5);
    }
};

$(function () {
    setTimeout(function () { setInterval("varName5()", 1) }, 5000);
});


