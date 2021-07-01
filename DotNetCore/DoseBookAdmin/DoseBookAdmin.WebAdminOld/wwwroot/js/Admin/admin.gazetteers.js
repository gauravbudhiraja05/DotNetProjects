$(document).ready(function () {

    //Select Current Menu
    $('.nav-li').removeClass('current');
    $('#menuItemSuperAdmin').addClass('current');

    // DataTable for Gazetteers
    var gazetteersListTable = $('#tblGazetteers').DataTable({

        "pageLength": 10
    });
    $(".dataTables_empty").text("There is no Gazetteer file yet.");

    var rowsCount = $('#tblGazetteers tbody tr').length;
    //alert(rowsCount);
    if (rowsCount < 10) {
        $("#tblGazetteers_paginate").hide();
    }
    else {
        $("#tblGazetteers_paginate").show();
    }

    //if (rowsCount <= 1) {
    //    $("#btnDeleteGazetteers").hide();
    //    //$(".table_checkbox").hide();
    //}
    //else {
    //    $("#btnDeleteGazetteers").show();
    //    // $(".table_checkbox").show();
    //}

    // Hide un-neccessary controls of datatable plugin for tblGazetteers table
    $('.dataTables_length').hide();
    $('.dataTables_filter').hide();
    $('.dataTables_info').hide();

   
});

// Delete all checked Gazetteers on top delete button click
$("#btnDeleteGazetteers").on("click", function () {

    var targetIds = {};
    targetIds.ItemIds = [];

    $('#tblGazetteers').find('input[type="checkbox"]:checked').each(function () {

        var row = $(this).parents('tr')[0];;
        var gazetteerId = parseInt($(row).find('span').text());
        targetIds.ItemIds.push(gazetteerId);
    });

    if (targetIds.ItemIds.length > 0) {
        swal({
            title: 'Are you sure you want to delete these Gazetteers?',
            text: "",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes',
            cancelButtonText: 'No'
        }).then(function (result) {

            if (result.value) {
                //  deleteUsers method to delete gazetteers
                gazetteersDataService.deleteGazetteers("/SuperAdmin/DeleteGazetteers", targetIds, deletedGazetteersCallback);
            }

        });
    }

    else {
        swal('Please check at least one Gazateer to delete.');
    }

});



// All data communication will be happened using this gazetteersDataService object from server
var gazetteersDataService = new function () {
    deleteGazetteers = function (url, gazetteers, callback) {
        $.post(url, { targetIds: gazetteers }, function (data) { callback(data) });
    }

    return {
        deleteGazetteers: deleteGazetteers
    };
}();

// Callback function of Deleted Gazetteers
var deletedGazetteersCallback = function (data) {
    console.table(data);
    swal(
        'The Gazetteers have been successfully deleted.',
        '',
        'success'
    ).then(function () {

        var rowsCount = $('#tblGazetteers tbody tr').length;

        //if (rowsCount <= 1) {
        //    $("#btnDeleteGazetteers").hide();
        //    //$(".table_checkbox").hide();
        //}
        //else {
        //    $("#btnDeleteGazetteers").show();
        //    // $(".table_checkbox").show();
        //}
        window.location.href = "/SuperAdmin/Gazetteers";
    });
}