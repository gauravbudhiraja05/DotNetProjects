var counter = 1;
$(document).ready(function () {

    // DataTable for Admin users
    var testTable = $('#tblMasterTests').DataTable({

        "pageLength": 10
    });

    $(".dataTables_empty").text("There are no master tests yet.");

    // Search on Admin user Grid using Search Text box
    $('#txtSearchMasterTestList').keyup(function () {
        testTable.search($(this).val()).draw();
    });

    var rowsCount = $('#tblMasterTests tbody tr').length;
    //alert(rowsCount);
    if (rowsCount < 10) {
        $("#tblMasterTests_paginate").hide();
    }
    else {
        $("#tblMasterTests_paginate").show();
    }

    if (rowsCount <= 1) {
        $("#btnDeleteAllCheckedMasterTest").hide();
        //$(".table_checkbox").hide();
    }
    else {
        $("#btnDeleteAllCheckedMasterTest").show();
        // $(".table_checkbox").show();
    }

    // Hide un-neccessary controls of datatable plugin for Doctor tables
    $('.dataTables_length').hide();
    $('.dataTables_filter').hide();
    $('.dataTables_info').hide();

    // Delete all checked user on top delete button click
    $("#btnDeleteAllCheckedMasterTest").on("click", function () {

        var targetIds = {};
        targetIds.ItemIds = [];

        $('#tblMasterTests').find('input[type="checkbox"]:checked').each(function () {

            var row = $(this).parents('tr')[0];;
            var userId = parseInt($(row).find('span').text());
            targetIds.ItemIds.push(userId);
        });

        if (targetIds.ItemIds.length > 0) {
            swal({
                title: 'Are you sure you want to delete these master tests?',
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
                    masterTestDataService.deleteMasterTests("/Test/DeleteMasterTest", targetIds, deletedMasterTestCallbackAllMasterTest);
                }
            });
        }

        else {
            swal('Please check at least one master test to delete using the checkboxes.');
        }
    });

    // Delete User Confirnation Modal Popup

    $('#tblMasterTests').on('click', '.masterTestItem', function () {
        // Find the targeted User Id that will be deleted
        var row = $(this).parents('tr')[0];;
        var userId = parseInt($(row).find('span').text());
        var targetIds = {};
        targetIds.ItemIds = [];
        targetIds.ItemIds.push(userId);

        swal({
            title: 'Are you sure you want to delete this master test?',
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
                masterTestDataService.deleteMasterTests("/Test/DeleteMasterTest", targetIds, deletedMasterTestCallback);
            }
        });
    });

    // All data communication will be happened using this doctorDataService object from server
    var masterTestDataService = new function () {
        deleteMasterTests = function (url, users, callback) {
            $.post(url, { targetIds: users }, function (data) { callback(data) });
        }
        return {
            deleteMasterTests: deleteMasterTests
        };
    }();

    // Callback function of Deleted Users
    var deletedMasterTestCallback = function (data) {
        console.table(data);
        swal(
            'The master test has been successfully deleted.',
            '',
            'success'
        ).then(function () {
            window.location.href = "/Test/MasterTest";
        });
    }

    // Callback function of Deleted Users
    var deletedMasterTestCallbackAllMasterTest = function (data) {
        console.table(data);
        swal(
            'The master tests have been successfully deleted.',
            '',
            'success'
        ).then(function () {
            window.location.href = "/Test/MasterTest";
        });
    }

    $('#Save_MasterTest_Btn').hide();

    $('#AddRow').unbind().click(function () {

        if ($('#tblMasterTests  tr.NewAddedRow').length == 0)
            counter = 1;

        $('#tblMasterTests tbody:last-child').append(
            '<tr id="tablerow' + counter + '" class="NewAddedRow">' +
            '<td style="width: 70px;">' +
            '</td>' +
            '<td>' +
            '<input type="text" class="text-box single-line" id="Test' + counter + '"  required="required" />' +
            '<span class="error_label" style="display:none;" id="TestError' + counter + '"></span>' +
            '</td>' +
            '<td colspan=2>' +
            '<button type="button" class="btn-new btn-primary" onclick="removeTr(' + counter + ');">Remove Row</button>' +
            '</td>' +
            '</tr>')

        $('#Save_MasterTest_Btn').show();

        $("#Test" + counter).autocomplete({
            source: function (request, response) {

                $.ajax({
                    type: "GET",
                    url: '/Test/TestAutoComplete/',
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
                $("#Test" + counter).val(i.item.val);
            },
            minLength: 3
        });

        $("#Test" + counter).change(function () {

            var record = counter - 1;

            $.get('/Test/ValidateTestExistOrNot/', { test: $("#Test" + record).val(), doctorId: "undefined", id: 0 },
                function (data) {
                    $('#TestError' + record).hide();
                    $('#TestError' + record).html("");
                    if (data != true) {
                        $('#TestError' + record).show();
                        $('#TestError' + record).html(data);
                    }
                });
        });

        counter++;
    });

    $('#Save_MasterTest_Btn').unbind().click(function () {

        if (!validateAddMasterTestList()) {
            return false;
        }
        else {

            var masterTestList = [];

            $('#tblMasterTests tr.NewAddedRow').each(function (index) {

                var masterTest = {};

                masterTest.TestName = $('#Test' + (index + 1)).val();
                masterTest.Type = "Master";
                masterTestList.push(masterTest);
            });

            $.ajax({
                type: "POST",
                url: '/Test/AddMasterTestList/',
                data: { masterTestList: masterTestList },
                success: function () {
                    swal(
                        'The master test has been successfully added.',
                        '',
                        'success'
                    ).then(function () {
                        window.location.href = "/Test/MasterTest";
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


function validateAddMasterTestList() {

    var valid = true;

    $('#tblMasterTests tr.NewAddedRow').each(function (index, tr) {

        var test = $('#Test' + (index + 1));
        var testError = $('#TestError' + (index + 1));



        testError.hide();
        testError.html("");

        if (test.val() == undefined || test.val() == "") {
            testError.show();
            testError.html("Please enter the test.")
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

    $('#tblMasterTests > tbody > tr').eq(rowIndex - 1).after(
        '<tr id="tablerow' + rowIndex + '" class="NewAddedRow">' +
        '<td style="width: 70px;">' +
        '</td>' +
        '<td>' +
        '<input type="text" class="text-box single-line" id="Test_Edit' + id + '"  required="required" />' +
        '<span class="error_label" style="display:none;" id="TestError_Edit' + id + '"></span>' +
        '</td>' +
        '<td>' +
        '<button type="button" class="btn-new btn-primary" onclick="UpdateRowData(' + id + ',' + rowIndex + ')">Update</button>' +
        '</td>' +
        '<td>' +
        '<button type="button" class="btn-new btn-primary" onclick="removeTr(' + rowIndex + ');">Cancel</button>' +
        '</td>' +
        '</tr>');

    $("#Test_Edit" + id).autocomplete({
        source: function (request, response) {

            $.ajax({
                type: "GET",
                url: '/Test/TestAutoComplete/',
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

    $("#Test_Edit" + id).change(function () {

        debugger;
        var record = counter - 1;

        $.get('/Test/ValidateTestExistOrNot/', { test: $("#Test_Edit" + id).val(), doctorId: "undefined", id: id },
            function (data) {
                $('#TestError' + record).hide();
                $('#TestError' + record).html("");
                if (data != true) {
                    $('#TestError' + record).show();
                    $('#TestError' + record).html(data);
                }
            });
    });

    var test = $('#tblMasterTests tr').eq(rowIndex).find('td').eq(1).html().trim();

    $('#Test_Edit' + id).val(test);

    counter++;
}

function UpdateRowData(id) {
    if (!validateEditMasterTestList(id)) {
        return false;
    }
    else {

        var masterTest = {};
        masterTest.Id = id;
        masterTest.TestName = $('#Test_Edit' + id).val();
        masterTest.Type = "Master";


        $.ajax({
            type: "POST",
            url: '/Test/UpdateMasterTest/',
            data: { masterTest: masterTest },
            success: function (data) {
                swal(
                    'The master test has been successfully updated.',
                    '',
                    'success'
                ).then(function () {
                    window.location.href = "/Test/MasterTest";
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

function validateEditMasterTestList(id) {

    var valid = true;

    var test = $('#Test_Edit' + id);
    var testError = $('#TestError_Edit' + id);

    testError.hide();
    testError.html("");

    if (test.val() == undefined || test.val() == "") {
        testError.show();
        testError.html("Please enter the test.")
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
        $('#Save_MasterTest_Btn').hide();
    }
    return false;
}



