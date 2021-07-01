var ChooseFolderArray = [];
$(document).ready(function () {



    //var ChooseDepartmentId = $("#hiddenDepartmentId").val();
    //AutoPopulateAllFolders(ChooseDepartmentId);

    $('.datepicker-here').datepicker({
        autoClose: true,
        format: 'mm/dd/yyyy',
        minDate: new Date(),
        position: 'top left', // Default position
        onHide: function (inst) {
            inst.update('position', 'top left'); // Update the position to the default again
        },
        onShow: function (inst, animationComplete) {
            // Just before showing the datepicker
            if (!animationComplete) {
                var iFits = false;
                // Loop through a few possible position and see which one fits
                $.each(['top left', 'left bottom', 'bottom left', 'top center', 'bottom center'], function (i, pos) {
                    if (!iFits) {
                        inst.update('position', pos);
                        var fits = isElementInViewport(inst.$datepicker[0]);
                        if (fits.all) {
                            iFits = true;
                        }
                    }
                });
            }
        },

    });

    $('[data-toggle="tooltip"]').tooltip();


    /*-----------------------------------
            Check All
    -----------------------------------*/
    $("#errorMsgFolderName").hide();
    $("#errorMsgSubFolderName").hide();
    var folderId = 0;
    var editfolderId = 0;
    var subfolderId = 0;
    var globalParentFolderId = 0;

    //var fileName = "";
    //var fileType = "";
    //var fileSize = "";


    /*-----------------------------------
       Table sort
-----------------------------------*/

    $(".table .table_title").click(function (e) {
        $(".table .table_title").not($(this)).removeClass("active");
        $(this).addClass("active");
        var iconClass = $(this).children(".icon").hasClass("desc") ? 'asc' : 'desc';
        $(this).children(".icon").removeClass("asc desc");
        $(this).children(".icon").addClass(iconClass);
    });


    $(".check_all").change(function () {  //"select all" change
        var status = this.checked; // "select all" checked status
        $(this).closest("table").find(".check_item").each(function () { //iterate all listed checkbox items
            this.checked = status; //change ".checkbox" checked status
        });
    });

    $('.check_item').change(function () {
        var st = false;
        $(this).closest("table").find(".check_item").each(function () {
            if ($(this).prop("checked")) {
                st = true;
            } else {
                st = false;
                return false;
            }
        });
        if (st) {
            $(this).closest("table").find(".check_all").prop("checked", true);
        } else {
            $(this).closest("table").find(".check_all").prop("checked", false);
        }
    });

    $(".folder_area .create_folder").click(function () {
        $(this).parent().parent().parent(".folder_area").children(".crate_folder_box").fadeToggle(200);
    });

    $(".folder-container li.folder-item").click(function () {
        $(this).parent(".folder-container").children(".folder-wrapper").fadeToggle(200);
    });

    $(".folder_name").click(function () {
        $(this).parent().children(".drag_list").slideToggle(300);
    });

    $(".create_folder").click(function (event) {
        //debugger;
        // event.preventDefault();
        var result = CheckIfDepartmentSelected();
        $("#errorMsgFolderName").hide();
        $("#errorMsgSubFolderName").hide();
        $("#errorDuplicateFolderName").hide();
        $("#errorDuplicateSubFolderName").hide();
        $("#folder_name").val("");
        if (result == false) {
            swal("Please select a department to create a new folder.", '', 'error').then(function (data) {
                $("html, body").animate({ scrollTop: 0 }, "slow");
                //$(".drop_menu").focus();
                $(".drop_menu").find(".drop_menu_sub").show();
            })

        }
        else {
            folderId = 0;
            editfolderId = 0;
            subfolderId = 0;
            globalParentFolderId = 0;
            $("#add_folder_popup, .popup_window_overlay").fadeToggle(200);
        }
    });


    $("#ChooseFolderName").on('change', function () {

        $("#ChooseSubFolderName").show();
        $("#ChooseSubFolderName").html("");
        var ddlParentFolderId = $(this).val();
        $.each(ChooseFolderArray, function (key, value) {
            if (value.folderParentId == ddlParentFolderId) {
                $("#ChooseSubFolderName").append($("<option></option>").val(value.folderId).html(value.folderName));
            }

        });

    });

    $(".doc_popup").click(function () {
        debugger;
        //$("#ChooseSubFolderName").hide();
        $("#ChooseFolderName").html("");
        $("#ChooseSubFolderName").html("");
        $.each(ChooseFolderArray, function (key, value) {
            if (value.folderParentId == 0) {
                $("#ChooseFolderName").append($("<option></option>").val(value.folderId).html(value.folderName));
            }

        });

        $('#div_docHeading').html("Add Document");
        $("#AddDocumentBtn").show();
        var result = CheckIfDepartmentSelected();
        $("#lblDocTitleError").text("");
        $("#lblDocDescriptionError").text("");
        $("#doctitle").val("");
        $("#docdescription").val("");
        $("#docFileName").text("");
        $("#lblDocError").text("");
        $("#errorDuplicateDocTitleName").hide();
        $("#User_ID").val('');
        $("#getHdnFolderDepartmentId").val($(this).attr("FolderDepartmentId"));

        //if (result == false) {
        //    swal("Please select a department to add document");
        //}
        //else {
        // debugger;
        var tempId = $(this).attr("id");
        var newtempId = tempId.substring(6, tempId.length);
        var newintegerTempId = parseInt(newtempId);
        subfolderId = newintegerTempId; //$(this).attr("id");
        // debugger;
        $("#OnlyForOpenParentFolder").val($(this).attr("ParentFolderpopupid"));
        $("#subfolderIdForDoc").val(subfolderId);
        $("#add_doc_popup, .popup_window_overlay").fadeToggle(200);
        //}

        var ddlParentFolderId = $(this).attr("ParentFolderpopupid");

        $.each(ChooseFolderArray, function (key, value) {
            if (value.folderParentId == ddlParentFolderId) {
                $("#ChooseSubFolderName").append($("<option></option>").val(value.folderId).html(value.folderName));
            }

        });


        $('#ChooseFolderName option[value=' + ddlParentFolderId + ']').attr("selected", true);
        $('#ChooseSubFolderName option[value=' + subfolderId + ']').attr("selected", true);

        //// Disable Link Destination by default
        //$("#LinkDestination").prop("disabled", true);

        //// Enable document upload by default
        //$("#UploadFile").prop("disabled", false);

        // Select Document Type by default ob creation document
        $("#Type").val("Document");

        // Show or Hide the File Upload and Link Destination
        EnableDisableLinkDestinationAndFileUpload("Document");

        // Adjust Save button margin from top
        $(".save_btn").css('margin-top', '-55px');

    });

    // Document Type dropdown change handle 
    $("#Type").on('change', function () {
        var docType = $(this).val();
        EnableDisableLinkDestinationAndFileUpload(docType);

        // Adjust Save button margin
        if (docType == 'Document') {
            $(".save_btn").css('margin-top', '-55px');
        }
        else {
            $(".save_btn").css('margin-top', '0');
        }
    });

   

    $(".editDocumentbtn").click(function () {
        //debugger;
        $("#ChooseFolderName").html("");
        $("#ChooseSubFolderName").html("");
        $.each(ChooseFolderArray, function (key, value) {
            if (value.folderParentId == 0) {
                $("#ChooseFolderName").append($("<option></option>").val(value.folderId).html(value.folderName));
            }

        });


        $('#div_docHeading').html("Edit Document");
        $("#AddDocumentBtn").show();

        var result = CheckIfDepartmentSelected();
        $("#lblDocTitleError").text("");
        $("#lblDocDescriptionError").text("");
        $("#errorDuplicateDocTitleName").hide();
        $("#lblDocError").text("");
        $("#getHdnFolderDepartmentId").val($(this).attr("FolderDepartmentId"));
        //if (result == false) {
        //    swal("Please select a department to edit document");
        //}
        //else {
        var EditdocumentId = $(this).attr("id");
        populateDocumentValue(EditdocumentId);
        var SubFolderpopId = $(this).attr("SubFolderpopId");
        $("#subfolderIdForDoc").val(SubFolderpopId);
        $("#OnlyForOpenParentFolder").val($(this).attr("ParentFolderpopupid"));

        var ddlParentFolderId = $(this).attr("ParentFolderpopupid");

        $.each(ChooseFolderArray, function (key, value) {
            if (value.folderParentId == ddlParentFolderId) {
                $("#ChooseSubFolderName").append($("<option></option>").val(value.folderId).html(value.folderName));
            }

        });


        $('#ChooseFolderName option[value=' + ddlParentFolderId + ']').attr("selected", true);
        $('#ChooseSubFolderName option[value=' + SubFolderpopId + ']').attr("selected", true);

        //}
    });

    $(".popup_close_btn").click(function () {
        $(".popup_window, .popup_window_overlay").fadeOut(200);
    });


    $(".folder_popup").click(function () {
        // event.preventDefault();
        $("#errorMsgFolderName").hide();
        $("#errorMsgSubFolderName").hide();
        $("#errorDuplicateFolderName").hide();
        $("#errorDuplicateSubFolderName").hide();
        $("#folder_name").val("");
        var result = CheckIfDepartmentSelected();

        //if (result == false) {
        //    swal("Please select a department to add sub folder");
        //}
        //else {
        folderId = 0;
        editfolderId = 0;
        subfolderId = 0;
        globalParentFolderId = 0;
        $("#add_folder_popup, .popup_window_overlay").fadeToggle(200);
        $("#getHdnFolderDepartmentId").val($(this).attr("FolderDepartmentId"));
        //debugger;
        var tempId = $(this).attr("id");
        var newtempId = tempId.substring(6, tempId.length);
        var newintegerTempId = parseInt(newtempId);
        folderId = newintegerTempId; //this.id;

        //}



    });

    $(".editFolderName").click(function () {
        $("#errorMsgFolderName").hide();
        $("#errorMsgSubFolderName").hide();
        $("#errorDuplicateFolderName").hide();
        $("#errorDuplicateSubFolderName").hide();
        $("#folder_name").val("");
        var result = CheckIfDepartmentSelected();

        //if (result == false) {
        //    swal("Please select a department to edit folder");
        //}
        //else {
        folderId = 0;
        globalParentFolderId = 0;
        // event.preventDefault();
        globalParentFolderId = $(this).attr("parentFolderId");
        $("#getHdnFolderDepartmentId").val($(this).attr("FolderDepartmentId"));
        editfolderId = $(this).attr("id");
        var folderName = $(this).attr("folderName");
        $(".txtfolderNamePopUp").val(folderName);
        $("#add_folder_popup, .popup_window_overlay").fadeToggle(200);
        //}

    });

    $("#saveFolderBtn").click(function () {

        var departmentId = $("#hiddenDepartmentId").val();
        var GetListByDepartmentId = departmentId;
        $("#errorDuplicateFolderName").hide();
        $("#errorDuplicateSubFolderName").hide();
        //debugger;

        //if department is not selected
        if (departmentId == 0 || departmentId == "") {

            GetListByDepartmentId = 0;
            departmentId = $("#getHdnFolderDepartmentId").val();
        }



        var folderName = $("#folder_name").val();
        if (folderName.trim() == "") {
            if (folderId == 0 || folderId == "") {
                $("#errorMsgFolderName").show();
                return false;
            }
            else {
                $("#errorMsgSubFolderName").show();
                return false;
            }

        }
        else {
            $("#errorMsgFolderName").hide();
            $("#errorMsgSubFolderName").hide();

        }

        if (globalParentFolderId != 0) {
            folderId = globalParentFolderId;
        }

        $.get("/Documents/IsParentFolderExist", { id: departmentId, name: folderName, paramParentFolderId: folderId, paramEditfolderId: editfolderId }).then(function (data) {
            if (data == false) {
                if (folderId > 0) {
                    $("#errorDuplicateSubFolderName").show();
                }
                else {
                    $("#errorDuplicateFolderName").show();
                }

            }

            else {
                $("#errorDuplicateFolderName").hide();
                $("#errorDuplicateSubFolderName").hide();
                var folder =
                    {
                        DepartmentId: departmentId,
                        ParentFolderId: folderId,
                        folderName: folderName,
                        FolderId: editfolderId,
                    }
                $(".popup_window, .popup_window_overlay").fadeOut(200);

                //  $.post("/Documents/AddFolders", { folder: folder }, function (data) { });
                $.ajax({
                    type: "POST",
                    url: "/Documents/AddFolders",
                    data: folder,
                    dataType: 'json',
                    traditional: true,
                    success: function (data) {

                        $(".popup_window, .popup_window_overlay").fadeOut(200);
                        if (editfolderId > 0) {
                            if (folderId == 0 || folderId == "") {
                                onSuccess("The folder name has been changed.", GetListByDepartmentId, 0, folderId);
                            }
                            else {
                                onSuccess("The sub folder name been changed.", GetListByDepartmentId, 0, folderId);
                            }

                        }
                        else {
                            if (folderId == 0 || folderId == "") {
                                onSuccess("A new folder was successfully created.", GetListByDepartmentId, 0, folderId);
                            }
                            else {
                                onSuccess("A new sub folder was successfully created.", GetListByDepartmentId, 0, folderId);
                            }
                        }

                    },
                    error: function (e) {
                        $("#btnSave").prop("disabled", false);
                        onFailed(e);
                    }
                });
            }
        });
    });



    $("#AddDocumentBtn").click(function () {

        $("#errorDuplicateDocTitleName").hide();
        // Validation is ok or not
        if (validateDocumentAdd() == false) {
            return false;
        }
        var documentId = $("#HiddenDocumentId").val();
        // debugger;
        var departmentId = $("#hiddenDepartmentId").val();
        var GetListByDepartmentIdDoc = departmentId;
        if (departmentId == 0 || departmentId == "") {
            GetListByDepartmentIdDoc = 0;
            departmentId = $("#getHdnFolderDepartmentId").val();
        }
        UploadFile(departmentId, documentId, GetListByDepartmentIdDoc);
    });
});


