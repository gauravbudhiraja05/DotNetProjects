$(document).ready(function () {

    //Select Current Menu
    $('.nav-li').removeClass('current');
    $('#menuItemSuperAdmin').addClass('current');

    $("#Edit_MiscSuggestion_Btn").click(function (event) {

        if (validateEditMiscSuggestionMessage() == false) {
            //$("#btnSave").prop("disabled", true);
            return false;
        }

        // Get form
        var form = $('#EditformMiscSuggestion')[0];

        // Create an FormData object
        var data = new FormData(form);

        $.ajax({
            type: "POST",
            enctype: 'multipart/form-data',
            url: "/MiscSuggestion/EditMiscSuggestion",
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
        'The misc suggestion was successfully updated.',
        '',
        'success'
    ).then(function () {
        window.location.href = "/MiscSuggestion/Index";

    });
};

var onFailed = function (context) {
    //alert("Failed");
};


// Validate Diagnostic Test form
var validateEditMiscSuggestionMessage = function () {
    var isValid = true;
    debugger;

    // check if the form input is valid
    if (!$("#EditformMiscSuggestion").valid()) {
        $("#EditformMiscSuggestion").submit();
        isValid = false;
    }

    if ($("select[name=DoctorId] option:selected").text() == "Select Doctor") {
        $("span[data-valmsg-for='DoctorId']").text("Please select the Doctor.");
        isValid = false;

    }
    else {
        $("span[data-valmsg-for='DoctorId']").text("");
    }

    if ($("#TestName").val().trim().length == 0) {
        $("span[data-valmsg-for='TestName']").text("Please enter the test name.");
        isValid = false;

    }
    else {
        $("span[data-valmsg-for='TestName']").text("");
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

