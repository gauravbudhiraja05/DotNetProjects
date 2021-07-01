$(document).ready(function () {
    //$(".overlay_account").click(function () {
    //    $("body").addClass("act_overlay");
    //});
    //$(".overlay_account_box .close").click(function () {
    //    $("body").removeClass("act_overlay");
    //})

    $(".overlay_account").click(function (event) {
        //event.preventDefault();
        $("#resetpassword_popup, .popup_window_overlay").fadeToggle(200);
    });

    $(".popup_close_btn").click(function () {
        $(".popup_window, .popup_window_overlay").fadeOut(200);
    });

});

$("#btnForgetSubmit").on("click", function () {

    var emailAddress = $("#yourEmail").val().trim();
    var isEmailReadyToSubmit = true;

    // Validate Email is entered or not
    if (emailAddress == '') {
        $("#yourEmailInputError").text("Please enter your email address.");
        isEmailReadyToSubmit = false;
        return false;
    }

    // Validate Email Format
    if (!validateEmail(emailAddress)) {
        $("#yourEmailInputError").text("Please enter a valid email address.");
        isEmailReadyToSubmit = false;
        return false;
    }

    // Validate email address from server
    var user = {
        Email: emailAddress
    };

    $.post("/Account/ValidateEMailExistOrNot", { user: user }, function (result) {
        if (result.isValid == false) {
            $("#yourEmailInputError").text(result.message);
            isEmailReadyToSubmit = false;
            return false;
        }
    });

    // If email ready to sumbit then submit it
    if (isEmailReadyToSubmit) {

        $("#yourEmailInputError").text("");
        $(".loaderModal").show();
        $.post("/Account/MailSendLinkToResetPassword", { user: user }, callbackForgetSubmit);
    }

});


function callbackForgetSubmit(data) {
    $(".loaderModal").hide();
    $("#yourEmail").val('');

    if (data.isSuccess) {

      
        //swal(
        //    'A reset password link has been sent to your email address, please check your email and proceed further',
        //    '',
        //    'success'
        //);

        swal({
            type: 'success',
            title: 'A reset password link has been sent to your email address. Please check your email and follow the link.',
            text: '',
            width: '500px'
        })
        //swal('Reset password link has been sent to your email. Please reset your password');
       

        //$('body').removeClass("act_overlay");

        ////$("#msgSendInfo").html("Reset password link has been sent to your email. Please reset your password");
        //swal(
        //    'Successfully',
        //    'Reset password link has been sent to your email. Please reset your password!',
        //    'success'
        //);
    }

    else {
        swal({
            type: 'error',
            title: data.message,
            text: '',
            width: '500px'
        })
    }
    $(".popup_window, .popup_window_overlay").fadeOut(200);
}

function validateEmail($email) {
    var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
    return emailReg.test($email);
}