// Validate add  document form
var validateDocumentAdd = function () {

    var isValid = true;

    if ($("#doctitle").val().trim().length == 0) {
        $("#lblDocTitleError").text("Please enter the document title.");
        isValid = false;
    }
    else {
        $("#lblDocTitleError").text("");
    }

    if ($("#doctitle").val().trim().length > 0) {
        if ($("#doctitle").val().trim().length > 300) {
            $("#lblDocTitleError").text("The document title should not be greater than 300 characters.");
            isValid = false;
        }
        else {
            $("#lblDocTitleError").text("");
        }

    }

    //if ($("#docdescription").val().trim().length == 0) {
    //    $("#lblDocDescriptionError").text("Please enter the document description. ");
    //    isValid = false;
    //}
    //else {
    //    $("#lblDocDescriptionError").text("");
    //}

    // Check publish date is entered or not
    if ($("#PublishDateDisplay").val() == '') {
        isValid = false;
        $("#lblDocPublishDateError").text("Please select the publish date.");
    }
    else {
        $("#lblDocPublishDateError").text("");
    }

    // check author name is entered or not
    if ($("#DefaultDocumentAddAuthorName").val() == '') {

        $("#lblDocAuthorNameError").text("Please enter author name");
        isValid = false;
    }
    else {
        $("#lblDocAuthorNameError").text("");
    }

    // Validate file upload and document link destination
    if ($("#Type").val() == "Document") {
        if ($("#docFileName").text().trim().length == 0) {
            $("#lblDocError").text("Please attach a document");
            isValid = false;
        }
        else {
            $("#lblDocError").text("");
        }
    }

    else {
        if ($("#LinkDestination").val().trim().length == 0) {
            $("#errorLinkDestination").text("Please enter the link destination.");
            isValid = false;
        }
        else {
            $("#errorLinkDestination").text("");
        }
    }
    



    return isValid;


}

