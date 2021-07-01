


$(document).ready(function () {
    //Select Current Menu
    $('.nav-li').removeClass('current');
    $('#menuItemSuperAdmin').addClass('current');

    // DataTable for Admin users
    var userListTable = $('#tblEndUsers').DataTable({

        "pageLength": 10
    });



    // Search on Admin user Grid using Search Text box
    $('#txtSearchEndUserList').keyup(function () {
        userListTable.search($(this).val()).draw();
    });


    var rowsCount = $('#tblEndUsers tbody tr').length;
    //alert(rowsCount);
    if (rowsCount < 10) {
        $("#tblEndUsers_paginate").hide();
    }
    else {
        $("#tblEndUsers_paginate").show();
    }

    if (rowsCount <= 1) {
        $("#btnDeleteAllCheckedEndUser").hide();
        //$(".table_checkbox").hide();
    }
    else {
        $("#btnDeleteAllCheckedEndUser").show();
        // $(".table_checkbox").show();
    }

    // Hide un-neccessary controls of datatable plugin for Admin User tables
    $('.dataTables_length').hide();
    $('.dataTables_filter').hide();
    $('.dataTables_info').hide();

    // Delete all checked user on top delete button click
    $("#btnDeleteAllCheckedEndUser").on("click", function () {

        var targetIds = {};
        targetIds.ItemIds = [];

        $('#tblEndUsers').find('input[type="checkbox"]:checked').each(function () {

            var row = $(this).parents('tr')[0];;
            var userId = parseInt($(row).find('span').text());
            targetIds.ItemIds.push(userId);
        });

        if (targetIds.ItemIds.length > 0) {
            swal({
                title: 'Are you sure, you want to delete these end users?',
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
                    adminUserDataService.deleteUsers("/SuperAdmin/DeleteAdminUsers", targetIds, deletedUserCallback);
                }

            });
        }

        else {
            swal('Please check at least one end user');
        }

    });

    // Delete User Confirnation Modal Popup
    $(".endUserItem").on('click', function () {

        // Find the targeted User Id that will be deleted
        var row = $(this).parents('tr')[0];;
        var userId = parseInt($(row).find('span').text());
        var targetIds = {};
        targetIds.ItemIds = [];
        targetIds.ItemIds.push(userId);

        swal({
            title: 'Are you sure, you want to delete this end user?',
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
                endUserDataService.deleteUsers("/EndUser/DeleteEndUsers", targetIds, deletedUserCallback);
            }
        });


    });

    // All data communication will be happened using this adminDataService object from server
    var endUserDataService = new function () {
        deleteUsers = function (url, users, callback) {
            $.post(url, { targetIds: users }, function (data) { callback(data) });
        }

        return {
            deleteUsers: deleteUsers
        };
    }();

    // Callback function of Deleted Users
    var deletedUserCallback = function (data) {
        console.table(data);
        swal(
            'Deleted!',
            'User has been deleted.',
            'success'
        ).then(function () {

            var rowsCount = $('#tblEndUsers tbody tr').length;

            if (rowsCount <= 1) {
                $("#btnDeleteAllCheckedEndUser").hide();
                //$(".table_checkbox").hide();
            }
            else {
                $("#btnDeleteAllCheckedEndUser").show();
                // $(".table_checkbox").show();
            }

            window.location.href = "/EndUser/Index";
        });
    }
});