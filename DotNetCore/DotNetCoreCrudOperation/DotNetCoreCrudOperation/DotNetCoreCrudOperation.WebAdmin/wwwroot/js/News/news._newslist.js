$(document).ready(function () {
    // DataTable

    //$(".table .table_title").click(function (e) {
    //    $(".table .table_title").not($(this)).removeClass("active");
    //    $(this).addClass("active");
    //    var iconClass = $(this).children(".icon").hasClass("desc") ? 'asc' : 'desc';
    //    $(this).children(".icon").removeClass("asc desc");
    //    $(this).children(".icon").addClass(iconClass);
    //});

    var newsListTable = $('#tblNewsList').DataTable({

        "pageLength": 10
    });

    $(".dataTables_empty").text("There is no news yet!");

    $('#txtSearchNewsUp').keyup(function () {
        newsListTable.search($(this).val()).draw();
    });
    

    var rowsCount = $('#tblNewsList tbody tr').length;
    //alert(rowsCount);
    if (rowsCount < 10) {
        $("#tblNewsList_paginate").hide();
    }
    else {
        $("#tblNewsList_paginate").show();
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

        $('#tblNewsList').find('input[type="checkbox"]:checked').each(function () {
            var row = $(this).parents('tr')[0];;
            var newsId = parseInt($(row).find('span').text());
            targetIds.ItemIds.push(newsId);
        });

       

        if (targetIds.ItemIds.length > 0) {
            swal({
                title: "Are you sure you want to delete these news items?",
                text: "",
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes',
                cancelButtonText: 'No',
                width: '500px'
            }).then(function (result) {

                if (result.value) {
                    //  deleteNews method to delete news
                    newsDataService.deleteNews("/News/DeleteNews", targetIds, deletedNewsCallbackAllNews);
                }

            });
        }

        else {
            swal('Please select at least one news item using the checkboxes.');
        }

    });


    $('#tblNewsList').on('click', '.newsItem', function () {
        // Find the targeted User Id that will be deleted
        var row = $(this).parents('tr')[0];;
        var newsId = parseInt($(row).find('span').text());
        var targetIds = {};
        targetIds.ItemIds = [];
        targetIds.ItemIds.push(newsId);

        swal({
            title: "Are you sure you want to delete this news item?",
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
                newsDataService.deleteNews("/News/DeleteNews", targetIds, deletedNewsCallback);
            }
        });
    });

    // Delete User Confirnation Modal Popup
    $(".newsItem").on('click', function () {
        //// Find the targeted User Id that will be deleted
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
        //        newsDataService.deleteNews("/News/DeleteNews", targetIds, deletedNewsCallback);
        //    }
        //});
    });

    // All data communication will be happened using this newsDataService object from server
    var newsDataService = new function () {
        deleteNews = function (url, users, callback) {
            $.post(url, { targetIds: users }, function (data) { callback(data) });
        }

        return {
            deleteNews: deleteNews
        };
    }();

    // Callback function of Deleted Users
    var deletedNewsCallback = function (data) {
        console.table(data);
        swal(
            'The news item has been successfully deleted.',
            '',
            'success'
        ).then(function () {
            var depId = $('#hdnDepartmentId').val();
            var deptName = $('#hdnDepartmentName').val();
           
            //alert(DepartmentId);
            //window.location.href = "Index?DepartmentId=" + foo;
            GetNewsByDepartmentWise(depId, deptName, $('#hdnUserRoleType').val());
            var rowsCount = $('#tblNewsList tbody tr').length;
            if (rowsCount <= 1) {
                $("#btnDeleteSelected").hide();
                //$(".table_checkbox").hide();
            }
            else {
                $("#btnDeleteSelected").show();
                //$(".table_checkbox").show();
            }
        });
    }

    // Callback function of Deleted Users
    var deletedNewsCallbackAllNews = function (data) {
        console.table(data);
        swal(
            'The news items have been successfully deleted.',
            '',
            'success'
        ).then(function () {
            var depId = $('#hdnDepartmentId').val();
            var deptName = $('#hdnDepartmentName').val();

            //alert(DepartmentId);
            //window.location.href = "Index?DepartmentId=" + foo;
            GetNewsByDepartmentWise(depId, deptName, $('#hdnUserRoleType').val());
            var rowsCount = $('#tblNewsList tbody tr').length;
            if (rowsCount <= 1) {
                $("#btnDeleteSelected").hide();
                //$(".table_checkbox").hide();
            }
            else {
                $("#btnDeleteSelected").show();
                //$(".table_checkbox").show();
            }
        });
    }
});

