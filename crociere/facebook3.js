window.fbAsyncInit = function () {
    FB.init({
        appId: '640834809264548',
        status: true, // controlla lo stato di login
        cookie: true, // abilita i cookie per consentire al server di accedere alla sessione
        xfbml: true  // abilita i tag FBML
    });



 

function loginWithFacebook() {
    FB.login(function (response) {
        if (response.authResponse) {
            console.log('Welcome!  Fetching your information.... ');
//            FB.api('/me', function (response) {
//                if (user != null) {
//                    scaricarichiesta('centroprogramma', 'layout');
//                }
//                else {
//                    // L'utente non è connesso
//                }
//            });
            scaricarichiesta('centroprogramma', 'layout');
        } else {
            console.log('User cancelled login or did not fully authorize.');
        }, 
        {scope: 'email,user_likes'});    
}



function caricarichiesta(nomediv, nomedivtoggle) {
    if (document.getElementById(nomediv).style.visibility == "visible") {
        document.getElementById(nomediv).style.visibility = "hidden";
        document.getElementById("layoutnascondi").style.visibility = "hidden";       
    } else {
        document.getElementById(nomediv).style.visibility = "visible";
       document.getElementById("layoutnascondi").style.visibility = "visible";
    }
    //toggleDisabled(document.getElementById(nomedivtoggle));
}

function scaricarichiesta(nomediv, nomedivtoggle) {
    document.getElementById(nomediv).innerHTML = "";
    document.getElementById(nomediv).style.visibility = "hidden";
    document.getElementById("layoutnascondi").style.visibility = "hidden";
  //  toggleDisabled(document.getElementById(nomedivtoggle));
 //   document.getElementById("fblog").style.visibility = "hidden";
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