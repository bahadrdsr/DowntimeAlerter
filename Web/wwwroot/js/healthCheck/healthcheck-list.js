$(document).ready(function () {
  var targetAppId = $("#targetAppIdInput").val();
  var ajaxUrl = "/HealthCheckResult/GetHealthCheckResults";
  $("#healthcheck-table").DataTable({
    ajax: {
      url: ajaxUrl,
      data: function (d) {
        return $.extend({}, d, {
          targetAppId: targetAppId && targetAppId !="00000000-0000-0000-0000-000000000000" ? targetAppId : null,
        });
      },
    },
    bServerSide: true,
    bProcessing: true,
    bSearchable: false,
    paging: true,
    searching : false,
    sPaginationType: "full_numbers",
    pageLength: 10,
    language: {
      emptyTable: "No record found.",
      processing:
        '<i class="fas fa-spinner fa-spin fa-2x fa-fw" style="color:#2a2b2b;"></i><span class="sr-only">Loading...</span> ',
    },

    createdRow: function (row, data, index) {
      if (data.result == -1) {
        row.cells[2].innerHTML =
          '<i class="fas fa-question-circle text-warning"></i>';
      }
      if (data.result == 1) {
        row.cells[2].innerHTML =
          '<i class="fas fa-check-circle text-success"></i>';
      }
      if (data.result == 0) {
        row.cells[2].innerHTML =
          '<i class="fas fa-times-circle text-danger"></i>';
      }
    },
    columns: [
      {
        data: "targetApp.name",
        autoWidth: true,
        sortable: false,
      },
      {
        data: "executionTime",
        autoWidth: true,
        sortable: false,
      },
      {
        data: "result",
        autoWidth: true,
        sortable: false,
      },
      {
        data: "statusCode",
        autoWidth: true,
        sortable: false,
      },
      {
        data: "message",
        autoWidth: true,
        sortable: false,
      },
    ],
  });
});
