$(document).ready(function () {

    var classificationDataListTable = $('#tblClassificationDataList').DataTable({
        "pageLength": 10
    });

    $(".dataTables_empty").text("There are no classification data yet.");

    $('#txtSearchClassificationDataUp').keyup(function () {
        classificationDataListTable.search($(this).val()).draw();
    });

    var rowsCount = $('#tblClassificationDataList tbody tr').length;
    //alert(rowsCount);
    if (rowsCount < 10) {
        $("#tblClassificationDataList_paginate").hide();
    }
    else {
        $("#tblClassificationDataList_paginate").show();
    }

    $('.dataTables_length').hide();
    $('.dataTables_filter').hide();
    $('.dataTables_info').hide();

});
