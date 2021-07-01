$(document).ready(function ()
{
    $('#menuItemVacancies').addClass('current');
    GetLeaveRequests('');
});
function GetLeaveRequests(status) {
    $("#hdnLeaveStatus").val(status);
    $.get('/LeaveManagement/GetAllLeaves?status=' + status, function (data) {
        $('#partialLeaveRequests').html(data);
        $('#partialLeaveRequests').show();
    });
}

function ShowLeavesByStatus(ref) {
    var status = ref.innerText;
    var selectedtext = ref.innerText;
    $("#aLeaveStatus").text(ref.innerText);
    if (ref.innerText === "All") {
        selectedtext = "Select leave status";
        status = "";
    } 
    $("#aLeaveStatus").text(selectedtext);
    GetLeaveRequests(status);
}

function DeleteLeaveRequest(leaveId) {
    var leaveIds = {};
    leaveIds.ItemIds = [];
    if (!isNaN(leaveId)) {
        leaveIds.ItemIds.push(leaveId);
    }
    swal({
        title: "Are you sure you want to cancel this leave request?",
        text: "",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes',
        cancelButtonText: 'No'
    }).then(function (result) {
        if (result.value) {
            leaveDataService.deleteLeaves("/LeaveManagement/DeleteLeaves", leaveIds, deletedLeaveCallback);
        }
    });
}

var leaveDataService = new function () {
    deleteLeaves = function (url, leaveIds, callback) {
        $.post(url, { leaveIds: leaveIds }, function (data) { callback(data) });
    }
    return {
        deleteLeaves: deleteLeaves
    };
}();

