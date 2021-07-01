$(document).ready(function () {

    //Select Current Menu
    $('.nav-li').removeClass('current');
    $('#menuItemSuperAdmin').addClass('current');

    $("#Save_Document_Btn").click(function (event) {

        if (validateAddDocumentMessage() == false) {
            //$("#btnSave").prop("disabled", true);
            return false;
        }

        // Get form
        var form = $('#AddformDocument')[0];

        // Create an FormData object
        var data = new FormData(form);

        $.ajax({
            type: "POST",
            enctype: 'multipart/form-data',
            url: "/Document/AddDocument",
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
        'The medicine dose was successfully created.',
        '',
        'success'
    ).then(function () {
        window.location.href = "/Document/Index";

    });
};

var onFailed = function (context) {
    //alert("Failed");
};


// Validate Doctor form
var validateAddDocumentMessage = function () {
    var isValid = true;
    debugger;

    // check if the form input is valid
    if (!$("#AddformDocument").valid()) {
        $("#AddformDocument").submit();
        isValid = false;
    }

    if ($("select[name=DoctorId] option:selected").text() == "Select Doctor") {
        $("span[data-valmsg-for='DoctorId']").text("Please select the Doctor.");
        isValid = false;

    }
    else {
        $("span[data-valmsg-for='DoctorId']").text("");
    }

    if ($("select[name=ClassificationResultId] option:selected").text() == "Select Classification Result") {
        $("span[data-valmsg-for='ClassificationResultId']").text("Please select the ClassificationResult.");
        isValid = false;

    }
    else {
        $("span[data-valmsg-for='ClassificationResultId']").text("");
    }

    if ($("#Label").val().trim().length == 0) {
        $("span[data-valmsg-for='Label']").text("Please enter the label.");
        isValid = false;

    }
    else {
        $("span[data-valmsg-for='Label']").text("");
    }

    if ($("#Description").val().trim().length == 0) {
        $("span[data-valmsg-for='Description']").text("Please enter the description.");
        isValid = false;

    }
    else {
        $("span[data-valmsg-for='Description']").text("");
    }

    return isValid;
}

