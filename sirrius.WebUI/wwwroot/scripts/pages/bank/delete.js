$(document).ready(function () {
    //$(".crm-shortcut-btn").attr('href', '/bank/index');
    //$(".crm-shortcut-btn").attr('data-original-title', 'Geri Dön').tooltip('update');
    //$(".crm-shortcut-btn").find('i').removeClass('fa-plus').addClass('fa-arrow-left');


    $(".btn-danger.rounded-plus").click(function () {
        var params = {
            "Id": $('#id').val(),  //0
            "Code": $('#code').text(),  //1
            "Name": $('#name').text(),//2
            "Description": $('#description').text() //3

        }

        apihelper.post("POST", '/bank/delete', params, (result) => {

            console.log(pageTAG.login, " login result :", result);

            if (result.done) {
                if (result.isRedirectRequired)
                    location.href = result.redirectUrl;
                else
                    console.log(pageTAG.bank, " ", result.message);
            }
            else {
                Notify(result.message, "error", 6000);
            }

        }, true, true);

    });
});