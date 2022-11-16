var table;

var Category = {
    init: function () {
        this.list();
    },
    list: function () {
        //set order by columns for datatables grid
        grid.order = ["Id", "Name"];

        let columnDefs = [
            { targets: [0], data: "id", className: "hidden" },
            // { targets: [1], data: "countryid", title: "<span style='font-weight: normal'>Ülke Adı</span>" },
            { targets: [1], data: "name", title: "<span style='font-weight: normal'>Kategori Adı</span>" },
            {
                "targets": [2],
                "data": null,
                "orderable": false,
                //"defaultContent": "<button class='btn btn-sm btn-info'>Detay</button>"
                "render": function (data, type, row) {
                    console.log("data : ", data);
                    console.log("row : ", row);
                    //return '<a class="btn btn-sm btn-success" data-id="' + data.id + '">Detay</a>';
                    return '<a class="btn btn-sm btn-success btn-padding" href="/category/details/' + data.id + '">Detaya Git</a>';
                }
            },
            {
                "targets": [3],
                "data": null,
                "orderable": false,
                //"defaultContent": "<button class='btn btn-sm btn-info'>Detay</button>"
                "render": function (data, type, row) {
                    console.log("data : ", data);
                    console.log("row : ", row);
                    /*return '<a class="btn btn-sm btn-warning" data-id="' + data.id + '">Edit</a>';*/
                    return '<a class="btn btn-sm btn-warning btn-padding" href="/category/edit/' + data.id + '">Güncelle</a>';
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

                    //return '<a class="btn btn-sm btn-danger" data-id="' + data.id + '">Sil</a>';
                    return '<a class="btn btn-sm btn-danger btn-padding" href="/category/delete/' + data.id + '">Sil</a>';
                }
            },
        ];

        //generate grid
        table = grid.generate("categories", columnDefs,
            //apiCaller.apiSharedEntityBaseUrl() + '/v1/sharedEntity/city?navProps=Country&Include=Id,Name,Country,Country.Name', "GET",
            //apiCaller.apiSharedEntityBaseUrl() + '/v1/sharedEntity/city?include=Id,Name', "GET",
            "/category/list");

        /****************************************************** */

        //console.log("token : ", apihelper.getToken());

        //apihelper.post("GET", "/bank/list", null, (result) => {
        //    console.log(result.data);
        //}, null, true);

        //apihelper.checkToken(() => {
        //    apihelper.post("GET", apihelper.apiBaseUrl() + '/api/bank/list', null, (data) => {
        //        console.log(data);
        //    }, apihelper.getToken(), true)
        //});
    }
}


$(document).ready(function () {

    Category.init();

    //$('#dt-basic-example').dataTable({
    //    responsive: true
    //});
});