$(document).ready(function () {
    GetPendingLeaveRequests();

    $(".confirmation_popup .rq_cancel_btn").click(function (event) {
        event.preventDefault();
        $(".confirmation_popup, .confirmation_popup_overlay").fadeOut(200);
    });
});

function GetPendingLeaveRequests() {
    $.get('/FrontEndHome/GetPendingLeaveRequests', function (data) {
        $('#partialPendingLeaveRequests').html(data);
        $('#partialPendingLeaveRequests').show();
    });
}

function EditPendingLeaveRequest(leaveID) {
    window.location.href = '/intranet/leaverequest?id=' + parseInt(leaveID);
}

function ChangePendingLeavePopUp(leaveID, startTime, startDayName, startDayOfMonth, startMonth, endTime, endDayName, endDayOfMonth, endMonth,leaveDuration) {
    event.preventDefault();   
    $("#hdnLeaveId").val(parseInt(leaveID));
    var leavemsg = "You’re requesting <strong> ";
    if (parseInt(leaveDuration) > 1) {
        leavemsg = leavemsg + leaveDuration + " days </strong> leave from " + startDayName + " <strong>" + startDayOfMonth + " " + startMonth + " (" + startTime + ")</strong> and returning on " + endDayName + " <strong>" + endDayOfMonth + " " + endMonth + " (" + endTime + ")</strong>.";
    }
    else {
        leavemsg = leavemsg + leaveDuration + " day </strong> leave from " + startDayName + " <strong>" + startDayOfMonth + " " + startMonth + " (" + startTime + ")</strong> and returning on " + endDayName + " <strong>" + endDayOfMonth + " " + endMonth + " (" + endTime + ")</strong>.";
    }
    $("#pLeaveMsg").html(leavemsg);
    $(".confirmation_popup, .confirmation_popup_overlay").fadeIn(200);
}

function ChangePendingLeaveStatus() {
    event.preventDefault();  
    window.location.href = '/intranet/cancel-confirmation?LeaveId=' + parseInt($("#hdnLeaveId").val());
}

function UpdateLeave() {
    window.location.href = '/intranet/leaverequest?id=' + parseInt($("#hdnLeaveId").val());
}