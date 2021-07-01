$(document).ready(function () {  
    var pageSize = 10;
    var newsListTable = $('#tblApprovedLeaveRequests').DataTable({
        "pageLength": pageSize,
        "bSort": false
    });

    var rowsCount = $('#tblApprovedLeaveRequests tbody tr').length;
    if (rowsCount < pageSize) {
        $("#tblApprovedLeaveRequests_paginate").hide();
    }
    else {
        $("#tblApprovedLeaveRequests_paginate").show();
    }

    $("#tblApprovedLeaveRequests_length").hide();
    $("#tblApprovedLeaveRequests_filter").hide();
    $("#tblApprovedLeaveRequests_info").hide();
});
