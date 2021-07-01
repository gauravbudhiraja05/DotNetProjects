var selectedDocuments = "";
$(document).ready(function () {

 
    var currentTab = location.hash
    if (currentTab) {
        $(".tab_list li").removeClass("current");
        //$(".tab_list li>a[href=" + currentTab + "]").closest("li").addClass("current");
        $('.tab_list li a[href="' + currentTab + '"]').closest("li").addClass("current");
        $(".tab_content").hide();
        $(currentTab).show();
        $(currentTab + "_btn").show()
    }

    // Select creation date as today
    $('#CreationDate').val(getFormattedDate());

    // Select publish date as today
    var datepicker = $('.datepicker-here').datepicker().data('datepicker');
    datepicker.selectDate(new Date());



    //Select Current Menu
    $('.nav-li').removeClass('current');
    $('#menuItemFAQs').addClass('current');


    //$('.form-control').datepicker({
    //    language: 'en',
    //    minDate: new Date()
    //});

    

    $("#showHideMultiSelectDiv").hide();
    //alert(5);

    $("#Multiple_select_docs").treeMultiselect();

    $("#ChooseToAttachDocs").change(function () {

        var selectedValue = $("#ChooseToAttachDocs option:selected").val();
        if (selectedValue === "Yes") {

            
            $("#showHideMultiSelectDiv").show();

        }
        else {
            refreshCheckedTree();
            $("#showHideMultiSelectDiv").hide();
        }
    });

    if ($("#ChooseToAttachDocs option:selected").val() === "Yes") {
        $("#showHideMultiSelectDiv").show();
    }
    if ($("#ChooseToAttachDocs option:selected").val() === "No") {
        $("#showHideMultiSelectDiv").hide();
    }





});


// Save FAQs using ajax

// Post data on save button click

$("#btnSave").click(function (event) {

    if ($("#DocumentIds").val() === "") {
        getCheckedTree();
    }
    //stop submit the form, we will post it manually.
   // event.preventDefault();

    // Validation is ok or not
    if (validateSaveFaq() === false) {
        return false;
    }

    $("#AnswerText").val($($("div.nicEdit-main")[0]).html());
    // Get form
    var form = $('#formFaqs')[0];


    // Create an FormData object 
    var data = new FormData(form);

    // If you want to add an extra field for the FormData
    data.append("TxtArea", $($("div.nicEdit-main")[0]).html());

    // disabled the submit button
    $("#btnSave").prop("disabled", true);

    $.ajax({
        type: "POST",
        enctype: 'multipart/form-data',
        url: "/FAQs/AddFaqs",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val());
            onBegin(xhr);
        },
        data: data,
        processData: false,
        contentType: false,
        cache: false,
        timeout: 600000,
        success: function (data) {
            $("#btnSave").prop("disabled", false);
            onSuccess(data);
        },
        error: function (e) {
            $("#btnSave").prop("disabled", false);
            onFailed(e);
        }
    });

});



// Validate Featured Message form
var validateSaveFaq = function () {

    var isValid = true;
    var isTab1Valid = true;
    var isTab2Valid = true;

    // check if the form input is valid
    if (!$("#formFaqs").valid()) {
        $("#formFaqs").submit();
        isValid = false;
    }

    // manual validation of nice editor for Content
    if ($("div.nicEdit-main").text().trim().length === 0) {
        $("span[data-valmsg-for='AnswerText']").text("Please enter the answer.");
        isValid = false;
        isTab1Valid = false;
    }
    else {
        $("span[data-valmsg-for='AnswerText']").text("");
    }

    // Check content image is browsed or not
    //if ($("#MessageImage").val().trim() === '') {
    //    isTab1Valid = false;
    //    isValid = false;
    //}

    if ($("#QuestionText").val().trim().length === 0) {
        $("span[data-valmsg-for='QuestionText']").text("Please enter the question text.");
        isValid = false;
        isTab1Valid = false;
    }
    else {
        $("span[data-valmsg-for='QuestionText']").text("");
    }

    if ($("#QuestionText").val().trim().length > 0) {
        if ($("#QuestionText").val().trim().length > 300) {
            $("span[data-valmsg-for='QuestionText']").text("The question text should not be greater than 300 characters.");
            isValid = false;
            isTab1Valid = false;
        }
        else {
            $("span[data-valmsg-for='QuestionText']").text("");
        }
    }


    // Check creation date is entered or not
    //if ($(".publishDate").val().trim() === '') {
    //    isTab2Valid = false;
    //    isValid = false;
    //}

    
    // check author name is entered or not
    if ($("#AuthorName").val().trim().length === 0) {
        $("span[data-valmsg-for='AuthorName']").text("Please enter the author name.");
        isValid = false;
        isTab2Valid = false;
    }
    else {
        $("span[data-valmsg-for='AuthorName']").text("");
    }

    if ($("#AuthorName").val().trim().length > 0) {
        if ($("#AuthorName").val().trim().length > 40) {
            $("span[data-valmsg-for='AuthorName']").text("The author name not must be greater than 40 characters.");
            isValid = false;
            isTab2Valid = false;
        }
        else {
            $("span[data-valmsg-for='AuthorName']").text("");
        }
    }


    // Click Tab1 if it has incomplete inputs
    if (!isTab1Valid) {
        $('#tabUL a[href="#tab_1"]').trigger('click');
    }

    // Click Tab2 if it has incomplete inputs
    else if (!isTab2Valid) {
        //$("#tab_2").click();
        $('#tabUL a[href="#tab_2"]').trigger('click');

        $("#formFaqs").submit();
        isValid = false;

    }

    return isValid;



}

