$(document).ready(function () {

    //Select Current Menu
    $('.nav-li').removeClass('current');
    $('#menuItemSuperAdmin').addClass('current');

    $("#Edit_MedicineDose_Btn").click(function (event) {

        if (validateAddMedicineDoseMessage() == false) {
            //$("#btnSave").prop("disabled", true);
            return false;
        }

        // Get form
        var form = $('#EditformMedicineDose')[0];

        // Create an FormData object
        var data = new FormData(form);

        $.ajax({
            type: "POST",
            enctype: 'multipart/form-data',
            url: "/MedicineDose/EditMedicineDose",
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
        'The medicine dose was successfully updated.',
        '',
        'success'
    ).then(function () {
        window.location.href = "/MedicineDose/Index";

    });
};

var onFailed = function (context) {
    //alert("Failed");
};


// Validate Doctor form
var validateAddMedicineDoseMessage = function () {
    var isValid = true;
    debugger;

    // check if the form input is valid
    if (!$("#EditformMedicineDose").valid()) {
        $("#EditformMedicineDose").submit();
        isValid = false;
    }

    if ($("select[name=DoctorId] option:selected").text() == "Select Doctor") {
        $("span[data-valmsg-for='DoctorId']").text("Please select the Doctor.");
        isValid = false;

    }
    else {
        $("span[data-valmsg-for='DoctorId']").text("");
    }

    if ($("#MedicineName").val().trim().length == 0) {
        $("span[data-valmsg-for='MedicineName']").text("Please enter the medicine name.");
        isValid = false;

    }
    else {
        $("span[data-valmsg-for='MedicineName']").text("");
    }

    if ($("#Frequency").val().trim() == "Select Frequency") {
        $("span[data-valmsg-for='Frequency']").text("Please select the frequency.");
        isValid = false;

    }
    else {
        $("span[data-valmsg-for='Frequency']").text("");
    }

    if ($("#Directions").val().trim() == "Select Direction") {
        $("span[data-valmsg-for='Directions']").text("Please enter the directions.");
        isValid = false;

    }
    else {
        $("span[data-valmsg-for='Directions']").text("");
    }

    if ($("#Label").val().trim().length == 0) {
        $("span[data-valmsg-for='Label']").text("Please enter the label.");
        isValid = false;

    }
    else {
        $("span[data-valmsg-for='Label']").text("");
    }

    if ($("#Duration").val() == "Select Duration") {
        $("span[data-valmsg-for='Duration']").text("Please enter the duration.");
        isValid = false;

    }
    else {
        $("span[data-valmsg-for='Duration']").text("");
    }


    if (parseInt($("#Dose").val().trim()) == 0) {
        $("span[data-valmsg-for='Dose']").text("Please enter the dose.");
        isValid = false;

    }
    else {
        $("span[data-valmsg-for='Dose']").text("");
    }

    if ($("#DoseUnit").val().trim() == "Select Dose Unit") {
        $("span[data-valmsg-for='DoseUnit']").text("Please enter the dose Unit.");
        isValid = false;

    }
    else {
        $("span[data-valmsg-for='DoseUnit']").text("");
    }

    return isValid;
}

