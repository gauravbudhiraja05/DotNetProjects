$(document).ready(function () {

    //Select Current Menu
    $('.nav-li').removeClass('current');
    $('#menuItemSuperAdmin').addClass('current');

});
var onUpdateBegin = function () {
    //alert("onBegin");
};



var onUpdateComplete = function () {
    //alert("onComplete");
};

var onUpdateSuccess = function (context) {

    swal(
        'Successfully',
        'You have updated end user',
        'success'
    ).then(function () {
        window.location.href = "/EndUser/Index";
    });
};

var onUpdateFailed = function (context) {
    //alert("Failed");
};