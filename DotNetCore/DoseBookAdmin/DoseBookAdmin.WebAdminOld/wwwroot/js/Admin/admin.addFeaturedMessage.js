$(document).ready(function () {

    $("#img1").hide();

    $("#fileUploaderImg1").change(function () {
        readIMG1(this);
    });
    
    bkLib.onDomLoaded(function () {
        new nicEditor({ fullPanel: true, maxHeight: 200 }).panelInstance('Content');
    });
    

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


    

    // Put Rich Text editor html content in Disabled TextArea Content on keyup
    $(document).on("keyup", "div.nicEdit-main", function () {
        $("#Content").val($("div.nicEdit-main").html());
    });

});

// Post data on save button click

$("#btnSave").click(function (event) {   

    $("#Content").val($("div.nicEdit-main").html()); 
    //stop submit the form, we will post it manually.
   // event.preventDefault();

    // Validation is ok or not
    if (validateFeaturedMessage() == false) {
        //$("#btnSave").prop("disabled", true);
        return false;
    }
    else {
        //$("#btnSave").prop("disabled", false);
    }

    // Get form
    var form = $('#formFeaturedMessage')[0]; 
    
    
    // Create an FormData object 
    var data = new FormData(form);

    // If you want to add an extra field for the FormData
    // data.append("CustomField", "This is some extra data, testing");

    // disabled the submit button
   // $("#btnSave").prop("disabled", true);

    $.ajax({
        type: "POST",
        enctype: 'multipart/form-data',
        url: "/SuperAdmin/AddFeaturedMessage",
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
            //$("#btnSave").prop("disabled", false);
            onSuccess(data);
        },
        error: function (e) {
           // $("#btnSave").prop("disabled", false);
            onFailed(e);
        }
    });

});



// Validate Featured Message form
var validateFeaturedMessage = function () {
    var isValid = true;
    var isTab1Valid = true;
    var isTab2Valid = true;

    // check if the form input is valid
    if (!$("#formFeaturedMessage").valid()) {
        $("#formFeaturedMessage").submit();
        isValid = false;
    }

    // manual validation of nice editor for Content
    if ($("div.nicEdit-main").text().trim().length == 0) {
        $("span[data-valmsg-for='Content']").text("Please enter the content.");
        isValid = false;
        isTab1Valid = false;
    }
    else {
        $("span[data-valmsg-for='Content']").text("");
    }

    // Check content image is browsed or not
    if ($("#fileUploaderImg1").val().trim() == '') {
        isTab1Valid = false;
        isValid = false;
        $("#MessageImage").text("Please upload an image.");
    }

    // Check creation date is entered or not
    //if ($("#CreationDate").val().trim() == '') {
    //    isTab2Valid = false;
    //    isValid = false;
    //}

    // check author name is entered or not
    if ($("#AuthorName").val().trim().length == 0) {
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
        $("#tab1").click();
    }

    // Click Tab2 if it has incomplete inputs
    else if (!isTab2Valid) {
        $("#tab2").click();
    }

    return isValid;


}

// On begin ajax request this function will be call
var onBegin = function (xhr) {
    $(".loaderModal").show();
};


// On success of ajax callback this function will be call
var onSuccess = function (context) {
    $(".loaderModal").hide();
    swal(
        'The featured message was successfully saved.',
        '',
        'success'
    ).then(function () {
        window.location.href = "/SuperAdmin/FeaturedMessage";

        });
    
};

// On failed/error of ajax 
var onFailed = function (context) {
    return false;
};

function readIMG1(input) {
    debugger;
    if (input.files && input.files[0]) {
        var checkValid = CheckFileTypeExtensionForFeatureMessage(input.files[0].name);
        if (checkValid == true) {
            var reader = new FileReader();

            reader.onload = copyImage;

            function copyImage(ev) {
                $('#img1').attr('src', ev.target.result);
                $("#img1").show();
            };

            reader.readAsDataURL(input.files[0]);
            $("#MessageImage").text("");
            //$("#btnSave").prop("disabled", false);
            $("#img1").show();

        }
        else {
            $("#MessageImage").text("Please upload an image.");
           // $("#btnSave").prop("disabled", true);
            $(input).val('');
            $("#img1").hide();
        }
    }
}

function CheckFileTypeExtensionForFeatureMessage(fileName) {
    var fileTypeExtension = fileName.substr(fileName.lastIndexOf('.')).toLowerCase();
    var _validFileExtensions = [".jpg", ".jpeg", ".bmp", ".gif", ".png"];
    var blnValid = false;
    for (var j = 0; j < _validFileExtensions.length; j++) {
        var sCurExtension = _validFileExtensions[j];
        if (fileTypeExtension == sCurExtension.toLowerCase()) {
            blnValid = true;
            break;
        }
    }

    if (!blnValid) {
        swal({
            type: 'error',
            //title: "Sorry, " + fileName + " is invalid, allowed extensions are: " + _validFileExtensions.join(", "),
            title: 'The image file types allowed .jpg, .jpeg, .bmp, .gif, .png.',
            text: '',
            width: '500px'
        })
        return false;
    }
    else { return true; }
}


function PutSampleContent(ctrl) {
    var value = $(ctrl.elm.parentElement.parentElement.parentElement).find("label").attr('for');

    //FeaturedMessageContent
    if (value == "Message_Content") {
        $("#divValueFeaturedMeesageContent div.nicEdit-main").html();
        $("#divValueFeaturedMeesageContent div.nicEdit-main").html($('#hdnDivValueFeaturedMeesageContent').html());
    }
}