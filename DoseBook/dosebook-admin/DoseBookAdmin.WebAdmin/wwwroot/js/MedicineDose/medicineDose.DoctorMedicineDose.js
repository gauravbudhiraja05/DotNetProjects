$(document).ready(function () {

    var doctorId = getParameterByName('doctorId');
    if (doctorId != '' && doctorId != null) {
        if (parseInt(doctorId) > 0) {
            var element = document.getElementById(doctorId);
            if (element != undefined || element != null) {
                GetMedicineDoseListByDoctorWise(doctorId, element.innerHTML);
            }
        }
        else {
            GetMedicineDoseListByDoctorWise(0, "All");
        }

    }
    else {
        GetMedicineDoseListByDoctorWise(0, 'Select Doctor');
    }

    $('.doctorClick').on('click', function () {
        var doctorId = parseInt(this.id);
        var doctorName = this.innerText;
        GetMedicineDoseListByDoctorWise(doctorId, doctorName);
    });

    var rowsCount = $('#tblDoctorMedicineDoses tbody tr').length;
    if (rowsCount <= 1) {
        $("#btnDeleteSelected").hide();
        // $(".table_checkbox").hide();
    }
    else {
        $("#btnDeleteSelected").show();
        //$(".table_checkbox").show();
    }

    $('#btnAddCancel, #btnAddHeaderClose').click(function () {
        $("#AddDoctorMedicineModal").modal("hide");
    });

    $("#btnAddDoctorMedicineDose").click(function () {
        if ($('#hdnDoctorId').val() == undefined || $('#hdnDoctorId').val == null || $('#hdnDoctorId').val() == "" || $('#hdnDoctorId').val() == "0") {
            swal("Please select a doctor to add the doctor medicine dose.", '', 'error').then(function () {
                $(".drop_menu").find(".drop_menu_sub").show();
            });
        }
        else {
            BindAddDoctorMedicineModalData();
        }
    });

    $('#btnAddSave').on("click", function () {

        if (validateAddDoctorMedicineDose() == false) {
            return false;
        }
        else {
            var doctorMedicineDose = {}

            AddProblemTags();

            doctorMedicineDose.MedicineName = $('#txtAddMedicineName').val().trim();
            doctorMedicineDose.DoctorId = $('#ddlAddDoctor').val().trim();
            doctorMedicineDose.Frequency = $('#ddlAddFrequency').val().trim();
            doctorMedicineDose.Directions = $('#ddlAddDirections').val().trim();
            doctorMedicineDose.Composition = $('#txtAddComposition').val().trim();
            doctorMedicineDose.Duration = $('#ddlAddDuration').val().trim();
            doctorMedicineDose.DoseUnit = $('#ddlAddDoseUnit').val().trim();
            doctorMedicineDose.Dose = $('#ddlAddDose').val().trim();
            doctorMedicineDose.ProblemTags = $('#txtAddProblemTagsMain').val().trim();

            $.ajax({
                type: "POST",
                url: '/MedicineDose/AddDoctorMedicineDose',
                data: { doctorMedicineDose: doctorMedicineDose },
                success: function () {
                    swal(
                        'The doctor medicine dose has been successfully created.',
                        '',
                        'success'
                    ).then(function () {
                        window.location.href = "/MedicineDose/DoctorMedicineDose?doctorId=" + $('#hdnDoctorId').val();
                    });
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        }
    });

    $("#txtAddMedicineName").autocomplete({
        source: function (request, response) {

            $.ajax({
                type: "GET",
                url: '/MedicineDose/MedicineDoseAutoComplete/',
                data: { prefix: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return item;
                    }))
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        },
        select: function (e, i) {
            $("#txtAddMedicineName").val(i.item.val);
        },
        minLength: 3
    });

    $("#txtAddMedicineName").change(function () {

        $.get('/MedicineDose/ValidateMedicineExistOrNot/', { medicineName: $("#txtAddMedicineName").val(), doctorId: $('#ddlAddDoctor').val(), medicineId: 0 },
            function (data) {
                $('#txtAddMedicineNameError').html('')
                if (data != true) {
                    $('#txtAddMedicineNameError').text(data);
                }
            });

        $.get('/MedicineDose/GetMedicineDetailByMedicineName/', { medicineName: $("#txtAddMedicineName").val(), type: "Doctor" },
            function (medicineDoseDto) {
                $('#txtAddMedicineName').val(medicineDoseDto.medicineName);
                $('#ddlAddFrequency').val(medicineDoseDto.frequency);
                $('#ddlAddDirections').val(medicineDoseDto.directions);
                $('#txtAddComposition').val(medicineDoseDto.composition);
                $('#txtAddMedicineName').val(medicineDoseDto.medicineName);
                $('#ddlAddDuration').val(medicineDoseDto.duration);
                $('#ddlAddDoseUnit').val(medicineDoseDto.doseUnit);
                $('#ddlAddDose').val(medicineDoseDto.dose);

                if (parseInt(medicineDoseDto.doctorId) > 0)
                    $('#ddlAddDoctor').val(medicineDoseDto.doctorId);

                if (medicineDoseDto.problemTags != null || medicineDoseDto.problemTags != "")
                    BindAddGetProblemTags(medicineDoseDto.problemTags);

            });

    });

    $('#btnEditCancel, #btnEditHeaderClose').click(function () {
        $("#EditDoctorMedicineModal").modal("hide");
    });

    $("#txtEditMedicineName").autocomplete({
        source: function (request, response) {

            $.ajax({
                type: "GET",
                url: '/MedicineDose/MedicineDoseAutoComplete/',
                data: { prefix: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return item;
                    }))
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        },
        select: function (e, i) {
            $("#txtEditMedicineName").val(i.item.val);
        },
        minLength: 3
    });

    $("#txtEditMedicineName").change(function () {

        $.get('/MedicineDose/ValidateMedicineExistOrNot/', { medicineName: $("#txtEditMedicineName").val(), doctorId: $('#ddlEditDoctor').val(), medicineId: $('#txtEditMedicineId').val() },
            function (data) {
                $('#txtEditMedicineNameError').html('')
                if (data != true) {
                    $('#txtEditMedicineNameError').text(data);
                }
            });

        $.get('/MedicineDose/GetMedicineDetailByMedicineName/', { medicineName: $("#txtEditMedicineName").val(), type: "Doctor" },
            function (medicineDoseDto) {
                $('#txtEditMedicineName').val(medicineDoseDto.medicineName);
                $('#ddlEditFrequency').val(medicineDoseDto.frequency);
                $('#ddlEditDirections').val(medicineDoseDto.directions);
                $('#txtEditComposition').val(medicineDoseDto.composition);
                $('#txtEditMedicineName').val(medicineDoseDto.medicineName);
                $('#ddlEditDuration').val(medicineDoseDto.duration);
                $('#ddlEditDoseUnit').val(medicineDoseDto.doseUnit);
                $('#ddlEditDose').val(medicineDoseDto.dose);

                if (parseInt(medicineDoseDto.doctorId) > 0)
                    $('#ddlEditDoctor').val(medicineDoseDto.doctorId);

                if (medicineDoseDto.problemTags != null || medicineDoseDto.problemTags != "")
                    BindEditGetProblemTags(medicineDoseDto.problemTags);

            });
    });

    $('#btnEditUpdate').on("click", function () {

        if (validateEditDoctorMedicineDose() == false) {
            return false;
        }
        else {
            var doctorMedicineDose = {}



            doctorMedicineDose.MedicineId = $('#txtEditMedicineId').val().trim();
            doctorMedicineDose.DoctorId = $('#ddlEditDoctor').val().trim();
            doctorMedicineDose.MedicineName = $('#txtEditMedicineName').val().trim();
            doctorMedicineDose.Frequency = $('#ddlEditFrequency').val().trim();
            doctorMedicineDose.Directions = $('#ddlEditDirections').val().trim();
            doctorMedicineDose.Composition = $('#txtEditComposition').val().trim();
            doctorMedicineDose.Duration = $('#ddlEditDuration').val().trim();
            doctorMedicineDose.DoseUnit = $('#ddlEditDoseUnit').val().trim();
            doctorMedicineDose.Dose = $('#ddlEditDose').val().trim();

            $.ajax({
                type: "POST",
                url: '/MedicineDose/EditDoctorMedicineDose',
                data: { doctorMedicineDose: doctorMedicineDose },
                success: function () {
                    swal(
                        'The doctor medicine dose has been successfully updated.',
                        '',
                        'success'
                    ).then(function () {
                        window.location.href = "/MedicineDose/DoctorMedicineDose?doctorId=" + $('#hdnDoctorId').val();
                    });
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        }
    });
});

function GetMedicineDoseListByDoctorWise(doctorId, doctorName) {
    $.get('/MedicineDose/GetMedicineDoseListByDoctorWise/' + doctorId, function (data) {
        $('#partialMedicineDose').html(data);
        $('#partialMedicineDose').show();

        $('#hdnDoctorId').val(doctorId);
        $('#hdnDoctorName').val(doctorName);

        var menuText = doctorName;
        if (menuText == "All") {
            menuText = "Select Doctor";
        }

        $(".drop_menu > ul > li > a")[0].innerText = menuText;

        $('#btnSearch').prop("disabled", false);
        $('#btnDeleteSelected').prop("disabled", false);
        $('#txtSearchMedicineDoseUp').prop("disabled", false);
    });
}

function AddProblemTags() {

    var tags = "";
    $('#txtAddProblemTags .ms-sel-item').each(function () {
        if (tags == "")
            tags = $(this).find('span').parent().text();
        else
            tags = tags + "," + $(this).find('span').parent().text();
    });
    $('#txtAddProblemTagsMain').val(tags);
}

function BindAddDoctorMedicineModalData() {
    $.ajax({
        type: "GET",
        url: '/MedicineDose/AddDoctorMedicineDose',
        data: { doctorId: $('#hdnDoctorId').val() },
        success: function (medicineDoseSimulationDto) {
            if (medicineDoseSimulationDto != null) {
                BindDropDownValues(medicineDoseSimulationDto, "Add");
                BindAddProblemTags();
                $('#ddlAddDoctor').val(medicineDoseSimulationDto.medicineDoseDto.doctorId);
                $("#AddDoctorMedicineModal").modal({ "backdrop": "static" });
            }
        },
        error: function (response) {
            alert(response.responseText);
        },
        failure: function (response) {
            alert(response.responseText);
        }
    });
}

function BindAddProblemTags() {
    $.ajax({
        type: "GET",
        url: '/MedicineDose/DoctorProblemTags/',
        success: function (data) {
            var arr;
            if (data == null) {
                arr = [];
            }
            else {
                arr = data.split(',');
            }
            arr = arr.filter(function (item, index, inputArray) {
                return inputArray.indexOf(item) == index;
            });
            $('#txtAddProblemTags').magicSuggest({
                data: arr
            });
        },
        error: function (response) {
            alert(response.responseText);
        },
        failure: function (response) {
            alert(response.responseText);
        }
    });
}

function BindDropDownValues(medicineDoseSimulationDto, methodType) {

    var optionValue = "";
    if (methodType == "Add") {
        $('#ddlAddDoctor').empty();
        $('#ddlAddFrequency').empty();
        $('#ddlAddDirections').empty();
        $('#ddlAddDuration').empty();
        $('#ddlAddDoseUnit').empty();
        $('#ddlAddDose').empty();
        if (medicineDoseSimulationDto.doctorDtoList.length > 0) {
            for (var i = 0; i < medicineDoseSimulationDto.doctorDtoList.length; i++) {
                optionValue = medicineDoseSimulationDto.doctorDtoList[i].doctorId;
                optionText = medicineDoseSimulationDto.doctorDtoList[i].doctorName;
                $('#ddlAddDoctor').append(`<option value="${optionValue}">${optionText}</option>`);
            }
        }
        if (medicineDoseSimulationDto.frequencyList.length > 0) {
            for (var i = 0; i < medicineDoseSimulationDto.frequencyList.length; i++) {
                optionValue = medicineDoseSimulationDto.frequencyList[i];
                $('#ddlAddFrequency').append(`<option value="${optionValue}">${optionValue}</option>`);
            }
        }
        if (medicineDoseSimulationDto.directionList.length > 0) {
            for (var i = 0; i < medicineDoseSimulationDto.directionList.length; i++) {
                optionValue = medicineDoseSimulationDto.directionList[i];
                $('#ddlAddDirections').append(`<option value="${optionValue}">${optionValue}</option>`);
            }
        }
        if (medicineDoseSimulationDto.durationList.length > 0) {
            for (var i = 0; i < medicineDoseSimulationDto.durationList.length; i++) {
                optionValue = medicineDoseSimulationDto.durationList[i];
                $('#ddlAddDuration').append(`<option value="${optionValue}">${optionValue}</option>`);
            }
        }
        if (medicineDoseSimulationDto.doseUnitList.length > 0) {
            for (var i = 0; i < medicineDoseSimulationDto.doseUnitList.length; i++) {
                optionValue = medicineDoseSimulationDto.doseUnitList[i];
                $('#ddlAddDoseUnit').append(`<option value="${optionValue}">${optionValue}</option>`);
            }
        }
        if (medicineDoseSimulationDto.doseList.length > 0) {
            for (var i = 0; i < medicineDoseSimulationDto.doseList.length; i++) {
                optionValue = medicineDoseSimulationDto.doseList[i];
                $('#ddlAddDose').append(`<option value="${optionValue}">${optionValue}</option>`);
            }
        }
    }
    else if (methodType == "Edit") {
        $('#ddlEditDoctor').empty();
        $('#ddlEditFrequency').empty();
        $('#ddlEditDirections').empty();
        $('#ddlEditDuration').empty();
        $('#ddlEditDoseUnit').empty();
        $('#ddlEditDose').empty();
        if (medicineDoseSimulationDto.doctorDtoList.length > 0) {
            for (var i = 0; i < medicineDoseSimulationDto.doctorDtoList.length; i++) {
                optionValue = medicineDoseSimulationDto.doctorDtoList[i].doctorId;
                optionText = medicineDoseSimulationDto.doctorDtoList[i].doctorName;
                $('#ddlEditDoctor').append(`<option value="${optionValue}">${optionText}</option>`);
            }
        }
        if (medicineDoseSimulationDto.frequencyList.length > 0) {
            for (var i = 0; i < medicineDoseSimulationDto.frequencyList.length; i++) {
                optionValue = medicineDoseSimulationDto.frequencyList[i];
                $('#ddlEditFrequency').append(`<option value="${optionValue}">${optionValue}</option>`);
            }
        }
        if (medicineDoseSimulationDto.directionList.length > 0) {
            for (var i = 0; i < medicineDoseSimulationDto.directionList.length; i++) {
                optionValue = medicineDoseSimulationDto.directionList[i];
                $('#ddlEditDirections').append(`<option value="${optionValue}">${optionValue}</option>`);
            }
        }
        if (medicineDoseSimulationDto.durationList.length > 0) {
            for (var i = 0; i < medicineDoseSimulationDto.durationList.length; i++) {
                optionValue = medicineDoseSimulationDto.durationList[i];
                $('#ddlEditDuration').append(`<option value="${optionValue}">${optionValue}</option>`);
            }
        }
        if (medicineDoseSimulationDto.doseUnitList.length > 0) {
            for (var i = 0; i < medicineDoseSimulationDto.doseUnitList.length; i++) {
                optionValue = medicineDoseSimulationDto.doseUnitList[i];
                $('#ddlEditDoseUnit').append(`<option value="${optionValue}">${optionValue}</option>`);
            }
        }
        if (medicineDoseSimulationDto.doseList.length > 0) {
            for (var i = 0; i < medicineDoseSimulationDto.doseList.length; i++) {
                optionValue = medicineDoseSimulationDto.doseList[i];
                $('#ddlEditDose').append(`<option value="${optionValue}">${optionValue}</option>`);
            }
        }
    }
}

function validateAddDoctorMedicineDose() {

    var isValid = true;

    if ($("#txtAddMedicineName").val().trim().length == 0) {
        $("#txtAddMedicineNameError").text("Please enter the medicine name.");
        isValid = false;
    }
    else {
        $("#txtAddMedicineNameError").text("");
    }

    if ($("#ddlAddDoctor").val().trim() == "0") {
        $("#ddlAddDoctorError").text("Please select the doctor.");
        isValid = false;
    }
    else {
        $("#ddlAddDoctorError").text("");
    }

    if ($("#ddlAddFrequency").val().trim() == "Select Frequency") {
        $("#ddlAddFrequncyError").text("Please select the frequency.");
        isValid = false;
    }
    else {
        $("#ddlAddFrequncyError").text("");
    }

    if ($("#ddlAddDirections").val().trim() == "Select Direction") {
        $("#ddlAddDirectionsError").text("Please enter the directions.");
        isValid = false;
    }
    else {
        $("#ddlAddDirectionsError").text("");
    }

    if ($("#txtAddComposition").val().trim().length == 0) {
        $("#txtAddCompositionError").text("Please enter the Composition.");
        isValid = false;
    }
    else {
        $("#txtAddCompositionError").text("");
    }

    if ($("#ddlAddDuration").val().trim() == "Select Duration") {
        $("#ddlAddDurationError").text("Please enter the duration.");
        isValid = false;
    }
    else {
        $("#ddlAddDurationError").text("");
    }

    if ($("#ddlAddDoseUnit").val().trim() == "Select Dose Unit") {
        $("#ddlAddDoseUnitError").text("Please enter the dose Unit.");
        isValid = false;
    }
    else {
        $("#ddlAddDoseUnitError").text("");
    }

    if ($("#ddlAddDose").val().trim() == "Select Dose") {
        $("#ddlAddDoseError").text("Please enter the dose.");
        isValid = false;
    }
    else {
        $("#ddlAddDoseError").text("");
    }

    return isValid;
}

function EditDoctorMedicineDose(medicineId) {

    $.ajax({
        type: "GET",
        url: '/MedicineDose/EditDoctorMedicineDose',
        data: { medicineId: medicineId },
        success: function (medicineDoseSimulationDto) {
            if (medicineDoseSimulationDto != null) {
                BindDropDownValues(medicineDoseSimulationDto, "Edit");
                BindEditProblemTags(medicineDoseSimulationDto.medicineDoseDto.problemTags);
                $('#txtEditMedicineId').val(medicineDoseSimulationDto.medicineDoseDto.medicineId);
                $('#ddlEditDoctor').val(medicineDoseSimulationDto.medicineDoseDto.doctorId);
                $('#txtEditMedicineName').val(medicineDoseSimulationDto.medicineDoseDto.medicineName);
                $('#ddlEditFrequency').val(medicineDoseSimulationDto.medicineDoseDto.frequency);
                $('#ddlEditDirections').val(medicineDoseSimulationDto.medicineDoseDto.directions);
                $('#txtEditComposition').val(medicineDoseSimulationDto.medicineDoseDto.composition);
                $('#ddlEditDuration').val(medicineDoseSimulationDto.medicineDoseDto.duration);
                $('#ddlEditDoseUnit').val(medicineDoseSimulationDto.medicineDoseDto.doseUnit);
                $('#ddlEditDose').val(medicineDoseSimulationDto.medicineDoseDto.dose);
                $("#EditDoctorMedicineModal").modal({ "backdrop": "static" });
            }
        },
        error: function (response) {
            alert(response.responseText);
        },
        failure: function (response) {
            alert(response.responseText);
        }
    });
}

function BindAddGetProblemTags(problemtags) {

    $.ajax({
        type: "GET",
        url: '/MedicineDose/DoctorProblemTags/',
        success: function (data) {
            var arr;
            if (data == null) {
                arr = [];
            }
            else {
                arr = data.split(',');
            }
            arr = arr.filter(function (item, index, inputArray) {
                return inputArray.indexOf(item) == index;
            });
            $('#txtAddProblemTags').magicSuggest({
                data: arr
            });
            $("#txtAddProblemTags div.ms-sel-ctn").empty();
            var arr = problemtags.split(",");

            $("#txtAddProblemTags div.ms-sel-ctn").append('<div class="none" style="display:none;"></div>')
            $("#txtAddProblemTags div.ms-sel-ctn input[type='text']").removeAttr('placeholder').width('0')
            for (var i = 0; i < arr.length; i++) {
                var problemTag = arr[i];
                $("#txtAddProblemTags div.ms-sel-ctn").append('<div class="ms-sel-item "><span class="ms-close-btn"></span>' + problemTag + '</div>');
                $("#txtAddProblemTags div.ms-sel-ctn div[style='display: none;']").append('<input type="hidden" value="' + problemTag + '">');
            }

        },
        error: function (response) {
            alert(response.responseText);
        },
        failure: function (response) {
            alert(response.responseText);
        }
    });

}

function BindEditGetProblemTags(problemtags) {

    $.ajax({
        type: "GET",
        url: '/MedicineDose/DoctorProblemTags/',
        success: function (data) {
            var arr;
            if (data == null) {
                arr = [];
            }
            else {
                arr = data.split(',');
            }
            arr = arr.filter(function (item, index, inputArray) {
                return inputArray.indexOf(item) == index;
            });
            $('#txtEditProblemTags').magicSuggest({
                data: arr
            });
            $("#txtEditProblemTags div.ms-sel-ctn").empty();
            var arr = problemtags.split(",");

            $("#txtEditProblemTags div.ms-sel-ctn").append('<div class="none" style="display:none;"></div>')
            $("#txtEditProblemTags div.ms-sel-ctn input[type='text']").removeAttr('placeholder').width('0')
            for (var i = 0; i < arr.length; i++) {
                var problemTag = arr[i];
                $("#txtEditProblemTags div.ms-sel-ctn").append('<div class="ms-sel-item "><span class="ms-close-btn"></span>' + problemTag + '</div>');
                $("#txtEditProblemTags div.ms-sel-ctn div[style='display: none;']").append('<input type="hidden" value="' + problemTag + '">');
            }

        },
        error: function (response) {
            alert(response.responseText);
        },
        failure: function (response) {
            alert(response.responseText);
        }
    });

}


function BindEditProblemTags(problemtags) {

    $.ajax({
        type: "GET",
        url: '/MedicineDose/DoctorProblemTags/',
        success: function (data) {
            var arr;
            if (data == null) {
                arr = [];
            }
            else {
                arr = data.split(',');
            }
            arr = arr.filter(function (item, index, inputArray) {
                return inputArray.indexOf(item) == index;
            });
            $('#txtEditProblemTags').magicSuggest({
                data: arr
            });
            $("#txtEditProblemTags div.ms-sel-ctn").empty();
            var arr = problemtags.split(",");

            $("#txtEditProblemTags div.ms-sel-ctn").append('<div class="none" style="display:none;"></div>')
            $("#txtEditProblemTags div.ms-sel-ctn input[type='text']").removeAttr('placeholder').width('0')
            for (var i = 0; i < arr.length; i++) {
                var problemTag = arr[i];
                $("#txtEditProblemTags div.ms-sel-ctn").append('<div class="ms-sel-item "><span class="ms-close-btn"></span>' + problemTag + '</div>');
                $("#txtEditProblemTags div.ms-sel-ctn div[style='display: none;']").append('<input type="hidden" value="' + problemTag + '">');
            }

        },
        error: function (response) {
            alert(response.responseText);
        },
        failure: function (response) {
            alert(response.responseText);
        }
    });
}

function validateEditDoctorMedicineDose() {

    var isValid = true;

    if ($("#txtEditMedicineName").val().trim().length == 0) {
        $("#txtEditMedicineNameError").text("Please enter the medicine name.");
        isValid = false;
    }
    else {
        $("#txtEditMedicineNameError").text("");
    }

    if ($("#ddlEditDoctor").val().trim() == "Select Doctor") {
        $("#ddlEditDoctorError").text("Please select the doctor.");
        isValid = false;
    }
    else {
        $("#ddlEditDoctorError").text("");
    }

    if ($("#ddlEditFrequency").val().trim() == "Select Frequency") {
        $("#ddlEditFrequncyError").text("Please select the frequency.");
        isValid = false;
    }
    else {
        $("#ddlEditFrequncyError").text("");
    }

    if ($("#ddlEditDirections").val().trim() == "Select Direction") {
        $("#ddlEditDirectionsError").text("Please enter the directions.");
        isValid = false;
    }
    else {
        $("#ddlEditDirectionsError").text("");
    }

    if ($("#txtEditComposition").val().trim().length == 0) {
        $("#txtEditCompositionError").text("Please enter the Composition.");
        isValid = false;
    }
    else {
        $("#txtEditCompositionError").text("");
    }

    if ($("#ddlEditDuration").val().trim() == "Select Duration") {
        $("#ddlEditDurationError").text("Please enter the duration.");
        isValid = false;
    }
    else {
        $("#ddlEditDurationError").text("");
    }

    if ($("#ddlEditDoseUnit").val().trim() == "Select Dose Unit") {
        $("#ddlEditDoseUnitError").text("Please enter the dose Unit.");
        isValid = false;
    }
    else {
        $("#ddlEditDoseUnitError").text("");
    }

    if ($("#ddlEditDose").val().trim() == "Select Dose") {
        $("#ddlEditDoseError").text("Please enter the dose.");
        isValid = false;
    }
    else {
        $("#ddlEditDoseError").text("");
    }

    return isValid;
}

