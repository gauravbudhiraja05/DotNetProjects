$(document).ready(function () {
    //Select Current Menu
    $('.nav-li').removeClass('current');
    $('#menuItemSuperAdmin').addClass('current');

    // DataTable for Month Stars
    var montStarListTable = $('#tblMonthStars').DataTable({

        "pageLength": 10
    });
    $(".dataTables_empty").text("There are no stars yet!");
    // Search on Month Stars Grid using Search Text box
    $('#txtSearchMonthStars').keyup(function () {
        montStarListTable.search($(this).val()).draw();
    });

    var rowsCount = $('#tblMonthStars tbody tr').length;
    //alert(rowsCount);
    if (rowsCount < 10) {
        $("#tblMonthStars_paginate").hide();
    }
    else {
        $("#tblMonthStars_paginate").show();
    }

    if (rowsCount <= 1) {
        $("#btnDeleteAllCheckedMonthStars").hide();
        //$(".table_checkbox").hide();
    }
    else {
        $("#btnDeleteAllCheckedMonthStars").show();
        // $(".table_checkbox").show();
    }

    // Hide un-neccessary controls of datatable plugin for Month Stars table
    $('.dataTables_length').hide();
    $('.dataTables_filter').hide();
    $('.dataTables_info').hide();

    // Delete all checked user on top delete button click
    $("#btnDeleteAllCheckedMonthStars").on("click", function () {

        var targetIds = {};
        targetIds.ItemIds = [];

        $('#tblMonthStars').find('input[type="checkbox"]:checked').each(function () {

            var row = $(this).parents('tr')[0];;
            var monthStarId = parseInt($(row).find('span').text());
            targetIds.ItemIds.push(monthStarId);
        });

        if (targetIds.ItemIds.length > 0) {
            swal({
                title: 'Are you sure you want to delete these stars?',
                text: "",
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes',
                cancelButtonText: 'No'
            }).then(function (result) {

                if (result.value) {
                    //  deleteUsers method to delete month stars
                    monthStarDataService.deleteMonthStar("/SuperAdmin/DeleteMonthStars", targetIds, deletedCheckedMonthStarCallback);
                }

            });
        }

        else {
            swal('Please select at least one group of stars to delete using the checkboxes.');
        }

    });

    // Delete Month Star Confirnation Modal Popup
    $(".monthStarItem").on('click', function () {

        // Find the targeted Month Star Id that will be deleted
        var row = $(this).parents('tr')[0];;
        var monthStarId = parseInt($(row).find('span').text());
        var targetIds = {};
        targetIds.ItemIds = [];
        targetIds.ItemIds.push(monthStarId);

        swal({
            title: 'Are you sure you want to delete these stars?',
            text: "",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes',
            cancelButtonText: 'No'
        }).then(function (result) {

            if (result.value) {
            
                monthStarDataService.deleteMonthStar("/SuperAdmin/DeleteMonthStars", targetIds, deletedMonthStarCallback);
            }
        });


    });

    // All data communication will be happened using this monthStarDataService object from server
    var monthStarDataService = new function () {
        deleteMonthStar = function (url, monthStars, callback) {
            $.post(url, { targetIds: monthStars }, function (data) { callback(data) });
        }

        return {
            deleteMonthStar: deleteMonthStar
        };
    }();

    // Callback function of Deleted month Stars
    var deletedMonthStarCallback = function (data) {
        console.table(data);
        swal(
            'The stars have been deleted successfully.',
            '',
            'success'
        ).then(function () {

            var rowsCount = $('#tblMonthStars tbody tr').length;
           
            if (rowsCount <= 1) {
                $("#btnDeleteAllCheckedMonthStars").hide();
                //$(".table_checkbox").hide();
            }
            else {
                $("#btnDeleteAllCheckedMonthStars").show();
                // $(".table_checkbox").show();
            }
            window.location.href = "/SuperAdmin/MonthStars";
        });
    }

    // Callback function of Deleted month Stars
    var deletedCheckedMonthStarCallback = function (data) {
        console.table(data);
        swal(
            'The stars have been successfully deleted.',
            '',
            'success'
        ).then(function () {

            var rowsCount = $('#tblMonthStars tbody tr').length;

            if (rowsCount <= 1) {
                $("#btnDeleteAllCheckedMonthStars").hide();
                //$(".table_checkbox").hide();
            }
            else {
                $("#btnDeleteAllCheckedMonthStars").show();
                // $(".table_checkbox").show();
            }
            window.location.href = "/SuperAdmin/MonthStars";
        });
    }
});