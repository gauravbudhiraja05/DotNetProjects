$(document).ready(function () {
    // DataTable
    //$(".table .table_title").click(function (e) {
    //    $(".table .table_title").not($(this)).removeClass("active");
    //    $(this).addClass("active");
    //    var iconClass = $(this).children(".icon").hasClass("desc") ? 'asc' : 'desc';
    //    $(this).children(".icon").removeClass("asc desc");
    //    $(this).children(".icon").addClass(iconClass);
    //});

    var doseDataListTable = $('#tblDiagnosticTests').DataTable({
        "pageLength": 10
    });

    $(".dataTables_empty").text("There are no diagnostic tests yet.");

    $('#txtSearchDiagnosticTestUp').keyup(function () {
        doseDataListTable.search($(this).val()).draw();
    });

    var rowsCount = $('#tblDiagnosticTests tbody tr').length;
    //alert(rowsCount);
    if (rowsCount < 10) {
        $("#tblDiagnosticTests_paginate").hide();
    }
    else {
        $("#tblDiagnosticTests_paginate").show();
    }

    if (rowsCount <= 1) {
        $("#btnDeleteSelected").hide();
        //$(".table_checkbox").hide();
    }
    else {
        $("#btnDeleteSelected").show();
        // $(".table_checkbox").show();
    }

    $('.dataTables_length').hide();
    $('.dataTables_filter').hide();
    $('.dataTables_info').hide();



    $('#btnAddDiagnosticTest').on('click', function () {
        if ($('#hdnDoctorId').val() != undefined) {
            if (parseInt($('#hdnDoctorId').val()) > 0) { window.location.href = "/DiagnosticTest/AddDiagnosticTest?DoctorId=" + $('#hdnDoctorId').val(); }
            else {
                swal("Please select a doctor to add the diagnostic test.", '', 'error').then(function () {
                    $(".drop_menu").find(".drop_menu_sub").show();
                });
            }
        }
    });

    // Delete all checked user on top delete button click
    $("#btnDeleteSelected").on("click", function () {
        var targetIds = {};
        targetIds.ItemIds = [];

        $('#tblDiagnosticTests').find('input[type="checkbox"]:checked').each(function () {

            var row = $(this).parents('tr')[0];;
            var newsId = parseInt($(row).find('span').text());
            targetIds.ItemIds.push(newsId);
        });

        if (targetIds.ItemIds.length > 0) {
            swal({
                title: 'Are you sure you want to delete these diagnostic tests?',
                text: "",
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes',
                cancelButtonText: 'No'
            }).then(function (result) {

                if (result.value) {
                    //  deleteNews method to delete news
                    diagnosticTestDataService.deleteDiagnosticTests("/DiagnosticTest/DeleteDiagnosticTest", targetIds, deletedDiagnosticTestCallbackAllDiagnosticTest);
                }
            });
        }

        else {
            swal('Please check at least one diagnostic test to delete using the checkboxes.');
        }

    });


    $('#tblDiagnosticTests').on('click', '.diagnosticTestItem', function () {
        var row = $(this).parents('tr')[0];;
        var newsId = parseInt($(row).find('span').text());
        var targetIds = {};
        targetIds.ItemIds = [];
        targetIds.ItemIds.push(newsId);

        swal({
            title: 'Are you sure you want to delete this diagnostic test?',
            text: "",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes',
            cancelButtonText: 'No'
        }).then(function (result) {

            if (result.value) {
                //  deleteNews method to delete news
                diagnosticTestDataService.deleteDiagnosticTests("/DiagnosticTest/DeleteDiagnosticTest", targetIds, deletedDiagnosticTestDoseCallback);
            }
        });
    });

   
    // All data communication will be happened using this medicineDoseDataService object from server
    var diagnosticTestDataService = new function () {
        deleteDiagnosticTests = function (url, users, callback) {
            $.post(url, { targetIds: users }, function (data) { callback(data) });
        }
        return {
            deleteDiagnosticTests: deleteDiagnosticTests
        };
    }();

    // Callback function of Deleted Users
    var deletedDiagnosticTestDoseCallback = function (data) {
        console.table(data);
        swal(
            'The diagnostic test has been successfully deleted.',
            '',
            'success'
        ).then(function () {
            window.location.href = "/DiagnosticTest/Index";
        });
    }

    // Callback function of Deleted Users
    var deletedDiagnosticTestCallbackAllDiagnosticTest = function (data) {
        console.table(data);
        swal(
            'The diagnostic tests have been successfully deleted.',
            '',
            'success'
        ).then(function () {
            window.location.href = "/DiagnosticTest/Index";
        });
    }
});

function EditRedirect(id) {
    $('#hdnDoctor_Back').val($('#hdnDoctorId').val());

    window.location.href = '/MedicineDose/EditMedicineDose?MedcineId=' + parseInt(id) + '&SelectedDoctorId=' + parseInt($('#hdnDoctorId').val());
}
