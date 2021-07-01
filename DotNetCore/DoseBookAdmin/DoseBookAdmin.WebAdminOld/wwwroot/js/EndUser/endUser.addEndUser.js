$(document).ready(function () {

    //Select Current Menu
    $('.nav-li').removeClass('current');
    $('#menuItemSuperAdmin').addClass('current');

});

var onBegin = function () {
    $(".loaderModal").show();
};



var onComplete = function () {
    //alert("onComplete");
};

var onSuccess = function (context) {
    $(".loaderModal").hide();
    swal(
        'Successfully',
        'You have saved end user!',
        'success'
    ).then(function () {
        window.location.href = "/EndUser/Index";

    });
};

var onFailed = function (context) {
    //alert("Failed");
};

