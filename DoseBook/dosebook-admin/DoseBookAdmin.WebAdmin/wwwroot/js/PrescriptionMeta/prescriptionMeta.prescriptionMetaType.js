var counter = 1;
$(document).ready(function () {

    // DataTable for Admin users
    var adviceTable = $('#tblPrescriptionMetaTypes').DataTable({

        "pageLength": 10
    });

    $(".dataTables_empty").text("There are no prescription meta type yet.");

    // Search on Admin user Grid using Search Text box
    $('#txtSearchPrescriptionMetaTypeList').keyup(function () {
        adviceTable.search($(this).val()).draw();
    });

    var rowsCount = $('#tblPrescriptionMetaTypes tbody tr').length;
    //alert(rowsCount);
    if (rowsCount < 10) {
        $("#tblPrescriptionMetaTypes_paginate").hide();
    }
    else {
        $("#tblPrescriptionMetaTypes_paginate").show();
    }

    if (rowsCount <= 1) {
        $("#btnDeleteAllCheckedPrescriptionMetaType").hide();
        //$(".table_checkbox").hide();
    }
    else {
        $("#btnDeleteAllCheckedPrescriptionMetaType").show();
        // $(".table_checkbox").show();
    }

    // Hide un-neccessary controls of datatable plugin for Doctor tables
    $('.dataTables_length').hide();
    $('.dataTables_filter').hide();
    $('.dataTables_info').hide();

    // Delete all checked user on top delete button click
    $("#btnDeleteAllCheckedMasterPrescriptionMetaType").on("click", function () {

        var targetIds = {};
        targetIds.ItemIds = [];

        $('#tblPrescriptionMetaTypes').find('input[type="checkbox"]:checked').each(function () {

            var row = $(this).parents('tr')[0];;
            var userId = parseInt($(row).find('span').text());
            targetIds.ItemIds.push(userId);
        });

        if (targetIds.ItemIds.length > 0) {
            swal({
                title: 'Are you sure you want to delete these prescription meta types?',
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
                    prescriptionMetaTypeDataService.deletePrescriptionMetaTypes("/PrescriptionMeta/DeletePrescriptionMetaType", targetIds, deletedPrescriptionMetaTypeCallbackAllPrescriptionMetaType);
                }
            });
        }

        else {
            swal('Please check at least one prescription meta type to delete using the checkboxes.');
        }

    });

    // Delete User Confirnation Modal Popup

    $('#tblPrescriptionMetaTypes').on('click', '.prescriptionMetaTypeItem', function () {
        // Find the targeted User Id that will be deleted
        var row = $(this).parents('tr')[0];;
        var userId = parseInt($(row).find('span').text());
        var targetIds = {};
        targetIds.ItemIds = [];
        targetIds.ItemIds.push(userId);

        swal({
            title: 'Are you sure you want to delete this prescription meta type?',
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
                prescriptionMetaTypeDataService.deletePrescriptionMetaTypes("/PrescriptionMeta/DeletePrescriptionMetaType", targetIds, deletedPrescriptionMetaTypeCallback);
            }
        });
    });

    // All data communication will be happened using this doctorDataService object from server
    var prescriptionMetaTypeDataService = new function () {
        deletePrescriptionMetaTypes = function (url, users, callback) {
            $.post(url, { targetIds: users }, function (data) { callback(data) });
        }
        return {
            deletePrescriptionMetaTypes: deletePrescriptionMetaTypes
        };
    }();

    // Callback function of Deleted Users
    var deletedPrescriptionMetaTypeCallback = function (data) {
        console.table(data);
        swal(
            'The prescription meta type has been successfully deleted.',
            '',
            'success'
        ).then(function () {
            window.location.href = "/PrescriptionMeta/PrescriptionMetaType";
        });
    }

    // Callback function of Deleted Users
    var deletedPrescriptionMetaTypeCallbackAllPrescriptionMetaType = function (data) {
        console.table(data);
        swal(
            'The prescription meta types have been successfully deleted.',
            '',
            'success'
        ).then(function () {
            window.location.href = "/PrescriptionMeta/PrescriptionMetaType";
        });
    }


    $('#Save_PrescriptionMetaType_Btn').hide();

    $('#AddRow').unbind().click(function () {

        if ($('#tblPrescriptionMetaTypes  tr.NewAddedRow').length == 0)
            counter = 1;

        $('#tblPrescriptionMetaTypes tbody:last-child').append(
            '<tr id="tablerow' + counter + '" class="NewAddedRow">' +
            '<td style="width: 70px;">' +
            '</td>' +
            '<td>' +
            '<input type="text" class="text-box single-line" id="Type' + counter + '"  required="required" />' +
            '<span class="error_label" style="display:none;" id="TypeError' + counter + '"></span>' +
            '</td>' +
            '<td colspan=2>' +
            '<button type="button" class="btn-new btn-primary" onclick="removeTr(' + counter + ');">Remove Row</button>' +
            '</td>' +
            '</tr>')

        $('#Save_PrescriptionMetaType_Btn').show();

        $("#Type" + counter).autocomplete({
            source: function (request, response) {

                $.ajax({
                    type: "GET",
                    url: '/PrescriptionMeta/TypeAutoComplete/',
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
                $("#Type" + counter).val(i.item.val);
            },
            minLength: 3
        });

        $("#Type" + counter).change(function () {

            var record = counter - 1;

            $.get('/PrescriptionMeta/ValidateTypeExistOrNot/', { type: $("#Type" + record).val(), id: 0 },
                function (data) {
                    $('#TypeError' + record).hide();
                    $('#TypeError' + record).html("");
                    if (data != true) {
                        $('#TypeError' + record).show();
                        $('#TypeError' + record).html(data);
                    }
                });
        });

        counter++;
    });

    $('#Save_PrescriptionMetaType_Btn').unbind().click(function () {

        if (!validateAddPrescriptionMetaTypeList()) {
            return false;
        }
        else {

            var prescriptionMetaTypeList = [];

            $('#tblPrescriptionMetaTypes tr.NewAddedRow').each(function (index) {

                var prescriptionMetaType = {};

                prescriptionMetaType.Type = $('#Type' + (index + 1)).val();
                prescriptionMetaTypeList.push(prescriptionMetaType);
            });

            $.ajax({
                type: "POST",
                url: '/PrescriptionMeta/AddPrescriptionMetaTypeList/',
                data: { prescriptionMetaTypeList: prescriptionMetaTypeList },
                success: function () {
                    swal(
                        'The prescription meta type has been successfully added.',
                        '',
                        'success'
                    ).then(function () {
                        window.location.href = "/PrescriptionMeta/PrescriptionMetaType";
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

function validateAddPrescriptionMetaTypeList() {

    var valid = true;

    $('#tblPrescriptionMetaTypes tr.NewAddedRow').each(function (index, tr) {

        var type = $('#Type' + (index + 1));
        var typeError = $('#TypeError' + (index + 1));



        typeError.hide();
        typeError.html("");

        if (type.val() == undefined || type.val() == "") {
            typeError.show();
            typeError.html("Please enter the prescription type.")
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

    $('#tblPrescriptionMetaTypes > tbody > tr').eq(rowIndex - 1).after(
        '<tr id="tablerow' + rowIndex + '" class="NewAddedRow">' +
        '<td style="width: 70px;">' +
        '</td>' +
        '<td>' +
        '<input type="text" class="text-box single-line" id="Type_Edit' + id + '"  required="required" />' +
        '<span class="error_label" style="display:none;" id="TypeError_Edit' + id + '"></span>' +
        '</td>' +
        '<td>' +
        '<button type="button" class="btn-new btn-primary" onclick="UpdateRowData(' + id + ',' + rowIndex + ')">Update</button>' +
        '</td>' +
        '<td>' +
        '<button type="button" class="btn-new btn-primary" onclick="removeTr(' + rowIndex + ');">Cancel</button>' +
        '</td>' +
        '</tr>');

    $("#Type_Edit" + id).autocomplete({
        source: function (request, response) {

            $.ajax({
                type: "GET",
                url: '/PrescriptionMeta/TypeAutoComplete/',
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
            $("#Type_Edit" + id).val(i.item.val);
        },
        minLength: 3
    });

    $("#Type_Edit" + id).change(function () {

        debugger;
        var record = counter - 1;

        $.get('/PrescriptionMeta/ValidateTypeExistOrNot/', { type: $("#Type_Edit" + id).val(), id: id },
            function (data) {
                $('#TypeError_Edit' + id).hide();
                $('#TypeError_Edit' + id).html("");
                if (data != true) {
                    $('#TypeError_Edit' + id).show();
                    $('#TypeError_Edit' + id).html(data);
                }
            });
    });

    var type = $('#tblPrescriptionMetaTypes tr').eq(rowIndex).find('td').eq(1).html().trim();

    $('#Type_Edit' + id).val(type);

    counter++;
}

function UpdateRowData(id) {
    if (!validateEditPrescriptionMetaTypeList(id)) {
        return false;
    }
    else {

        var prescriptionMetaType = {};
        prescriptionMetaType.Id = id;
        prescriptionMetaType.Type = $('#Type_Edit' + id).val();


        $.ajax({
            type: "POST",
            url: '/PrescriptionMeta/UpdatePrescriptionMetaType/',
            data: { prescriptionMetaType: prescriptionMetaType },
            success: function (data) {
                swal(
                    'The prescription meta type has been successfully updated.',
                    '',
                    'success'
                ).then(function () {
                    window.location.href = "/PrescriptionMeta/PrescriptionMetaType";
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

function validateEditPrescriptionMetaTypeList(id) {

    var valid = true;

    var type = $('#Type_Edit' + id);
    var typeError = $('#TypeError_Edit' + id);

    typeError.hide();
    typeError.html("");

    if (type.val() == undefined || type.val() == "") {
        typeError.show();
        typeError.html("Please enter the prescription type.")
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
        $('#Save_PrescriptionMetaType_Btn').hide();
    }
    return false;
}




