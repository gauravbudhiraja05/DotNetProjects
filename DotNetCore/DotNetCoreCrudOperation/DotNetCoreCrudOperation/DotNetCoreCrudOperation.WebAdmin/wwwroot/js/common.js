$(document).ready(function () {

    $("#btnChangePassword").click(function () {

        $.get('/Home/ChangePassword', function (data) {

            $('#partialResetPassword').html(data);
            $('#partialResetPassword').show();
            $("#resetpassword_popup, .popup_window_overlay").fadeToggle(200);
        });

    });



    //$("#btnChangePassword").click(function (event) {
    //    event.preventDefault();
    //    $("#resetpassword_popup, .popup_window_overlay").fadeToggle(200);
    //});

    $('#btnResetPassword').click(function () {
        if (ValidateResetPassword()) {

            $(".loaderModal").show();
            $.ajax({
                type: "Post",
                url: "/Home/ValidateOldPassword",
                data: { Password: $('#txtOldPassword').val() },
                success: function (result) {
                    if (result == false) {
                        $('#span_ConfirmPassword').html('Old password does not match with our database record.');
                        $(".loaderModal").hide();
                        return false;
                    }
                    else if (result == true) {
                        ChangePasswordForLoginUser();
                        return true;
                    }
                },
                error: function (response) {
                    alert('eror');
                }
            });

        }
    });
});

function ChangePasswordForLoginUser() {
    $.ajax({
        type: "Post",
        url: "/Home/ChangePasswordForLoginUser",
        data: { NewPassword: $('#txtNewPassword').val(), ConfirmPassword: $('#txtConfirmPassword').val() },
        success: function (result) {
            if (result.isSuccess == true) {
                swal("Password changed successfully.");
                $(".loaderModal").hide();
                $('body').removeClass("act_overlay");
                $('#txtOldPassword').val('');
                $('#txtNewPassword').val('');
                $('#txtConfirmPassword').val('');
                return true;
            }
        },
        error: function (response) {
           alert('eror');
        }
    });
}

function ValidateResetPassword() {
    $('#span_OldPassoword').html('');
    $('#span_NewPassword').html('');
    $('#span_ConfirmPassword').html('');

    if ($('#txtOldPassword').val() == '') {
        $('#span_OldPassoword').html('Please enter old password.');
        return false;
    }
    else if ($('#txtNewPassword').val() == '') {
        $('#span_NewPassword').html('Please enter new password.');
        return false;
    }

    else if ($('#txtConfirmPassword').val() == '') {
        $('#span_ConfirmPassword').html('Please enter confirm password.');
        return false;
    }

    else if ($('#txtNewPassword').val() != '') {

        if ($('#txtOldPassword').val() == $('#txtNewPassword').val()) {
            $('#span_NewPassword').html('Old password & new password should not be same.');
            return false;
        }

        else if ($('#txtNewPassword').val() != $('#txtConfirmPassword').val()) {
            $('#span_ConfirmPassword').html('New password & confirm password does not match.');
            return false;
        }

        else {
            return true;
        }
    }
}


function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

function CheckFileTypeExtension(fileName) {
    var fileTypeExtension = fileName.substr(fileName.lastIndexOf('.')).toLowerCase();
    var _validFileExtensions = [".jpg", ".jpeg", ".bmp", ".gif", ".png"];
    var blnValid = false;
    for (var j = 0; j < _validFileExtensions.length; j++) {
        var sCurExtension = _validFileExtensions[j];
        if (fileTypeExtension == sCurExtension.toLowerCase()) {
            blnValid = true;
            break;
        }
    }

    if (!blnValid) {
        swal({
            type: 'error',
            //title: "Sorry, " + fileName + " is invalid, allowed extensions are: " + _validFileExtensions.join(", "),
            title: 'The image file types allowed .jpg, .jpeg, .bmp, .gif, .png.',
            text: '',
            width: '500px'
        })
        return false;
    }
    else { return true; }
}


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