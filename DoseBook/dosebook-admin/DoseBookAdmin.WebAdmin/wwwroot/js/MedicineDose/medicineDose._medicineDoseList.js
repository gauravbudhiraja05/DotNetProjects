$(document).ready(function () {
    // DataTable
    //$(".table .table_title").click(function (e) {
    //    $(".table .table_title").not($(this)).removeClass("active");
    //    $(this).addClass("active");
    //    var iconClass = $(this).children(".icon").hasClass("desc") ? 'asc' : 'desc';
    //    $(this).children(".icon").removeClass("asc desc");
    //    $(this).children(".icon").addClass(iconClass);
    //});

    var medicineDoseListTable = $('#tblMedicineDoses').DataTable({
        "pageLength": 10
    });

    $(".dataTables_empty").text("There are no medicine doses yet.");

    $('#txtSearchMedicineDoseUp').keyup(function () {
        medicineDoseListTable.search($(this).val()).draw();
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
                    medicineDoseService.deleteMedicineDoses("/MedicineDose/DeleteDoctorMedicineDose", targetIds, deletedMedicineDoseCallbackAllMedicineDoses);
                }
            });
        }

        else {
            swal('Please check at least one medicine dose to delete using the checkboxes.');
        }

    });


    $('#tblMedicineDoses').on('click', '.medicineDoseItem', function () {
        var row = $(this).parents('tr')[0];;
        var newsId = parseInt($(row).find('span').text());
        var targetIds = {};
        targetIds.ItemIds = [];
        targetIds.ItemIds.push(newsId);

        swal({
            title: 'Are you sure you want to delete this doctor medicine dose?',
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
                medicineDoseService.deleteMedicineDosess("/MedicineDose/DeleteDoctorMedicineDose", targetIds, deletedMedicineDoseDoseCallback);
            }
        });
    });


    // All data communication will be happened using this medicineDoseDataService object from server
    var medicineDoseService = new function () {
        deleteMedicineDoses = function (url, users, callback) {
            $.post(url, { targetIds: users }, function (data) { callback(data) });
        }
        return {
            deleteMedicineDoses: deleteMedicineDoses
        };
    }();

    // Callback function of Deleted Users
    var deletedMedicineDoseDoseCallback = function (data) {
        console.table(data);
        swal(
            'The medicine dose has been successfully deleted.',
            '',
            'success'
        ).then(function () {
            window.location.href = "/MedicineDose/DoctorMedicineDose";
        });
    }

    // Callback function of Deleted Users
    var deletedMedicineDoseCallbackAllMedicineDoses = function (data) {
        console.table(data);
        swal(
            'The medicine doses have been successfully deleted.',
            '',
            'success'
        ).then(function () {
            window.location.href = "/MedicineDose/DoctorMedicineDose";
        });
    }
});
