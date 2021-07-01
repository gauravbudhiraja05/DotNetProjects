$(document).ready(function () {
    // DataTable
    //$(".table .table_title").click(function (e) {
    //    $(".table .table_title").not($(this)).removeClass("active");
    //    $(this).addClass("active");
    //    var iconClass = $(this).children(".icon").hasClass("desc") ? 'asc' : 'desc';
    //    $(this).children(".icon").removeClass("asc desc");
    //    $(this).children(".icon").addClass(iconClass);
    //});

    var doseDataListTable = $('#tblDocuments').DataTable({
        "pageLength": 10
    });

    $(".dataTables_empty").text("There are no document yet.");

    $('#txtSearchDoseDataUp').keyup(function () {
        doseDataListTable.search($(this).val()).draw();
    });

    var rowsCount = $('#tblDocuments tbody tr').length;
    //alert(rowsCount);
    if (rowsCount < 10) {
        $("#tblDocuments_paginate").hide();
    }
    else {
        $("#tblDocuments_paginate").show();
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



    $('#btnAddDocument').on('click', function () {
        if ($('#hdnDoctorId').val() != undefined) {
            if (parseInt($('#hdnDoctorId').val()) > 0) { window.location.href = "/Document/AddDocument?DoctorId=" + $('#hdnDoctorId').val(); }
            else {
                swal("Please select a doctor to add the document.", '', 'error').then(function () {
                    $(".drop_menu").find(".drop_menu_sub").show();
                });
            }
        }
    });

    // Delete all checked user on top delete button click
    $("#btnDeleteSelected").on("click", function () {
        var targetIds = {};
        targetIds.ItemIds = [];

        $('#tblDocuments').find('input[type="checkbox"]:checked').each(function () {

            var row = $(this).parents('tr')[0];;
            var newsId = parseInt($(row).find('span').text());
            targetIds.ItemIds.push(newsId);
        });

        if (targetIds.ItemIds.length > 0) {
            swal({
                title: 'Are you sure you want to delete these documents?',
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
                    documentDataService.deleteDocuments("/Document/DeleteDocument", targetIds, deletedDocumentCallbackAllDocument);
                }
            });
        }

        else {
            swal('Please select at least one document to delete using the checkboxes.');
        }

    });


    $('#tblDocuments').on('click', '.documentItem', function () {
        var row = $(this).parents('tr')[0];;
        var newsId = parseInt($(row).find('span').text());
        var targetIds = {};
        targetIds.ItemIds = [];
        targetIds.ItemIds.push(newsId);

        swal({
            title: 'Are you sure you want to delete this document?',
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
                documentDataService.deleteDocuments("/Document/DeleteDocument", targetIds, deletedDocumentCallback);
            }
        });
    });

   
    // All data communication will be happened using this documentDataService object from server
    var documentDataService = new function () {
        deleteDocuments = function (url, users, callback) {
            $.post(url, { targetIds: users }, function (data) { callback(data) });
        }
        return {
            deleteDocuments: deleteDocuments
        };
    }();

    // Callback function of Deleted Users
    var deletedDocumentCallback = function (data) {
        console.table(data);
        swal(
            'The document has been successfully deleted.',
            '',
            'success'
        ).then(function () {
            window.location.href = "/Document/Index";
        });
    }

    // Callback function of Deleted Users
    var deletedDocumentCallbackAllDocument = function (data) {
        console.table(data);
        swal(
            'The documents have been successfully deleted.',
            '',
            'success'
        ).then(function () {
            window.location.href = "/Document/Index";
        });
    }
});

function EditRedirect(id) {
    $('#hdnDoctor_Back').val($('#hdnDoctorId').val());

    window.location.href = '/Document/EditDocument?MedcineId=' + parseInt(id) + '&SelectedDoctorId=' + parseInt($('#hdnDoctorId').val());
}
