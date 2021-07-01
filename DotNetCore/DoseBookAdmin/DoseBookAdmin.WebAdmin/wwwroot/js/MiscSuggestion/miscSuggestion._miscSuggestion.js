$(document).ready(function () {
    // DataTable
    //$(".table .table_title").click(function (e) {
    //    $(".table .table_title").not($(this)).removeClass("active");
    //    $(this).addClass("active");
    //    var iconClass = $(this).children(".icon").hasClass("desc") ? 'asc' : 'desc';
    //    $(this).children(".icon").removeClass("asc desc");
    //    $(this).children(".icon").addClass(iconClass);
    //});

    var doseDataListTable = $('#tblMiscSuggestions').DataTable({
        "pageLength": 10
    });

    $(".dataTables_empty").text("There are no misc suggestion yet.");

    $('#txtSearchDoseDataUp').keyup(function () {
        doseDataListTable.search($(this).val()).draw();
    });

    var rowsCount = $('#tblMiscSuggestions tbody tr').length;
    //alert(rowsCount);
    if (rowsCount < 10) {
        $("#tblMiscSuggestions_paginate").hide();
    }
    else {
        $("#tblMiscSuggestions_paginate").show();
    }

    if (rowsCount <= 1) {
        $("#btnDeleteSelected").hide();
        //$(".table_checkbox").hide();
    }
    else {
        $("#btnDeleteSelected").show();
        // $(".table_checkbox").show();
    }

    $('.dataTables_length').hide();
    $('.dataTables_filter').hide();
    $('.dataTables_info').hide();



    $('#btnAddMiscSuggestion').on('click', function () {
        if ($('#hdnDoctorId').val() != undefined) {
            if (parseInt($('#hdnDoctorId').val()) > 0) { window.location.href = "/MiscSuggestion/AddMiscSuggestion?DoctorId=" + $('#hdnDoctorId').val(); }
            else {
                swal("Please select a doctor to add the misc suggestion.", '', 'error').then(function () {
                    $(".drop_menu").find(".drop_menu_sub").show();
                });
            }
        }
    });

    // Delete all checked user on top delete button click
    $("#btnDeleteSelected").on("click", function () {
        var targetIds = {};
        targetIds.ItemIds = [];

        $('#tbldMiscSuggestions').find('input[type="checkbox"]:checked').each(function () {

            var row = $(this).parents('tr')[0];;
            var newsId = parseInt($(row).find('span').text());
            targetIds.ItemIds.push(newsId);
        });

        if (targetIds.ItemIds.length > 0) {
            swal({
                title: 'Are you sure you want to delete these misc suggestions?',
                text: "",
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes',
                cancelButtonText: 'No'
            }).then(function (result) {

                if (result.value) {
                    //  deleteNews method to delete news
                    miscSuggestionDataService.deleteMiscSuggestions("/MiscSuggestion/DeleteMiscSuggestion", targetIds, deletedMiscSuggestionCallbackAllMiscSuggestion);
                }
            });
        }

        else {
            swal('Please check at least one misc suggestion to delete using the checkboxes.');
        }
    });


    $('#tblMiscSuggestions').on('click', '.miscSuggestionItem', function () {
        var row = $(this).parents('tr')[0];;
        var newsId = parseInt($(row).find('span').text());
        var targetIds = {};
        targetIds.ItemIds = [];
        targetIds.ItemIds.push(newsId);

        swal({
            title: 'Are you sure you want to delete this misc suggestion?',
            text: "",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes',
            cancelButtonText: 'No'
        }).then(function (result) {

            if (result.value) {
                //  deleteNews method to delete news
                miscSuggestionDataService.deleteMiscSuggestions("/MiscSuggestion/DeleteMiscSuggestion", targetIds, deletedMiscSuggestionCallback);
            }
        });
    });

   
    // All data communication will be happened using this medicineDoseDataService object from server
    var miscSuggestionDataService = new function () {
        deleteMiscSuggestions = function (url, users, callback) {
            $.post(url, { targetIds: users }, function (data) { callback(data) });
        }
        return {
            deleteMiscSuggestions: deleteMiscSuggestions
        };
    }();

    // Callback function of Deleted Users
    var deletedMiscSuggestionCallback = function (data) {
        console.table(data);
        swal(
            'The misc suggestion has been successfully deleted.',
            '',
            'success'
        ).then(function () {
            window.location.href = "/MiscSuggestion/Index";
        });
    }

    // Callback function of Deleted Users
    var deletedMiscSuggestionCallbackAllMiscSuggestion = function (data) {
        console.table(data);
        swal(
            'The medicine doses have been successfully deleted.',
            '',
            'success'
        ).then(function () {
            window.location.href = "/MiscSuggestion/Index";
        });
    }
});

function EditRedirect(id) {
    $('#hdnDoctor_Back').val($('#hdnDoctorId').val());

    window.location.href = '/MiscSuggestion/EditMiscSuggestion?MedcineId=' + parseInt(id) + '&SelectedDoctorId=' + parseInt($('#hdnDoctorId').val());
}
