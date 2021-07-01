$(document).ready(function () {

    //Select Current Menu
    $('.nav-li').removeClass('current');
    $('#menuItemSuperAdmin').addClass('current');

    $("#Save_Doctor_Btn").click(function (event) {

       

        if (validateAddDoctorMessage() == false) {
            //$("#btnSave").prop("disabled", true);
            return false;
        }

        // Get form
        var form = $('#AddformDoctor')[0];

        // Create an FormData object
        var data = new FormData(form);

        $.ajax({
            type: "POST",
            enctype: 'multipart/form-data',
            url: "/Doctor/AddDoctor",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val());
                onBegin(xhr);
            },
            data: data,
            processData: false,
            contentType: false,
            cache: false,
            timeout: 600000,
            success: function (data) {
                //$("#btnSave").prop("disabled", false);
                onSuccess(data);
            },
            error: function (e) {
                // $("#btnSave").prop("disabled", false);
                onFailed(e);
            }
        });
    });
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
        'The doctor was successfully created.',
        '',
        'success'
    ).then(function () {
        window.location.href = "/Doctor/Index";

    });
};

var onFailed = function (context) {
    //alert("Failed");
};


// Validate Doctor form
var validateAddDoctorMessage = function () {
    var isValid = true;
    debugger;

    // check if the form input is valid
    if (!$("#AddformDoctor").valid()) {
        $("#AddformDoctor").submit();
        isValid = false;
    }

    if ($("#DoctorName").val().trim().length == 0) {
        $("span[data-valmsg-for='DoctorName']").text("Please enter the doctor name.");
        isValid = false;
        
    }
    else {
        $("span[data-valmsg-for='DoctorName']").text("");
    }

    if ($("#TelephoneNumber").val().trim().length == 0) {
        $("span[data-valmsg-for='TelephoneNumber']").text("Please enter the telephone number.");
        isValid = false;

    }
    else {
        $("span[data-valmsg-for='TelephoneNumber']").text("");
    }

    return isValid;
}

