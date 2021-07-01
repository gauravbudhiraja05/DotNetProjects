$(document).ready(function () {
    // DataTable
    //$(".table .table_title").click(function (e) {
    //    $(".table .table_title").not($(this)).removeClass("active");
    //    $(this).addClass("active");
    //    var iconClass = $(this).children(".icon").hasClass("desc") ? 'asc' : 'desc';
    //    $(this).children(".icon").removeClass("asc desc");
    //    $(this).children(".icon").addClass(iconClass);
    //});

    var doseDataListTable = $('#tblMedicineDoses').DataTable({
        "pageLength": 10
    });

    $(".dataTables_empty").text("There are no medicine dose yet.");

    $('#txtSearchDoseDataUp').keyup(function () {
        doseDataListTable.search($(this).val()).draw();
    });

    var rowsCount = $('#tblMedicineDoses tbody tr').length;
    //alert(rowsCount);
    if (rowsCount < 10) {
        $("#tblMedicineDoses_paginate").hide();
    }
    else {
        $("#tblMedicineDoses_paginate").show();
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



    $('#btnAddMedicineDose').on('click', function () {
        if ($('#hdnDoctorId').val() != undefined) {
            if (parseInt($('#hdnDoctorId').val()) > 0) { window.location.href = "/MedicineDose/AddMedicineDose?DoctorId=" + $('#hdnDoctorId').val(); }
            else {
                swal("Please select a doctor to add the medicine dose.", '', 'error').then(function () {
                    $(".drop_menu").find(".drop_menu_sub").show();
                });
            }
        }
    });

    // Delete all checked user on top delete button click
    $("#btnDeleteSelected").on("click", function () {
        var targetIds = {};
        targetIds.ItemIds = [];

        $('#tblMedicineDoses').find('input[type="checkbox"]:checked').each(function () {

            var row = $(this).parents('tr')[0];;
            var newsId = parseInt($(row).find('span').text());
            targetIds.ItemIds.push(newsId);
        });

        if (targetIds.ItemIds.length > 0) {
            swal({
                title: 'Are you sure you want to delete these medicine doses?',
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
                    medicineDoseDataService.deleteMedicineDoses("/MedicineDose/DeleteMedicineDose", targetIds, deletedMedicineDoseCallbackAllMedicineDose);
                }
            });
        }

        else {
            swal('Please select at least one medicine dose to delete using the checkboxes.');
        }

    });


    $('#tblMedicineDoses').on('click', '.medicineDoseItem', function () {
        var row = $(this).parents('tr')[0];;
        var newsId = parseInt($(row).find('span').text());
        var targetIds = {};
        targetIds.ItemIds = [];
        targetIds.ItemIds.push(newsId);

        swal({
            title: 'Are you sure you want to delete this medicine dose?',
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
                medicineDoseDataService.deleteMedicineDoses("/MedicineDose/DeleteMedicineDose", targetIds, deletedMedicineDoseCallback);
            }
        });
    });

   
    // All data communication will be happened using this medicineDoseDataService object from server
    var medicineDoseDataService = new function () {
        deleteMedicineDoses = function (url, users, callback) {
            $.post(url, { targetIds: users }, function (data) { callback(data) });
        }
        return {
            deleteMedicineDoses: deleteMedicineDoses
        };
    }();

    // Callback function of Deleted Users
    var deletedMedicineDoseCallback = function (data) {
        console.table(data);
        swal(
            'The medicine dose has been successfully deleted.',
            '',
            'success'
        ).then(function () {
            window.location.href = "/MedicineDose/Index";
        });
    }

    // Callback function of Deleted Users
    var deletedMedicineDoseCallbackAllMedicineDose = function (data) {
        console.table(data);
        swal(
            'The medicine doses have been successfully deleted.',
            '',
            'success'
        ).then(function () {
            window.location.href = "/MedicineDose/Index";
        });
    }
});

function EditRedirect(id) {
    $('#hdnDoctor_Back').val($('#hdnDoctorId').val());

    window.location.href = '/MedicineDose/EditMedicineDose?MedcineId=' + parseInt(id) + '&SelectedDoctorId=' + parseInt($('#hdnDoctorId').val());
}
