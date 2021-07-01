$(document).ready(function () {
    //Select Current Menu
    $('.nav-li').removeClass('current');
    $('#menuItemSuperAdmin').addClass('current');

    // DataTable for Admin users
    var userListTable = $('#tblDoctors').DataTable({

        "pageLength": 10
    });

    $(".dataTables_empty").text("There are no doctors yet.");

    // Search on Admin user Grid using Search Text box
    $('#txtSearchDoctorList').keyup(function () {
        userListTable.search($(this).val()).draw();
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
});




