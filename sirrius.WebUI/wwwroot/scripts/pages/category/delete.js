$(document).ready(function () {
    $(".crm-shortcut-btn").attr('href', '/category/index');
    $(".crm-shortcut-btn").attr('data-original-title', 'Geri Dön').tooltip('update');
    $(".crm-shortcut-btn").find('i').removeClass('fa-plus').addClass('fa-arrow-left');


    $(".btn-danger.rounded-plus").click(function () {
        var params = {
            "Id": $('#id').val(),  //0
            "Name": $('#name').text(),//2
        }

        apihelper.post("POST", '/category/delete', params, (result) => {

            console.log(pageTAG.category, " login result :", result);

            if (result.done) {
                if (result.isRedirectRequired)
                    location.href = result.redirectUrl;
                else
                    console.log(pageTAG.category, " ", result.message);
            }
            else {
                Notify(result.message, "error", 6000);
            }

        }, null, true);

    });
});