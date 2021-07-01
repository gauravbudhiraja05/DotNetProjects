$(document).ready(function () {

    //Select Current Menu
    $('.nav-li').removeClass('current');
    $('#menuItemSuperAdmin').addClass('current');

    var currentTab = location.hash
    if (currentTab) {
        $(".tab_list li").removeClass("current");
        //$(".tab_list li>a[href=" + currentTab + "]").closest("li").addClass("current");
        $('.tab_list li a[href="' + currentTab + '"]').closest("li").addClass("current");
        $(".tab_content").hide();
        $(currentTab).show();
        $(currentTab + "_btn").show()
    }


    $('.upload').change(function () {
        var filename = $(this).val().split('\\').pop();
        var displayFileName = "FileName : " + filename;
        $("#docFileName").text(displayFileName);
        ValidateUploadedFile();
    });



});

// Validate Gazetteers form
var ValidateUploadedFile = function () {

    var allowedFiles = ["xlsx", "xlsm"];
    var filename = $('.upload').val().split('\\').pop();
    var lblError = $("#lblDocError");
    if ($.inArray(filename.split('.')[1], allowedFiles) == -1) {
        //lblError.html("Please upload files having extensions: <b>" + allowedFiles.join(', ') + "</b> only.");
        lblError.html("Please upload files of type .xlsx and .xlsm only.");
        $("#AddDocumentBtn").hide();
        return false;
    }
    lblError.html('');
    $("#btnSave").show();
    return true;
}



// Post data on save button click

$("#btnSave").click(function (event) {

    //stop submit the form, we will post it manually.
    //event.preventDefault();
    // Validation is ok or not
    if (ValidateUploadedFile() == false) {
        return false;
    }

    // Get form
    var form = $('#formGazetteers')[0];


    // Create an FormData object 
    var data = new FormData(form);

    // If you want to add an extra field for the FormData
    // data.append("CustomField", "This is some extra data, testing");

    // disabled the submit button
    $("#btnSave").prop("disabled", true);

    $.ajax({
        type: "POST",
        enctype: 'multipart/form-data',
        url: "/SuperAdmin/AddGazetteers",
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



// On begin ajax request this function will be call
var onBegin = function (xhr) {
    $(".loaderModal").show();
};


// On success of ajax callback this function will be call
var onSuccess = function (context) {
    $(".loaderModal").hide();
    if (context.isSuccess == true) {
        swal(
            'The Gazetteer has been successfully updated.',
            '',
            'success'
        ).then(function () {
            window.location.href = "/SuperAdmin/Gazetteers";

        });
    }
    else {

        swal({
            title: context.message,
            text: "",
            type: 'error',
            width: '500px'
        })
    }
};

// On failed/error of ajax 
var onFailed = function (context) {
    return false;
};