// On begin ajax request this function will be call
var onBegin = function (xhr) {
    $(".loaderModal").show();
};

// On success of ajax callback this function will be call
var onSuccess = function (context, departmentId, OpenSubPopUpId, OpenParentPupuId) {
    $(".loaderModal").hide();
    swal(
        context,
        '',
        'success'
    ).then(function () {
        //debugger;
        GetDocumentsByDepartmentWise_new(departmentId, OpenSubPopUpId, OpenParentPupuId);

    });
};




// On failed/error of ajax 
var onFailed = function (context) {
    return false;
};

function GetDocumentsByDepartmentWise_new(departmentId, OpenSubPopUpId, OpenParentPupuId) {
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

        // debugger;
        var FolderIdStartChar = "Folder";
        if (OpenSubPopUpId > 0) {
            var parentopenonlypopup = $("#" + FolderIdStartChar + OpenSubPopUpId).attr("ParentFolderpopupid");
            $("#" + FolderIdStartChar + parentopenonlypopup).prev().trigger('click');
            $("#" + FolderIdStartChar + OpenSubPopUpId).prev().trigger('click');
        }
        else {
            $("#" + FolderIdStartChar + OpenParentPupuId).prev().trigger('click');
        }

        //$(".openSubFolder").trigger('click');
    });
}

