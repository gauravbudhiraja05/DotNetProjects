$(document).ready(function () {
    GetApprovedLeaveRequests();
});

function GetApprovedLeaveRequests() {
    $.get('/FrontEndHome/GetApprovedLeaveRequests', function (data) {
        $('#partialApprovedLeaveRequests').html(data);
        $('#partialApprovedLeaveRequests').show();
    });
}

function EditApprovedLeaveRequest(id) {    
    window.location.href = '/intranet/leaverequest?id=' + parseInt(id);
}