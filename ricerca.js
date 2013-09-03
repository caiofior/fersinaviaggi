var compagnia = new Object();
var nave = new Object();
var porto = new Object();
var portocompagnia = new Object();
var naveporto = new Object();
var compagniaporto = new Object();
var navecompagnia = new Object();

var indicezona = "'-1'";
var indicemese = "'-1'";
var indicecompagnia = "'-1'";
var indicenave = "'-1'";
var indiceporto = "'-1'";


function setInit(chooser, elemento, oggetto) {
    var newElem;
    var where = (navigator.appName == "Microsoft Internet Explorer") ? -1 : null;
    var cityChooser = document.getElementById(elemento);
    while (cityChooser.options.length) {
        cityChooser.remove(0);
    }
    var choice = chooser;
    var db = oggetto[choice];
    newElem = document.createElement("option");
    newElem.text = "- Selezionare -";
    if (elemento == 'DropNave') {
        newElem.text = "- Tutte le navi -";
    }
    if (elemento == 'DropCompagnia') {
        newElem.text = "- Tutte le compagnie -";
    }
    if (elemento == 'DropPorto') {
        newElem.text = "- Tutti i porti -";
    }
    newElem.value = "-1";
    cityChooser.add(newElem, where);
    if (choice != "") {
        for (var i = 0; i < db.length; i++) {
            newElem = document.createElement("option");
            newElem.text = db[i].text;
            newElem.value = db[i].value;
            cityChooser.add(newElem, where);
        }
    }
    cityChooser.value = chooser;    
}


function cambia(chooser, elemento, oggetto, indice) {
    var newElem;
    var where = (navigator.appName == "Microsoft Internet Explorer") ? -1 : null;
    var cityChooser = chooser.form.elements[elemento];
    while (cityChooser.options.length) {
        cityChooser.remove(0);
    }
    var choice = chooser.options[chooser.selectedIndex].value;
    if (indice ==  'indicecompagnia') {
        indicecompagnia = "'" + chooser.options[chooser.selectedIndex].value +"'";
    }
    if (indice == 'indicenave') {
        indicenave = "'" + chooser.options[chooser.selectedIndex].value + "'";
    }
    if (indice == 'indiceporto') {
        indiceporto = "'" + chooser.options[chooser.selectedIndex].value + "'";
    }
    var db = oggetto[choice];
    newElem = document.createElement("option");
    newElem.text = "- Selezionare -";
    if (elemento == 'DropNave') {
        newElem.text = "- Tutte le navi -";
    }
    if (elemento == 'DropCompagnia') {
        newElem.text = "- Tutte le compagnie -";
    }
    if (elemento == 'DropPorto') {
        newElem.text = "- Tutti i porti -";
    }
    newElem.value = "-1";
    cityChooser.add(newElem, where);
    if (choice != "") {
        for (var i = 0; i < db.length; i++) {
            newElem = document.createElement("option");
            newElem.text = db[i].text;
            newElem.value = db[i].value;
            cityChooser.add(newElem, where);
        }
    }
 mettiindice();
}

function mettiindice() {
    var ico = document.getElementById("DropCompagnia");
    var ina = document.getElementById("DropNave");
    var ipo = document.getElementById("DropPorto");
    var izo = document.getElementById("DropZona");
    var ime = document.getElementById("DropMese");
    if (indicezona != '-1') {
        for (var h = 0; h < izo.options.length; h++) {
            if ("'" + izo.options[h].value + "'" == indicezona) {
                izo.selectedIndex = h;
                break;
            }
        }
    }
    if (indicemese != '-1') {
        for (var w = 0; w < ime.options.length; w++) {
            if ("'" + ime.options[w].value + "'" == indicemese) {
                ime.selectedIndex = w;
                break;
            }
        }
    }
    if (indicecompagnia != '-1') {
        for (var k = 0; k < ico.options.length; k++) {
            if ("'"+ico.options[k].value+"'" == indicecompagnia) {
                ico.selectedIndex = k;                
                break;
            }
        }
    }
    if (indicenave != '-1') {        
        for (var x= 0; x < ina.options.length; x++) {
            if ("'" + ina.options[x].value + "'" == indicenave) {
                ina.selectedIndex = x;                
                break;
            }
        }
    }
    if (indiceporto != '-1') {
        for (var y = 0; y < ipo.options.length; y++) {
            if ("'" + ipo.options[y].value + "'" == indiceporto) {
                ipo.selectedIndex = y;                 
                break;
            }
        }
    }
}

function cambiaindice(chooser, indice) {
    var newElem;
    var where = (navigator.appName == "Microsoft Internet Explorer") ? -1 : null;
    if (indice == 'indicezona') {
        indicezona = "'" + chooser.options[chooser.selectedIndex].value + "'";
    }
    if (indice == 'indicemese') {
        indicemese = "'" + chooser.options[chooser.selectedIndex].value + "'";
    }
    mettiindice();
}


function cerca() {
    var izo = document.getElementById("DropZona");
    var ime = document.getElementById("DropMese");
    var ico = document.getElementById("DropCompagnia");
    var ina = document.getElementById("DropNave");
    var ipo = document.getElementById("DropPorto");
    var spezzata = "spezzata=0";
    var querysql = "";
    if (izo.selectedIndex > 0){
        querysql = querysql + "zona=" + izo.options[izo.selectedIndex].value + "&";
    }
    if (ime.selectedIndex > 0) {
        querysql = querysql + "mese=" + ime.options[ime.selectedIndex].value + "&";
    }
    if (ico.selectedIndex > 0) {
        querysql = querysql + "compagnia=" + ico.options[ico.selectedIndex].value + "&";
    }
    if (ina.selectedIndex > 0) {
        querysql = querysql + "nave=" + ina.options[ina.selectedIndex].value + "&";
    }
    if (ipo.selectedIndex > 0) {
        querysql = querysql + "porto=" + ipo.options[ipo.selectedIndex].value + "&";
    }
    top.location.href = "crociere/crociere.aspx?" + spezzata + "&" + querysql;
}

function annulla() {
    var spezzata = "spezzata=0";
    top.location.href = "crociere/crociere.aspx?" + spezzata;
}