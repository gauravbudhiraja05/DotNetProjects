$(document).ready(function () {
    var value = $('#hdnSelectedDepartmentId').val();

    //var attr3 = $("img3").attr('src');
    //if (attr3 == "" || attr3 == null || attr3 == undefined)
    //{
    //    $("#img3").hide();
    //}

    //var attr4 = $("img4").attr('src');
    //if (attr4 == "" || attr4 == null || attr4 == undefined) {
    //    $("#img4").hide();
    //}

    $("#EditfileUploaderImg1").change(function () {
        readIMG1(this);
    });

    $("#EditfileUploaderImg2").change(function () {
        readIMG2(this);
    });

    $("#EditfileUploaderImg3").change(function () {
        readIMG3(this);
    });

    $("#EditfileUploaderImg4").change(function () {
        readIMG4(this);
    });

    var currentTab = location.hash
    if (currentTab) {
        $(".tab_list li").removeClass("current");
        //$(".tab_list li>a[href=" + currentTab + "]").closest("li").addClass("current");
        $('.tab_list li a[href="' + currentTab + '"]').closest("li").addClass("current");
        $(".tab_content").hide();
        $(currentTab).show();
        $(currentTab + "_btn").show()
    }


    $('.form-control').datepicker({

        language: 'en',
        minDate: new Date(),
        autoClose: true,
    });

    $('#btnBack').on('click', function () {
        var departmentId = parseInt($('#hdnSelectedDepartmentId').val());
        window.location.href = "Index?DepartmentId=" + departmentId;
        //onClick="window.location.href='@Url.Action("Index","News")'"
    });

    $('.upload_remove').on('click', function () {
        var ctrlId = $(this).attr("id");
        var fileUploadedId = String(ctrlId.split('_')[1]);
        var imgId = String(ctrlId.split('_')[2]);

        $('#' + fileUploadedId).val("");
        $('#' + imgId).hide();
        $('#' + imgId).attr("src", "");
        $(this).hide();
    });

    //// Put Rich Text editor html content in Disabled TextArea Content on keyup
    //$(document).on("keyup", "div.nicEdit-main", function () {
    //    if ($($("div.nicEdit-main")[0]).html() != "<br>")
    //        $("#Content1").val($($("div.nicEdit-main")[0]).html());

    //    if ($($("div.nicEdit-main")[1]).html() != "<br>")
    //        $("#Content2").val($($("div.nicEdit-main")[1]).html());
    //});

    // Post data on save button click
    $("#btnSave").click(function (event) {

        //stop submit the form, we will post it manually.
        //event.preventDefault();

        // Validation is ok or not
        if (validateNews() == false) {
            return false;
        }

        CheckImageUpdated();

        $("#Content1").val($($("div.nicEdit-main")[0]).html());
        $("#Content2").val($($("div.nicEdit-main")[1]).html());

        // Get form
        var form = $('#formNews')[0];


        // Create an FormData object 
        var data = new FormData(form);

        // If you want to add an extra field for the FormData
        // data.append("CustomField", "This is some extra data, testing");

        // disabled the submit button
        $("#btnSave").prop("disabled", true);

        $.ajax({
            type: "POST",
            enctype: 'multipart/form-data',
            url: "/News/Update",
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

    // Validate News form
    var validateNews = function () {
        var isValid = true;
        var isTab1Valid = true;
        var isTab2Valid = true;
        var isTab3Valid = true;

        // check if the form input is valid
        if (!$("#formNews").valid()) {
            $("#formNews").submit();
            //isTab1Valid = false;
            isValid = false;
        }

        if ($('#Title').val() == '') {
            isTab1Valid = false;
            isValid = false;
        }

        if ($('#Title').val().trim().length > 80) {
            isTab1Valid = false;
            isValid = false;
            isValid = false;
        }

        if ($('#TeaserText').val().trim() == '') {
            isTab1Valid = false;
            isValid = false;
        }

        if ($('#TeaserText').val().trim().length > 300) {
            isTab1Valid = false;
            isValid = false;
        }

        // manual validation of nice editor for Content1
        if ($($("div.nicEdit-main")[0]).text().length == 0) {
            $("span[data-valmsg-for='Content1']").text("Please enter the news content.");
            isValid = false;
            isTab1Valid = false;
        }
        else {
            $("span[data-valmsg-for='Content1']").text("");
        }


        //// manual validation of nice editor for Content2
        //if ($($("div.nicEdit-main")[1]).text().length == 0) {
        //    $("span[data-valmsg-for='Content2']").text("Please enter the content 2");
        //    isValid = false;
        //    isTab1Valid = false;
        //}
        //else {
        //    $("span[data-valmsg-for='Content2']").text("");
        //}

        //// Check Thumbnail image is browsed or not
        //if ($('#img1').attr('src') == '') {
        //    isTab2Valid = false;
        //    isValid = false;
        //}

        //// Check main image is browsed or not
        //if ($('#img2').attr('src') == '') {
        //    isTab2Valid = false;
        //    isValid = false;
        //}

        //// Check Additional image 1 is browsed or not
        //if ($('#img3').attr('src') == '') {
        //    isTab2Valid = false;
        //    isValid = false;
        //}

        //// Check Additional image 2 is browsed or not
        //if ($('#img4').attr('src') == '') {
        //    isTab2Valid = false;
        //    isValid = false;
        //}

        // Check published date is entered or not
        if ($("#PublishDateDisplay").val() == '') {
            isTab3Valid = false;
            isValid = false;
        }

        // check author name is entered or not
        if ($("#AuthorName").val() == '') {
            isTab3Valid = false;
            isValid = false;
        }

        //// Click Tab1 if it has incomplete inputs
        if (!isTab1Valid) {
            //$("#tab_1").click();
            $('#tabUL a[href="#tab_1"]').trigger('click');
        }

        // Click Tab2 if it has incomplete inputs
        else if (!isTab2Valid) {
            // $("#tab_2").click();
            $('#tabUL a[href="#tab_2"]').trigger('click');
        }

        // Click Tab3 if it has incomplete inputs
        else if (!isTab3Valid) {
            // $("#tab_3").click();
            $('#tabUL a[href="#tab_3"]').trigger('click');
        }

        return isValid;
    }


    //$("input:file[class=upload]").change(function (e) {
    //    var FileUpload = $(this).attr('id');
    //    if (this.files && this.files[0]) {
    //        if (CheckFileTypeExtensionForNews(this.files[0].name)) {
    //            var reader = new FileReader();
    //            reader.onload = function (e) {

    //                $("#" + FileUpload).next("img").attr('src', e.target.result);

    //            }
    //            reader.readAsDataURL(this.files[0]);
    //        }
    //    }

    //});

    //<![CDATA[
    bkLib.onDomLoaded(function () {
        new nicEditor({ fullPanel: true, maxHeight: 200 }).panelInstance('Content1');
        new nicEditor({ fullPanel: true, maxHeight: 200 }).panelInstance('Content2');
    });
    //]]>

    //Select Current Menu
    $('.nav-li').removeClass('current');
    $('#menuItemNews').addClass('current');
});

var onBegin = function () {
    $(".loaderModal").show();
};



var onComplete = function () {
    //alert("onComplete");
};

var onSuccess = function (context) {
    $(".loaderModal").hide();

    swal(
        'The news item has been successfully updated.',
        '',
        'success'
    ).then(function () {
        var departmentId = parseInt($('#hdnSelectedDepartmentId').val());
        //var departmentId = parseInt($('#hdnDepartmentId').val());
        window.location.href = "Index?DepartmentId=" + departmentId;
        //window.location.href = "/News/Index";

    });
};

var onFailed = function (context) {
    //alert("Failed");
};

function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

function readIMG1(input) {
    if (input.files && input.files[0]) {
        var checkValid = CheckFileTypeExtensionForNews(input.files[0].name);
        if (checkValid == true) {
            var reader = new FileReader();

            reader.onload = copyImage;

            function copyImage(ev) {
                $('#img1').attr('src', ev.target.result);
                $("#img1").show();
                $("#remove_EditfileUploaderImg1_img1").show();
            };

            reader.readAsDataURL(input.files[0]);
        }
        else {
            $("#EditfileUploaderImg1").val('');
        }
    }
}
function readIMG2(input) {
    if (input.files && input.files[0]) {
        var checkValid = CheckFileTypeExtensionForNews(input.files[0].name);
        if (checkValid == true) {
            var reader = new FileReader();

            reader.onload = copyImage;

            function copyImage(ev) {
                $('#img2').attr('src', ev.target.result);
                $("#img2").show();
                $("#remove_EditfileUploaderImg2_img2").show();
            };

            reader.readAsDataURL(input.files[0]);
        }
        else {
            $("#EditfileUploaderImg2").val('');
        }
    }
}
function readIMG3(input) {
    if (input.files && input.files[0]) {
        var checkValid = CheckFileTypeExtensionForNews(input.files[0].name);
        if (checkValid == true) {
            var reader = new FileReader();

            reader.onload = copyImage;

            function copyImage(ev) {
                $('#img3').attr('src', ev.target.result);
                $("#img3").show();
                $("#remove_EditfileUploaderImg3_img3").show();
            };

            reader.readAsDataURL(input.files[0]);
        }
        else {
            $("#EditfileUploaderImg3").val('');
        }
    }
}
function readIMG4(input) {
    if (input.files && input.files[0]) {
        var checkValid = CheckFileTypeExtensionForNews(input.files[0].name);
        if (checkValid == true) {
            var reader = new FileReader();

            reader.onload = copyImage;

            function copyImage(ev) {
                $('#img4').attr('src', ev.target.result);
                $("#img4").show();
                $("#remove_EditfileUploaderImg4_img4").show();
            };

            reader.readAsDataURL(input.files[0]);
        }
        else {
            $("#EditfileUploaderImg4").val('');
        }
    }
}

function CheckFileTypeExtensionForNews(fileName) {
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
            title: "The image file types allowed .jpg, .jpeg, .bmp, .gif, .png.",
            text: '',
            width: '500px'
        })
        return false;
    }
    else { return true; }
}

function CheckImageUpdated() {
    debugger;
    if ($('#EditfileUploaderImg1').val() == "" && $('#img1').attr('src') == "") {
        $('#ThumbnailImage').val('');
    }
    if ($('#EditfileUploaderImg2').val() == "" && $('#img2').attr('src') == "") {
        $('#MainImage').val('');
    }
    if ($('#EditfileUploaderImg3').val() == "" && $('#img3').attr('src') == "") {
        $('#AdditionalImage1').val('');
    }
    if ($('#EditfileUploaderImg4').val() == "" && $('#img4').attr('src') == "") {
        $('#AdditionalImage2').val('');
    }
}

function PutSampleContent(ctrl) {
    var value = $(ctrl.elm.parentElement.parentElement.parentElement).find("label").attr('for');

    //NewsContent1
    if (value == "NewsContent1") {
        $("#divValueNewsContent1 div.nicEdit-main").html();
        $("#divValueNewsContent1 div.nicEdit-main").html($('#hdnDivValueNewsContent1').html());
    }
    //NewsContent2
    else if (value == "NewsContent2") {
        $("#divValueNewsContent2 div.nicEdit-main").html();
        $("#divValueNewsContent2 div.nicEdit-main").html($('#hdnDivValueNewsContent2').html());
    }
}