var table;
var currentCodes = [], accountingCodes = [];

var Bank = {
    init: function () {
        this.list();
    },
    list: function () {
        //set order by columns for datatables grid
        grid.order = ["Id", "Code", "Name"];

        let columnDefs = [
            { targets: [0], data: "id", className: "hidden" },
            { targets: [1], data: "code", title: "<span style='font-weight: normal'>Banka Kodu</span>" },
            { targets: [2], data: "name", title: "<span style='font-weight: normal'>Banka Adı</span>" },
            {
                "targets": [3],
                "data": null,
                "orderable": false,
                //"defaultContent": "<button class='btn btn-sm btn-info'>Detay</button>"
                "render": function (data, type, row) {
                    console.log("data : ", data);
                    console.log("row : ", row);
                    //return '<a class="btn btn-sm btn-success" data-id="' + data.id + '">Detay</a>';
                    return '<a class="btn btn-sm btn-success btn-padding" href="/bank/details/' + data.id + '">Detay Git</a>';
                }
            },
            {
                "targets": [4],
                "data": null,
                "orderable": false,
                //"defaultContent": "<button class='btn btn-sm btn-info'>Detay</button>"
                "render": function (data, type, row) {
                    console.log("data : ", data);
                    console.log("row : ", row);
                    /*return '<a class="btn btn-sm btn-warning" data-id="' + data.id + '">Edit</a>';*/
                    return '<a class="btn btn-sm btn-warning btn-padding" href="/bank/edit/' + data.id + '">Güncelle</a>';
                }
            },
            {
                "targets": [5],
                "data": null,
                "orderable": false,
                //"defaultContent": "<button class='btn btn-sm btn-info'>Detay</button>"
                "render": function (data, type, row) {
                    console.log("data : ", data);
                    console.log("row : ", row);

                    //return '<a class="btn btn-sm btn-danger" data-id="' + data.id + '">Sil</a>';
                    return '<a class="btn btn-sm btn-danger btn-padding" href="/bank/delete/' + data.id + '">Sil</a>';
                }
            },
        ];

        //generate grid
        table = grid.generate("banks", columnDefs,
            //apiCaller.apiSharedEntityBaseUrl() + '/v1/sharedEntity/city?navProps=Country&Include=Id,Name,Country,Country.Name', "GET",
            //apiCaller.apiSharedEntityBaseUrl() + '/v1/sharedEntity/city?include=Id,Name', "GET",
            "/bank/list");

        /****************************************************** */

        //console.log("token : ", apihelper.getToken());

        //apihelper.post("GET", "/bank/list", null, (result) => {
        //    console.log(result.data);
        //}, true, true);

        //apihelper.checkToken(() => {
        //    apihelper.post("GET", apihelper.apiBaseUrl() + '/api/bank/list', null, (data) => {
        //        console.log(data);
        //    }, apihelper.getToken(), true)
        //});
    }
}


$(document).ready(function () {

    Bank.init();

    $('#fl_current_codes,#fl_accounting_codes').select2({
        placeholder: "Secin",
        language: "tr",
        allowClear: true,
        //dropdownParent: $(".example-modal-default-transparent")
    });

    getCodes();

    var $modal = $(".example-modal-default-transparent");

    $modal.on("shown.bs.modal", function () {
        console.log("popup loaded");

        loader.circle.show();

        loadData(currentCodes, "#fl_current_codes");
        loadData(accountingCodes, "#fl_accounting_codes");

        loader.circle.hide();

        //    setTimeout(() => {
        //        loader.circle.hide();
        //    }, 1000);
    });

    //$('#dt-basic-example').dataTable({
    //    responsive: true
    //});
});

var getCodes = function () {
    $.getJSON("/bank/getbypasscodes", function (result) {
        if (result.done === true) {
            //currentCodes = result.currentCodes;
            //accountingCodes = result.accountingCodes;

            currentCodes = result.data.items;
            accountingCodes = result.data.items;

        }
    });
};

var loadData = function (data, filter) {
    console.log(data);
    var list = [];

    data.forEach(item => {
        list.push({
            id: item.id,
            text: item.name,
            /*selected: (item.code === "TR" ? "selected" : "")*/
            //selected: (item.code === "0001" ? "selected" : "")
        });
    });

    list.unshift({ id: "0", text: "Seçin" });

    $(filter).select2('destroy').empty().select2(
        {
            placeholder: "Seçin",
            data: list,
            language: "tr",
            dropdownParent: $(".example-modal-default-transparent")
        }
    );
}