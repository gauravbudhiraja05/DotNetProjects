
var onBegin = function () {
   
};



var onComplete = function () {
    
};

var onSuccess = function (context) {
    swal(
        'The password has been changed successfully.',
        '',
        'success'
    ).then(function () {
        window.location.href = "/Account/Login";

    });
    //swal('Password has been reset successfully!').then(function () {
    //    window.location.href = "/Account/Login";

    //});
    //$(".popup_window, .popup_window_overlay").fadeOut(200);
};

var onFailed = function (context) {
    //alert("Failed");
};