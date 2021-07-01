$(document).ready(function () {
    //Select Current Menu
    $('.nav-li').removeClass('current');
    $('#menuItemSuperAdmin').addClass('current');

    // DataTable for Patient Users
    var userListTable = $('#tblUsers').DataTable({

        "pageLength": 10
    });

    $(".dataTables_empty").text("There are no users yet.");

    // Search on Admin user Grid using Search Text box
    $('#txtSearchUserList').keyup(function () {
        userListTable.search($(this).val()).draw();
    });

    var rowsCount = $('#tblUsers tbody tr').length;
    //alert(rowsCount);
    if (rowsCount < 10) {
        $("#tblUsers_paginate").hide();
    }
    else {
        $("#tblUsers_paginate").show();
    }

    if (rowsCount <= 1) {
        $("#btnDeleteAllCheckedUser").hide();
        //$(".table_checkbox").hide();
    }
    else {
        $("#btnDeleteAllCheckedUser").show();
        // $(".table_checkbox").show();
    }

    // Hide un-neccessary controls of datatable plugin for Doctor tables
    $('.dataTables_length').hide();
    $('.dataTables_filter').hide();
    $('.dataTables_info').hide();

    // Delete all checked user on top delete button click
    $("#btnDeleteAllCheckedUser").on("click", function () {

        var targetIds = {};
        targetIds.ItemIds = [];

        $('#tblDoctors').find('input[type="checkbox"]:checked').each(function () {

            var row = $(this).parents('tr')[0];;
            var userId = parseInt($(row).find('span').text());
            targetIds.ItemIds.push(userId);
        });

        if (targetIds.ItemIds.length > 0) {
            swal({
                title: 'Are you sure you want to delete these users?',
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
                    userDataService.deleteUsers("/User/DeleteUser", targetIds, deletedUserCallbackAllUser);
                }

            });
        }

        else {
            swal('Please check at least one user to delete using the checkboxes.');
        }

    });

    // Delete User Confirmation Modal Popup

    $('#tblUsers').on('click', '.userItem', function () {
        // Find the targeted User Id that will be deleted
        var row = $(this).parents('tr')[0];;
        var userId = parseInt($(row).find('span').text());
        var targetIds = {};
        targetIds.ItemIds = [];
        targetIds.ItemIds.push(userId);

        swal({
            title: 'Are you sure you want to delete this user?',
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
                userDataService.deleteUsers("/User/DeleteUser", targetIds, deletedUserCallback);
            }
        });
    });

    // All data communication will be happened using this doctorDataService object from server
    var userDataService = new function () {
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
            'The user has been successfully deleted.',
            '',
            'success'
        ).then(function () {
            window.location.href = "/User/Index";
        });
    }

    // Callback function of Deleted Users
    var deletedUserCallbackAllUser = function (data) {
        console.table(data);
        swal(
            'The users have been successfully deleted.',
            '',
            'success'
        ).then(function () {
            window.location.href = "/User/Index";
        });
    }
});




