$(document).ready(function () {
    // DataTable
    //$(".table .table_title").click(function (e) {
    //    $(".table .table_title").not($(this)).removeClass("active");
    //    $(this).addClass("active");
    //    var iconClass = $(this).children(".icon").hasClass("desc") ? 'asc' : 'desc';
    //    $(this).children(".icon").removeClass("asc desc");
    //    $(this).children(".icon").addClass(iconClass);
    //});

    var doseDataListTable = $('#tblClassificationResults').DataTable({
        "pageLength": 10
    });

    $(".dataTables_empty").text("There are no classification result yet.");

    $('#txtSearchClassificationResultUp').keyup(function () {
        doseDataListTable.search($(this).val()).draw();
    });

    var rowsCount = $('#tblClassificationResults tbody tr').length;
    //alert(rowsCount);
    if (rowsCount < 10) {
        $("#tblClassificationResults_paginate").hide();
    }
    else {
        $("#tblClassificationResults_paginate").show();
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



    $('#btnAddClassificationResult').on('click', function () {
        if ($('#hdnDoctorId').val() != undefined) {
            if (parseInt($('#hdnDoctorId').val()) > 0) { window.location.href = "/ClassificationResult/AddClassificationResult?DoctorId=" + $('#hdnDoctorId').val(); }
            else {
                swal("Please select a doctor to add the classification result.", '', 'error').then(function () {
                    $(".drop_menu").find(".drop_menu_sub").show();
                });
            }
        }
    });

    // Delete all checked user on top delete button click
    $("#btnDeleteSelected").on("click", function () {
        var targetIds = {};
        targetIds.ItemIds = [];

        $('#tblClassificationResults').find('input[type="checkbox"]:checked').each(function () {

            var row = $(this).parents('tr')[0];;
            var newsId = parseInt($(row).find('span').text());
            targetIds.ItemIds.push(newsId);
        });

        if (targetIds.ItemIds.length > 0) {
            swal({
                title: 'Are you sure you want to delete these classification results?',
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
                    classificationResultService.deleteClassificationResults("/ClassificationResult/DeleteClassificationResult", targetIds, deletedClassificationResultCallbackAllClassificationResult);
                }
            });
        }

        else {
            swal('Please select at least one classification result to delete using the checkboxes.');
        }

    });


    $('#tblClassificationResults').on('click', '.classificationResultItem', function () {
        var row = $(this).parents('tr')[0];;
        var newsId = parseInt($(row).find('span').text());
        var targetIds = {};
        targetIds.ItemIds = [];
        targetIds.ItemIds.push(newsId);

        swal({
            title: 'Are you sure you want to delete this classification result?',
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
                classificationResultService.deleteClassificationResults("/ClassificationResult/DeleteClassificationResult", targetIds, deletedClassificationResultCallback);
            }
        });
    });

   
    // All data communication will be happened using this medicineDoseDataService object from server
    var classificationResultService = new function () {
        deleteClassificationResults = function (url, users, callback) {
            $.post(url, { targetIds: users }, function (data) { callback(data) });
        }
        return {
            deleteClassificationResults: deleteClassificationResults
        };
    }();

    // Callback function of Deleted Users
    var deletedClassificationResultCallback = function (data) {
        console.table(data);
        swal(
            'The classification result has been successfully deleted.',
            '',
            'success'
        ).then(function () {
            window.location.href = "/ClassificationResult/Index";
        });
    }

    // Callback function of Deleted Users
    var deletedClassificationResultCallbackAllClassificationResult = function (data) {
        console.table(data);
        swal(
            'The classification results have been successfully deleted.',
            '',
            'success'
        ).then(function () {
            window.location.href = "/ClassificationResult/Index";
        });
    }
});

function EditRedirect(id) {
    $('#hdnDoctor_Back').val($('#hdnDoctorId').val());
    window.location.href = '/ClassificationResult/EditClassificationResult?ClassificationResultId=' + parseInt(id) + '&SelectedDoctorId=' + parseInt($('#hdnDoctorId').val());
}
