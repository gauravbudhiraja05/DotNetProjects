
$(document).ready(function () {


    // DataTable
    var FaqListTable = $('#tblAdminFAQs').DataTable({

        "pageLength": 10
    });
    $(".dataTables_empty").text("There are no FAQs yet.");
    $('#txtSearchFaqList').keyup(function () {
        FaqListTable.search($(this).val()).draw();
    });

    var rowsCount = $('#tblAdminFAQs tbody tr').length;
    //alert(rowsCount);
    if (rowsCount < 10) {
        $("#tblAdminFAQs_paginate").hide();
    }
    else
    {
        $("#tblAdminFAQs_paginate").show();
    }
    if (rowsCount <= 1) {
        $("#btnDeleteAllCheckedFaqs").hide();
        //$(".table_checkbox").hide();
    }
    else {
        $("#btnDeleteAllCheckedFaqs").show();
        // $(".table_checkbox").show();
    }

    $('.dataTables_length').hide();
    $('.dataTables_filter').hide();
    $('.dataTables_info').hide();


    $("#checkall_Faq").change(function () {  //"select all" change
        var status = this.checked; // "select all" checked status
        $(this).closest("table").find(".check_item").each(function () { //iterate all listed checkbox items
            if ($('#hdnUserLoggedInType').val() != undefined && $('#hdnUserLoggedInType').val() != null && $('#hdnUserLoggedInType').val() != "") {
                if ($('#hdnUserLoggedInType').val() == "DA")
                    if (this.id == "checkFaqItem_" + $('#hdnDeptAdminLoggedInId').val()) {
                        this.checked = status; //change ".checkbox" checked status
                    }
                    //else {
                    //    this.checked = status; //change ".checkbox" checked status
                    //}
            }
            else {
                this.checked = status; //change ".checkbox" checked status
            }
        });
    });
    // Delete all checked faq on top delete button click
    $("#btnDeleteAllCheckedFaqs").on("click", function () {

        var targetIds = {};
        targetIds.ItemIds = [];

        $('#tblAdminFAQs').find('input[type="checkbox"]:checked').each(function () {

            var row = $(this).parents('tr')[0];;
            var faqId = parseInt($(row).find('span').text());
            targetIds.ItemIds.push(faqId);
        });

        if (targetIds.ItemIds.length > 0) {
            swal({
                title: 'Are you sure you want to delete these FAQs?',
                text: "",
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes',
                cancelButtonText: 'No'
            }).then((result) => {

                if (result.value) {
                    //  deleteUsers method to delete admin users
                    faqDataService.deleteFaqs("/FAQs/DeleteFaqs", targetIds, deletedFaqCallbackAllFAQs);
                }

            });
        }

        else {
            swal('Please select at least one FAQ to delete using the checkboxes.');
        }

    });


    // Delete User Confirnation Modal Popup
    $(".FaqItem").on('click', function () {

        // Find the targeted User Id that will be deleted
        var row = $(this).parents('tr')[0];;
        var faqId = parseInt($(row).find('span').text());
        var targetIds = {};
        targetIds.ItemIds = [];
        targetIds.ItemIds.push(faqId);

        swal({
            title: 'Are you sure you want to delete this FAQ?',
            text: "",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes',
            cancelButtonText: 'No'
        }).then((result) => {

            if (result.value) {
                //  deleteUsers method to delete admin users
                faqDataService.deleteFaqs("/FAQs/DeleteFaqs", targetIds, deletedFaqCallback);
            }
        });


    });



    // All data communication will be happened using this faqDataService object from server
    var faqDataService = new function () {
        deleteFaqs = function (url, faqs, callback) {
            $.post(url, { targetIds: faqs }, function (data) { callback(data) });
        }

        return {
            deleteFaqs: deleteFaqs
        };
    }();

    // Callback function of Deleted Faqs
    var deletedFaqCallback = function (data) {
        console.table(data);
        swal(
            'The FAQ has been successfully deleted.',
            '',
            'success'
        ).then(function () {
            var rowsCount = $('#tblAdminFAQs tbody tr').length;
            if (rowsCount <= 1) {
                $("#btnDeleteAllCheckedFaqs").hide();
                //$(".table_checkbox").hide();
            }
            else {
                $("#btnDeleteAllCheckedFaqs").show();
                // $(".table_checkbox").show();
            }
            window.location.href = "/FAQs/Index";
        });
    }

    // Callback function of Deleted Faqs
    var deletedFaqCallbackAllFAQs = function (data) {
        console.table(data);
        swal(
            'The FAQS have been successfully deleted.',
            '',
            'success'
        ).then(function () {
            var rowsCount = $('#tblAdminFAQs tbody tr').length;
            if (rowsCount <= 1) {
                $("#btnDeleteAllCheckedFaqs").hide();
                //$(".table_checkbox").hide();
            }
            else {
                $("#btnDeleteAllCheckedFaqs").show();
                // $(".table_checkbox").show();
            }
            window.location.href = "/FAQs/Index";
        });
    }
});