function UploadFile(departmentId, documentId, GetListByDepartmentIdDoc) {

    //debugger;
    var subFolderPopUpId = $("#subfolderIdForDoc").val();
    subFolderPopUpId = $('#ChooseSubFolderName :selected').val();
    var form = $('#FormUpload')[0];
    var dataString = new FormData(form);
    dataString.append("DepartmentId", departmentId);
    dataString.append("SubfolderId", subFolderPopUpId);
    dataString.append("DocumentId", documentId);
    var documentSuccessMsg = "";
    if (documentId == 0 || documentId == "" || documentId == undefined) {
        documentSuccessMsg = "Your document has been added successfully.";
    }
    else {
        documentSuccessMsg = "Your document has been updated successfully.";
    }
    $("#errorDuplicateDocTitleName").hide();
    $.ajax({
        url: '/Documents/IsDocumentTitleExist',
        type: 'POST',
        data: dataString,
        cache: false,
        contentType: false,
        processData: false,
        //Ajax events
        success: function (data) {

            if (data == false) {

                $("#errorDuplicateDocTitleName").show();

            }

            else {
                $(".loaderModal").show();
                $("#errorDuplicateDocTitleName").hide();
                $.ajax({
                    url: '/Documents/Upload',
                    type: 'POST',
                    data: dataString,
                    cache: false,
                    contentType: false,
                    processData: false,
                    //Ajax events
                    success: function (data) {

                        $(".popup_window, .popup_window_overlay").fadeOut(200);
                        // debugger;
                        onSuccess(documentSuccessMsg, GetListByDepartmentIdDoc, subFolderPopUpId, 0);
                    },
                    error: function (e) {
                        $("#AddDocumentBtn").prop("disabled", false);
                        onFailed(e);
                    }
                    // Form data

                    //Options to tell jQuery not to process data or worry about content-type.

                });
            }
        },
        error: function (e) {

        }

    });
}

