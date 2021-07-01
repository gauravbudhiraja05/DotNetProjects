$(document).ready(function () {


    $("#fileUploaderImg1").change(function () {
        readIMG1(this);
    });

    //Select Current Menu
    $('.nav-li').removeClass('current');
    $('#menuItemSuperAdmin').addClass('current');

    $("#Edit_Doctor_Btn").click(function (event) {

        if (validateEditDoctorMessage() == false) {
            //$("#btnSave").prop("disabled", true);
            return false;
        }
        
        // Get form
        var form = $('#EditformDoctor')[0];


        // Create an FormData object 
        var data = new FormData(form);

        $.ajax({
            type: "POST",
            enctype: 'multipart/form-data',
            url: "/Doctor/EditDoctor",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val());
                onUpdateBegin(xhr);
            },
            data: data,
            processData: false,
            contentType: false,
            cache: false,
            timeout: 600000,
            success: function (data) {
                //$("#btnSave").prop("disabled", false);
                onUpdateSuccess(data);
            },
            error: function (e) {
                // $("#btnSave").prop("disabled", false);
                onUpdateFailed(e);
            }
        });

    });

});
var onUpdateBegin = function () {
    //alert("onBegin");
};


var onUpdateComplete = function () {
    //alert("onComplete");
};

var onUpdateSuccess = function (data) {
    if (data.isSuccess == true) {
        swal({
            title: data.message,
            text: "",
            type: 'success'
        }).then(function () {
            window.location.href = "/Doctor/Index";
        });
    }
    else {
        swal({
            title: data.message,
            text: "",
            type: 'error',
            width:'500px'
        });
    }
};

var onUpdateFailed = function (context) {
    //alert("Failed");
};

// Validate Doctor form
var validateEditDoctorMessage = function () {
    var isValid = true;
    

    // check if the form input is valid
    if (!$("#EditformDoctor").valid()) {
        $("#EditformDoctor").submit();
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