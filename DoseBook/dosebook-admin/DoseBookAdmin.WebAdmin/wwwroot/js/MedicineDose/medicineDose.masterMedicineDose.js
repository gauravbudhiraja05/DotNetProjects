$(document).ready(function () {

    // DataTable for Admin users
    var medicineDoseTable = $('#tblMasterMedicineDoses').DataTable({

        "pageLength": 10
    });

    $(".dataTables_empty").text("There are no master medicine doses yet.");

    // Search on Admin user Grid using Search Text box
    $('#txtSearchMasterMasterMedicineDoseList').keyup(function () {
        medicineDoseTable.search($(this).val()).draw();
    });

    var rowsCount = $('#tblMasterMedicineDoses tbody tr').length;
    //alert(rowsCount);
    if (rowsCount < 10) {
        $("#tblMasterMedicineDoses_paginate").hide();
    }
    else {
        $("#tblMasterMedicineDoses_paginate").show();
    }

    if (rowsCount <= 1) {
        $("#btnDeleteAllCheckedMasterMedicineDose").hide();
        //$(".table_checkbox").hide();
    }
    else {
        $("#btnDeleteAllCheckedMasterMedicineDose").show();
        // $(".table_checkbox").show();
    }

    // Hide un-neccessary controls of datatable plugin for Doctor tables
    $('.dataTables_length').hide();
    $('.dataTables_filter').hide();
    $('.dataTables_info').hide();

    // Delete all checked user on top delete button click
    $("#btnDeleteAllCheckedMasterMedicineDose").on("click", function () {

        var targetIds = {};
        targetIds.ItemIds = [];

        $('#tblMasterMedicineDoses').find('input[type="checkbox"]:checked').each(function () {

            var row = $(this).parents('tr')[0];;
            var userId = parseInt($(row).find('span').text());
            targetIds.ItemIds.push(userId);
        });

        if (targetIds.ItemIds.length > 0) {
            swal({
                title: 'Are you sure you want to delete these master medicine doses?',
                text: "",
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes',
                cancelButtonText: 'No',
                width: '500px'
            }).then(function (result) {
                if (result.value) {
                    //  deleteUsers method to delete admin users
                    masterMedicineDoseDataService.deleteMasterMedicineDoses("/MedicineDose/DeleteMasterMedicineDose", targetIds, deletedMasterMedicineDoseCallbackAllMasterMedicineDose);
                }
            });
        }

        else {
            swal('Please check at least one master medicine dose to delete using the checkboxes.');
        }

    });

    // Delete User Confirnation Modal Popup

    $('#tblMasterMedicineDoses').on('click', '.masterMedicineDoseItem', function () {
        // Find the targeted User Id that will be deleted
        var row = $(this).parents('tr')[0];;
        var userId = parseInt($(row).find('span').text());
        var targetIds = {};
        targetIds.ItemIds = [];
        targetIds.ItemIds.push(userId);

        swal({
            title: 'Are you sure you want to delete this master medicine dose?',
            text: "",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes',
            cancelButtonText: 'No'
        }).then(function (result) {

            if (result.value) {
                //  deleteUsers method to delete admin users
                masterMedicineDoseDataService.deleteMasterMedicineDoses("/MedicineDose/DeleteMasterMedicineDose", targetIds, deletedMasterMedicineDoseCallback);
            }
        });
    });

    // All data communication will be happened using this doctorDataService object from server
    var masterMedicineDoseDataService = new function () {
        deleteMasterMedicineDoses = function (url, users, callback) {
            $.post(url, { targetIds: users }, function (data) { callback(data) });
        }
        return {
            deleteMasterMedicineDoses: deleteMasterMedicineDoses
        };
    }();

    // Callback function of Deleted Users
    var deletedMasterMedicineDoseCallback = function (data) {
        console.table(data);
        swal(
            'The master advice has been successfully deleted.',
            '',
            'success'
        ).then(function () {
            window.location.href = "/MedicineDose/MasterMedicineDose";
        });
    }

    // Callback function of Deleted Users
    var deletedMasterMedicineDoseCallbackAllMasterMedicineDose = function (data) {
        console.table(data);
        swal(
            'The master advices have been successfully deleted.',
            '',
            'success'
        ).then(function () {
            window.location.href = "/MedicineDose/MasterMedicineDose";
        });
    }

    $('#btnAddCancel, #btnAddHeaderClose').click(function () {
        $("#AddMasterMedicineModal").modal("hide");
    });

    $("#btnAddMasterMedicineDose").click(function () {
        BindAddMasterMedicineModalData();
    });

    $('#btnAddSave').on("click", function () {

        if (validateAddMasterMedicineDose() == false) {
            return false;
        }
        else {
            var masterMedicineDose = {}

            masterMedicineDose.MedicineName = $('#txtAddMedicineName').val().trim();
            masterMedicineDose.Frequency = $('#ddlAddFrequency').val().trim();
            masterMedicineDose.Directions = $('#ddlAddDirections').val().trim();
            masterMedicineDose.Composition = $('#txtAddComposition').val().trim();
            masterMedicineDose.Duration = $('#ddlAddDuration').val().trim();
            masterMedicineDose.DoseUnit = $('#ddlAddDoseUnit').val().trim();
            masterMedicineDose.Dose = $('#ddlAddDose').val().trim();

            $.ajax({
                type: "POST",
                url: '/MedicineDose/AddMasterMedicineDose',
                data: { masterMedicineDose: masterMedicineDose },
                success: function () {
                    swal(
                        'The master medicine dose has been successfully created.',
                        '',
                        'success'
                    ).then(function () {
                        window.location.href = "/MedicineDose/MasterMedicineDose";
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

        $.get('/MedicineDose/ValidateMedicineExistOrNot/', { medicineName: $("#txtAddMedicineName").val(), doctorId: "undefined", medicineId: 0 },
            function (data) {
                $('#txtAddMedicineNameError').html('')
                if (data != true) {
                    $('#txtAddMedicineNameError').text(data);
                }

            });

        $.get('/MedicineDose/GetMedicineDetailByMedicineName/', { medicineName: $("#txtAddMedicineName").val(), type: "Master" },
            function (medicineDoseDto) {
                $('#txtAddMedicineName').val(medicineDoseDto.medicineName);
                $('#ddlAddFrequency').val(medicineDoseDto.frequency);
                $('#ddlAddDirections').val(medicineDoseDto.directions);
                $('#txtAddComposition').val(medicineDoseDto.composition);
                $('#txtAddMedicineName').val(medicineDoseDto.medicineName);
                $('#ddlAddDuration').val(medicineDoseDto.duration);
                $('#ddlAddDoseUnit').val(medicineDoseDto.doseUnit);
                $('#ddlAddDose').val(medicineDoseDto.dose);
            });
    });

    $('#btnEditCancel, #btnEditHeaderClose').click(function () {
        $("#EditMasterMedicineModal").modal("hide");
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

        $.get('/MedicineDose/ValidateMedicineExistOrNot/', { medicineName: $("#txtEditMedicineName").val(), doctorId: "undefined", medicineId: $('#txtEditMedicineId').val() },
            function (data) {
                $('#txtEditMedicineNameError').html('')
                if (data != true) {
                    $('#txtEditMedicineNameError').text(data);
                }
            });

        $.get('/MedicineDose/GetMedicineDetailByMedicineName/', { medicineName: $("#txtEditMedicineName").val(), type: "Master" },
            function (medicineDoseDto) {
                $('#txtEditMedicineName').val(medicineDoseDto.medicineName);
                $('#ddlEditFrequency').val(medicineDoseDto.frequency);
                $('#ddlEditDirections').val(medicineDoseDto.directions);
                $('#txtEditComposition').val(medicineDoseDto.composition);
                $('#txtEditMedicineName').val(medicineDoseDto.medicineName);
                $('#ddlEditDuration').val(medicineDoseDto.duration);
                $('#ddlEditDoseUnit').val(medicineDoseDto.doseUnit);
                $('#ddlEditDose').val(medicineDoseDto.dose);
            });
    });

    $('#btnEditUpdate').on("click", function () {

        if (validateEditMasterMedicineDose() == false) {
            return false;
        }
        else {
            var masterMedicineDose = {}

            masterMedicineDose.MedicineId = $('#txtEditMedicineId').val().trim();
            masterMedicineDose.MedicineName = $('#txtEditMedicineName').val().trim();
            masterMedicineDose.Frequency = $('#ddlEditFrequency').val().trim();
            masterMedicineDose.Directions = $('#ddlEditDirections').val().trim();
            masterMedicineDose.Composition = $('#txtEditComposition').val().trim();
            masterMedicineDose.Duration = $('#ddlEditDuration').val().trim();
            masterMedicineDose.DoseUnit = $('#ddlEditDoseUnit').val().trim();
            masterMedicineDose.Dose = $('#ddlEditDose').val().trim();

            $.ajax({
                type: "POST",
                url: '/MedicineDose/EditMasterMedicineDose',
                data: { masterMedicineDose: masterMedicineDose },
                success: function () {
                    swal(
                        'The master medicine dose has been successfully updated.',
                        '',
                        'success'
                    ).then(function () {
                        window.location.href = "/MedicineDose/MasterMedicineDose";
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

function BindAddMasterMedicineModalData() {
    $.ajax({
        type: "GET",
        url: '/MedicineDose/AddMasterMedicineDose',
        success: function (medicineDoseSimulationDto) {
            if (medicineDoseSimulationDto != null) {
                BindDropDownValues(medicineDoseSimulationDto, "Add");
                $("#AddMasterMedicineModal").modal({ "backdrop": "static" });
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

function BindDropDownValues(medicineDoseSimulationDto, methodType) {

    var optionValue = "";
    if (methodType == "Add") {
        $('#ddlAddFrequency').empty();
        $('#ddlAddDirections').empty();
        $('#ddlAddDuration').empty();
        $('#ddlAddDoseUnit').empty();
        $('#ddlAddDose').empty();
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
        $('#ddlEditFrequency').empty();
        $('#ddlEditDirections').empty();
        $('#ddlEditDuration').empty();
        $('#ddlEditDoseUnit').empty();
        $('#ddlEditDose').empty();
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

function validateAddMasterMedicineDose() {

    var isValid = true;

    if ($("#txtAddMedicineName").val().trim().length == 0) {
        $("#txtAddMedicineNameError").text("Please enter the medicine name.");
        isValid = false;
    }
    else {
        $("#txtAddMedicineNameError").text("");
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

function EditMasterMedicineDose(medicineId) {

    $.ajax({
        type: "GET",
        url: '/MedicineDose/EditMasterMedicineDose',
        data: { medicineId: medicineId },
        success: function (medicineDoseSimulationDto) {
            if (medicineDoseSimulationDto != null) {
                BindDropDownValues(medicineDoseSimulationDto, "Edit");
                $('#txtEditMedicineId').val(medicineDoseSimulationDto.medicineDoseDto.medicineId);
                $('#txtEditMedicineName').val(medicineDoseSimulationDto.medicineDoseDto.medicineName);
                $('#ddlEditFrequency').val(medicineDoseSimulationDto.medicineDoseDto.frequency);
                $('#ddlEditDirections').val(medicineDoseSimulationDto.medicineDoseDto.directions);
                $('#txtEditComposition').val(medicineDoseSimulationDto.medicineDoseDto.composition);
                $('#txtEditMedicineName').val(medicineDoseSimulationDto.medicineDoseDto.medicineName);
                $('#ddlEditDuration').val(medicineDoseSimulationDto.medicineDoseDto.duration);
                $('#ddlEditDoseUnit').val(medicineDoseSimulationDto.medicineDoseDto.doseUnit);
                $('#ddlEditDose').val(medicineDoseSimulationDto.medicineDoseDto.dose);
                $("#EditMasterMedicineModal").modal({ "backdrop": "static" });
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

function validateEditMasterMedicineDose() {

    var isValid = true;

    if ($("#txtEditMedicineName").val().trim().length == 0) {
        $("#txtEditMedicineNameError").text("Please enter the medicine name.");
        isValid = false;
    }
    else {
        $("#txtEditMedicineNameError").text("");
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



