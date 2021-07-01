$(document).ready(function () { 
    var pageSize = 10;
    var newsListTable = $('#tblPendingLeaveRequestList').DataTable({
        "pageLength": pageSize,
        "bSort": false
    });

    var rowsCount = $('#tblPendingLeaveRequestList tbody tr').length;

    if (rowsCount < pageSize) {
        $("#tblPendingLeaveRequestList_paginate").hide();
    }
    else {
        $("#tblPendingLeaveRequestList_paginate").show();
    }
    $("#tblPendingLeaveRequestList_length").hide(); 
    $("#tblPendingLeaveRequestList_filter").hide();
    $("#tblPendingLeaveRequestList_info").hide();
});

