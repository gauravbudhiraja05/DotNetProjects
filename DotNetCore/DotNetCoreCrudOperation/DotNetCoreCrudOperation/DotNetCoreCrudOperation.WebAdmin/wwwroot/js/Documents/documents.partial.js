

$(document).ready(function () {

    $(function () {
       // debugger;
        $(".drag_list").sortable({
            placeholder: "ui-state-highlight"
        });

        $(".drag_list").disableSelection();

        if ($('.ui-state-default .drag_list').size() > 0) {
            $('.ui-state-default .drag_list').parent('.ui-state-default').addClass("has_sub");
        }
    });

    // Select creation date as today
    $('#CreationDate').val(getFormattedDate());

   


   
    var deleteBoxItem = "";

    $('.upload').change(function () {
        //debugger;
        var sizeinbytes = this.files[0].size;
        //var fSExt = new Array('Bytes', 'KB', 'MB', 'GB');
        //fSize = sizeinbytes; i = 0; while (fSize > 900) { fSize /= 1024; i++; }

        //alert((Math.round(fSize * 100) / 100) + ' ' + fSExt[i]);

        sizeinbytes = sizeinbytes / 1048576; //size in mb 
        //alert("Uploaded File Size is" + sizeinbytes + "MB");

        if (sizeinbytes > 20)
        {
            var lblError = $("#lblDocError");
            lblError.html("Please upload files having size less than 20 mb.");
            $("#AddDocumentBtn").hide();
            return false;
        }

        var filename = $(this).val().split('\\').pop();
        var displayFileName = "FileName : " + filename;
        $("#docFileName").text(displayFileName);
        ValidateUploadedFile();
    });

    // Select publish date as today
    var datepicker = $('.datepicker-here').datepicker().data('datepicker');
    datepicker.selectDate(new Date());
    var ValidateUploadedFile = function () {
        var allowedFiles = ["pdf", "xlsx", "xls", "doc", "docx", "ppt", "pptx"];
        var filename = $('.upload').val().split('\\').pop();
        var lblError = $("#lblDocError");
        if ($.inArray(filename.split('.')[1], allowedFiles) == -1) {
            lblError.html("Please upload files having extensions: <b>" + allowedFiles.join(', ') + "</b> only.");
            $("#AddDocumentBtn").hide();
            return false;
        }
        lblError.html('');
        $("#AddDocumentBtn").show();
        return true;
    }

    //var ValidateUploadedFile = function ()
    //{
    //    var allowedFiles = [".pdf",".xlsx",".xls",".doc",".docx",".ppt",".pptx"];
    //    var fileUpload = $(".upload");
    //    var lblError = $("#lblDocError");
    //    var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
    //    if (!regex.test(fileUpload.val().toLowerCase())) {
    //        lblError.html("Please upload files having extensions: <b>" + allowedFiles.join(', ') + "</b> only.");
    //        $("#AddDocumentBtn").hide();
    //        return false;
    //    }
    //    lblError.html('');
    //    $("#AddDocumentBtn").show();
    //    return true;
    //}

    var headingOfDocumnetList = $('#hdnDepartmentName').val();
    if (headingOfDocumnetList == "Select Departments")
    {
        headingOfDocumnetList = "All"
    }
    $('#headingH1').html(headingOfDocumnetList + ' Documents');

    $('.delete_item').on('click', function () {
        var type = ($(this).attr('class')).split(' ')[1];
        var value = $(this).attr('id');
        var targetIds = {};
        targetIds.ItemIds = [];
        targetIds.ItemTypes = [];
        targetIds.ItemIds.push(value);
        targetIds.ItemTypes.push(type);
        var msgText = "";
       // debugger;
        if (type == "documentfolder") {

            $("#OnlyForOpenParentFolder").val($(this).attr("ParentFolderpopupid"));
            $("#OnlyForOpenSubFolder").val($(this).attr("SubFolderpopId"));

            deleteBoxItem = "Document has been deleted successsfully";
            msgText = "Are you sure, you want to delete this document?";
            swal({
                title: msgText,
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
                    documentsDataService.deleteDocumentsOrFolders("/Documents/DeleteDocumentorFolder", targetIds, deletedDocumentsCallback);
                }
            });
        }

        else
        {
            $("#OnlyForOpenParentFolder").val($(this).attr("ParentFolderpopupid"));
            $("#OnlyForOpenSubFolder").val($(this).attr("SubFolderpopId"));

            var parentIdForDelete = $(this).attr("ParentFolderpopupid");
            var swalTitleText = "";
            debugger;
            if (parentIdForDelete == undefined || parentIdForDelete == 0 || parentIdForDelete == "") {
                swalTitleText = "You cannot delete this folder. Please delete any documents within it first.";
                deleteBoxItem = "The folder has been deleted successfully. ";
                msgText = "Are you sure you want to delete this folder and its related documents?";
            }
            else
            {
                swalTitleText = "You can not delete this sub folder. Please delete any documents within it first.";
                deleteBoxItem = "The sub folder has been deleted successfully. ";
                msgText = "Are you sure you want to delete this sub folder and its related documents?";
            }

            $.get("/Documents/IsFolderDeletable", { id: value }).then(function (data) {

                if (data == false) {
                    swal({
                        title: swalTitleText,
                        text: "",
                        type: 'warning'
                    });
                }

                else {
                    //deleteBoxItem = "The folder has been deleted successfully.";
                    //msgText = "Are you sure you want to delete this folder and its related documents?";
                    swal({
                        title: msgText,
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
                            documentsDataService.deleteDocumentsOrFolders("/Documents/DeleteDocumentorFolder", targetIds, deletedDocumentsCallback);
                        }
                    });
                }
            });
           
        }
        

        //alert(value);
    });

    // All data communication will be happened using this newsDataService object from server
    var documentsDataService = new function () {
        deleteDocumentsOrFolders = function (url, users, callback) {
            $.post(url, { targetIds: users }, function (data) { callback(data) });
        }

        return {
            deleteDocumentsOrFolders: deleteDocumentsOrFolders
        };
    }();


    // Callback function of Deleted Users
    var deletedDocumentsCallback = function (data) {
       
        swal(
            deleteBoxItem,
            '',
            'success'
        ).then(function () {
            //var foo = $('#hdnDepartmentId').val();
            //debugger;
            var departmentId = $("#hiddenDepartmentId").val();
            var ParentPopupId = $("#OnlyForOpenParentFolder").val();
            var SubFolderPopupId = $("#OnlyForOpenSubFolder").val();
            //window.location.href = "/Documents/Index?DepartmentId=" + foo;
            GetDocumentsByDepartmentWise_newDelete(departmentId, ParentPopupId, SubFolderPopupId);
        });
    }
});

function GetDocumentsByDepartmentWise_newDelete(departmentId, ParentPopupId, SubFolderPopupId) {
    $.get('/Documents/GetDocumentsByDepartmentWise/' + departmentId, function (data) {
        $('#partialDocumentList').html(data);
        $('#partialDocumentList').show();

        //$('#hdnDepartmentId').val(departmentId);
        //$('#hdnDepartmentName').val(departmentName);

        if (departmentId == 0) {
            $('#btnSearch').prop("disabled", true);
            $('#txtSearchNewsUp').prop("disabled", true);
        }

        if (departmentId > 0) {
            $('#btnSearch').prop("disabled", false);
            $('#txtSearchNewsUp').prop("disabled", false);
        }
        //debugger;
        var FolderIdStartChar = "Folder";
        if (SubFolderPopupId > 0) {
           
            $("#" + FolderIdStartChar +ParentPopupId).prev().trigger('click');
            $("#" + FolderIdStartChar +SubFolderPopupId).prev().trigger('click');
        }
        else {
            $("#" + FolderIdStartChar +ParentPopupId).prev().trigger('click');
        }

    });
}