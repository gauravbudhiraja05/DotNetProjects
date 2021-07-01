
var userListTable = null;

$(document).ready(function () {

    // Call AdminUser List partial view using ajax
    $.get('/SuperAdmin/AdminUserList/', function (data) {
        $("#divAdminUserList").html(data);

        // DataTable for Admin users
         userListTable = $('#tblAdminUsers').DataTable({
            "order": [[2, "asc"]],
            "pageLength": 10
        });
        $(".dataTables_empty").text("There are no admin users yet.");

        var rowsCount = $('#tblAdminUsers tbody tr').length;
        //alert(rowsCount);
        if (rowsCount < 10) {
            $("#tblAdminUsers_paginate").hide();
        }
        else {
            $("#tblAdminUsers_paginate").show();
        }

        if (rowsCount <= 1) {
            $("#btnDeleteAllCheckedAdminUser").hide();
            //$(".table_checkbox").hide();
        }
        else {
            $("#btnDeleteAllCheckedAdminUser").show();
            // $(".table_checkbox").show();
        }

        // Hide un-neccessary controls of datatable plugin for Admin User tables
        $('.dataTables_length').hide();
        $('.dataTables_filter').hide();
        $('.dataTables_info').hide();

        // Search on Admin user Grid using Search Text box
        $('#txtSearchAdminUserList').keyup(function () {
            userListTable.search($(this).val()).draw();
        });

        // Search/Filter on Admin User grid using role type dropdown
        $('#ddlRoleType').change(function () {
            userListTable.search($(this).val()).draw();
        });

        $("#checkall_Admin").change(function () {  //"select all" change
            var status = this.checked; // "select all" checked status
            $(this).closest("table").find(".check_item").each(function () { //iterate all listed checkbox items

                if (this.id != "checkitem1_" + $('#hdnSuperAdminLoggedInId').val()) {
                    this.checked = status; //change ".checkbox" checked status
                }

            });
        });

        // Delete all checked user on top delete button click
        $("#btnDeleteAllCheckedAdminUser").on("click", function () {

            var targetIds = {};
            targetIds.ItemIds = [];

            $('#tblAdminUsers').find('input[type="checkbox"]:checked').each(function () {

                var row = $(this).parents('tr')[0];;
                var userId = parseInt($(row).find('span').text());
                //if ($('hdnSuperAdminLoggedInId').val() != undefined) {
                //    var superAdminLoggedInid = parseInt($('hdnSuperAdminLoggedInId').val());
                //    if (superAdminLoggedInid != userId)
                targetIds.ItemIds.push(userId);
                //}

            });

            if (targetIds.ItemIds.length > 0) {
                swal({
                    title: 'Are you sure you want to delete these admin users?',
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
                        adminUserDataService.deleteUsers("/SuperAdmin/DeleteAdminUsers", targetIds, deletedCheckedUserCallback);
                    }

                });
            }

            else {
                swal('Please select at least one user to delete using the checkboxes.');
            }

        });

        // Delete User Confirnation Modal Popup
        $(".adminUserItem").on('click', function () {

            // Find the targeted User Id that will be deleted
            var row = $(this).parents('tr')[0];;
            var userId = parseInt($(row).find('span').text());
            var targetIds = {};
            targetIds.ItemIds = [];
            targetIds.ItemIds.push(userId);

            swal({
                title: 'Are you sure you want to delete this admin user?',
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


        });


    });

    //Select Current Menu
    $('.nav-li').removeClass('current');
    $('#menuItemSuperAdmin').addClass('current');

   

    // All data communication will be happened using this adminDataService object from server
    var adminUserDataService = new function () {
        deleteUsers = function (url, users, callback) {
            $.post(url, { targetIds: users }, function (data) { callback(data) });
        }

        return {
            deleteUsers: deleteUsers
        };
    }();

    // Callback function of Deleted multiple Users
    var deletedCheckedUserCallback = function (data) {
        console.table(data);
        swal(
            'The admin users have been successfully deleted.',
            '',
            'success'
        ).then(function () {
            var rowsCount = $('#tblAdminUsers tbody tr').length;

            if (rowsCount <= 1) {
                $("#btnDeleteAllCheckedAdminUser").hide();
                //$(".table_checkbox").hide();
            }
            else {
                $("#btnDeleteAllCheckedAdminUser").show();
                // $(".table_checkbox").show();
            }
            window.location.href = "/SuperAdmin/AdminUsers";
        });
    }

    // Callback function of Deleted single User
    var deletedUserCallback = function (data) {
        console.table(data);
        swal(
            'The admin user has been successfully deleted.',
            '',
            'success'
        ).then(function () {
            var rowsCount = $('#tblAdminUsers tbody tr').length;

            if (rowsCount <= 1) {
                $("#btnDeleteAllCheckedAdminUser").hide();
                //$(".table_checkbox").hide();
            }
            else {
                $("#btnDeleteAllCheckedAdminUser").show();
                // $(".table_checkbox").show();
            }
            window.location.href = "/SuperAdmin/AdminUsers";
        });
    }

    // Role Dropdown filter
    $('.roleClick').on('click', function () {
        var roleName = this.innerText;
        if (roleName == "All") {
            menuText = "Select Roles";

            // Search on Admin user with empty text
             userListTable.search('').draw();
        }
        
        if (roleName == 'Super Administrator') {

            menuText = "Super Administrator";
            // Search on Admin user with empty text
            userListTable.search(roleName).draw();
        }

        if (roleName == 'Departmental Administrator') {

            menuText = "Departmental Administrator";
            // Search on Admin user with empty text
            userListTable.search(roleName).draw();
        }
        if (roleName == 'Line Manager') {

            menuText = "Line Manager";
            // Search on Admin user with empty text
            userListTable.search(roleName).draw();
        }

        // Select Role in dropdown 
        $(".drop_menu > ul > li > a")[0].innerText = menuText;
    });
});