$(document).ready(function () {

    debugger;
    $('.document_listing').sortable();
    //save the changed order of documents and folder
    $('.drag_list_parentFolder').on('sortupdate', function () {

        var FolderOrderList = [];
        //var itemOrder = $('.document_listing div.parent_folder').sortable("toArray");
        var itemOrder = $(this).sortable("toArray");

        for (var i = 0; i < itemOrder.length; i++) {
            itemOrder[i] = itemOrder[i].substring(17, itemOrder[i].length);
            var item = {
                "position": (i + 1),
                "id": itemOrder[i]
            };
            FolderOrderList.push(item);
        }

        SaveParentFolderOrder(FolderOrderList);
    });

    $('.drag_list_SubFolder').on('sortupdate', function (e) {

        var FolderOrderList = [];
        //var itemOrder = $('.document_listing div.parent_folder div.sub_folder').sortable("toArray");
        var itemOrder = $(this).sortable("toArray");

        for (var i = 0; i < itemOrder.length; i++) {
            itemOrder[i] = itemOrder[i].substring(14, itemOrder[i].length);
            var item = {
                "position": (i + 1),
                "id": itemOrder[i]
            };
            FolderOrderList.push(item);
        }

        SaveParentFolderOrder(FolderOrderList);
        e.stopImmediatePropagation();
    });

    $('.drag_list_Documents').on('sortupdate', function (e) {

        var DocumentOrderList = [];
        //var itemOrder = $('.document_listing div.parent_folder div.sub_folder').sortable("toArray");
        var itemOrder = $(this).sortable("toArray");

        for (var i = 0; i < itemOrder.length; i++) {
            itemOrder[i] = itemOrder[i].substring(14, itemOrder[i].length);
            var item = {
                "position": (i + 1),
                "id": itemOrder[i]
            };
            DocumentOrderList.push(item);
        }

        SaveDocumentOrder(DocumentOrderList);
        e.stopImmediatePropagation();
    });


});

function SaveParentFolderOrder(FolderOrderList)
{
    //FolderOrderList = JSON.stringify({ 'FolderOrderList': FolderOrderList });
    $.post("/Documents/ChangeFolderOrder", { FolderOrderList: FolderOrderList }, function (data) {

        //alert(data);
    });
}

function SaveDocumentOrder(DocumentOrderList) {
    //FolderOrderList = JSON.stringify({ 'FolderOrderList': FolderOrderList });
    $.post("/Documents/ChangeDocumentOrder", { DocumentOrderList: DocumentOrderList }, function (data) {

        //alert(data);
    });
}