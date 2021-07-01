$(document).ready(function () {
    // DataTable
    //$(".table .table_title").click(function (e) {
    //    $(".table .table_title").not($(this)).removeClass("active");
    //    $(this).addClass("active");
    //    var iconClass = $(this).children(".icon").hasClass("desc") ? 'asc' : 'desc';
    //    $(this).children(".icon").removeClass("asc desc");
    //    $(this).children(".icon").addClass(iconClass);
    //});

    var newsListTable = $('#tblVacancyList').DataTable({

        "pageLength": 10
    });

    $(".dataTables_empty").text("There are no vacancies yet.");

    $('#txtSearchNewsUp').keyup(function () {
        newsListTable.search($(this).val()).draw();
    });

    var rowsCount = $('#tblVacancyList tbody tr').length;
    //alert(rowsCount);
    if (rowsCount < 10) {
        $("#tblVacancyList_paginate").hide();
    }
    else {
        $("#tblVacancyList_paginate").show();
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



    $('#btnAddVacancy').on('click', function () {
        if ($('#hdnDepartmentId').val() != undefined) {
            if (parseInt($('#hdnDepartmentId').val()) > 0) { window.location.href = "/Vacancies/Add?DepartmentId=" + $('#hdnDepartmentId').val(); }
            else {
                swal("Please select a department to add the vacancy.", '', 'error').then(function () {
                    $(".drop_menu").find(".drop_menu_sub").show();
                });
            }
        }
    });

    // Delete all checked user on top delete button click
    $("#btnDeleteSelected").on("click", function () {
        var targetIds = {};
        targetIds.ItemIds = [];

        $('#tblVacancyList').find('input[type="checkbox"]:checked').each(function () {

            var row = $(this).parents('tr')[0];;
            var newsId = parseInt($(row).find('span').text());
            targetIds.ItemIds.push(newsId);
        });

        if (targetIds.ItemIds.length > 0) {
            swal({
                title: 'Are you sure you want to delete these vacancies?',
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
                    vacancyDataService.deleteVacancy("/Vacancies/DeleteVacancy", targetIds, deletedVacancyCallbackAllVacancies);
                }

            });
        }

        else {
            swal('Please select at least one vacancy to delete using the checkboxes.');
        }

    });


    $('#tblVacancyList').on('click', '.vacancyItem', function () {
        var row = $(this).parents('tr')[0];;
        var newsId = parseInt($(row).find('span').text());
        var targetIds = {};
        targetIds.ItemIds = [];
        targetIds.ItemIds.push(newsId);

        swal({
            title: 'Are you sure you want to delete this vacancy?',
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
                vacancyDataService.deleteVacancy("/Vacancies/DeleteVacancy", targetIds, deletedVacancyCallback);
            }
        });
    });

    // Delete User Confirnation Modal Popup
    $(".vacancyItem").on('click', function () {
        // Find the targeted User Id that will be deleted
        //var row = $(this).parents('tr')[0];;
        //var newsId = parseInt($(row).find('span').text());
        //var targetIds = {};
        //targetIds.ItemIds = [];
        //targetIds.ItemIds.push(newsId);

        //swal({
        //    title: 'Are you sure,?',
        //    text: "You won't be able to revert this!",
        //    type: 'warning',
        //    showCancelButton: true,
        //    confirmButtonColor: '#3085d6',
        //    cancelButtonColor: '#d33',
        //    confirmButtonText: 'Yes, delete it!'
        //}).then(function (result) {

        //    if (result.value) {
        //        //  deleteNews method to delete news
        //        vacancyDataService.deleteVacancy("/Vacancies/DeleteVacancy", targetIds, deletedVacancyCallback);
        //    }
        //});
    });

    // All data communication will be happened using this newsDataService object from server
    var vacancyDataService = new function () {
        deleteVacancy = function (url, users, callback) {
            $.post(url, { targetIds: users }, function (data) { callback(data) });
        }

        return {
            deleteVacancy: deleteVacancy
        };
    }();

    // Callback function of Deleted Users
    var deletedVacancyCallback = function (data) {
        console.table(data);
        swal(
            'The vacancy has been successfully deleted.',
            '',
            'success'
        ).then(function () {
            var depId = $('#hdnDepartmentId').val();
            //alert(DepartmentId);
            //window.location.href = "Index?DepartmentId=" + foo;
            var deptName = $('#hdnDepartmentName').val();
            //alert(DepartmentId);
            //window.location.href = "Index?DepartmentId=" + foo;
            GetVacancyByDepartmentWise(depId, deptName, $('#hdnUserRoleType').val());
            var rowsCount = $('#tblVacancyList tbody tr').length;
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
    var deletedVacancyCallbackAllVacancies = function (data) {
        console.table(data);
        swal(
            'The vacancies have been successfully deleted.',
            '',
            'success'
        ).then(function () {
            var depId = $('#hdnDepartmentId').val();
            //alert(DepartmentId);
            //window.location.href = "Index?DepartmentId=" + foo;
            var deptName = $('#hdnDepartmentName').val();
            //alert(DepartmentId);
            //window.location.href = "Index?DepartmentId=" + foo;
            GetVacancyByDepartmentWise(depId, deptName, $('#hdnUserRoleType').val());
            var rowsCount = $('#tblVacancyList tbody tr').length;
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
    $('#hdnDepartmentId_Back').val($('#hdnDepartmentId').val());

    window.location.href = '/Vacancies/Edit?VacancyId=' + parseInt(id) + '&SelectedDepartmentId=' + parseInt($('#hdnDepartmentId').val());
}
