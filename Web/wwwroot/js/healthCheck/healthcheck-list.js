$(document).ready(function () {
    $('#healthcheck-table')
        .DataTable({
            "sAjaxSource": "/HealthCheckResult/GetHealthCheckResults",
            "bServerSide": true,
            "bProcessing": true,
            "bSearchable": true,
            "language": {
                "emptyTable": "No record found.",
                "processing":
                    '<i class="fas fa-spinner fa-spin fa-2x fa-fw" style="color:#2a2b2b;"></i><span class="sr-only">Loading...</span> '
            },

            "createdRow": function (row, data, index) {
                if (data[3] == -1) {
                    row.cells[2].innerHTML = '<i class="fas fa-question-circle text-warning"></i>'
                }
                if (data[3] == 1) {
                    row.cells[2].innerHTML = '<i class="fas fa-check-circle text-success"></i>'
                }
                if (data[3] == 0) {
                    row.cells[2].innerHTML = '<i class="fas fa-times-circle text-danger"></i>'
                }
            },
            "columns": [
                {
                    "data": "targetApp.name",
                    "autoWidth": true,
                    "sortable": false
                },
                {
                    "data": "executionTime",
                    "autoWidth": true,
                    "sortable": false
                },
                {
                    "data": "result",
                    "autoWidth": true,
                    "sortable": false
                },
                {
                    "data": "statusCode",
                    "autoWidth": true,
                    "sortable": false
                },
                {
                    "data": "message",
                    "autoWidth": true,
                    "sortable": false
                },
            ]
        });
}); 
