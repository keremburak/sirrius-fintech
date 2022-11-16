var grid = {
    order: [],
    allcount: 0,
    generate: (gridName, columnDefs, url, parameters, callback) => {
        // debugger;

        $.fn.dataTable.ext.errMode = 'throw';

        let table = $('#' + gridName).DataTable({
            language: { url: "../grid-language/turkish.json" },
            responsive: true,
            //info: false,  //display info part at the bottom=left
            autoWidth: false,
            cache: false,
            scrollCollapse: true,
            paging: true,
            pagingType: 'numbers',
            //pageResize: true,
            destroy: true,
            lengthMenu: [[10, 25, 50, 100, -1], [10, 25, 50, 100, 'Tümü']],
            pageLength: 10,
            "drawCallback": function (settings) {
                //alert('DataTables has redrawn the table');

                if (callback) callback();
            },
            //searching: false,
            "processing": "true",
            "serverSide": "true",
            "ajax": {
                "url": url,
                "data": function (d) {
                    //debugger;

                    console.log(d);

                    console.log(grid.order);

                    //set order array in calling JS file.(e.g. index.js and etc)
                    let order = grid.order;
                    let columnIndex = d.order[0].column;

                    console.log(order[columnIndex]);

                    //return {
                    //    //searchStrings: [d.search.value],
                    //    searchStrings: d.search.value !== "" ? d.search.value : [],
                    //    index: Math.floor(d.start / d.length), //d.draw - 1, 
                    //    size: (d.length !== -1 ? d.length : parseInt(grid.allcount)),
                    //    orderBy: order[columnIndex] + (d.order[0].dir === "asc" ? "" : ":D")
                    //}

                    return {
                        search: d.search.value !== "" ? d.search.value : [],
                        index: Math.floor(d.start / d.length), //d.draw - 1, 
                        size: (d.length !== -1 ? d.length : parseInt(grid.allcount)),
                        orderBy: order[columnIndex] + (d.order[0].dir === "asc" ? "" : ":D"),
                        //querystring: parameters != undefined ? JSON.stringify(parameters) : ""
                    }
                },
                //"beforeSend": xhr => {   //Include the bearer token in header                    
                //    //console.log(localStorage.token);
                //    xhr.setRequestHeader("Authorization", 'Bearer ' + token);
                //},
                contentType: "application/json",
                dataType: "json",
                dataSrc: function (data) {
                    //in order to get all records when select 'All' in length menu of datatables

                    //$("#" + gridName).find("thead").find("tr").find("th:first").css('width', '15px');

                    grid.allcount = data.data.count;

                    data.recordsFiltered = data.data.count;
                    data.recordsTotal = data.data.count;
                    data.data = data.data.items;

                    return data.data;
                }
            },
            columnDefs: columnDefs,
            //'order': [[2, 'asc']]
        });

        return table;
    }
}
