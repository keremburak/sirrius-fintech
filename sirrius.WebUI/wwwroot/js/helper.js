function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}

function showResult(data) {
    if (data.Done || data.done) {
        if (data.Message != null)
            Notify(data.Message, "success");
        else
            Notify("İşlem başarılıdır.", "success");

        if (data.RedirectUrl != null)
            window.location = data.RedirectUrl;
        if (data.redirectUrl != null)
            window.location = data.redirectUrl;
    }
    else {
        if (data.Message != null)
            Notify(data.Message, "danger");
        else
            Notify("Hata meydana geldi. İşleminiz tamamlanamamıştır!", "danger");
    }
}

var handleNumericInput = function () {
    if ($().autoNumeric) {
        $('.numericText').autoNumeric('init', { aDec: ',', aSep: '', mDec: 0 });
        $('.numericFloatText').autoNumeric('init', { aDec: ',', aSep: '', mDec: 3 });
    }
};

var initSelect2 = function () {
    $("select.select2").select2({
        placeholder: "Seçiniz",
        allowClear: true
    });
};

toastr.options = {
    "closeButton": false,
    "debug": false,
    "newestOnTop": true,
    "progressBar": true,
    "positionClass": "toast-top-right",
    "preventDuplicates": true,
    "onclick": null,
    "showDuration": 300,
    "hideDuration": 100,
    "timeOut": 4000,
    "extendedTimeOut": 1000,
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}

function Notify(message, type, timeout) {
    switch (type) {
        case "success":
            toastr["success"](message);
            break;
        case "warning":
            toastr["warning"](message);
            break;
        case "info":
            toastr["info"](message);
            break;
        case "error":
            toastr["error"](message, undefined, { iconClass: "toast-error-bgcolor", timeOut: timeout ? timeout : toastr.options.timeOut });
            break;
        case "danger":
            toastr["error"](message, undefined, { iconClass: "toast-error-bgcolor", timeOut: timeout ? timeout : toastr.options.timeOut });
            break;
        //case "success":
        //    toastr.success(message, notifierOptions.success);
        //    break;
        //case "warning":
        //    toastr.warning(message, notifierOptions.warning);
        //    break;
        //case "info":
        //    toastr.info(message,undefined, notifierOptions.info);
        //    break;
        //case "error":
        //    toastr.error(message, undefined, { iconClass: "toast-error-bgcolor", timeOut: 2500 });
        //    break;
        //case "danger":
        //    toastr.error(message, undefined, { iconClass: "toast-error-bgcolor", timeOut: 2500 });
        //    break;
    }

}

var notifierOptions = {
    success: {
        "closeButton": false,
        "debug": false,
        "newestOnTop": true,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "preventDuplicates": true,
        "onclick": null,
        "showDuration": 300,
        "hideDuration": 100,
        "timeOut": 4000,
        "extendedTimeOut": 1000,
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    },
    error: {
        "closeButton": false,
        "debug": false,
        "newestOnTop": true,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "preventDuplicates": true,
        "onclick": null,
        "showDuration": 300,
        "hideDuration": 100,
        "timeOut": 4000,
        "extendedTimeOut": 1000,
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut",
        "iconClass": "toast-error-bgcolor"
    },
    warning: {
        "closeButton": false,
        "debug": false,
        "newestOnTop": true,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "preventDuplicates": true,
        "onclick": null,
        "showDuration": 300,
        "hideDuration": 100,
        "timeOut": 4000,
        "extendedTimeOut": 1000,
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    },
    info: {
        "closeButton": false,
        "debug": false,
        "newestOnTop": true,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "preventDuplicates": true,
        "onclick": null,
        "showDuration": 300,
        "hideDuration": 100,
        "timeOut": 4000,
        "extendedTimeOut": 1000,
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }
}

var pageTAG = {
    login: "[LOGIN]",
    home: "[HOME]",
    bank: "[BANK]",
    bankoperation: "[BANK_OPERATION]",
    ftpconnection: "[FTP_CONNECTION]",
    country: "[COUNTRY]",
    city: "[CITY]",
    customer: "[CUSTOMER]",
    category: "[CATEGORY]",
    company: "[COMPANY]",
    user: "[USER]",
    companybankaccount: "[COMPANY_BANK_ACCOUNT]"
}

var loader = {
    circle: $('.circleloader')
}


/* chartjs functions */
function getLegendPosition() {
    return ($(window).width() < 1024)
        ? 'bottom'
        : 'right';
}

//date formatting for turkish
Date.prototype.toTurkishFormatDate = function (format) {
    var date = this,
        day = date.getDate(),
        weekDay = date.getDay(),
        month = date.getMonth() + 1,
        year = date.getFullYear(),
        hours = date.getHours(),
        minutes = date.getMinutes(),
        seconds = date.getSeconds();

    var monthNames = new Array("Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık");
    var dayNames = new Array("Pazar", "Pazartesi", "Salı", "Çarşamba", "Perşembe", "Cuma", "Cumartesi");

    if (!format) {
        format = "dd.MM.yyyy";
    }


    format = format.replace("mm", month.toString().padStart(2, "0"));

    format = format.replace("MM", monthNames[month]);

    if (format.indexOf("yyyy") > -1) {
        format = format.replace("yyyy", year.toString());
    } else if (format.indexOf("yy") > -1) {
        format = format.replace("yy", year.toString().substr(2, 2));
    }

    format = format.replace("dd", day.toString().padStart(2, "0"));

    format = format.replace("DD", dayNames[weekDay]);

    if (format.indexOf("HH") > -1) {
        format = format.replace("HH", hours.toString().replace(/^(\d)$/, '0$1'));
    }

    if (format.indexOf("hh") > -1) {
        if (hours > 12) {
            hours -= 12;
        }

        if (hours === 0) {
            hours = 12;
        }
        format = format.replace("hh", hours.toString().replace(/^(\d)$/, '0$1'));
    }

    if (format.indexOf("ii") > -1) {
        format = format.replace("ii", minutes.toString().replace(/^(\d)$/, '0$1'));
    }

    if (format.indexOf("ss") > -1) {
        format = format.replace("ss", seconds.toString().replace(/^(\d)$/, '0$1'));
    }

    return format;
};