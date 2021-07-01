var counter = 1;
$(document).ready(function () {

    var doctorAdviceListTable = $('#tblDoctorAdviceList').DataTable({
        "pageLength": 10
    });

    $(".dataTables_empty").text("There are no doctor advices yet.");

    $('#txtSearchDoctorAdviceUp').keyup(function () {
        doctorAdviceListTable.search($(this).val()).draw();
    });

    var rowsCount = $('#tblDoctorAdviceList tbody tr').length;
    //alert(rowsCount);
    if (rowsCount < 10) {
        $("#tblDoctorAdviceList_paginate").hide();
    }
    else {
        $("#tblDoctorAdviceList_paginate").show();
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

        $('#tblDoctorAdviceList').find('input[type="checkbox"]:checked').each(function () {

            var row = $(this).parents('tr')[0];;
            var newsId = parseInt($(row).find('span').text());
            targetIds.ItemIds.push(newsId);
        });

        if (targetIds.ItemIds.length > 0) {
            swal({
                title: 'Are you sure you want to delete these doctor advices?',
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
                    doctorAdviceService.deleteDoctorService("/Advice/DeleteDoctorAdvice", targetIds, deletedDoctorAdviceCallbackAllDoctorAdvices);
                }
            });
        }
        else {
            swal('Please check at least one doctor advice to delete using the checkboxes.');
        }
    });

    $('#tblDoctorAdviceList').on('click', '.doctorAdviceItem', function () {
        var row = $(this).parents('tr')[0];;
        var newsId = parseInt($(row).find('span').text());
        var targetIds = {};
        targetIds.ItemIds = [];
        targetIds.ItemIds.push(newsId);

        swal({
            title: 'Are you sure you want to delete this doctor advice?',
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
                doctorAdviceService.deleteDoctorService("/Advice/DeleteDoctorAdvice", targetIds, deletedDoctorAdviceDoseCallback);
            }
        });
    });

    // All data communication will be happened using this medicineDoseDataService object from server
    var doctorAdviceService = new function () {
        deleteDoctorService = function (url, users, callback) {
            $.post(url, { targetIds: users }, function (data) { callback(data) });
        }
        return {
            deleteDoctorService: deleteDoctorService
        };
    }();

    // Callback function of Deleted Users
    var deletedDoctorAdviceDoseCallback = function (data) {
        console.table(data);
        swal(
            'The doctor advice has been successfully deleted.',
            '',
            'success'
        ).then(function () {
            window.location.href = "/Advice/DoctorAdvice";
        });
    }

    // Callback function of Deleted Users
    var deletedDoctorAdviceCallbackAllDoctorAdvices = function (data) {
        console.table(data);
        swal(
            'The doctor advices have been successfully deleted.',
            '',
            'success'
        ).then(function () {
            window.location.href = "/Advice/DoctorAdvice";
        });
    }

    $('#Save_DoctorAdvice_Btn').hide();

    $('#AddRow').unbind().click(function () {

        if ($('#hdnDoctorId').val() == undefined || $('#hdnDoctorId').val == null || $('#hdnDoctorId').val() == "" || $('#hdnDoctorId').val() == "0") {
            swal("Please select a doctor to add the doctor advice.", '', 'error').then(function () {
                $(".drop_menu").find(".drop_menu_sub").show();
            });
        }
        else {

            if ($('#tblDoctorAdviceList  tr.NewAddedRow').length == 0)
                counter = 1;

            $('#tblDoctorAdviceList tbody:last-child').append(
                '<tr id="tablerow' + counter + '" class="NewAddedRow">' +
                '<td style="width: 70px;">' +
                '</td>' +
                '<td>' +
                '<select class="select-css" style="width:!00%" id="ddlDoctor' + counter + '"  name="ddlDoctor">' +
                '<option value="">Select Doctor</option>' +
                '</select>' +
                '<span class="error_label" style="display:none;" id="ddlDoctorError' + counter + '"></span>' +
                '</td>' +
                '<td>' +
                '<input type="text" class="text-box single-line" id="Description' + counter + '"  required="required" />' +
                '<span class="error_label" style="display:none;" id="DescriptionError' + counter + '"></span>' +
                '</td>' +
                '<td>' +
                '<input type="name" class="text-box single-line" id="ProblemTags_Main' + counter + '"  required="required" />' +
                '<span class="error_label" style="display:none;" id="ProblemTagsError' + counter + '"></span>' +
                '<input type="hidden" id="ProblemTags_Sub' + counter + '"   ' +
                '</td>' +
                '<td colspan=2>' +
                '<button type="button" class="btn-new btn-primary" onclick="removeTr(' + counter + ');">Remove Row</button>' +
                '</td>' +
                '</tr>')

            if ($('.drop_menu ul li ul li').length > 0) {

                $('.drop_menu ul li ul li').each(function () {
                    $('#ddlDoctor' + counter).append($('<option>', {
                        value: $(this).find('a').attr('id'),
                        text: $(this).find('a').html()
                    }));
                });
            }

            $('#Save_DoctorAdvice_Btn').show();

            $("#Description" + counter).autocomplete({
                source: function (request, response) {

                    $.ajax({
                        type: "GET",
                        url: '/Advice/AdviceAutoComplete/',
                        data: { prefix: request.term },
                        success: function (data) {
                            response($.map(data, function (item) {
                                return item;
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    $("#Description" + counter).val(i.item.val);
                },
                minLength: 3
            });

            $("#Description" + counter).change(function () {

                var record = counter - 1;

                $.get('/Advice/ValidateAdviceExistOrNot/', { description: $("#Description" + record).val(), doctorId: $('#ddlDoctor' + record).val(), id: 0 },
                    function (data) {
                        $('#DescriptionError' + record).hide();
                        $('#DescriptionError' + record).html("");
                        if (data != true) {
                            $('#DescriptionError' + record).show();
                            $('#DescriptionError' + record).html(data);
                        }
                    });
            });


            BindProblemTaggingSource(counter);
            counter++;
        }
    });

    $('#Save_DoctorAdvice_Btn').unbind().click(function () {

        if (!validateAddDoctorAdviceList()) {
            return false;
        }
        else {

            var doctorAdviceList = [];

            $('#tblDoctorAdviceList tr.NewAddedRow').each(function (index) {

                AddProblemTags(index);

                var doctorAdvice = {};
                doctorAdvice.DoctorId = $('#ddlDoctor' + (index + 1)).val();
                doctorAdvice.Description = $('#Description' + (index + 1)).val();
                doctorAdvice.ProblemTags = $('#ProblemTags_Sub' + (index + 1)).val();
                doctorAdvice.Type = "Doctor";
                doctorAdviceList.push(doctorAdvice);
            });

            $.ajax({
                type: "POST",
                url: '/Advice/AddDoctorAdviceList/',
                data: { doctorAdviceList: doctorAdviceList },
                success: function () {
                    swal(
                        'The doctor advice has been successfully added.',
                        '',
                        'success'
                    ).then(function () {
                        window.location.href = "/Advice/DoctorAdvice?doctorId=" + $('#hdnDoctorId').val();
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

function AddProblemTags(index) {
    debugger;
    var tags = "";
    $('#ProblemTags_Main1 .ms-sel-item').each(function () {
        if (tags == "")
            tags = $(this).find('span').parent().text();
        else
            tags = tags + "," + $(this).find('span').parent().text();
    });
    $('#ProblemTags_Sub' + (index + 1)).val(tags);
}

function BindProblemTaggingSource(counter) {

    $.ajax({
        type: "GET",
        url: '/Advice/DoctorProblemTags/',
        success: function (data) {
            var arr;
            if (data == null) {
                arr = [];
            }
            else {
                arr = data.split(',');
            }
            arr = arr.filter(function (item, index, inputArray) {
                return inputArray.indexOf(item) == index;
            });

            $('#ProblemTags_Main' + counter).magicSuggest({
                data: arr
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

function validateAddDoctorAdviceList() {

    var valid = true;

    $('#tblDoctorAdviceList tr.NewAddedRow').each(function (index, tr) {

        var doctorName = $('#ddlDoctor' + (index + 1));
        var doctorNameError = $('#ddlDoctorError' + (index + 1));
        var description = $('#Description' + (index + 1));
        var descriptionError = $('#DescriptionError' + (index + 1));
        var problemTags = $('#ProblemTags_Main' + (index + 1) + ' .ms-sel-item ');
        var problemTagsError = $('#ProblemTagsError' + (index + 1));



        doctorNameError.hide();
        doctorNameError.html("");
        descriptionError.hide();
        descriptionError.html("");
        problemTagsError.hide();
        problemTagsError.html("");

        if (doctorName.val() == undefined || doctorName.val() == "") {
            doctorNameError.show();
            doctorNameError.html("Please select doctor ");
            valid = false;
        }

        if (description.val() == undefined || description.val() == "") {
            descriptionError.show();
            descriptionError.html("Please enter the description.")
            valid = false;
        }

        if (problemTags.length == 0) {
            problemTagsError.show();
            problemTagsError.html("Please enter the problem tags.")
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

    $('#tblDoctorAdviceList > tbody > tr').eq(rowIndex - 1).after(
        '<tr id="tablerow' + rowIndex + '" class="NewAddedRow">' +
        '<td style="width: 70px;">' +
        '</td>' +
        '<td>' +
        '<select class="select-css" style="width:!00%" id="ddlDoctor_Edit' + id + '"  name="ddlDoctor">' +
        '<option value="">Select Doctor</option>' +
        '</select>' +
        '<span class="error_label" style="display:none;" id="ddlDoctorError_Edit' + id + '"></span>' +
        '</td>' +
        '<td>' +
        '<input type="text" class="text-box single-line" id="Description_Edit' + id + '"  required="required" />' +
        '<span class="error_label" style="display:none;" id="DescriptionError_Edit' + id + '"></span>' +
        '</td>' +
        '<td>' +
        '<input type="name" class="text-box single-line" id="ProblemTags_Main_Edit' + id + '"  required="required" />' +
        '<span class="error_label" style="display:none;" id="ProblemTagsError_Edit' + id + '"></span>' +
        '<input type="hidden" id="ProblemTags_Sub_Edit' + id + '"   ' +
        '</td>' +
        '<td>' +
        '<button type="button" class="btn-new btn-primary" onclick="UpdateRowData(' + id + ',' + rowIndex + ')">Update</button>' +
        '</td>' +
        '<td>' +
        '<button type="button" class="btn-new btn-primary" onclick="removeTr(' + rowIndex + ');">Cancel</button>' +
        '</td>' +
        '</tr>');

    if ($('.drop_menu ul li ul li').length > 0) {

        $('.drop_menu ul li ul li').each(function () {
            $('#ddlDoctor_Edit' + id).append($('<option>', {
                value: $(this).find('a').attr('id'),
                text: $(this).find('a').html()
            }));
        });
    }

    $("#Description_Edit" + id).autocomplete({
        source: function (request, response) {

            $.ajax({
                type: "GET",
                url: '/Advice/AdviceAutoComplete/',
                data: { prefix: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return item;
                    }))
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        },
        select: function (e, i) {
            $("#Description").val(i.item.val);
        },
        minLength: 3
    });

    $("#Description_Edit" + id).change(function () {

        var record = counter - 1;
        debugger;
        $.get('/Advice/ValidateAdviceExistOrNot/', { description: $("#Description_Edit" + id).val(), doctorId: $('#ddlDoctor_Edit' + id).val(), id: id },
            function (data) {
                $('#DescriptionError' + record).hide();
                $('#DescriptionError' + record).html("");
                if (data != true) {
                    $('#DescriptionError' + record).show();
                    $('#DescriptionError' + record).html(data);
                }
            });
    });

    var doctorName = $('#tblDoctorAdviceList tr').eq(rowIndex).find('td').eq(1).html().trim();
    var description = $('#tblDoctorAdviceList tr').eq(rowIndex).find('td').eq(2).html().trim();
    var problemtags = $('#tblDoctorAdviceList tr').eq(rowIndex).find('td').eq(3).html().trim();

    BindEditProblemTaggingSource(id, problemtags);

    //$('#ddlDoctor_Edit' + id).val(doctorId);
    $("#ddlDoctor_Edit" + id + " option").filter(function () {
        return $(this).text() == doctorName;
    }).prop("selected", true);

    $('#Description_Edit' + id).val(description);
    //$('#ProblemTags_Main_Edit' + id).val(problemtags);

    counter++;
}

function BindEditProblemTaggingSource(id, problemtags) {

    $.ajax({
        type: "GET",
        url: '/Advice/DoctorProblemTags/',
        success: function (data) {
            var arr;
            if (data == null) {
                arr = [];
            }
            else {
                arr = data.split(',');
            }
            arr = arr.filter(function (item, index, inputArray) {
                return inputArray.indexOf(item) == index;
            });

            $('#ProblemTags_Main_Edit' + id).magicSuggest({
                data: arr
            });

            var arr = problemtags.split(",");
            $("#ProblemTags_Main_Edit" + id + " div.ms-sel-ctn").append('<div class="none" style="display:none;"></div>')
            $("#ProblemTags_Main_Edit" + id + "  div.ms-sel-ctn input[type='text']").removeAttr('placeholder').width('0')
            for (var i = 0; i < arr.length; i++) {
                var problemTag = arr[i];
                $("#ProblemTags_Main_Edit" + id + " div.ms-sel-ctn").append('<div class="ms-sel-item "><span class="ms-close-btn"></span>' + problemTag + '</div>');
                $("#ProblemTags_Main_Edit" + id + " div.ms-sel-ctn div[style='display: none;']").append('<input type="hidden" value="' + problemTag + '">');
            }
        },
        error: function (response) {
            alert(response.responseText);
        },
        failure: function (response) {
            alert(response.responseText);
        }
    });
}

function UpdateRowData(id) {
    if (!validateEditDoctorAdviceList(id)) {
        return false;
    }
    else {

        EditProblemTags(id);

        var doctorAdvice = {};
        doctorAdvice.Id = id;
        doctorAdvice.DoctorId = $('#ddlDoctor_Edit' + id).val();
        doctorAdvice.Description = $('#Description_Edit' + id).val();
        doctorAdvice.ProblemTags = $('#ProblemTags_Sub_Edit' + id).val();
        doctorAdvice.Type = "Doctor";


        $.ajax({
            type: "POST",
            url: '/Advice/UpdateDoctorAdvice/',
            data: { doctorAdvice: doctorAdvice },
            success: function (data) {
                swal(
                    'The doctor advice has been successfully updated.',
                    '',
                    'success'
                ).then(function () {
                    window.location.href = "/Advice/DoctorAdvice?doctorId=" + $('#hdnDoctorId').val();
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

function EditProblemTags(id) {
    debugger;
    var tags = "";
    $('#ProblemTags_Main_Edit' + id + ' .ms-sel-item').each(function () {
        if (tags == "")
            tags = $(this).find('span').parent().text();
        else
            tags = tags + "," + $(this).find('span').parent().text();
    });
    $('#ProblemTags_Sub_Edit' + (id)).val(tags);
}

function validateEditDoctorAdviceList(id) {

    var valid = true;

    var doctorName = $('#ddlDoctor_Edit' + id);
    var doctorNameError = $('#ddlDoctorError_Edit' + id);
    var description = $('#Description_Edit' + id);
    var descriptionError = $('#DescriptionError_Edit' + id);
    var problemTags = $('#ProblemTags_Main_Edit' + id + ' .ms-sel-item ');
    var problemTagsError = $('#ProblemTagsError_Edit' + id);

    doctorNameError.hide();
    doctorNameError.html("");
    descriptionError.hide();
    descriptionError.html("");
    problemTagsError.hide();
    problemTagsError.html("");

    if (doctorName.val() == undefined || doctorName.val() == "") {
        doctorNameError.show();
        doctorNameError.html("Please select doctor ");
        valid = false;
    }

    if (description.val() == undefined || description.val() == "") {
        descriptionError.show();
        descriptionError.html("Please enter the description.")
        valid = false;
    }

    if (problemTags.length == 0) {
        problemTagsError.show();
        problemTagsError.html("Please enter the problem tags.")
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
        $('#Save_DoctorAdvice_Btn').hide();
    }
    return false;
}