function populateDocumentValue(id) {

    //debugger;

    $.ajax({
        url: '/Documents/populateDocumentValue/' + id,
        type: 'POST',
        dataType: 'json',

        //Ajax events
        success: function (data) {
            // debugger;
            $("#add_doc_popup, .popup_window_overlay").fadeToggle(200);
            $("#User_ID").val(data.documentCode);
            $("#HiddenDocumentId").val(data.documentId);
            $("#docFileTypeId").val(data.fileTypeId);
            $("#doctitle").val(data.documentTitle);
            $("#docdescription").val(data.documentDescription);
            var docFileName = "FileName: " + data.fileName;
            $("#docFileName").text(docFileName);

            $("#CreationDate").val(data.creationDate);
            $("#PublishDateDisplay").val(data.publishDateDisplay);
            $("#DefaultDocumentAddAuthorName").val(data.defaultDocumentAddAuthorName);

            // For Feature
            if (data.isFeatureDocument == true) {

                $("#IsFeaturedDocBox").prop('checked', true);
            }
            else {
                $("#IsFeaturedDocBox").prop('checked', false);
            }

            // For Publish
            if (data.isPublishDocument == true) {

                $("#IsPublishedDocBox").prop('checked', true);
            }
            else {
                $("#IsPublishedDocBox").prop('checked', false);
            }

            // For Type dropdown
         
            $('#Type').val(data.type);
            //$('#Type option:selected').removeAttr('selected');	
            //$('#Type option[value=' + data.type + ']').attr("selected", true);

            // Enable or disable the Link Destination and File Upload on the basis of Type
            EnableDisableLinkDestinationAndFileUpload(data.type);

            // Display Link Destination in edit text box
            if (data.type == "Link") {
                $("#LinkDestination").val(data.linkDestination);
            }

            // Adjust Save button margin from top
            if (data.type == 'Document') {
                $(".save_btn").css('margin-top', '-55px');
            }
            else {
                $(".save_btn").css('margin-top', '0');
            }

        },
        error: function (e) {

        }


    });
}

