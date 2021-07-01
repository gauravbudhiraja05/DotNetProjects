$(document).ready(function () {
    // DataTable
    //$(".table .table_title").click(function (e) {
    //    $(".table .table_title").not($(this)).removeClass("active");
    //    $(this).addClass("active");
    //    var iconClass = $(this).children(".icon").hasClass("desc") ? 'asc' : 'desc';
    //    $(this).children(".icon").removeClass("asc desc");
    //    $(this).children(".icon").addClass(iconClass);
    //});

    var doseDataListTable = $('#tblDoseDataList').DataTable({
        "pageLength": 10
    });

    $(".dataTables_empty").text("There are no dose data yet.");

    $('#txtSearchDoseDataUp').keyup(function () {
        doseDataListTable.search($(this).val()).draw();
    });

    var rowsCount = $('#tblDoseDataList tbody tr').length;
    //alert(rowsCount);
    if (rowsCount < 10) {
        $("#tblDoseDataList_paginate").hide();
    }
    else {
        $("#tblDoseDataList_paginate").show();
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



    $('#btnAddDoseData').on('click', function () {
        if ($('#hdnDoseMetaTypeId').val() != undefined) {
            if (parseInt($('#hdnDoseMetaTypeId').val()) > 0) { window.location.href = "/DoseData/Add?DoseMetaTypeId=" + $('#hdnDoseMetaTypeId').val(); }
            else {
                swal("Please select a dose meta type to add the dose data.", '', 'error').then(function () {
                    $(".drop_menu").find(".drop_menu_sub").show();
                });
            }
        }
    });

    // Delete all checked user on top delete button click
    $("#btnDeleteSelected").on("click", function () {
        var targetIds = {};
        targetIds.ItemIds = [];

        $('#tblDoseDataList').find('input[type="checkbox"]:checked').each(function () {

            var row = $(this).parents('tr')[0];;
            var newsId = parseInt($(row).find('span').text());
            targetIds.ItemIds.push(newsId);
        });

        if (targetIds.ItemIds.length > 0) {
            swal({
                title: 'Are you sure you want to delete these dose data?',
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
                    doseDataService.deletedoseData("/DoseData/DeleteDoseDataByIds", targetIds, deletedDoseDataCallbackAllDoseDatas);
                }

            });
        }

        else {
            swal('Please select at least one dose data to delete using the checkboxes.');
        }

    });


    $('#tblDoseDataList').on('click', '.doseDataItem', function () {
        var row = $(this).parents('tr')[0];;
        var newsId = parseInt($(row).find('span').text());
        var targetIds = {};
        targetIds.ItemIds = [];
        targetIds.ItemIds.push(newsId);

        swal({
            title: 'Are you sure you want to delete this doseData?',
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
                doseDataService.deleteDoseData("/DoseData/DeleteDoseDataByIds", targetIds, deletedDoseDataCallback);
            }
        });
    });

   
    // All data communication will be happened using this newsDataService object from server
    var doseDataService = new function () {
        deleteDoseData = function (url, users, callback) {
            $.post(url, { targetIds: users }, function (data) { callback(data) });
        }
        return {
            deleteDoseData: deleteDoseData
        };
    }();

    // Callback function of Deleted Users
    var deletedDoseDataCallback = function (data) {
        console.table(data);
        swal(
            'The dose data has been successfully deleted.',
            '',
            'success'
        ).then(function () {
            var doseMetaTypeId = $('#hdnDoseMetaTypeId').val();
            //alert(DepartmentId);
            //window.location.href = "Index?DepartmentId=" + foo;
            var doseMetaTypeName = $('#hdnDoseMetaTypeName').val();
            //alert(DepartmentId);
            //window.location.href = "Index?DepartmentId=" + foo;
            GetDoseDataByDoseMetaTypeWise(doseMetaTypeId, doseMetaTypeName);
            var rowsCount = $('#tblDoseDataList tbody tr').length;
            //alert(rowsCount);
            if (rowsCount <= 1) {
                $("#btnDeleteSelected").hide();
                //$(".table_checkbox").hide();
            }
            else {
                $("#btnDeleteSelected").show();
                // $(".table_checkbox").show();
            }
        });
    }

    // Callback function of Deleted Users
    var deletedDoseDataCallbackAllDoseDatas = function (data) {
        console.table(data);
        swal(
            'The doseDatas have been successfully deleted.',
            '',
            'success'
        ).then(function () {
            var doseMetaTypeId = $('#hdnDoseMetaTypeId').val();
            //alert(DepartmentId);
            //window.location.href = "Index?DepartmentId=" + foo;
            var doseMetaTypeName = $('#hdnDoseMetaTypeName').val();
            //alert(DepartmentId);
            //window.location.href = "Index?DepartmentId=" + foo;
            GetDoseDataByDoseMetaTypeWise(doseMetaTypeId, doseMetaTypeName);
            var rowsCount = $('#tblDoseDataList tbody tr').length;
            //alert(rowsCount);
            if (rowsCount <= 1) {
                $("#btnDeleteSelected").hide();
                //$(".table_checkbox").hide();
            }
            else {
                $("#btnDeleteSelected").show();
                // $(".table_checkbox").show();
            }
        });
    }
});

function EditRedirect(id) {
    $('#hdnDoseMetaType_Back').val($('#hdnDoseMetaTypeId').val());

    window.location.href = '/DoseData/Edit?DoseDataId=' + parseInt(id) + '&SelectedDoseMetaTypeId=' + parseInt($('#hdnDoseMetaTypeId').val());
}
