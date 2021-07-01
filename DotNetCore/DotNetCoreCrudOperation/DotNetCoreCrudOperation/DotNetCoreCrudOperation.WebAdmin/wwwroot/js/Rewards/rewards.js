$(document).ready(function () {
    //Select Current Menu
    $('.nav-li').removeClass('current');
    $('#menuItemRewards').addClass('current');

    // DataTable for rewards
    var rewardListTable = $('#tblRewards').DataTable({

        "pageLength": 10
    });
    $(".dataTables_empty").text("There are no rewards yet!");

    // Search on rewards Grid using Search Text box
    $('#txtSearchRewardList').keyup(function () {
        rewardListTable.search($(this).val()).draw();
    });

    var rowsCount = $('#tblRewards tbody tr').length;
    
    if (rowsCount < 10) {
        $("#tblRewards_paginate").hide();
    }
    else {
        $("#tblRewards_paginate").show();
    }

    if (rowsCount <= 1) {
        $("#btnDeleteAllCheckedRewards").hide();
        
    }
    else {
        $("#btnDeleteAllCheckedRewards").show();
        
    }

    // Hide un-neccessary controls of datatable plugin for rewards table
    $('.dataTables_length').hide();
    $('.dataTables_filter').hide();
    $('.dataTables_info').hide();

    // Delete all checked user on top delete button click
    $("#btnDeleteAllCheckedRewards").on("click", function () {

        var targetIds = {};
        targetIds.ItemIds = [];

        $('#tblRewards').find('input[type="checkbox"]:checked').each(function () {

            var row = $(this).parents('tr')[0];;
            var monthStarId = parseInt($(row).find('span').text());
            targetIds.ItemIds.push(monthStarId);
        });

        if (targetIds.ItemIds.length > 0) {
            swal({
                title: 'Are you sure you want to delete these rewards?',
                text: "",
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes',
                cancelButtonText: 'No'
            }).then(function (result) {

                if (result.value) {
                    //  deleteUsers method to delete rewards
                    rewardDataService.deleteReward("/Reward/DeleteRewards", targetIds, deletedCheckedRewardCallback);
                }

            });
        }

        else {
            swal('Please select at least one group of rewards to delete using the checkboxes.');
        }

    });

    // Delete reward Confirnation Modal Popup
    $(".deleteRewardItem").on('click', function () {

        // Find the targeted rewards Id that will be deleted
        var row = $(this).parents('tr')[0];;
        var monthStarId = parseInt($(row).find('span').text());
        var targetIds = {};
        targetIds.ItemIds = [];
        targetIds.ItemIds.push(monthStarId);

        swal({
            title: 'Are you sure you want to delete these rewards?',
            text: "",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes',
            cancelButtonText: 'No'
        }).then(function (result) {

            if (result.value) {

                rewardDataService.deleteReward("/Reward/DeleteRewards", targetIds, deletedRewardCallback);
            }
        });


    });

    // All data communication will be happened using this rewardDataService object from server
    var rewardDataService = new function () {
        deleteReward = function (url, rewards, callback) {
            $.post(url, { targetIds: rewards }, function (data) { callback(data) });
        }

        return {
            deleteReward: deleteReward
        };
    }();

    // Callback function of Deleted rewards
    var deletedRewardCallback = function (data) {
        console.table(data);
        swal(
            'The reward have been deleted successfully.',
            '',
            'success'
        ).then(function () {

            var rowsCount = $('#tblRewards tbody tr').length;

            if (rowsCount <= 1) {
                $("#btnDeleteAllCheckedRewards").hide();
                
            }
            else {
                $("#btnDeleteAllCheckedRewards").show();
                
            }
            window.location.href = "/Reward/Index";
        });
    }

    // Callback function of Deleted rewards
    var deletedCheckedRewardCallback = function (data) {
        console.table(data);
        swal(
            'The rewards have been successfully deleted.',
            '',
            'success'
        ).then(function () {

            var rowsCount = $('#tblRewards tbody tr').length;

            if (rowsCount <= 1) {
                $("#btnDeleteAllCheckedRewards").hide();
                
            }
            else {
                $("#btnDeleteAllCheckedRewards").show();
                
            }
            window.location.href = "/Reward/Index";
        });
    }
});