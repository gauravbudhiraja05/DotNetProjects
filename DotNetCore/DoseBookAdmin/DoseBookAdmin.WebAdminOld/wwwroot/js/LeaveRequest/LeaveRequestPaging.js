$(document).ready(function () {    
    var pageSize = 10;
    var leaveRequestsListTable = $('#tblLeaveRequests').DataTable({
        "pageLength": pageSize
    });
    $(".dataTables_empty").text("There are no leaves yet!");

    // Search on rewards Grid using Search Text box
    $('#txtSearchLeaveRequest').keyup(function () {        
        leaveRequestsListTable.search($(this).val()).draw();
    });

    var rowsCount = $('#tblLeaveRequests tbody tr').length;

    if (rowsCount < pageSize) {
        $("#tblLeaveRequests_paginate").hide();
    }
    else {
        $("#tblLeaveRequests_paginate").show();
    }

    if (rowsCount <= 1) {
        $("#btnDeleteAllCheckedRewards").hide();
    }
    else {
        $("#btnDeleteAllCheckedRewards").show();
    }

    // Hide un-neccessary controls of datatable plugin for rewards table
    $('.dataTables_length').hide();
    $('.dataTables_filter').hide();
    $('.dataTables_info').hide();
});

// Callback function of Deleted Users
var deletedLeaveCallback = function (data) {  
    swal(
        'The leave request has been successfully cancelled.',
        '',
        'success'
    ).then(function () {
        var status = $("#hdnLeaveStatus").val();
        GetLeaveRequests(status);
        var rowsCount = $('#tblLeaveRequests tbody tr').length;
        if (rowsCount <= 1) {
            $("#btnDeleteSelected").hide();
        }
        else {
            $("#btnDeleteSelected").show();
        }
    });
}


// Delete all checked user on top delete button click
$("#btnDeleteLeaves").on("click", function () {    
    var leaveIds = {};
    leaveIds.ItemIds = [];
    $('#tblLeaveRequests').find('input[type="checkbox"]:checked').each(function () {
        var row = $(this).parents('tr')[0];
        var LeaveId = parseInt($(row).find('span').text());
        if (!isNaN(LeaveId)) {
            leaveIds.ItemIds.push(LeaveId);
        }

    });
    if (leaveIds.ItemIds.length > 0) {
        swal({
            title: "Are you sure you want to delete these leave requests?",
            text: "",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes',
            cancelButtonText: 'No',
            width: '500px'
        }).then(function (result) {
            if (result.value) {                
                leaveDataService.deleteLeaves("/LeaveManagement/DeleteLeaves", leaveIds, deletedLeaveCallback);
            }
        });
    }
    else {
        swal('Please select at least one leave request using the checkboxes.');
    }

});