$(document).ready(function () {
    $(".crm-shortcut-btn").attr('href', '/category/index');
    $(".crm-shortcut-btn").attr('data-original-title', 'Geri Dön').tooltip('update');
    $(".crm-shortcut-btn").find('i').removeClass('fa-plus').addClass('fa-arrow-left');

    $("#edit-category-form").submit(function (event) {

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

            params.append("Id", $('#id').val());  //0
            //params.append("Code", $('#code').val());  //1
            params.append("Name", $('#name').val()); //2  


            apihelper.post("POST", '/category/edit', params, (result) => {

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

            }, null, true, true);
        }

        // Add bootstrap 4 was-validated classes to trigger validation messages
        vForm.addClass('was-validated');
    });
});