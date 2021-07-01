$(document).ready(function () {

    //Select Current Menu
    $('.nav-li').removeClass('current');
    $('#menuItemSuperAdmin').addClass('current');

    $("select[name=DoctorId]").change(function (event) {
        $('#ddlClassification').empty()
        $('#divClassification').hide();
    });

    $("#ClassificationTypeId").change(function (event) {

        var doctorId = $("select[name=DoctorId] option:selected").val();
        var classificationTypeName = $("select[name=ClassificationTypeId] option:selected").text();

        $.ajax({
            type: "GET",
            url: "/ClassificationResult/GetClassificationResultData",
            contentType: "application/json",
            data: { doctorId: doctorId, classificationTypeName: classificationTypeName },
            dataType: "json",
            success: function (response) {
                if (response.length > 0) {
                    $('#ddlClassification').empty()
                    $('#divClassification').show();
                    if (classificationTypeName == "TEST") {
                        $('#lblClassification').text("Diagnostic Test *");
                    }
                    else if (classificationTypeName == "MISC") {
                        $('#lblClassification').text("Misc Suggestion *");
                    }
                    else if (classificationTypeName == "MEDICINE") {
                        $('#lblClassification').text("Medicine Dose *");
                    }

                    var res = response;
                    var optionText, optionValue;
                    for (var i = 0; i < res.length; i++) {
                        if (classificationTypeName == "TEST" || classificationTypeName == "MISC") {
                            optionText = res[0].testName;
                            optionValue = res[0].testId;
                        }
                        else if (classificationTypeName == "MEDICINE") {
                            optionText = res[0].medicineName;
                            optionValue = res[0].medicineId;
                        }
                        $("#ddlClassification").append(`<option value="${optionValue}">${optionText}</option>`);
                    }
                    $("#ddlClassification").prepend("<option value='0' selected='Select Option'>Select Option</option>");
                }
                else {
                    alert("No Records Found");
                    $('#divClassification').hide();
                }
                //alert(response);
            },
            failure: function (response) {
                alert(response);
            }
        });
    });

    $("#Save_ClassificationResult_Btn").click(function (event) {

        if (validateAddClassificationResultMessage() == false) {
            //$("#btnSave").prop("disabled", true);
            return false;
        }

        // Get form
        var form = $('#AddformClassificationResult')[0];

        // Create an FormData object
        var data = new FormData(form);

        $.ajax({
            type: "POST",
            enctype: 'multipart/form-data',
            url: "/ClassificationResult/AddClassificationResult",
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
        'The classification result was successfully created.',
        '',
        'success'
    ).then(function () {
        window.location.href = "/ClassificationResult/Index";

    });
};

var onFailed = function (context) {
    //alert("Failed");
};


// Validate Doctor form
var validateAddClassificationResultMessage = function () {
    var isValid = true;
    debugger;

    // check if the form input is valid
    if (!$("#AddformClassificationResult").valid()) {
        $("#AddformClassificationResult").submit();
        isValid = false;
    }

    if ($("select[name=DoctorId] option:selected").text() == "Select Doctor") {
        $("span[data-valmsg-for='DoctorId']").text("Please select the Doctor.");
        isValid = false;

    }
    else {
        $("span[data-valmsg-for='DoctorId']").text("");
    }

    if ($("#ClassificationResultName").val().trim().length == 0) {
        $("span[data-valmsg-for='ClassificationResultName']").text("Please enter the classification result name.");
        isValid = false;

    }
    else {
        $("span[data-valmsg-for='ClassificationResultName']").text("");
    }

    if ($("select[name=ClassificationTypeId] option:selected").text() == "Select Classification Type") {
        $("span[data-valmsg-for='ClassificationTypeId']").text("Please select the classification type.");
        isValid = false;

    }
    else {
        $("span[data-valmsg-for='ClassificationTypeId']").text("");
    }


    //$("#ddlClassification option:selected").text()
    if ($("#ddlClassification option:selected").text() == "Select Option" || $("#ddlClassification option:selected").text() == undefined) {
        $("span[data-valmsg-for='ClassificationId']").text("Please select the Option Type.");
        isValid = false;

    }
    else {
        $("span[data-valmsg-for='ClassificationId']").text("");
    }

    return isValid;
}

