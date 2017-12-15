function loadNumber() {
    function s4() {
        return Math.floor((1 + Math.random()) * 0x10000).toString(16).substring(1);
    }
    return s4() + s4() + s4() + s4() + s4() + s4() + s4() + s4();
}

function adBuildParameters(params){
    var res = "";
    for(var key in params){
        if(!params.hasOwnProperty(key)) continue;
        res += (key + "=")+ encodeURIComponent(params[key])+"&";
    }
    if (res.length>0){
        return res.substr(0,res.length-1);
    }
    return res;
}

function adGetRequest(url,handler){
    var xmlHttp;
    if (window.XMLHttpRequest) {
        xmlHttp = new XMLHttpRequest();
    } else {
        xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
    }
    xmlHttp.onreadystatechange = function() {
        if (xmlHttp.readyState == 4) {
            if (xmlHttp.status==200){
                handler(null,xmlHttp);
            }else if(xmlHttp.status != 0){//got response
                handler("Bad status",xmlHttp);
            }
        }
    }
    xmlHttp.timeout = 10000;
    xmlHttp.ontimeout = function(){
        handler("Timed out",xmlHttp);
    }
    xmlHttp.open("GET", url, true);
    xmlHttp.send();
}

function addImgRequest(url,handler){
    var img = new Image();
    img.onerror = function(){
        handler("img:error")
    }
    img.onabort = function(){
        handler("img:abort")
    }
    img.onload = function(){
        handler(null);
    }
    img.src = url;
}

function addScriptRequest(url,handler){
    var script = document.createElement("script");
    script.type = "text/javascript";
    script.onerror = function(){
        handler("img:error")
    }
    script.onabort = function(){
        handler("img:abort")
    }
    script.onload = function(){
        handler(null);
    }
    script.src = url;
    script.async = true;
    document.head.appendChild(script);
}

function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for(var i=0; i<ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0)==' ') c = c.substring(1);
        if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
    }
    return "";
}
var adSendCount = 1;
var mTimeout;
var mPid = loadNumber();
var mEvent = "v";
var mDelay = 5000;
function sendPVTrack(){
    var href = window.location.href;
    var _utk = "-1";
    if (typeof (Storage) !== "undefined"){
        _utk = localStorage.getItem("_utk");
        if (_utk == undefined || _utk == null){
            _utk = "-1";
        }
    }
    addScriptRequest("//lg1.logging.admicro.vn/pvck?"+adBuildParameters({"sc":adSendCount,"clc":href,"pid":mPid,"i":mEvent,"utk":_utk,"_rnd":Math.random()}),function(err){
        if (err!=null){
            clearTimeout(mTimeout);
            adSendCount++;
            mTimeout = setTimeout(sendPVTrack,mDelay);

        }else{

            if(mEvent=="v"){//switch to tos mode.
                mEvent = "p";
                mDelay = 30000;
            }
            adSendCount = 1;
            mTimeout = setTimeout(sendPVTrack,mDelay);
        }
    })
}
//sendPVTrack();