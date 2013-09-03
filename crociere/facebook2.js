



window.fbAsyncInit = function () {
    // init the FB JS SDK
    FB.init({
        appId: '640834809264548', // App ID from the App Dashboard
        //        channelUrl: '//' + window.location.hostname + '/test/', // Channel File for x-domain communication
        status: true, // check the login status upon init?
        cookie: true, // set sessions cookies to allow your server to access the session?
        xfbml: true  // parse XFBML tags on this page?
    });

    FB.Event.subscribe('edge.create', function (response) {
        connesso();
    }
    );

    FB.Event.subscribe('auth.authResponseChange', function (response) {
        $("#like").fadeIn();
        $("#login").hide();
        FB.api("me/likes/202899166418298", function (response) {
            if (response.data.length == 1) {
                connesso();
            } else {

            }
        });
    });


    FB.getLoginStatus(function (response) {
        if (response.status === 'connected') {
            // the user is logged in and has authenticated your
            // app, and response.authResponse supplies
            // the user's ID, a valid access token, a signed
            // request, and the time the access token 
            // and signed request each expire
            var uid = response.authResponse.userID;
            var accessToken = response.authResponse.accessToken;
            //1.l 'utente è connesso a Facebook e ha autenticato l'applicazione (collegato)
        } else if (response.status === 'not_authorized') {
            // the user is logged in to Facebook, 
            // but has not authenticated your app
            //2.L 'utente è connesso a Facebook, ma non ha autenticato l'applicazione 
        } else {
            // the user isn't logged in to Facebook.
            // 3.L 'utente non è connesso a Facebook in questo momento e quindi non sappiamo se hanno autenticato l'applicazione o meno (sconosciuto)
        }
    });

    //    FB.getLoginStatus(function (response) {
    //        if (response.status == 'connected') {
    //            $("#like").fadeIn();
    //            $("#login").hide();
    //            FB.api("me/likes/202899166418298", function (response) {
    //                if (response.data.length == 1) { //there should only be a single value inside "data"
    //                    connesso();
    //                } else {

    //                }
    //            });

    //        } else if (response.status == 'not_authorized') {
    //            $("#like").fadeIn();
    //            $("#login").hide();
    //            //            FB.api('me/likes/202899166418298', function (response) {
    //            //                if (response.data.length == 1) { //there should only be a single value inside "data"
    //            //                    connesso();
    //            //                    alert("ecco2")
    //            //                } else {
    //            //                    alert("ecco1")
    //            //                }
    //            //            });
    //            
    //            FB.api({
    //                method: 'fql.query',
    //                query: 'SELECT uid FROM page_fan WHERE uid=user_id AND page_id=202899166418298'
    //            }, function (resp) {
    //                if (resp.length) {
    //                    alert('A fan!')
    //                } else {
    //                    alert('Not a fan!');
    //                }
    //            }
    //);

    //        } else {
    //            $("#like").fadeIn();
    //            $("#login").hide();
    //        }
    //    });




}



function connesso() {
    scaricarichiesta('centroprogramma', 'layout');
    setCookie("fb", 13445, 100)
    //           $.post("", { url: "http://www.facebook.com/pages/Webtutsinfo/202899166418298" }, function (data) {
    //              location.href = data;
    //                $("#like").fadeOut();

    //           });

}

 (function(d, s, id){
     var js, fjs = d.getElementsByTagName(s)[0];
     if (d.getElementById(id)) {return;}
     js = d.createElement(s); js.id = id;
     js.src = "//connect.facebook.net/en_US/all.js";
     fjs.parentNode.insertBefore(js, fjs);
   }(document, 'script', 'facebook-jssdk'));

function caricarichiesta(nomediv, nomedivtoggle) {
    if (document.getElementById(nomediv).style.visibility == "visible") {
        document.getElementById(nomediv).style.visibility = "hidden";
        document.getElementById("layoutnascondi2").style.visibility = "hidden";       
    } else {
        document.getElementById(nomediv).style.visibility = "visible";
       document.getElementById("layoutnascondi2").style.visibility = "visible";
    }
    //toggleDisabled(document.getElementById(nomedivtoggle));
}

function scaricarichiesta(nomediv, nomedivtoggle) {
    document.getElementById(nomediv).innerHTML = "";
    document.getElementById(nomediv).style.visibility = "hidden";
    document.getElementById("layoutnascondi2").style.visibility = "hidden";
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

function setCookie(c_name, value, expiredays) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + expiredays);
    document.cookie = c_name + "=" + escape(value) +
((expiredays == null) ? "" : ";expires=" + exdate.toGMTString()) + "; path=/" + "; domain=fersinaviaggi.it";
}