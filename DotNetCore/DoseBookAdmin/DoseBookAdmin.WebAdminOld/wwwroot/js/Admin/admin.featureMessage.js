


$(document).ready(function () {
    //Select Current Menu
    $('.nav-li').removeClass('current');
    $('#menuItemSuperAdmin').addClass('current');

    // DataTable for Admin users
    var userListTable = $('#tblFeaturdeMessageList').DataTable({

        "pageLength": 10
    });

   


    $(".dataTables_empty").text("There are no featured messages yet.");
    // Search on Admin user Grid using Search Text box
    $('#txtSearchFeaturedMessageList').keyup(function () {
        userListTable.search($(this).val()).draw();
    });

    var rowsCount = $('#tblFeaturdeMessageList tbody tr').length;
    //alert(rowsCount);
    if (rowsCount < 10) {
        $("#tblFeaturdeMessageList_paginate").hide();
    }
    else {
        $("#tblFeaturdeMessageList_paginate").show();
    }

    if (rowsCount <= 1) {
        $("#btnDeleteAllCheckedFeaturedMessage").hide();
        //$(".table_checkbox").hide();
    }
    else {
        $("#btnDeleteAllCheckedFeaturedMessage").show();
        // $(".table_checkbox").show();
    }

    // Hide un-neccessary controls of datatable plugin for Admin User tables
    $('.dataTables_length').hide();
    $('.dataTables_filter').hide();
    $('.dataTables_info').hide();

    // Delete all checked fetured message on top delete button click
    $("#btnDeleteAllCheckedFeaturedMessage").on("click", function () {

        var targetIds = {};
        targetIds.ItemIds = [];
        var count = 0;
        $('#tblFeaturdeMessageList').find('input[type="checkbox"]:checked').each(function () {
            var messageId = $(this).parents('tr').attr("id")
            targetIds.ItemIds.push(messageId);
        });

        if (targetIds.ItemIds.length > 0) {
            swal({
                title: 'Are you sure you want to delete these featured messages?',
                text: "",
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes',
                cancelButtonText: 'No'
            }).then(function (result) {

                if (result.value) {
                 
                    featuredMessageDataService.deleteFeaturedMessage("/SuperAdmin/DeleteFeaturedMessage", targetIds, deletedCheckedMessagesCallback);
                }

            });
        }

        else {
            swal('Please select at least one featured message to delete using the checkboxes.');
        }

    });

    // Delete Featured Message Confirnation Modal Popup
    $(".featureMessageItem").on('click', function () {

        // Find the targeted User Id that will be deleted
        var row = $(this).parents('tr')[0];;
        var userId = parseInt($(row).find('span').text());
        var targetIds = {};
        targetIds.ItemIds = [];
        targetIds.ItemIds.push(userId);

        swal({
            title: 'Are you sure you want to delete this featured message?',
            text: "",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',  
            confirmButtonText: 'Yes',
            cancelButtonText: 'No'
        }).then(function (result) {

            if (result.value) {
                //  deleteUsers method to delete featured message
                featuredMessageDataService.deleteFeaturedMessage("/SuperAdmin/DeleteFeaturedMessage", targetIds, deletedMessagesCallback);
            }
        });


    });

    // All data communication will be happened using this featuredMessageDataService object from server
    var featuredMessageDataService = new function () {
        deleteFeaturedMessage = function (url, messages, callback) {
            $.post(url, { targetIds: messages }, function (data) { callback(data) });
        }

        return {
            deleteFeaturedMessage: deleteFeaturedMessage
        };
    }();

    // Callback function of Deleted messages
    var deletedMessagesCallback = function (data) {
        swal(
            'The featured messages has been successfully deleted.',
            '',
            'success'
        ).then(function () {
            var rowsCount = $('#tblFeaturdeMessageList tbody tr').length;
            
            if (rowsCount <= 1) {
                $("#btnDeleteAllCheckedFeaturedMessage").hide();
                //$(".table_checkbox").hide();
            }
            else {
                $("#btnDeleteAllCheckedFeaturedMessage").show();
                // $(".table_checkbox").show();
            }
            window.location.href = "/SuperAdmin/FeaturedMessage";
        });
    }

    // Callback function of checked Deleted messages
    var deletedCheckedMessagesCallback = function (data) {
        swal(
            'The featured messages have been successfully deleted.',
            '',
            'success'
        ).then(function () {
            var rowsCount = $('#tblFeaturdeMessageList tbody tr').length;

            if (rowsCount <= 1) {
                $("#btnDeleteAllCheckedFeaturedMessage").hide();
                //$(".table_checkbox").hide();
            }
            else {
                $("#btnDeleteAllCheckedFeaturedMessage").show();
                // $(".table_checkbox").show();
            }
            window.location.href = "/SuperAdmin/FeaturedMessage";
        });
    }
});

