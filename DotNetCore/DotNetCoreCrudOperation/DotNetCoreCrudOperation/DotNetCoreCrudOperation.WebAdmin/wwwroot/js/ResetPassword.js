$(document).ready(function () {

    $(".popup_close_btn").click(function () {
        $(".popup_window, .popup_window_overlay").fadeOut(200);
    });

    //$('#btnResetPassword').click(function () {
    //    if (ValidateResetPassword()) {

    //        $(".loaderModal").show();
    //        $.ajax({
    //            type: "Post",
    //            url: "/Home/ValidateOldPassword",
    //            data: { Password: $('#txtOldPassword').val() },
    //            success: function (result) {
    //                if (result == false) {
    //                    $('#span_ConfirmPassword').html('Old password does not match with our database record.');
    //                    $(".loaderModal").hide();
    //                    return false;
    //                }
    //                else if (result == true) {
    //                    ChangePasswordForLoginUser();
    //                    return true;
    //                }
    //            },
    //            error: function (response) {
    //                debugger;
    //                alert('eror');
    //            }
    //        });

    //    }
    //});

});

var onBegin = function () {
    $(".loaderModal").show()
};



var onComplete = function () {
    //alert("onComplete");
};

var onSuccess = function (context) {
    debugger;
    swal(
        'Password has been changed successfully!',
        '',
        'success'
    ).then(function () {
        $(".loaderModal").hide();
        $(".popup_window, .popup_window_overlay").fadeOut(200);
        $('#txtOldPassword').val('');
        $('#txtNewPassword').val('');
        $('#txtConfirmPassword').val('');
        return true;
    });
};

var onFailed = function (context) {
    //alert("Failed");
};


//function ChangePasswordForLoginUser() {
//    $.ajax({
//        type: "Post",
//        url: "/Home/ChangePasswordForLoginUser",
//        data: { NewPassword: $('#txtNewPassword').val(), ConfirmPassword: $('#txtConfirmPassword').val() },
//        success: function (result) {
//            if (result.isSuccess == true) {
//                //swal("Password changed successfully.");
//                swal(
//                    'Password has been changed successfully!',
//                    '',
//                    'success'
//                ).then(function () {
//                    $(".loaderModal").hide();
//                    $(".popup_window, .popup_window_overlay").fadeOut(200);
//                    $('#txtOldPassword').val('');
//                    $('#txtNewPassword').val('');
//                    $('#txtConfirmPassword').val('');
//                    return true;
//                });
//                //$(".loaderModal").hide();
//                //$(".popup_window, .popup_window_overlay").fadeOut(200);
//                //$('#txtOldPassword').val('');
//                //$('#txtNewPassword').val('');
//                //$('#txtConfirmPassword').val('');
//                //return true;
//            }
//        },
//        error: function (response) {
//            debugger;
//            alert('eror');
//        }
//    });
//}

//function ValidateResetPassword() {
//    $('#span_OldPassoword').html('');
//    $('#span_NewPassword').html('');
//    $('#span_ConfirmPassword').html('');

//    if ($('#txtOldPassword').val() == '') {
//        $('#span_OldPassoword').html('Please enter old password.');
//        return false;
//    }
//    else if ($('#txtNewPassword').val() == '') {
//        $('#span_NewPassword').html('Please enter new password.');
//        return false;
//    }

//    else if ($('#txtConfirmPassword').val() == '') {
//        $('#span_ConfirmPassword').html('Please enter confirm password.');
//        return false;
//    }

//    else if ($('#txtNewPassword').val() != '') {

//        if ($('#txtOldPassword').val() == $('#txtNewPassword').val()) {
//            $('#span_NewPassword').html('Old password & new password should not be same.');
//            return false;
//        }

//        else if ($('#txtNewPassword').val() != $('#txtConfirmPassword').val()) {
//            $('#span_ConfirmPassword').html('New password & confirm password does not match.');
//            return false;
//        }

//        else {
//            return true;
//        }
//    }
//}
