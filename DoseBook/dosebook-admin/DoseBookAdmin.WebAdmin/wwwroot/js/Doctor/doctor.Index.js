var counter = 1;
$(document).ready(function () {

    // DataTable for Admin users
    var adviceTable = $('#tblDoctors').DataTable({

        "pageLength": 10
    });

    $(".dataTables_empty").text("There are no doctors yet.");

    // Search on Admin user Grid using Search Text box
    $('#txtSearchDoctorList').keyup(function () {
        adviceTable.search($(this).val()).draw();
    });

    var rowsCount = $('#tblDoctors tbody tr').length;
    //alert(rowsCount);
    if (rowsCount < 10) {
        $("#tblDoctors_paginate").hide();
    }
    else {
        $("#tblDoctors_paginate").show();
    }

    if (rowsCount <= 1) {
        $("#btnDeleteAllCheckedDoctor").hide();
        //$(".table_checkbox").hide();
    }
    else {
        $("#btnDeleteAllCheckedDoctor").show();
        // $(".table_checkbox").show();
    }

    // Hide un-neccessary controls of datatable plugin for Doctor tables
    $('.dataTables_length').hide();
    $('.dataTables_filter').hide();
    $('.dataTables_info').hide();

    // Delete all checked user on top delete button click
    $("#btnDeleteAllCheckedDoctor").on("click", function () {

        var targetIds = {};
        targetIds.ItemIds = [];

        $('#tblDoctors').find('input[type="checkbox"]:checked').each(function () {

            var row = $(this).parents('tr')[0];;
            var userId = parseInt($(row).find('span').text());
            targetIds.ItemIds.push(userId);
        });

        if (targetIds.ItemIds.length > 0) {
            swal({
                title: 'Are you sure you want to delete these doctors?',
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
                    doctorDataService.deleteDoctors("/Doctor/DeleteDoctor", targetIds, deletedDoctorCallbackAllDoctor);
                }
            });
        }

        else {
            swal('Please check at least one doctor to delete using the checkboxes.');
        }

    });

    // Delete User Confirnation Modal Popup

    $('#tblDoctors').on('click', '.doctorItem', function () {
        // Find the targeted User Id that will be deleted
        var row = $(this).parents('tr')[0];;
        var userId = parseInt($(row).find('span').text());
        var targetIds = {};
        targetIds.ItemIds = [];
        targetIds.ItemIds.push(userId);

        swal({
            title: 'Are you sure you want to delete this doctor?',
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
                doctorDataService.deleteDoctors("/Doctor/DeleteDoctor", targetIds, deletedDoctorCallback);
            }
        });
    });

    // All data communication will be happened using this doctorDataService object from server
    var doctorDataService = new function () {
        deleteDoctors = function (url, users, callback) {
            $.post(url, { targetIds: users }, function (data) { callback(data) });
        }
        return {
            deleteDoctors: deleteDoctors
        };
    }();

    // Callback function of Deleted Users
    var deletedDoctorCallback = function (data) {
        console.table(data);
        swal(
            'The doctor has been successfully deleted.',
            '',
            'success'
        ).then(function () {
            window.location.href = "/Doctor/Index";
        });
    }

    // Callback function of Deleted Users
    var deletedDoctorCallbackAllDoctor = function (data) {
        console.table(data);
        swal(
            'The doctors have been successfully deleted.',
            '',
            'success'
        ).then(function () {
            window.location.href = "/Doctor/Index";
        });
    }


    $('#Save_Doctor_Btn').hide();

    $('#AddRow').unbind().click(function () {

        if ($('#tblDoctors  tr.NewAddedRow').length == 0)
            counter = 1;

        $('#tblDoctors tbody:last-child').append(
            '<tr id="tablerow' + counter + '" class="NewAddedRow">' +
            '<td style="width: 70px;">' +
            '</td>' +
            '<td>' +
            '<input type="text" class="text-box single-line" id="DoctorName' + counter + '"  required="required" />' +
            '<span class="error_label" style="display:none;" id="DoctorNameError' + counter + '"></span>' +
            '</td>' +
            '<td>' +
            '<input type="text" class="text-box single-line" id="DoctorEmail' + counter + '"  required="required" />' +
            '<span class="error_label" style="display:none;" id="DoctorEmailError' + counter + '"></span>' +
            '</td>' +
            '<td>' +
            '<input type="text" class="text-box single-line" id="DoctorTelephoneNo' + counter + '"  required="required" />' +
            '<span class="error_label" style="display:none;" id="DoctorTelephoneNoError' + counter + '"></span>' +
            '</td>' +
            '<td colspan=2>' +
            '<button type="button" class="btn-new btn-primary" onclick="removeTr(' + counter + ');">Remove Row</button>' +
            '</td>' +
            '</tr>')

        $('#Save_Doctor_Btn').show();

        $("#DoctorName" + counter).autocomplete({
            source: function (request, response) {

                $.ajax({
                    type: "GET",
                    url: '/Doctor/DoctorNameAutoComplete/',
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
                $("#DoctorName" + counter).val(i.item.val);
            },
            minLength: 3
        });

        $("#DoctorName" + counter).change(function () {

            var record = counter - 1;

            $.get('/Doctor/ValidateDoctorNameExistOrNot/', { doctorName: $("#DoctorName" + record).val(), id: 0 },
                function (data) {
                    $('#DoctorNameError' + record).hide();
                    $('#DoctorNameError' + record).html("");
                    if (data != true) {
                        $('#DoctorNameError' + record).show();
                        $('#DoctorNameError' + record).html(data);
                    }
                });
        });

        counter++;
    });

    $('#Save_Doctor_Btn').unbind().click(function () {

        if (!validateAddDoctorList()) {
            return false;
        }
        else {

            var doctorList = [];

            $('#tblDoctors tr.NewAddedRow').each(function (index) {

                var doctor = {};

                doctor.DoctorName = $('#DoctorName' + (index + 1)).val();
                doctor.DoctorEmail = $('#DoctorEmail' + (index + 1)).val();
                doctor.TelephoneNumber = $('#DoctorTelephoneNo' + (index + 1)).val();
                doctorList.push(doctor);
            });

            $.ajax({
                type: "POST",
                url: '/Doctor/AddDoctorList/',
                data: { doctorList: doctorList },
                success: function () {
                    swal(
                        'The doctor has been successfully added.',
                        '',
                        'success'
                    ).then(function () {
                        window.location.href = "/Doctor/Index";
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

function validateAddDoctorList() {

    var valid = true;

    $('#tblDoctors tr.NewAddedRow').each(function (index, tr) {

        var doctorName = $('#DoctorName' + (index + 1));
        var doctorNameError = $('#DoctorNameError' + (index + 1));
        var doctorEmail = $('#DoctorEmail' + (index + 1));
        var doctorEmailError = $('#DoctorEmailError' + (index + 1));
        var doctorTelephoneNo = $('#DoctorTelephoneNo' + (index + 1));
        var doctorTelephoneNoError = $('#DoctorTelephoneNoError' + (index + 1));

        doctorNameError.hide();
        doctorNameError.html("");
        doctorEmailError.hide();
        doctorEmailError.html("");
        doctorTelephoneNoError.hide();
        doctorTelephoneNoError.html("");

        if (doctorName.val() == undefined || doctorName.val() == "") {
            doctorNameError.show();
            doctorNameError.html("Please enter the doctor name.")
            valid = false;
        }

        if (doctorEmail.val() == undefined || doctorEmail.val() == "") {
            doctorEmailError.show();
            doctorEmailError.html("Please enter the doctor email.")
            valid = false;
        }
        if (doctorTelephoneNo.val() == undefined || doctorTelephoneNo.val() == "") {
            doctorTelephoneNoError.show();
            doctorTelephoneNoError.html("Please enter the doctor telephoen number.")
            valid = false;
        }
    });

    return valid;
}

function EditRedirect(id, rowIndex) {

    var cuurentPageNumber = parseInt($('.paginate_button.current').html());
    if (cuurentPageNumber > 1) {
        rowIndex = rowIndex - 10 * (cuurentPageNumber - 1);
    }

    $('#tblDoctors > tbody > tr').eq(rowIndex - 1).after(
        '<tr id="tablerow' + rowIndex + '" class="NewAddedRow">' +
        '<td style="width: 70px;">' +
        '</td>' +
        '<td>' +
        '<input type="text" class="text-box single-line" id="DoctorName_Edit' + id + '"  required="required" />' +
        '<span class="error_label" style="display:none;" id="DoctorNameError_Edit' + id + '"></span>' +
        '</td>' +
        '<td>' +
        '<input type="text" class="text-box single-line" id="DoctorEmail_Edit' + id + '"  required="required" />' +
        '<span class="error_label" style="display:none;" id="DoctorEmailError_Edit' + id + '"></span>' +
        '</td>' +
        '<td>' +
        '<input type="text" class="text-box single-line" id="DoctorTelephoneNo_Edit' + id + '"  required="required" />' +
        '<span class="error_label" style="display:none;" id="DoctorTelephoneNoError_Edit' + id + '"></span>' +
        '</td>' +
        '<td>' +
        '<button type="button" class="btn-new btn-primary" onclick="UpdateRowData(' + id + ',' + rowIndex + ')">Update</button>' +
        '</td>' +
        '<td>' +
        '<button type="button" class="btn-new btn-primary" onclick="removeTr(' + rowIndex + ');">Cancel</button>' +
        '</td>' +
        '</tr>');

    $("#DoctorName_Edit" + id).autocomplete({
        source: function (request, response) {

            $.ajax({
                type: "GET",
                url: '/Doctor/DoctorNameAutoComplete/',
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
            $("#DoctorName_Edit" + id).val(i.item.val);
        },
        minLength: 3
    });

    $("#DoctorName_Edit" + id).change(function () {

        var record = counter - 1;

        $.get('/Doctor/ValidateDoctorNameExistOrNot/', { doctorName: $("#DoctorName_Edit" + id).val(), id: id },
            function (data) {
                $('#DoctorNameError_Edit' + record).hide();
                $('#DoctorNameError_Edit' + record).html("");
                if (data != true) {
                    $('#DoctorNameError_Edit' + record).show();
                    $('#DoctorNameError_Edit' + record).html(data);
                }
            });
    });

    var doctorName = $('#tblDoctors tr').eq(rowIndex).find('td').eq(1).html().trim();
    var doctorEmail = $('#tblDoctors tr').eq(rowIndex).find('td').eq(2).html().trim();
    var doctorTelephoneNo = $('#tblDoctors tr').eq(rowIndex).find('td').eq(3).html().trim();

    $('#DoctorName_Edit' + id).val(doctorName);
    $('#DoctorEmail_Edit' + id).val(doctorEmail);
    $('#DoctorTelephoneNo_Edit' + id).val(doctorTelephoneNo);

    counter++;
}

function UpdateRowData(id) {
    if (!validateEditDoctorList(id)) {
        return false;
    }
    else {

        var doctor = {};
        doctor.DoctorId = id;
        doctor.DoctorName = $('#DoctorName_Edit' + id).val();
        doctor.DoctorEmail = $('#DoctorEmail_Edit' + id).val();
        doctor.TelephoneNumber = $('#DoctorTelephoneNo_Edit' + id).val();

        $.ajax({
            type: "POST",
            url: '/Doctor/UpdateDoctor/',
            data: { doctor: doctor },
            success: function (data) {
                swal(
                    'The doctor has been successfully updated.',
                    '',
                    'success'
                ).then(function () {
                    window.location.href = "/Doctor/Index";
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
}

function validateEditDoctorList(id) {

    var valid = true;

    var doctorName = $('#DoctorName_Edit' + id);
    var doctorNameError = $('#DoctorNameError_Edit' + id);
    var doctorEmail = $('#DoctorEmail_Edit' + id);
    var doctorEmailError = $('#DoctorEmailError_Edit' + id);
    var doctorTelephoneNo = $('#DoctorTelephoneNo_Edit' + id);
    var doctorTelephoneNoError = $('#DoctorTelephoneNoError_Edit' + id);

    doctorNameError.hide();
    doctorNameError.html("");
    doctorEmailError.hide();
    doctorEmailError.html("");
    doctorTelephoneNoError.hide();
    doctorTelephoneNoError.html("");

    if (doctorName.val() == undefined || doctorName.val() == "") {
        doctorNameError.show();
        doctorNameError.html("Please enter the doctor name.")
        valid = false;
    }

    if (doctorEmail.val() == undefined || doctorEmail.val() == "") {
        doctorEmailError.show();
        doctorEmailError.html("Please enter the doctor email.")
        valid = false;
    }

    if (doctorTelephoneNo.val() == undefined || doctorTelephoneNo.val() == "") {
        doctorTelephoneNoError.show();
        doctorTelephoneNoError.html("Please enter the doctor telephone no.")
        valid = false;
    }

    return valid;
}

function removeTr(index) {
    if (counter > 0) {
        $('#tablerow' + index).remove();
        counter--;
    }
    if (counter == 1) {
        $('#Save_Doctor_Btn').hide();
    }
    return false;
}




