$(document).ready(function () {


    $("#ValueBackgrountImageData").change(function () {
        readBackgroundImageData(this);
    });


    //Communication Image Data
    $("#CommunicationIconData").change(function () {
        readCommunicationIconData(this);
    });

    $("#CommunicationImageData").change(function () {
        readCommunicationImageData(this);
    });


    //Dedication Image
    $("#DedicationIconData").change(function () {
        readDedicationIconData(this);
    });

    $("#DedicationImageData").change(function () {
        readDedicationImageData(this);
    });


    //Care Image
    $("#CareIconData").change(function () {
        readCareIconData(this);
    });

    $("#CareImageData").change(function () {
        readCareImageData(this);
    });

    //Excellent Icon image
    $("#ExcellentIconData").change(function () {
        readExcellentIconData(this);
    });

    $("#ExcellentImageData").change(function () {
        readExcellentImageData(this);
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

    $('body').on('change', 'input:file[class=upload]', function (e) {
        var FileUpload = $(this).attr('id');
        if (this.files && this.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {

                $("#" + FileUpload).next("img").attr('src', e.target.result);

            }
            reader.readAsDataURL(this.files[0]);
        }
    });


    bkLib.onDomLoaded(function () {
        new nicEditor({ fullPanel: true, maxHeight: 200 }).panelInstance('ValueTopRightText');
        new nicEditor({ fullPanel: true, maxHeight: 200 }).panelInstance('ValueTopLeftText');
        new nicEditor({ fullPanel: true, maxHeight: 200 }).panelInstance('CommunicationContent');;
        new nicEditor({ fullPanel: true, maxHeight: 200 }).panelInstance('DedicationContent');
        new nicEditor({ fullPanel: true, maxHeight: 200 }).panelInstance('CareContent');
        new nicEditor({ fullPanel: true, maxHeight: 200 }).panelInstance('ExcellentContent');
    });

    ////Select Current Menu
    //$('.nav-li').removeClass('current');
    //$('#menuItemAdminUsers').addClass('current');

    // Put Rich Text editor html content in Disabled TextArea Content on keyup
    $('body').on("keyup", "#divValueTopLeftText div.nicEdit-main", function () {

        $('body #ValueTopLeftText').val($(this).html());
    });

    $('body').on("keyup", "#divValueTopRightText div.nicEdit-main", function () {

        $('body #ValueTopRightText').val($(this).html());
    });

    $('body').on("keyup", "#divCommunicationContent div.nicEdit-main", function () {

        $('body #CommunicationContent').val($(this).html());
    });

    $('body').on("keyup", "#divDedicationContent div.nicEdit-main", function () {

        $('body #DedicationContent').val($(this).html());
    });

    $('body').on("keyup", "#divCareContent div.nicEdit-main", function () {

        $('body #CareContent').val($(this).html());
    });

    $('body').on("keyup", "#divExcellentContent div.nicEdit-main", function () {

        $('body #ExcellentContent').val($(this).html());
    });

});


// Image Validation


// Post data on save button click
$("#btnSave").click(function (event) {

    // Read html content from editor
    $('body #ValueTopLeftText').val($('#divValueTopLeftText div.nicEdit-main').html());
    $('body #ValueTopRightText').val($('#divValueTopRightText div.nicEdit-main').html());

    //$('body #CommunicationContent').val($('#divCommunicationContent div.nicEdit-main').html());
    //$('body #DedicationContent').val($('#divDedicationContent div.nicEdit-main').html());
    //$('body #CareContent').val($('#divCareContent div.nicEdit-main').html());
    //$('body #ExcellentContent').val($('#divExcellentContent div.nicEdit-main').html());

    $('body #CommunicationContent').val($('#divCommunicationContent div.nicEdit-main').text());
    $('body #DedicationContent').val($('#divDedicationContent div.nicEdit-main').text());
    $('body #CareContent').val($('#divCareContent div.nicEdit-main').text());
    $('body #ExcellentContent').val($('#divExcellentContent div.nicEdit-main').text());

    //stop submit the form, we will post it manually.
    //event.preventDefault();

    // Validation is ok or not
    if (validateOurValues() == false) {
        return false;
    }

    // Get form
    var form = $('#formOurValues')[0];


    // Create an FormData object 
    var data = new FormData(form);

    // If you want to add an extra field for the FormData
    // data.append("CustomField", "This is some extra data, testing");

    // disabled the submit button
    $("#btnSave").prop("disabled", true);

    $.ajax({
        type: "POST",
        enctype: 'multipart/form-data',
        url: "/SuperAdmin/UpdateOurValues",
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
var validateOurValues = function () {
    var isValid = true;
    var isTab1Valid = true;
    var isTab2Valid = true;
    var isTab3Valid = true;
    var isTab4Valid = true;
    var isTab5Valid = true;


    var ValueBackgrountImage = $("#img1").attr("src");
    var SubStringValueBackgrountImage = ValueBackgrountImage.substring(ValueBackgrountImage.lastIndexOf("/") + 1, ValueBackgrountImage.length);
    if (SubStringValueBackgrountImage.trim() == '') {
        isTab1Valid = false;
        isValid = false;
        $("#lblErrorValueBackgrountImageData").text("Please upload an image.");
    }
    else
    {
        $("#lblErrorValueBackgrountImageData").text("");
    }

    // manual validation of nice editor for 
    if ($("#divValueTopLeftText div.nicEdit-main").text().trim().length == 0) {
        $("span[data-valmsg-for='ValueTopLeftText']").text("Please enter the top left content.");
        isValid = false;
        isTab1Valid = false;
    }
    else {
        $("span[data-valmsg-for='ValueTopLeftText']").text("");
    }




    if ($("#divValueTopRightText div.nicEdit-main").text().trim().length == 0) {
        $("span[data-valmsg-for='ValueTopRightText']").text("Please enter the top right content.");
        isValid = false;
        isTab1Valid = false;
    }
    else {
        $("span[data-valmsg-for='ValueTopRightText']").text("");
    }

    //communication icon
    var CommunicationIconImage = $("#img2").attr("src");
    var SubStringCommunicationIconImage = CommunicationIconImage.substring(CommunicationIconImage.lastIndexOf("/") + 1, CommunicationIconImage.length);
    if (SubStringCommunicationIconImage.trim() == '') {
        isValid = false;
        isTab2Valid = false;
        $("#lblErrorCommunicationIcon").text("Please upload an image.");
    }
    else {
        $("#lblErrorCommunicationIcon").text("");
    }

    //communication image
    var CommunicationImageImage = $("#img22").attr("src");
    var SubStringCommunicationImageImage = CommunicationImageImage.substring(CommunicationImageImage.lastIndexOf("/") + 1, CommunicationImageImage.length);
    if (SubStringCommunicationImageImage.trim() == '') {
        isValid = false;
        isTab2Valid = false;
        $("#lblErrorCommunicationImage").text("Please upload an image.");
    }
    else {
        $("#lblErrorCommunicationImage").text("");
    }

    if ($("#divCommunicationContent div.nicEdit-main").text().trim().length == 0) {
        $("span[data-valmsg-for='CommunicationContent']").text("Please enter the Communication content.");
        isValid = false;
        isTab2Valid = false;
    }
    else {
        $("span[data-valmsg-for='CommunicationContent']").text("");
    }

    //Dedication icon
    var DedicationIconData = $("#img3").attr("src");
    var SubStringDedicationIconData = DedicationIconData.substring(DedicationIconData.lastIndexOf("/") + 1, DedicationIconData.length);
    if (SubStringDedicationIconData.trim() == '') {
        isValid = false;
        isTab3Valid = false;
        $("#lblErrorDedicationIconData").text("Please upload an image.");
    }
    else {
        $("#lblErrorDedicationIconData").text("");
    }

    //Dedication image
    var DedicationImageData = $("#img33").attr("src");
    var SubStringDedicationImageData = DedicationImageData.substring(DedicationImageData.lastIndexOf("/") + 1, DedicationImageData.length);
    if (SubStringDedicationImageData.trim() == '') {
        isValid = false;
        isTab3Valid = false;
        $("#lblErrorDedicationImageData").text("Please upload an image.");
    }
    else {
        $("#lblErrorDedicationImageData").text("");
    }

    if ($("#divDedicationContent div.nicEdit-main").text().trim().length == 0) {
        $("span[data-valmsg-for='DedicationContent']").text("Please enter the Dedication content.");
        isValid = false;
        isTab3Valid = false;
    }
    else {
        $("span[data-valmsg-for='DedicationContent']").text("");
    }

    //Care icon
    var CareIconData = $("#img4").attr("src");
    var SubStringCareIconData = CareIconData.substring(CareIconData.lastIndexOf("/") + 1, CareIconData.length);
    if (SubStringCareIconData.trim() == '') {
        isValid = false;
        isTab4Valid = false;
        $("#lblErrorCareIconData").text("Please upload an image.");
    }
    else {
        $("#lblErrorCareIconData").text("");
    }

    //Care image
    var CareImageData = $("#img44").attr("src");
    var SubStringCareImageData = CareImageData.substring(CareImageData.lastIndexOf("/") + 1, CareImageData.length);
    if (SubStringCareImageData.trim() == '') {
        isValid = false;
        isTab4Valid = false;
        $("#lblErrorCareImageData").text("Please upload an image.");
    }
    else {
        $("#lblErrorCareImageData").text("");
    }

    if ($("#divCareContent div.nicEdit-main").text().trim().length == 0) {
        $("span[data-valmsg-for='CareContent']").text("Please enter the Care content.");
        isValid = false;
        isTab4Valid = false;
    }
    else {
        $("span[data-valmsg-for='CareContent']").text("");
    }


    //Excellence icon
    var ExcellenceIconData = $("#img5").attr("src");
    var SubStringExcellenceIconData = ExcellenceIconData.substring(ExcellenceIconData.lastIndexOf("/") + 1, ExcellenceIconData.length);
    if (SubStringExcellenceIconData.trim() == '') {
        isValid = false;
        isTab5Valid = false;
        $("#lblErrorExcellentIconData").text("Please upload an image.");
    }
    else {
        $("#lblErrorExcellentIconData").text("");
    }

    //Excellence image
    var ExcellenceImageData = $("#img55").attr("src");
    var SubStringExcellenceImageData = ExcellenceImageData.substring(ExcellenceImageData.lastIndexOf("/") + 1, ExcellenceImageData.length);
    if (SubStringExcellenceImageData.trim() == '') {
        isValid = false;
        isTab5Valid = false;
        $("#lblErrorExcellentImageData").text("Please upload an image.");
    }
    else {
        $("#lblErrorExcellentImageData").text("");
    }

    if ($("#divExcellentContent div.nicEdit-main").text().trim().length == 0) {
        $("span[data-valmsg-for='ExcellentContent']").text("Please enter the Excellence content.");
        isValid = false;
        isTab5Valid = false;
    }
    else {
        $("span[data-valmsg-for='ExcellentContent']").text("");
    }

    // check value title is entered or not
    if ($("#ValueTitle").val().trim() == '') {
        isTab1Valid = false;
        isValid = false;
    }

    // check communication title is entered or not
    if ($("#CommunicationTitle").val().trim() == '') {
        isTab2Valid = false;
        isValid = false;
    }

    // check Dedication title is entered or not
    if ($("#DedicationTitle").val().trim() == '') {
        isTab3Valid = false;
        isValid = false;
    }

    // check Care title is entered or not
    if ($("#CareTitle").val().trim() == '') {
        isTab4Valid = false;
        isValid = false;
    }

    // check Care title is entered or not
    if ($("#CareTitle").val().trim() == '') {
        isTab4Valid = false;
        isValid = false;
    }

    // check Excellent title is entered or not
    if ($("#ExcellentTitle").val().trim() == '') {
        isTab4Valid = false;
        isValid = false;
    }

    // Click Tab1 if it has incomplete inputs
    if (!isTab1Valid) {
        $("#tab1").click();
    }

    // Click Tab2 if it has incomplete inputs
    else if (!isTab2Valid) {
        $("#tab2").click();
    }

    // Click Tab3 if it has incomplete inputs
    else if (!isTab3Valid) {
        $("#tab3").click();
    }

    // Click Tab4 if it has incomplete inputs
    else if (!isTab4Valid) {
        $("#tab4").click();
    }

    // Click Tab5 if it has incomplete inputs
    else if (!isTab5Valid) {
        $("#tab5").click();
    }

    // Submit the form to display the error message if isValid=false
    if (isValid == false) {
        $("#formOurValues").submit();
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
        'The Our Values information was successfully updated.',
        '',
        'success'
    );

    //.then(function () {    window.location.href = "/SuperAdmin/OurValues";})


};

// On failed/error of ajax 
var onFailed = function (context) {
    return false;
};


function readBackgroundImageData(input) {
    debugger;
    if (input.files && input.files[0]) {
        var checkValid = CheckFileTypeExtension(input.files[0].name);
        if (checkValid == true) {
            var reader = new FileReader();

            reader.onload = copyImage;

            function copyImage(ev) {
                $('#img1').attr('src', ev.target.result);
                $("#img1").show();
            };

            reader.readAsDataURL(input.files[0]);
            $("#lblErrorValueBackgrountImageData").text("");
            //$("#btnSave").prop("disabled", false);
            $("#img1").show();

        }
        else {
            $("#lblErrorValueBackgrountImageData").text("Please upload an image.");
            // $("#btnSave").prop("disabled", true);
            $(input).val('');
            $("#img1").hide();
            $("#img1").attr("src", "");
        }
    }
}

function readCommunicationIconData(input) {
    debugger;
    if (input.files && input.files[0]) {
        var checkValid = CheckFileTypeExtension(input.files[0].name);
        if (checkValid == true) {
            var reader = new FileReader();

            reader.onload = copyImage;

            function copyImage(ev) {
                $('#img2').attr('src', ev.target.result);
                $("#img2").show();
            };

            reader.readAsDataURL(input.files[0]);
            $("#lblErrorCommunicationIcon").text("");
            //$("#btnSave").prop("disabled", false);
            $("#img2").show();

        }
        else {
            $("#lblErrorCommunicationIcon").text("Please upload an image.");
            // $("#btnSave").prop("disabled", true);
            $(input).val('');
            $("#img2").hide();
            $("#img2").attr("src", "");
        }
    }
}

function readCommunicationImageData(input) {
    debugger;
    if (input.files && input.files[0]) {
        var checkValid = CheckFileTypeExtension(input.files[0].name);
        if (checkValid == true) {
            var reader = new FileReader();

            reader.onload = copyImage;

            function copyImage(ev) {
                $('#img22').attr('src', ev.target.result);
                $("#img22").show();
            };

            reader.readAsDataURL(input.files[0]);
            $("#lblErrorCommunicationImage").text("");
            //$("#btnSave").prop("disabled", false);
            $("#img22").show();

        }
        else {
            $("#lblErrorCommunicationImage").text("Please upload an image.");
            // $("#btnSave").prop("disabled", true);
            $(input).val('');
            $("#img22").hide();
            $("#img22").attr("src", "");
        }
    }
}

function readDedicationIconData(input) {
    debugger;
    if (input.files && input.files[0]) {
        var checkValid = CheckFileTypeExtension(input.files[0].name);
        if (checkValid == true) {
            var reader = new FileReader();

            reader.onload = copyImage;

            function copyImage(ev) {
                $('#img3').attr('src', ev.target.result);
                $("#img3").show();
            };

            reader.readAsDataURL(input.files[0]);
            $("#lblErrorDedicationIconData").text("");
            //$("#btnSave").prop("disabled", false);
            $("#img3").show();

        }
        else {
            $("#lblErrorDedicationIconData").text("Please upload an image.");
            // $("#btnSave").prop("disabled", true);
            $(input).val('');
            $("#img3").hide();
            $("#img3").attr("src", "");
        }
    }
}

function readDedicationImageData(input) {
    debugger;
    if (input.files && input.files[0]) {
        var checkValid = CheckFileTypeExtension(input.files[0].name);
        if (checkValid == true) {
            var reader = new FileReader();

            reader.onload = copyImage;

            function copyImage(ev) {
                $('#img33').attr('src', ev.target.result);
                $("#img33").show();
            };

            reader.readAsDataURL(input.files[0]);
            $("#lblErrorDedicationImageData").text("");
            //$("#btnSave").prop("disabled", false);
            $("#img33").show();

        }
        else {
            $("#lblErrorDedicationImageData").text("Please upload an image.");
            // $("#btnSave").prop("disabled", true);
            $(input).val('');
            $("#img33").hide();
            $("#img33").attr("src", "");
        }
    }
}

function readCareIconData(input) {
    debugger;
    if (input.files && input.files[0]) {
        var checkValid = CheckFileTypeExtension(input.files[0].name);
        if (checkValid == true) {
            var reader = new FileReader();

            reader.onload = copyImage;

            function copyImage(ev) {
                $('#img4').attr('src', ev.target.result);
                $("#img4").show();
            };

            reader.readAsDataURL(input.files[0]);
            $("#lblErrorCareIconData").text("");
            //$("#btnSave").prop("disabled", false);
            $("#img4").show();

        }
        else {
            $("#lblErrorCareIconData").text("Please upload an image.");
            // $("#btnSave").prop("disabled", true);
            $(input).val('');
            $("#img4").hide();
            $("#img4").attr("src", "");
        }
    }
}

function readCareImageData(input) {
    debugger;
    if (input.files && input.files[0]) {
        var checkValid = CheckFileTypeExtension(input.files[0].name);
        if (checkValid == true) {
            var reader = new FileReader();

            reader.onload = copyImage;

            function copyImage(ev) {
                $('#img44').attr('src', ev.target.result);
                $("#img44").show();
            };

            reader.readAsDataURL(input.files[0]);
            $("#lblErrorCareImageData").text("");
            //$("#btnSave").prop("disabled", false);
            $("#img44").show();

        }
        else {
            $("#lblErrorCareImageData").text("Please upload an image.");
            // $("#btnSave").prop("disabled", true);
            $(input).val('');
            $("#img44").hide();
            $("#img44").attr("src", "");
        }
    }
}

function readExcellentIconData(input) {
    debugger;
    if (input.files && input.files[0]) {
        var checkValid = CheckFileTypeExtension(input.files[0].name);
        if (checkValid == true) {
            var reader = new FileReader();

            reader.onload = copyImage;

            function copyImage(ev) {
                $('#img5').attr('src', ev.target.result);
                $("#img5").show();
            };

            reader.readAsDataURL(input.files[0]);
            $("#lblErrorExcellentIconData").text("");
            //$("#btnSave").prop("disabled", false);
            $("#img5").show();

        }
        else {
            $("#lblErrorExcellentIconData").text("Please upload an image.");
            // $("#btnSave").prop("disabled", true);
            $(input).val('');
            $("#img5").hide();
            $("#img5").attr("src", "");
        }
    }
}

function readExcellentImageData(input) {
    debugger;
    if (input.files && input.files[0]) {
        var checkValid = CheckFileTypeExtension(input.files[0].name);
        if (checkValid == true) {
            var reader = new FileReader();

            reader.onload = copyImage;

            function copyImage(ev) {
                $('#img55').attr('src', ev.target.result);
                $("#img55").show();
            };

            reader.readAsDataURL(input.files[0]);
            $("#lblErrorExcellentImageData").text("");
            //$("#btnSave").prop("disabled", false);
            $("#img55").show();

        }
        else {
            $("#lblErrorExcellentImageData").text("Please upload an image.");
            // $("#btnSave").prop("disabled", true);
            $(input).val('');
            $("#img55").hide();
            $("#img55").attr("src", "");
        }
    }
}
function PutSampleContent(ctrl) {
    var value = $(ctrl.elm.parentElement.parentElement.parentElement).find("label").attr('for');

    //Intro Top Left Content
    if (value == "Values_Top-Left-Text") {
        $("#divValueTopLeftText div.nicEdit-main").html();
        $("#divValueTopLeftText div.nicEdit-main").html($('#hdnDivValueTopLeftText').html());
    }
    //Intro Top Right Content
    else if (value == "Values_Top-Right-Text") {
        $("#divValueTopRightText div.nicEdit-main").html();
        $("#divValueTopRightText div.nicEdit-main").html($('#hdnDivValueTopRightText').html());
    }
    //Communication Content
    else if (value == "Communication_Content") {
        $("#divCommunicationContent div.nicEdit-main").html();
        $("#divCommunicationContent div.nicEdit-main").html($('#hdnDivValueCommunicationContent').html());
    }
    //Dedication Content
    else if (value == "Dedication_Content") {
        $("#divDedicationContent div.nicEdit-main").html();
        $("#divDedicationContent div.nicEdit-main").html($('#hdnDivValueDedicationContent').html());
    }
    //Care Content
    else if (value == "Care_Content") {
        $("#divCareContent div.nicEdit-main").html();
        $("#divCareContent div.nicEdit-main").html($('#hdnDivValueCareContent').html());
    }
    //Excellence Content
    else if (value == "Excellent_Content") {
        $("#divExcellentContent div.nicEdit-main").html();
        $("#divExcellentContent div.nicEdit-main").html($('#hdnDivValueExcellentContent').html());
    }
}