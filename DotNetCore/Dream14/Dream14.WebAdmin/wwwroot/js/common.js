$(document).ready(function () {

    $("#btnChangePassword").click(function () {

        $.get('/Home/ChangePassword', function (data) {

            $('#partialResetPassword').html(data);
            $('#partialResetPassword').show();
            $("#resetpassword_popup, .popup_window_overlay").fadeToggle(200);
        });

    });

});


// retunr date into MM/DD/YYYY
function getFormattedDate() {
    var date = new Date();
    var year = date.getFullYear();

    var month = (1 + date.getMonth()).toString();
    month = month.length > 1 ? month : '0' + month;

    var day = date.getDate().toString();
    day = day.length > 1 ? day : '0' + day;

    return month + '/' + day + '/' + year;
}