

  // Additional JS functions here
window.fbAsyncInit = function () {

    FB.init({
        appId: '362694293830161', // App ID
        channelUrl: '//www.fersinaviaggi.it/channel.html', // Channel File
        status: true, // check login status
        cookie: true, // enable cookies to allow the server to access the session
        xfbml: true,  // parse XFBML
        oauth: true

    });

    // Additional init code here
    FB.Event.subscribe('auth.authResponseChange', function (response) {
        // Here we specify what we do with the response anytime this event occurs. 
        if (response.status === 'connected') {
            // The response object is returned with a status field that lets the app know the current
            // login status of the person. In this case, we're handling the situation where they 
            // have logged in to the app.
            //connesso();
            var uid = response.authResponse.userID;
            var accessToken = response.authResponse.accessToken;
            FB.api({
                method: 'fql.query',
                query: 'SELECT uid FROM page_fan WHERE uid=' + uid + ' AND page_id=202899166418298'
            }, function (resp) {
                if (resp.length) {
                    connesso();
                } else {
                    nonregistrato();
                }
            }
                        );
        } else if (response.status === 'not_authorized') {
            // In this case, the person is logged into Facebook, but not into the app, so we call
            // FB.login() to prompt them to do so. 
            // In real-life usage, you wouldn't want to immediately prompt someone to login 
            // like this, for two reasons:
            // (1) JavaScript created popup windows are blocked by most browsers unless they 
            // result from direct interaction from people using the app (such as a mouse click)
            // (2) it is a bad experience to be continually prompted to login upon page load.
            FB.login();
        } else {
            // In this case, the person is not logged into Facebook, so we call the login() 
            // function to prompt them to do so. Note that at this stage there is no indication
            // of whether they are logged into the app. If they aren't then they'll see the Login
            // dialog right after they log in to Facebook. 
            // The same caveats as above apply to the FB.login() call here.
            FB.login();
        }
    });
    FB.getLoginStatus(function (response) {
        //$("#likediv").fadeOut();
    });

    FB.api("me/likes/202899166418298", function (response) {
        if (response.data.length == 1) {
            connesso();
        }
    });

};

  // Load the SDK asynchronously
  (function(d){
     var js, id = 'facebook-jssdk', ref = d.getElementsByTagName('script')[0];
     if (d.getElementById(id)) {return;}
     js = d.createElement('script'); js.id = id; js.async = true;
     js.src = "//connect.facebook.net/en_US/all.js";
     ref.parentNode.insertBefore(js, ref);
   }(document));


 function testAPI() {
     connesso();
     console.log('Benvenuto!  Recupero le tue informazioni.... ');
     FB.api('/me', function (response) {
         connesso();
     });
 }

function connesso() {
    scaricarichiesta('centroprogramma', 'layout');
    setCookie("fb", 13445, 100)
    //           $.post("", { url: "http://www.facebook.com/pages/Webtutsinfo/202899166418298" }, function (data) {
    //              location.href = data;
    //                $("#like").fadeOut();

    //           });

}



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

function nonregistrato() {
    //var nomediv = "centroprogramma";
    //var nomediv2 = "layout";
    var ecco = 'scaricarichiesta("centroprogramma","layout")';
    document.getElementById("centroprogramma").innerHTML = "<div style='text-align:center; width:550px;'><img src='../images/logofacebook.gif' alt='logo facebook' /><div style='color:#136697;margin-top:10px; margin-bottom:10px; text-align:center;'>SE VUOI RIMANERE AGGIORNATO<br/>SULLE NOSTRE OFFERTE<br/>CLICCA SU MI PIACE<br/></div><iframe src='http://www.facebook.com/plugins/likebox.php?id=202899166418298&amp;width=235&amp;connections=4&amp;stream=false&amp;header=false&amp;height=157' scrolling='no' frameborder='0' style='border:none; overflow:hidden; width:265px; height:157px; left:-5px; overflow:hidden; position:relative; top:-1px;' allowTransparency='true' ></iframe><br/><br/><input type='button' value='Prosegui' onclick='"+ ecco + "'>";
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


