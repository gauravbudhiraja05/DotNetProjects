$(document).ready(function () {
    //Select Current Menu
    $('.nav-li').removeClass('current');
    $('#menuItemSuperAdmin').addClass('current');

    // DataTable for Admin users
    var userListTable = $('#tblDepartments').DataTable({

        "pageLength": 10
    });

    $(".dataTables_empty").text("There are no departments yet.");

    // Search on Admin user Grid using Search Text box
    $('#txtSearchDepartmentList').keyup(function () {
        userListTable.search($(this).val()).draw();
    });

    var rowsCount = $('#tblDepartments tbody tr').length;
    //alert(rowsCount);
    if (rowsCount < 10) {
        $("#tblDepartments_paginate").hide();
    }
    else {
        $("#tblDepartments_paginate").show();
    }

    if (rowsCount <= 1) {
        $("#btnDeleteAllCheckedDepartament").hide();
        //$(".table_checkbox").hide();
    }
    else {
        $("#btnDeleteAllCheckedDepartament").show();
        // $(".table_checkbox").show();
    }

    // Hide un-neccessary controls of datatable plugin for Admin User tables
    $('.dataTables_length').hide();
    $('.dataTables_filter').hide();
    $('.dataTables_info').hide();

    // Delete all checked user on top delete button click
    $("#btnDeleteAllCheckedDepartament").on("click", function () {

        var targetIds = {};
        targetIds.ItemIds = [];

        $('#tblDepartments').find('input[type="checkbox"]:checked').each(function () {

            var row = $(this).parents('tr')[0];;
            var userId = parseInt($(row).find('span').text());
            targetIds.ItemIds.push(userId);
        });

        if (targetIds.ItemIds.length > 0) {
            swal({
                title: 'Are you sure you want to delete these departments?',
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
                    departmentDataService.deleteDepartments("/Department/DeleteDepartment", targetIds, deletedCheckedDepartmentCallback_New);
                }

            });
        }

        else {
            swal('Please check at least one department to delete using the checkboxes.');
        }

    });

    // Delete User Confirnation Modal Popup

    $('#tblDepartments').on('click', '.departmentItem', function () {
        // Find the targeted User Id that will be deleted
        var row = $(this).parents('tr')[0];;
        var userId = parseInt($(row).find('span').text());
        var targetIds = {};
        targetIds.ItemIds = [];
        targetIds.ItemIds.push(userId);

        swal({
            title: 'Are you sure you want to delete this department?',
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
                departmentDataService.deleteDepartments("/Department/DeleteDepartment", targetIds, deletedDepartmentCallback_New);
            }
        });
    });

    $(".endUserItem").on('click', function () {

        //// Find the targeted User Id that will be deleted
        //var row = $(this).parents('tr')[0];;
        //var userId = parseInt($(row).find('span').text());
        //var targetIds = {};
        //targetIds.ItemIds = [];
        //targetIds.ItemIds.push(userId);

        //swal({
        //    title: 'Are you sure,?',
        //    text: "You won't be able to revert this!",
        //    type: 'warning',
        //    showCancelButton: true,
        //    confirmButtonColor: '#3085d6',
        //    cancelButtonColor: '#d33',
        //    confirmButtonText: 'Yes, delete it!'
        //}).then(function (result) {

        //    if (result.value) {
        //        //  deleteUsers method to delete admin users
        //        endUserDataService.deleteUsers("/EndUser/DeleteEndUsers", targetIds, deletedUserCallback);
        //    }
        //});


    });

    // All data communication will be happened using this adminDataService object from server
    var departmentDataService = new function () {
        deleteDepartments = function (url, users, callback) {
            $.post(url, { targetIds: users }, function (data) { callback(data) });
        }

        return {
            deleteDepartments: deleteDepartments
        };
    }();

    // Callback function of Checked Deleted Users
    var deletedCheckedDepartmentCallback_New = function (data) {
        console.table(data);
        if (data.isSuccess == false) {

            if (data.message == "1") {
                swal(
                    'You cannot delete these department(s), it is associated with Admin users.',
                    '',
                    'error'
                ).then(function () {

                    var rowsCount = $('#tblDepartments tbody tr').length;

                    if (rowsCount <= 1) {
                        $("#btnDeleteAllCheckedDepartament").hide();
                        //$(".table_checkbox").hide();
                    }
                    else {
                        $("#btnDeleteAllCheckedDepartament").show();
                        // $(".table_checkbox").show();
                    }

                    window.location.href = "/Department/Index";
                });
            }
            else if (data.message == "2") {
                swal(
                    'You cannot delete these department(s), it is associated with News.',
                    '',
                    'error'
                ).then(function () {

                    var rowsCount = $('#tblDepartments tbody tr').length;

                    if (rowsCount <= 1) {
                        $("#btnDeleteAllCheckedDepartament").hide();
                        //$(".table_checkbox").hide();
                    }
                    else {
                        $("#btnDeleteAllCheckedDepartament").show();
                        // $(".table_checkbox").show();
                    }

                    window.location.href = "/Department/Index";
                });
            }
            else if (data.message == "3") {
                swal(
                    'You cannot delete these department(s), it is associated with Vacancies.',
                    '',
                    'error'
                ).then(function () {

                    var rowsCount = $('#tblDepartments tbody tr').length;

                    if (rowsCount <= 1) {
                        $("#btnDeleteAllCheckedDepartament").hide();
                        //$(".table_checkbox").hide();
                    }
                    else {
                        $("#btnDeleteAllCheckedDepartament").show();
                        // $(".table_checkbox").show();
                    }

                    window.location.href = "/Department/Index";
                });
            }
            else if (data.message == "4") {
                swal(
                    'You cannot delete these department(s), it is associated with Document Folders.',
                    '',
                    'error'
                ).then(function () {

                    var rowsCount = $('#tblDepartments tbody tr').length;

                    if (rowsCount <= 1) {
                        $("#btnDeleteAllCheckedDepartament").hide();
                        //$(".table_checkbox").hide();
                    }
                    else {
                        $("#btnDeleteAllCheckedDepartament").show();
                        // $(".table_checkbox").show();
                    }

                    window.location.href = "/Department/Index";
                });
            }
            else if (data.message == "5") {
                swal(
                    'You cannot delete these department(s), it is associated with Documents.',
                    '',
                    'error'
                ).then(function () {

                    var rowsCount = $('#tblDepartments tbody tr').length;

                    if (rowsCount <= 1) {
                        $("#btnDeleteAllCheckedDepartament").hide();
                        //$(".table_checkbox").hide();
                    }
                    else {
                        $("#btnDeleteAllCheckedDepartament").show();
                        // $(".table_checkbox").show();
                    }

                    window.location.href = "/Department/Index";
                });
            }

        }
        else {
            swal(
                'The department(s) have been successfully deleted.',
                '',
                'success'
            ).then(function () {

                var rowsCount = $('#tblDepartments tbody tr').length;

                if (rowsCount <= 1) {
                    $("#btnDeleteAllCheckedDepartament").hide();
                    //$(".table_checkbox").hide();
                }
                else {
                    $("#btnDeleteAllCheckedDepartament").show();
                    // $(".table_checkbox").show();
                }

                window.location.href = "/Department/Index";
            });
        }
    }



    // Callback function of Deleted Users
    var deletedDepartmentCallback_New = function (data) {
        console.table(data);
        if (data.isSuccess == false) {

            if (data.message == "1") {
                swal(
                    'You can not delete this department, it is associated with Admin users.',
                    '',
                    'error'
                ).then(function () {

                    var rowsCount = $('#tblDepartments tbody tr').length;

                    if (rowsCount <= 1) {
                        $("#btnDeleteAllCheckedDepartament").hide();
                        //$(".table_checkbox").hide();
                    }
                    else {
                        $("#btnDeleteAllCheckedDepartament").show();
                        // $(".table_checkbox").show();
                    }

                    window.location.href = "/Department/Index";
                });
            }
            else if (data.message == "2") {
                swal(
                    'You can not delete this department, it is associated with News.',
                    '',
                    'error'
                ).then(function () {

                    var rowsCount = $('#tblDepartments tbody tr').length;

                    if (rowsCount <= 1) {
                        $("#btnDeleteAllCheckedDepartament").hide();
                        //$(".table_checkbox").hide();
                    }
                    else {
                        $("#btnDeleteAllCheckedDepartament").show();
                        // $(".table_checkbox").show();
                    }

                    window.location.href = "/Department/Index";
                });
            }
            else if (data.message == "3") {
                swal(
                    'You can not delete this department, it is associated with Vacancies.',
                    '',
                    'error'
                ).then(function () {

                    var rowsCount = $('#tblDepartments tbody tr').length;

                    if (rowsCount <= 1) {
                        $("#btnDeleteAllCheckedDepartament").hide();
                        //$(".table_checkbox").hide();
                    }
                    else {
                        $("#btnDeleteAllCheckedDepartament").show();
                        // $(".table_checkbox").show();
                    }

                    window.location.href = "/Department/Index";
                });
            }
            else if (data.message == "4") {
                swal(
                    'You can not delete this department, it is associated with Folders.',
                    '',
                    'error'
                ).then(function () {

                    var rowsCount = $('#tblDepartments tbody tr').length;

                    if (rowsCount <= 1) {
                        $("#btnDeleteAllCheckedDepartament").hide();
                        //$(".table_checkbox").hide();
                    }
                    else {
                        $("#btnDeleteAllCheckedDepartament").show();
                        // $(".table_checkbox").show();
                    }

                    window.location.href = "/Department/Index";
                });
            }
            else if (data.message == "5") {
                swal(
                    'You can not delete this department, it is associated with Documents.',
                    '',
                    'error'
                ).then(function () {

                    var rowsCount = $('#tblDepartments tbody tr').length;

                    if (rowsCount <= 1) {
                        $("#btnDeleteAllCheckedDepartament").hide();
                        //$(".table_checkbox").hide();
                    }
                    else {
                        $("#btnDeleteAllCheckedDepartament").show();
                        // $(".table_checkbox").show();
                    }

                    window.location.href = "/Department/Index";
                });
            }

        }
        else {
            swal(
                'The department has been successfully deleted.',
                '',
                'success'
            ).then(function () {

                var rowsCount = $('#tblDepartments tbody tr').length;

                if (rowsCount <= 1) {
                    $("#btnDeleteAllCheckedDepartament").hide();
                    //$(".table_checkbox").hide();
                }
                else {
                    $("#btnDeleteAllCheckedDepartament").show();
                    // $(".table_checkbox").show();
                }

                window.location.href = "/Department/Index";
            });
        }
    }

});

   


