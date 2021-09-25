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