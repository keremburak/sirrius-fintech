$(document).ready(function () {
    //$('[data-toggle="password"]').each(function () {
    //    var input = $(this);
    //    var eye_btn = $(this).parent().find('.input-group-text');
    //    eye_btn.css('cursor', 'pointer').addClass('input-password-hide');
    //    eye_btn.on('click', function () {
    //        if (eye_btn.hasClass('input-password-hide')) {
    //            eye_btn.removeClass('input-password-hide').addClass('input-password-show');
    //            eye_btn.find('.fa').removeClass('fa-eye').addClass('fa-eye-slash')
    //            input.attr('type', 'text');
    //        } else {
    //            eye_btn.removeClass('input-password-show').addClass('input-password-hide');
    //            eye_btn.find('.fa').removeClass('fa-eye-slash').addClass('fa-eye')
    //            input.attr('type', 'password');
    //        }
    //    });
    //});



    $('#login-form').validate({
        rules: {
            username: {
                required: true,
                //email: true
            },
            password: {
                required: true,
                //minlength: 5
            }
        },
        messages: {
            //firstname: "Enter your firstname",
            //lastname: "Enter your lastname",
            //username: {
            //    required: "Enter a username",
            //    minlength: jQuery.format("Enter at least {0} characters"),
            //    remote: jQuery.format("{0} is already in use")
            //}

            username: {
                required: "Kullanici adi veya e-posta girin.",
                //email: "E-posta hatali"
            },
            password: {
                required: "Sifre girin"
            }
        },
        errorElement: 'span',
        errorPlacement: function (error, element) {
            error.addClass('invalid-feedback');
            element.closest('.input-group').append(error);
        },
        highlight: function (element, errorClass, validClass) {
            $(element).addClass('is-invalid');
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass('is-invalid');
        },
        submitHandler: function (form) {

            var params = {
                "username": $("#username").val(),
                "password": $("#password").val(),
            };

            apihelper.post("POST", '/login/index', params, (result) => {

                console.log(pageTAG.login, " login result :", result);


                if (result.done) {
                    if (["superadmin", "admin", "user"].includes(result.data.roleName.toLowerCase()))
                        location.href = '/home/index';
                    else
                        Notify("Giris icin gerekli yetkiye sahip degilsiniz", "error");
                }
                else {
                    Notify(result.message, "error");
                }

            }, true, true);
        }
    });

});

function password_show_hide() {
    var intPassword = document.getElementById("password");
    var show_lock = document.getElementById("show_lock");
    var hide_lock = document.getElementById("hide_lock");
    hide_lock.classList.remove("d-none");
    if (intPassword.type === "password") {
        intPassword.type = "text";
        show_lock.style.display = "none";
        hide_lock.style.display = "block";
    } else {
        intPassword.type = "password";
        show_lock.style.display = "block";
        hide_lock.style.display = "none";
    }
}