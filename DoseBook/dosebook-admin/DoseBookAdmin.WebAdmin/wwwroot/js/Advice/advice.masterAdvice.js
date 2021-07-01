var counter = 1;
$(document).ready(function () {

    // DataTable for Admin users
    var adviceTable = $('#tblMasterAdvices').DataTable({

        "pageLength": 10
    });

    $(".dataTables_empty").text("There are no master advices yet.");

    // Search on Admin user Grid using Search Text box
    $('#txtSearchMasterAdviceList').keyup(function () {
        adviceTable.search($(this).val()).draw();
    });

    var rowsCount = $('#tblMasterAdvices tbody tr').length;
    //alert(rowsCount);
    if (rowsCount < 10) {
        $("#tblMasterAdvices_paginate").hide();
    }
    else {
        $("#tblMasterAdvices_paginate").show();
    }

    if (rowsCount <= 1) {
        $("#btnDeleteAllCheckedMasterAdvice").hide();
        //$(".table_checkbox").hide();
    }
    else {
        $("#btnDeleteAllCheckedMasterAdvice").show();
        // $(".table_checkbox").show();
    }

    // Hide un-neccessary controls of datatable plugin for Doctor tables
    $('.dataTables_length').hide();
    $('.dataTables_filter').hide();
    $('.dataTables_info').hide();

    // Delete all checked user on top delete button click
    $("#btnDeleteAllCheckedMasterAdvice").on("click", function () {

        var targetIds = {};
        targetIds.ItemIds = [];

        $('#tblMasterAdvices').find('input[type="checkbox"]:checked').each(function () {

            var row = $(this).parents('tr')[0];;
            var userId = parseInt($(row).find('span').text());
            targetIds.ItemIds.push(userId);
        });

        if (targetIds.ItemIds.length > 0) {
            swal({
                title: 'Are you sure you want to delete these master advices?',
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
                    masterAdviceDataService.deleteMasterAdvices("/Advice/DeleteMasterAdvice", targetIds, deletedMasterAdviceCallbackAllMasterAdvice);
                }
            });
        }

        else {
            swal('Please check at least one master advice to delete using the checkboxes.');
        }

    });

    // Delete User Confirnation Modal Popup

    $('#tblMasterAdvices').on('click', '.masterAdviceItem', function () {
        // Find the targeted User Id that will be deleted
        var row = $(this).parents('tr')[0];;
        var userId = parseInt($(row).find('span').text());
        var targetIds = {};
        targetIds.ItemIds = [];
        targetIds.ItemIds.push(userId);

        swal({
            title: 'Are you sure you want to delete this master advice?',
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
                masterAdviceDataService.deleteMasterAdvices("/Advice/DeleteMasterAdvice", targetIds, deletedMasterAdviceCallback);
            }
        });
    });

    // All data communication will be happened using this doctorDataService object from server
    var masterAdviceDataService = new function () {
        deleteMasterAdvices = function (url, users, callback) {
            $.post(url, { targetIds: users }, function (data) { callback(data) });
        }
        return {
            deleteMasterAdvices: deleteMasterAdvices
        };
    }();

    // Callback function of Deleted Users
    var deletedMasterAdviceCallback = function (data) {
        console.table(data);
        swal(
            'The master advice has been successfully deleted.',
            '',
            'success'
        ).then(function () {
            window.location.href = "/Advice/MasterAdvice";
        });
    }

    // Callback function of Deleted Users
    var deletedMasterAdviceCallbackAllMasterAdvice = function (data) {
        console.table(data);
        swal(
            'The master advices have been successfully deleted.',
            '',
            'success'
        ).then(function () {
            window.location.href = "/Advice/MasterAdvice";
        });
    }


    $('#Save_MasterAdvice_Btn').hide();

    $('#AddRow').unbind().click(function () {

        if ($('#tblMasterAdvices  tr.NewAddedRow').length == 0)
            counter = 1;

        $('#tblMasterAdvices tbody:last-child').append(
            '<tr id="tablerow' + counter + '" class="NewAddedRow">' +
            '<td style="width: 70px;">' +
            '</td>' +
            '<td>' +
            '<input type="text" class="text-box single-line" id="Description' + counter + '"  required="required" />' +
            '<span class="error_label" style="display:none;" id="DescriptionError' + counter + '"></span>' +
            '</td>' +
            '<td colspan=2>' +
            '<button type="button" class="btn-new btn-primary" onclick="removeTr(' + counter + ');">Remove Row</button>' +
            '</td>' +
            '</tr>')

        $('#Save_MasterAdvice_Btn').show();

        $("#Description" + counter).autocomplete({
            source: function (request, response) {

                $.ajax({
                    type: "GET",
                    url: '/Advice/AdviceAutoComplete/',
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
                $("#Description").val(i.item.val);
            },
            minLength: 3
        });

        $("#Description" + counter).change(function () {

            var record = counter - 1;

            $.get('/Advice/ValidateAdviceExistOrNot/', { description: $("#Description" + record).val(), doctorId: "undefined", id: 0 },
                function (data) {
                    $('#DescriptionError' + record).hide();
                    $('#DescriptionError' + record).html("");
                    if (data != true) {
                        $('#DescriptionError' + record).show();
                        $('#DescriptionError' + record).html(data);
                    }
                });
        });

        counter++;
    });

    $('#Save_MasterAdvice_Btn').unbind().click(function () {

        if (!validateAddMasterAdviceList()) {
            return false;
        }
        else {

            var masterAdviceList = [];

            $('#tblMasterAdvices tr.NewAddedRow').each(function (index) {

                var masterAdvice = {};

                masterAdvice.Description = $('#Description' + (index + 1)).val();
                masterAdvice.Type = "Master";
                masterAdviceList.push(masterAdvice);
            });

            $.ajax({
                type: "POST",
                url: '/Advice/AddMasterAdviceList/',
                data: { masterAdviceList: masterAdviceList },
                success: function () {
                    swal(
                        'The master advice has been successfully added.',
                        '',
                        'success'
                    ).then(function () {
                        window.location.href = "/Advice/MasterAdvice";
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

function validateAddMasterAdviceList() {

    var valid = true;

    $('#tblMasterAdvices tr.NewAddedRow').each(function (index, tr) {

        var description = $('#Description' + (index + 1));
        var descriptionError = $('#DescriptionError' + (index + 1));



        descriptionError.hide();
        descriptionError.html("");

        if (description.val() == undefined || description.val() == "") {
            descriptionError.show();
            descriptionError.html("Please enter the description.")
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

    $('#tblMasterAdvices > tbody > tr').eq(rowIndex - 1).after(
        '<tr id="tablerow' + rowIndex + '" class="NewAddedRow">' +
        '<td style="width: 70px;">' +
        '</td>' +
        '<td>' +
        '<input type="text" class="text-box single-line" id="Description_Edit' + id + '"  required="required" />' +
        '<span class="error_label" style="display:none;" id="DescriptionError_Edit' + id + '"></span>' +
        '</td>' +
        '<td>' +
        '<button type="button" class="btn-new btn-primary" onclick="UpdateRowData(' + id + ',' + rowIndex + ')">Update</button>' +
        '</td>' +
        '<td>' +
        '<button type="button" class="btn-new btn-primary" onclick="removeTr(' + rowIndex + ');">Cancel</button>' +
        '</td>' +
        '</tr>');

    $("#Description_Edit" + id).autocomplete({
        source: function (request, response) {

            $.ajax({
                type: "GET",
                url: '/Advice/AdviceAutoComplete/',
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
            $("#Description_Edit" + id).val(i.item.val);
        },
        minLength: 3
    });

    $("#Description_Edit" + id).change(function () {

        debugger;
        var record = counter - 1;

        $.get('/Advice/ValidateAdviceExistOrNot/', { description: $("#Description_Edit" + id).val(), doctorId: "undefined", id: id },
            function (data) {
                $('#DescriptionError' + record).hide();
                $('#DescriptionError' + record).html("");
                if (data != true) {
                    $('#DescriptionError' + record).show();
                    $('#DescriptionError' + record).html(data);
                }
            });
    });

    var description = $('#tblMasterAdvices tr').eq(rowIndex).find('td').eq(1).html().trim();

    $('#Description_Edit' + id).val(description);

    counter++;
}

function UpdateRowData(id) {
    if (!validateEditMasterAdviceList(id)) {
        return false;
    }
    else {

        var masterAdvice = {};
        masterAdvice.Id = id;
        masterAdvice.Description = $('#Description_Edit' + id).val();
        masterAdvice.Type = "Master";


        $.ajax({
            type: "POST",
            url: '/Advice/UpdateMasterAdvice/',
            data: { masterAdvice: masterAdvice },
            success: function (data) {
                swal(
                    'The master advice has been successfully updated.',
                    '',
                    'success'
                ).then(function () {
                    window.location.href = "/Advice/MasterAdvice";
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

function validateEditMasterAdviceList(id) {

    var valid = true;

    var description = $('#Description_Edit' + id);
    var descriptionError = $('#DescriptionError_Edit' + id);

    descriptionError.hide();
    descriptionError.html("");

    if (description.val() == undefined || description.val() == "") {
        descriptionError.show();
        descriptionError.html("Please enter the description.")
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
        $('#Save_MasterAdvice_Btn').hide();
    }
    return false;
}




