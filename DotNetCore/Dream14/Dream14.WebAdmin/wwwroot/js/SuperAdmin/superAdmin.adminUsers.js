$(document).ready(function () {
    //Select Current Menu
    $('.nav-li').removeClass('current');
    $('#menuItemSuperAdmin').addClass('current');

    // DataTable for Admin users
    var userListTable = $('#tblAdmins').DataTable({

        "pageLength": 10
    });

    $(".dataTables_empty").text("There are no admins yet.");

    // Search on Admin user Grid using Search Text box
    $('#txtSearchAdminList').keyup(function () {
        userListTable.search($(this).val()).draw();
    });

    var rowsCount = $('#tblAdmins tbody tr').length;
    //alert(rowsCount);
    if (rowsCount < 10) {
        $("#tblAdmins_paginate").hide();
    }
    else {
        $("#tblAdmins_paginate").show();
    }

    if (rowsCount <= 1) {
        $("#btnDeleteAllCheckedAdmin").hide();
        //$(".table_checkbox").hide();
    }
    else {
        $("#btnDeleteAllCheckedAdmin").show();
        // $(".table_checkbox").show();
    }

    // Hide un-neccessary controls of datatable plugin for Admin User tables
    $('.dataTables_length').hide();
    $('.dataTables_filter').hide();
    $('.dataTables_info').hide();

    // Delete all checked user on top delete button click
    $("#btnDeleteAllCheckedAdmin").on("click", function () {

        var targetIds = {};
        targetIds.ItemIds = [];

        $('#tblAdmins').find('input[type="checkbox"]:checked').each(function () {

            var row = $(this).parents('tr')[0];;
            var userId = parseInt($(row).find('span').text());
            targetIds.ItemIds.push(userId);
        });

        if (targetIds.ItemIds.length > 0) {
            swal({
                title: 'Are you sure you want to delete these admins?',
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
                    adminDataService.deleteAdmins("/SuperAdmin/DeleteAdminUsers", targetIds, deletedCheckedAdminCallback);
                }
            });
        }

        else {
            swal('Please check at least one admin to delete using the checkboxes.');
        }

    });

    // Delete User Confirnation Modal Popup

    $('#tblAdmins').on('click', '.adminItem', function () {
        // Find the targeted User Id that will be deleted
        var row = $(this).parents('tr')[0];;
        var userId = parseInt($(row).find('span').text());
        var targetIds = {};
        targetIds.ItemIds = [];
        targetIds.ItemIds.push(userId);

        swal({
            title: 'Are you sure you want to delete this admin?',
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
                adminDataService.deleteAdmins("/SuperAdmin/DeleteAdminUsers", targetIds, deletedAdminCallback);
            }
        });
    });

    // All data communication will be happened using this adminDataService object from server
    var adminDataService = new function () {
        deleteAdmins = function (url, users, callback) {
            $.post(url, { targetIds: users }, function (data) { callback(data) });
        }

        return {
            deleteAdmins: deleteAdmins
        };
    }();

    // Callback function of Checked Deleted Users
    var deletedCheckedAdminCallback = function (data) {
        console.table(data);
        if (data.isSuccess == false) {

            if (data.message == "1") {
                swal(
                    'You cannot delete these admin(s), it is associated with other master users.',
                    '',
                    'error'
                ).then(function () {

                    var rowsCount = $('#tblAdmins tbody tr').length;

                    if (rowsCount <= 1) {
                        $("#btnDeleteAllCheckedAdmin").hide();
                        //$(".table_checkbox").hide();
                    }
                    else {
                        $("#btnDeleteAllCheckedAdmin").show();
                        // $(".table_checkbox").show();
                    }

                    window.location.href = "/SuperAdmin/AdminUsers";
                });
            }
            else if (data.message == "2") {
                swal(
                    'You cannot delete these admin(s), it is associated with other agent users.',
                    '',
                    'error'
                ).then(function () {

                    var rowsCount = $('#tblAdmins tbody tr').length;

                    if (rowsCount <= 1) {
                        $("#btnDeleteAllCheckedAdmin").hide();
                        //$(".table_checkbox").hide();
                    }
                    else {
                        $("#btnDeleteAllCheckedAdmin").show();
                        // $(".table_checkbox").show();
                    }

                    window.location.href = "/SuperAdmin/AdminUsers";
                });
            }
            else if (data.message == "3") {
                swal(
                    'You cannot delete these admin(s), it is associated with other frontend users.',
                    '',
                    'error'
                ).then(function () {

                    var rowsCount = $('#tblAdmins tbody tr').length;

                    if (rowsCount <= 1) {
                        $("#btnDeleteAllCheckedAdmin").hide();
                        //$(".table_checkbox").hide();
                    }
                    else {
                        $("#btnDeleteAllCheckedAdmin").show();
                        // $(".table_checkbox").show();
                    }

                    window.location.href = "/SuperAdmin/AdminUsers";
                });
            }
        }
        else {
            swal(
                'The admin(s) have been successfully deleted.',
                '',
                'success'
            ).then(function () {

                var rowsCount = $('#tblAdmins tbody tr').length;

                if (rowsCount <= 1) {
                    $("#btnDeleteAllCheckedAdmin").hide();
                    //$(".table_checkbox").hide();
                }
                else {
                    $("#btnDeleteAllCheckedAdmin").show();
                    // $(".table_checkbox").show();
                }

                window.location.href = "/SuperAdmin/AdminUsers";
            });
        }
    }



    // Callback function of Deleted Users
    var deletedAdminCallback = function (data) {
        if (data.isSuccess == false) {

            if (data.message == "1") {
                swal(
                    'You cannot delete these admin(s), it is associated with other master users.',
                    '',
                    'error'
                ).then(function () {

                    var rowsCount = $('#tblAdmins tbody tr').length;

                    if (rowsCount <= 1) {
                        $("#btnDeleteAllCheckedAdmin").hide();
                        //$(".table_checkbox").hide();
                    }
                    else {
                        $("#btnDeleteAllCheckedAdmin").show();
                        // $(".table_checkbox").show();
                    }

                    window.location.href = "/SuperAdmin/AdminUsers";
                });
            }
            else if (data.message == "2") {
                swal(
                    'You cannot delete these admin(s), it is associated with other agent users.',
                    '',
                    'error'
                ).then(function () {

                    var rowsCount = $('#tblAdmins tbody tr').length;

                    if (rowsCount <= 1) {
                        $("#btnDeleteAllCheckedAdmin").hide();
                        //$(".table_checkbox").hide();
                    }
                    else {
                        $("#btnDeleteAllCheckedAdmin").show();
                        // $(".table_checkbox").show();
                    }

                    window.location.href = "/SuperAdmin/AdminUsers";
                });
            }
            else if (data.message == "3") {
                swal(
                    'You cannot delete these admin(s), it is associated with other frontend users.',
                    '',
                    'error'
                ).then(function () {

                    var rowsCount = $('#tblAdmins tbody tr').length;

                    if (rowsCount <= 1) {
                        $("#btnDeleteAllCheckedAdmin").hide();
                        //$(".table_checkbox").hide();
                    }
                    else {
                        $("#btnDeleteAllCheckedAdmin").show();
                        // $(".table_checkbox").show();
                    }

                    window.location.href = "/SuperAdmin/AdminUsers";
                });
            }
        }
        else {
            swal(
                'The admin(s) have been successfully deleted.',
                '',
                'success'
            ).then(function () {

                var rowsCount = $('#tblAdmins tbody tr').length;

                if (rowsCount <= 1) {
                    $("#btnDeleteAllCheckedAdmin").hide();
                    //$(".table_checkbox").hide();
                }
                else {
                    $("#btnDeleteAllCheckedAdmin").show();
                    // $(".table_checkbox").show();
                }

                window.location.href = "/SuperAdmin/AdminUsers";
            });
        }
    }

});




