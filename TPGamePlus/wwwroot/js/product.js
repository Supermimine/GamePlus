var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url":"/Admin/ListProduct/GetAll"
        },
        "columns": [
            { "data":"name","width":"15%"},
            { "data":"actualPrice","width":"15%"},
            { "data":"price","width":"15%"},
            { "data":"quantity","width":"15%"},
        ]
    });
}