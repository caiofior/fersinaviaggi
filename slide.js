

$('.ppt li:gt(0)').hide();
$('.ppt li:last').addClass('last');
var cur = $('.ppt li:first');

function animate() {
    cur.fadeOut(1000);
    if (cur.attr('class') == 'last')
        cur = $('.ppt li:first');
    else
        cur = cur.next();
    cur.fadeIn(1000);
}


$(function () {
    setInterval("animate()", 5000);
});


var varCounter = 0;
var posx = 55;
var varName = function () {
    if (varCounter < 220) {
        varCounter++;
        if (document.getElementById("scontomsc") != null) {
            document.getElementById("scontomsc").style.top = varCounter + "px";
            document.getElementById("scontomsc").style.left = "50%";
            document.getElementById("scontomsc").style.marginLeft = "80px";
        }
    } else {
//        if (posx > 40) {
//            posx = posx - 1;
//            document.getElementById("scontomsc").style.top = varCounter + "px";
//            document.getElementById("scontomsc").style.left = posx + "%";
//        } else {
            clearInterval(varName);
//        }
    }
};


var varCounter2 = 0;
var varName2 = function () {
    if (varCounter2 < 220) {
        varCounter2++;
        if (document.getElementById("scontocosta") != null) {
            document.getElementById("scontocosta").style.top = varCounter2 + "px";
            document.getElementById("scontocosta").style.left = "50%";
            document.getElementById("scontocosta").style.marginLeft = "-180px";
        }
    } else {
        clearInterval(varName2);
    }
};

$(function () {
    if (document.getElementById("scontocosta") != null) {
        document.getElementById("scontocosta").style.visibility = "visible";
    }
   // if (document.getElementById("scontomsc") != null) {
   //     document.getElementById("scontomsc").style.visibility = "visible";
   // }
    setInterval("varName2()", 1);
    //setInterval("varName()", 5);
});