// On begin ajax request this function will be call
var onBegin = function (xhr) {
    $(".loaderModal").show();
};


// On success of ajax callback this function will be call
var onSuccess = function (data) {

    $(".loaderModal").hide();

    if (data.isSuccess) {
        swal(
            'Your FAQ has been successfully added.',
            '',
            'success'
        ).then(function () {
            window.location.href = "/FAQs/Index";

        });
    }
    else {
        swal(
            data.message,
            '',
            'error'
        );
    }
};

// On failed/error of ajax 
var onFailed = function (context) {
    return false;
};

function refreshCheckedTree()
{
    debugger;
    var selectobject = document.getElementById("Multiple_select_docs").getElementsByTagName("option");
   
    var selectobject = $("[id*=treemultiselect-0-]:checked");
    

    for (i = 0; i < selectobject.length; i++) {

        $(selectobject[i]).removeAttr('checked');

     
    }
    $(".tree-multiselect .section").addClass('collapsed');
    $(".selected").html("");
    $("#DocumentIds").val("");

}


function getCheckedTree() {
    //debugger;
    var selectobject = document.getElementById("Multiple_select_docs").getElementsByTagName("option");


    var tempCtr = 0;
    var $checkedText = $("#checkedTree");

    var selectobject = $("[id*=treemultiselect-0-]:checked");
    $checkedText.val("");
    var checkValueArray = [];
    for (i = 0; i < selectobject.length; i++) {
        if (tempCtr === 0) {
            tempCtr = 1;
            console.log(selectobject[i])
            $checkedText.val($(selectobject[i]).parent().data("value"));

        } else {
            $checkedText.val($checkedText.val() + $(selectobject[i]).parent().data("value"));
        }
        checkValueArray.push($(selectobject[i]).parent().data("value"));
    }
    selectedDocuments = "";
    //selectedDocuments.write(checkValueArray.join(", "));
    for (var i = 0; i < checkValueArray.length; i++) {
        if (selectedDocuments === "") {
            selectedDocuments = checkValueArray[i];
        }
        else {
            selectedDocuments = selectedDocuments + "," + checkValueArray[i];
        }
    }

    $("#DocumentIds").val(selectedDocuments);
}



// Edit faq Starts

// Edit FAQs using ajax

// Post data on Edit button click

$("#btnEdit").click(function (event) {

    if ($("#DocumentIds").val() === "")
    {
        getCheckedTree();
    }
    //stop submit the form, we will post it manually.
    //event.preventDefault();

    // Validation is ok or not
    if (validateEditFaqMessage() === false) {
        return false;
    }

    // Get form
    var form = $('#EditformFaqs')[0];


    // Create an FormData object 
    var data = new FormData(form);

    // If you want to add an extra field for the FormData
    data.append("TxtArea", $($("div.nicEdit-main")[0]).html());

    // disabled the submit button
    $("#btnEdit").prop("disabled", true);

    $.ajax({
        type: "POST",
        enctype: 'multipart/form-data',
        url: "/FAQs/EditFaqs",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val());
            onBegin(xhr);
        },
        data: data,
        processData: false,
        contentType: false,
        cache: false,
        timeout: 600000,
        success: function (data) {
            $("#btnEdit").prop("disabled", false);
            onSuccess(data);
        },
        error: function (e) {
            $("#btnEdit").prop("disabled", false);
            onFailed(e);
        }
    });

});

// Validate Featured Message form
var validateEditFaqMessage = function () {
    var isValid = true;
    var isTab1Valid = true;
    var isTab2Valid = true;

    // check if the form input is valid
    if (!$("#EditformFaqs").valid()) {
        $("#EditformFaqs").submit();
        isValid = false;
    }

    // manual validation of nice editor for Content
    if ($("div.nicEdit-main").text().trim().length === 0) {
        $("span[data-valmsg-for='AnswerText']").text("Please enter the question text.");
        isValid = false;
        isTab1Valid = false;
    }
    else {
        $("span[data-valmsg-for='AnswerText']").text("");
    }

    // Check content image is browsed or not
    //if ($("#MessageImage").val().trim() === '') {
    //    isTab1Valid = false;
    //    isValid = false;
    //}

    // Check creation date is entered or not
    if ($(".publishDate").val().trim() === '') {
        isTab2Valid = false;
        isValid = false;
    }

    // check author name is entered or not
    if ($("#EditAuthorName").val().trim() === '') {
        isTab2Valid = false;
        isValid = false;
    }

    // Click Tab1 if it has incomplete inputs
    if (!isTab1Valid) {
        $('#EditTabUL a[href="#tab_1"]').trigger('click');
    }

    // Click Tab2 if it has incomplete inputs
    else if (!isTab2Valid) {
        //$("#tab_2").click();
        $('#EditTabUL a[href="#tab_2"]').trigger('click');

        $("#EditformFaqs").submit();
        isValid = false;

    }

    return isValid;


}

var onBeginEdit = function () {

};

var onCompleteEdit = function () {

};

var onSuccessEdit = function (context) {

    swal(
        'Your FAQ has been successfully updated.',
        '',
        'success'
    ).then(function () {
        window.location.href = "/FAQs/Index";

    });
};

var onFailedEdit = function (context) {

};

// Edit Faq Ends

function PutSampleContent(ctrl) {
    var value = $(ctrl.elm.parentElement.parentElement.parentElement).find("label").attr('for');

    //FAQAnswerText
    if (value === "QA_Question-Answer") {
        $("#divValueFAQAnswerText div.nicEdit-main").html();
        $("#divValueFAQAnswerText div.nicEdit-main").html($('#hdnDivValueFAQAnswerText').html());
    }
}