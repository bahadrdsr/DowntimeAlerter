$(document).ready(function () {
    $('#app-table')
        .DataTable({
            buttons: [
                {
                    text: 'My button',
                    action: function (e, dt, node, config) {
                        alert('Button activated');
                    }
                }
            ],
            "sAjaxSource": "/TargetApp/GetTargetApps",
            "bServerSide": true,
            "bProcessing": true,
            "bSearchable": true,
            "paging": true,
            "sPaginationType": "full_numbers",
            "pageLength": 10,
            "language": {
                "emptyTable": "No record found.",
                "processing":
                    '<i class="fas fa-spinner fa-spin fa-2x fa-fw" style="color:#2a2b2b;"></i><span class="sr-only">Loading...</span> '
            },

            "createdRow": function (row, data, index) {
                if (data.isActive) {
                    row.cells[3].innerHTML = '<i class="fas fa-play text-success"></i>'
                } else {
                    row.cells[3].innerHTML = '<i class="fas fa-stop text-danger"></i>'
                }
            },
            "columns": [
                {
                    "data": "name",
                    "autoWidth": true,
                    "sortable": false
                },
                {
                    "data": "url",
                    "autoWidth": true,
                    "sortable": false
                },
                {
                    "data": "interval",
                    "autoWidth": true,
                    "sortable": false
                },
                {
                    "data": "isActive",
                    "autoWidth": true,
                    "sortable": false
                },
                {
                    mRender: function (data, type, row) {
                        return `<a class="app-edit btn" data-id="${row.id}" id="edit-${row.id}" data-target="upsertAppModal"><i class="fas fa-pencil-alt"></i></a><a class="app-view btn" data-id="${row.id}" id="view-${row.id}"><i class="fas fa-bars"></i></a><a class="app-delete btn" data-id="${row.id}"  id="delete-${row.id}" data-target="deleteModal"><i class="fas fa-trash text-danger"></i></a>`
                    }
                }
                // { "data": null, "defaultContent": '<button class="btn"><i class="fas fa-pencil-alt"></i></button><button class="btn"><i class="fas fa-bars"></i></button><button class="btn"><i class="fas fa-trash text-danger"></i></button>', "sortable": false }
            ],
            "initComplete": function (settings, json) {
                bindCustomButtons();
            }
        });
    $('#app-table').on('draw.dt', function () {
        bindCustomButtons();
    });
});
var bindCustomButtons = function () {
    $('.app-create').click(function () {
        $('#upsertId').val(null);
        $('#upsert-form')[0].reset();
        $("#upsertAppModal").modal('show');
    });
    $('.app-edit').click(function () {
        var id = $(this).data('id');
        getTargetApp(id);
        $("#upsertAppModal").modal('show');
    });
    $('.app-delete').click(function () {
        var id = $(this).data('id');
        $('#deleteId').val(id);
        $("#deleteModal").modal('show');
    });

    $('#upsertAppModal').on('hidden.bs.modal', function (e) {
        console.log(e);
    })
}

var getTargetApp = function (id) {
    $.get('/TargetApp/GetTargetApp?id=' + id, function (data) {
        // set input values
        $('#upsertId').val(id);
        $('#Id').val(data.id);
        $('#Name').val(data.name);
        $('#Url').val(data.url);
        $('#IsActive').val(data.isActive.toString());
        $('#Interval').val(data.interval);

    });
}