function AutoPopulateAllFolders(id) {



    $.ajax({
        url: '/Documents/AutoPopulateAllFolders/' + id,
        type: 'Get',
        dataType: 'json',

        //Ajax events
        success: function (data) {
            // debugger;
            ChooseFolderArray = data;
            //console.log(ChooseFolderArray);
            $('.subFolderCountDiv').each(function (index, value) {
                var count = $('.documentCountDiv', this).length;
                var text = "(" + count + ")";
                text = "<cite>" + text + "</cite>"
                //console.log($(this).children('div.openSubFolder').text());
                var subFolderName = $(this).children('div.openSubFolder').text();
                text = subFolderName + " " + text;
                $(this).children('div.openSubFolder').html(text);

            });
        },
        error: function (e) {

        }


    });
}

function CheckIfDepartmentSelected() {
    var Isvalid = true;
    var departmentId = $("#hiddenDepartmentId").val();

    if (departmentId == undefined || departmentId == 0) {
        Isvalid = false;
    }
    else {
        Isvalid = true;
    }
    return Isvalid
};

function EnableDisableLinkDestinationAndFileUpload(docType) {
   
    if (docType == 'Document') {

        // Disable or hide Link Destination
            $("#divLinkDestination").hide();
            $("#LinkDestination").prop("disabled", true);

        // Enable or show document upload
            $("#divFileUpload").show();
            $("#UploadFile").prop("disabled", false);

        // Remove Link Destination text
        $("#LinkDestination").val('');
    }

    if (docType == 'Link') {

        // Enable or show Link Destination
           $("#divLinkDestination").show();
           $("#LinkDestination").prop("disabled", false);

        // Disable or hide document upload
            $("#divFileUpload").hide();
            $("#UploadFile").prop("disabled", true);

        // Remove browsed document
        $("#UploadFile").val('');
        $("#docFileName").html('');
    }
}
//$(window).load(function () {
//    debugger;
//    $('.subFolderCountDiv').each(function (index,value) {
//        var count = $('.documentCountDiv', this).length;
//        var text = count + "Docs";
//        $(this).children('.DocumentCountem').eq(index).text(text);
//    });

//});