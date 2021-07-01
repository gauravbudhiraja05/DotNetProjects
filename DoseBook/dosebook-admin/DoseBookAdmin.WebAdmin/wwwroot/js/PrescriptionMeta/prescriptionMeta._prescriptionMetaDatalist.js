var counter = 1;
$(document).ready(function () {
    var prescriptionMetaDataListTable = $('#tblPrescriptionMetaDataList').DataTable({
        "pageLength": 10
    });

    $(".dataTables_empty").text("There are no prescription meta data yet.");

    $('#txtSearcPrescriptionMetaDataUp').keyup(function () {
        prescriptionMetaDataListTable.search($(this).val()).draw();
    });

    var rowsCount = $('#tblPrescriptionMetaDataList tbody tr').length;
    if (rowsCount < 10) {
        $("#tblPrescriptionMetaDataList_paginate").hide();
    }
    else {
        $("#tblPrescriptionMetaDataList_paginate").show();
    }

    if (rowsCount <= 1) {
        $("#btnDeleteSelected").hide();
    }
    else {
        $("#btnDeleteSelected").show();
    }

    $('.dataTables_length').hide();
    $('.dataTables_filter').hide();
    $('.dataTables_info').hide();

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
                title: 'Are you sure you want to delete these prescription meta data?',
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
                    prescriptionMetaDataService.deleteprescriptionMetaData("/PrescriptionMeta/DeletePrescriptionMetaDataByIds", targetIds, deletedPrescriptionMetaDataCallbackAllPrescriptionMetaDatas);
                }

            });
        }

        else {
            swal('Please select at least one prescription meta data to delete using the checkboxes.');
        }

    });

    $('#tblPrescriptionMetaDataList').on('click', '.prescriptionMetaDataItem', function () {
        var row = $(this).parents('tr')[0];;
        var newsId = parseInt($(row).find('span').text());
        var targetIds = {};
        targetIds.ItemIds = [];
        targetIds.ItemIds.push(newsId);

        swal({
            title: 'Are you sure you want to delete this prescription meta data?',
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
                prescriptionMetaDataService.prescriptionMetaData("/PrescriptionMeta/DeletePrescriptionMetaDataByIds", targetIds, deletedPrescriptionMetaDataCallback);
            }
        });
    });


    // All data communication will be happened using this newsDataService object from server
    var prescriptionMetaDataService = new function () {
        prescriptionMetaData = function (url, users, callback) {
            $.post(url, { targetIds: users }, function (data) { callback(data) });
        }
        return {
            prescriptionMetaData: prescriptionMetaData
        };
    }();

    // Callback function of Deleted Users
    var deletedPrescriptionMetaDataCallback = function (data) {
        console.table(data);
        swal(
            'The prescription meta data has been successfully deleted.',
            '',
            'success'
        ).then(function () {
            window.location.href = "/PrescriptionMeta/Index?prescriptionMetaDataType=" + $('#hdnPrescriptionMetaTypeName').val();
        });
    }

    // Callback function of Deleted Users
    var deletedPrescriptionMetaDataCallbackAllPrescriptionMetaDatas = function (data) {
        console.table(data);
        swal(
            'The prescription meta datas have been successfully deleted.',
            '',
            'success'
        ).then(function () {
            window.location.href = "/PrescriptionMeta/Index?prescriptionMetaDataType=" + $('#hdnPrescriptionMetaTypeName').val();
        });
    }

    $('#Save_PrescriptionMetaData_Btn').hide();

    $('#AddRow').unbind().click(function () {
        debugger;
        if ($('#hdnPrescriptionMetaTypeId').val() == undefined ||
            $('#hdnPrescriptionMetaTypeId').val() == null ||
            $('#hdnPrescriptionMetaTypeId').val() == "" ||
            $('#hdnPrescriptionMetaTypeId').val() == "Select PrescriptionType") {
            swal("Please select a prescription meta type to add the prescription meta data.", '', 'error').then(function () {
                $(".drop_menu").find(".drop_menu_sub").show();
            });
        }
        else {

            if ($('#tblPrescriptionMetaDataList  tr.NewAddedRow').length == 0)
                counter = 1;

            $('#tblPrescriptionMetaDataList tbody:last-child').append(
                '<tr id="tablerow' + counter + '" class="NewAddedRow">' +
                '<td style="width: 70px;">' +
                '</td>' +
                '<td>' +
                '<select class="select-css" id="ddlPrescriptionType' + counter + '"  name="ddlPrescriptionType">' +
                '<option value="">Select Prescription Type</option>' +
                '</select>' +
                '<span class="error_label" style="display:none;" id="ddlPrescriptionTypeError' + counter + '"></span>' +
                '</td>' +
                '<td>' +
                '<input type="text" class="text-box single-line" id="Name' + counter + '"  required="required" />' +
                '<span class="error_label" style="display:none;" id="NameError' + counter + '"></span>' +
                '</td>' +
                '<td>' +
                '<input type="name" class="text-box single-line" id="OrderNumber' + counter + '"  required="required" />' +
                '<span class="error_label" style="display:none;" id="OrderNumberError' + counter + '"></span>' +
                '</td>' +
                '<td colspan=2>' +
                '<button type="button" class="btn-new btn-primary" onclick="removeTr(' + counter + ');">Remove Row</button>' +
                '</td>' +
                '</tr>')

            if ($('.drop_menu ul li ul li').length > 0) {
                $('.drop_menu ul li ul li').each(function () {
                    if ($(this).find("a").html() != "All") {
                        $('#ddlPrescriptionType' + counter).append($('<option>', {
                            value: $(this).find('a').attr('id'),
                            text: $(this).find('a').html()
                        }));
                    }
                });
            }

            $("#ddlPrescriptionType" + counter).val($('div.drop_menu ul li a').html());

            $('#Save_PrescriptionMetaData_Btn').show();
            counter++;
        }
    });

    $('#Save_PrescriptionMetaData_Btn').unbind().click(function () {

        if (!validateDoseMetaList()) {
            return false;
        }
        else {
            var prescriptionMetaDataList = [];

            $('#tblPrescriptionMetaDataList tr.NewAddedRow').each(function (index) {
                var prescriptionMetaData = {};
                prescriptionMetaData.Type = $('#ddlPrescriptionType' + (index + 1)).val();
                prescriptionMetaData.Name = $('#Name' + (index + 1)).val();
                prescriptionMetaData.OrderNumber = $('#OrderNumber' + (index + 1)).val();

                prescriptionMetaDataList.push(prescriptionMetaData);
            });

            $.ajax({
                type: "POST",
                url: '/PrescriptionMeta/AddPrescriptionMetaDataList/',
                data: { prescriptionMetaDataList: prescriptionMetaDataList },
                success: function () {
                    swal(
                        'The prescription data has been successfully added.',
                        '',
                        'success'
                    ).then(function () {
                        window.location.href = "/PrescriptionMeta/Index?prescriptionMetaDataType=" + $('#hdnPrescriptionMetaTypeName').val();
                    });
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        }
    });

});


function validateDoseMetaList() {

    var valid = true;

    $('#tblPrescriptionMetaDataList tr.NewAddedRow').each(function (index, tr) {
        debugger;

        var type = $('#ddlPrescriptionType' + (index + 1));
        var typeError = $('#ddlPrescriptionTypeError' + (index + 1));
        var name = $('#Name' + (index + 1));
        var nameError = $('#NameError' + (index + 1));
        var order = $('#OrderNumber' + (index + 1));
        var orderError = $('#OrderNumberError' + (index + 1));

        typeError.hide();
        typeError.html("");
        nameError.hide();
        nameError.html("");
        orderError.hide();
        orderError.html("");

        if (type.val() == undefined || type.val() == "") {
            typeError.show();
            typeError.html("Please Select Prescription Type");
            valid = false;
        }

        if (name.val() == undefined || name.val() == "") {
            nameError.show();
            nameError.html("Please enter the value.")
            valid = false;
        }

        if (name.val() != "" && type.val() != "" && type.val() == "Dose" && !$.isNumeric(name.val())) {
            nameError.show();
            nameError.html("Only numeric Values Allowed.")
            valid = false;
        }

        if (order.val() == undefined || order.val() == "") {
            orderError.show();
            orderError.html("Please enter the order number.")
            valid = false;
        }

        if (order.val() != "" && !$.isNumeric(order.val())) {
            orderError.show();
            orderError.html("Only numeric Values Allowed.")
            valid = false;
        }
    });

    return valid;
}

function EditRedirect(id, rowIndex) {

    var cuurentPageNumber = parseInt($('.paginate_button.current').html());
    if (cuurentPageNumber > 1) {
        rowIndex = rowIndex - 10 * (cuurentPageNumber - 1);
    }

    $('#tblPrescriptionMetaDataList > tbody > tr').eq(rowIndex - 1).after(
        '<tr id="tablerow' + (rowIndex) + '" class="NewAddedRow">' +
        '<td style="width: 70px;">' +
        '</td>' +
        '<td>' +
        '<select class="select-css" id="ddlPrescriptionType_Edit' + id + '"  name="ddlPrescriptionType">' +
        '<option value="">Select Prescription Type</option>' +
        //'<option value="FREQUENCY">FREQUENCY</option>' +
        //'<option value="DOSE">DOSE</option>' +
        //'<option value="DOSEUNIT">DOSEUNIT</option>' +
        //'<option value="DURATION">DURATION</option>' +
        //'<option value="DIRECTION">DIRECTION</option>' +
        '</select>' +
        '<span class="error_label" style="display:none;" id="ddlPrescriptionTypeError_Edit' + id + '"></span>' +
        '</td>' +
        '<td>' +
        '<input type="text" class="text-box single-line" id="Name_Edit' + id + '"  required="required" />' +
        '<span class="error_label" style="display:none;" id="NameError_Edit' + id + '"></span>' +
        '</td>' +
        '<td>' +
        '<input type="name" class="text-box single-line" id="OrderNumber_Edit' + id + '"  required="required" />' +
        '<span class="error_label" style="display:none;" id="OrderNumberError_Edit' + id + '"></span>' +
        '</td>' +
        '<td>' +
        '<button type="button" class="btn-new btn-primary" onclick="UpdateRowData(' + id + ',' + rowIndex + ');">Update</button>' +
        '</td>' +
        '<td>' +
        '<button type="button" class="btn-new btn-primary" onclick="removeTr(' + rowIndex + ');">Cancel</button>' +
        '</td>' +
        '</tr>');


    if ($('.drop_menu ul li ul li').length > 0) {

        $('.drop_menu ul li ul li').each(function () {
            if ($(this).find("a").html() != "All") {
                $('#ddlPrescriptionType_Edit' + id).append($('<option>', {
                    value: $(this).find('a').attr('id'),
                    text: $(this).find('a').html()
                }));
            }
        });
    }


    var type = $('#tblPrescriptionMetaDataList tr').eq(rowIndex).find('td').eq(1).html().trim();
    var value = $('#tblPrescriptionMetaDataList tr').eq(rowIndex).find('td').eq(2).html().trim();
    var orderNumber = $('#tblPrescriptionMetaDataList tr').eq(rowIndex).find('td').eq(3).html().trim();


    $('#ddlPrescriptionType_Edit' + id).val(type);
    $('#Name_Edit' + id).val(value);
    $('#OrderNumber_Edit' + id).val(orderNumber);

    counter++;
}


function UpdateRowData(id) {
    if (!validateDoseMetaListEdit(id)) {
        return false;
    }
    else {

        var prescriptionMetaData = {}

        prescriptionMetaData.id = id;
        prescriptionMetaData.Type = $('#ddlPrescriptionType_Edit' + id).val();
        prescriptionMetaData.Name = $('#Name_Edit' + id).val();
        prescriptionMetaData.OrderNumber = $('#OrderNumber_Edit' + id).val();


        $.ajax({
            type: "POST",
            url: '/PrescriptionMeta/UpdatePrescriptionMetaData/',
            data: { prescriptionMetaData: prescriptionMetaData },
            success: function (data) {
                swal(
                    'The prescription meta data has been successfully updated.',
                    '',
                    'success'
                ).then(function () {
                    window.location.href = "/PrescriptionMeta/Index?prescriptionMetaDataType=" + $('#hdnPrescriptionMetaTypeName').val();
                });
            },
            error: function (response) {
                alert(response.responseText);
            },
            failure: function (response) {
                alert(response.responseText);
            }
        });
    }
}

function validateDoseMetaListEdit(id) {

    var valid = true;

    var type = $('#ddlPrescriptionType_Edit' + id);
    var typeError = $('#ddlPrescriptionTypeError_Edit' + id);
    var name = $('#Name_Edit' + id);
    var nameError = $('#NameError_Edit' + id);
    var order = $('#OrderNumber_Edit' + id);
    var orderError = $('#OrderNumberError_Edit' + id);

    typeError.hide();
    typeError.html("");
    nameError.hide();
    nameError.html("");
    orderError.hide();
    orderError.html("");

    if (type.val() == undefined || type.val() == "") {
        typeError.show();
        typeError.html("Please Select Prescription Type");
        valid = false;
    }

    if (name.val() == undefined || name.val() == "") {
        nameError.show();
        nameError.html("Please enter the value.")
        valid = false;
    }

    if (name.val() != "" && type.val() != "" && type.val() == "Dose" && !$.isNumeric(name.val())) {
        nameError.show();
        nameError.html("Only numeric Values Allowed.")
        valid = false;
    }

    if (order.val() == undefined || order.val() == "") {
        orderError.show();
        orderError.html("Please enter the order number.")
        valid = false;
    }

    if (order.val() != "" && !$.isNumeric(order.val())) {
        orderError.show();
        orderError.html("Only numeric Values Allowed.")
        valid = false;
    }

    return valid;
}

function removeTr(index) {
    if (counter > 0) {
        $('#tablerow' + index).remove();
        counter--;
    }
    if (counter == 1) {
        $('#Save_PrescriptionMetaData_Btn').hide();
    }
    return false;
}
