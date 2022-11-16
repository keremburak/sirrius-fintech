var ajaxDone, ajaxMessage;

var apihelper = {
    tag: "[API_CALLER]",
    //apiBaseUrl: function () {
    //    return "http://localhost:5000";
    //},
    post: function (method, url, params, done, async, loading, formdata, showMessage) {
        loading = typeof loading !== "undefined" ? loading : false;
        if (loading) {
            //$(document).on({
            //    //ajaxStart: function () { $("body").addClass("circleloader"); },
            //    //ajaxStop: function () { $("body").removeClass("circleloader"); }
            //    ajaxStart: function () { loader.circle.show(); },
            //    ajaxStop: function () { loader.circle.hide(); }
            //});

            loader.circle.show();
        }

        var options = {};

        options.url = url;
        options.type = method;
        options.crossDomain = true;
        options.async = async;
        options.dataType = "json";

        //if (token !== undefined && token !== null) {
        //    //add token to request
        //    options.beforeSend = xhr => {   //Include the bearer token in header
        //        xhr.setRequestHeader("Authorization", 'Bearer ' + token);
        //    }

        //    //options.contentType = "multipart/formdata";
        //    //options.contentType = "application/x-www-form-urlencoded";
        //    //options['Content-Type'] = "application/x-www-form-urlencoded";
        //}
        if (formdata === undefined) {
            if (params !== null && Object.keys(params).length !== 0 && params.constructor === Object) {
                options.data = JSON.stringify(params);
                options.contentType = "application/json";
            }
        }
        else {
            options.data = params;
            options.contentType = false;
            options.processData = false;
            options.traditional = true;
        };

        ajaxDone = done;
        ajaxMessage = showMessage !== "undefined" ? showMessage : true;

        // options.headers = {
        //     "accept": "application/json",
        //     "Access-Control-Allow-Origin": "*",
        //     "Access-Control-Allow-Method": "POST, GET, OPTIONS, PUT, DELETE"
        // }

        options.success = result => {
            onSuccess(result);
        };

        options.error = (request, textStatus, errorThrown) => {  //request : XHRequest
            console.log("ApiCaller_Error - responseObject : ", request);
            console.log("ApiCaller_Error - textStatus : ", textStatus);
            console.log("ApiCaller_Error - errorThrown : ", errorThrown);

            onFailure({ "request": request, "textStatus": textStatus, "errorThrown": errorThrown });
        }
        //send post action
        $.ajax(options);
    },
    //checkToken: function (caller) {
    //    //if token is expired
    //    if (Cookies.get("crmtoken") === null) {
    //        if (Cookies.get("crmreftoken") !== null) {
    //            let payload = decodeURIComponent(escape(atob(Cookies.get("crmreftoken"))));

    //            apihelper.post("POST", apihelper.apiBaseUrl() + '/api/auth/new-token', { refreshToken: payload }, (result) => {
    //                Cookies.set("crmtoken", btoa(unescape(encodeURIComponent(result.Data))), { expires: 1 / 48 });

    //                if (caller && caller instanceof Function)
    //                    caller();
    //            });
    //        }
    //        else location.href = location.origin + '/login';
    //    }
    //    else {
    //        if (caller && caller instanceof Function)
    //            caller();
    //    }
    //},
    //getToken: function () {
    //    //let token = JSON.parse(atob(Cookies.get("crmtoken")));
    //    let token = decodeURIComponent(escape(atob(Cookies.get("crmtoken"))));

    //    console.log("access token :", token);

    //    return token;
    //}
};

function onSuccess(result) {
    //if (ajaxMessage)
    //    showResult(r);

    //var result = r;
    //if (r.d) {
    //    result = r.d;
    //}
    loader.circle.hide();

    if (ajaxDone && typeof ajaxDone == "function") {
        ajaxDone(result);
    }
}

function onFailure(result) {
    loader.circle.hide();

    console.log("error : ", result);
    //if (r.statusText == "Forbidden") {
    //    showResult({ Done: false, Message: "Bu işlem için yetkiniz bulunmamaktadır..." });
    //}
    //else if (ajaxMessage) {
    //    showResult(r);
    //}
}
