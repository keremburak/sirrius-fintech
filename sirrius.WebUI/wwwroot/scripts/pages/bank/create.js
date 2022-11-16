$(document).ready(function () {
    //$(".crm-shortcut-btn").attr('href', '/bank/index');
    //$(".crm-shortcut-btn").attr('data-original-title', 'Geri Dön').tooltip('update');
    //$(".crm-shortcut-btn").find('i').removeClass('fa-plus').addClass('fa-arrow-left');

    $('#countries').select2({
        theme: 'bootstrap4',
        placeholder: "Ülke seçin",
        allowClear: true,
        language: "tr"
    });

    $("#create-bank-form").submit(function (event) {

        // make selected form variable
        var vForm = $(this);

        /*
        If not valid prevent form submit
        https://developer.mozilla.org/en-US/docs/Web/API/HTMLSelectElement/checkValidity
        */
        if (vForm[0].checkValidity() === false) {
            event.preventDefault();
            event.stopPropagation();
        } else {

            event.preventDefault(); // avoid to execute the actual submit of the form.

            var params = new FormData();

            params.append("Code", $('#code').val());  //0
            params.append("Name", $('#name').val()); //1
            params.append("Desc", $('#description').val()); //2
            params.append("CountryId", $('#countries option:selected').val());  //0

            apihelper.post("POST", '/bank/create', params, (result) => {

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

            }, true, true, true);
        }

        // Add bootstrap 4 was-validated classes to trigger validation messages
        vForm.addClass('was-validated');
    });
});