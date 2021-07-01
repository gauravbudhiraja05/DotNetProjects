$(document).ready(function () {

    // Select publish date as today
    var datepicker = $('.datepicker-here').datepicker().data('datepicker');
    datepicker.selectDate(new Date());

    $("#img1").hide();
    $("#img2").hide();
    $("#img3").hide();
    $("#img4").hide();

    $("#fileUploaderImg1").change(function () {
        readIMG1(this);
    });

    $("#fileUploaderImg2").change(function () {
        readIMG2(this);
    });

    $("#fileUploaderImg3").change(function () {
        readIMG3(this);
    });

    $("#fileUploaderImg4").change(function () {
        readIMG4(this);
    });

    // Select creation date as today
    $('#datetimepicker1').val(getFormattedDate());

    var currentTab = location.hash
    if (currentTab) {
        $(".tab_list li").removeClass("current");
        //$(".tab_list li>a[href=" + currentTab + "]").closest("li").addClass("current");
        $('.tab_list li a[href="' + currentTab + '"]').closest("li").addClass("current");
        $(".tab_content").hide();
        $(currentTab).show();
        $(currentTab + "_btn").show()
    }


    // Nice Editor
    //<![CDATA[
    bkLib.onDomLoaded(function () {
        new nicEditor({ fullPanel: true, maxHeight: 200 }).panelInstance('Content1');
    });
    //]]>

    // Nice Editor
    //<![CDATA[
    bkLib.onDomLoaded(function () {
        new nicEditor({ fullPanel: true, maxHeight: 200 }).panelInstance('Content2');
    });
    //]]>

    // Put Rich Text editor html content in Disabled TextArea Content on keyup
    //$(document).on("keyup", "div.nicEdit-main", function () {
    //    if ($($("div.nicEdit-main")[0]).html() != "<br>")
    //        $("#Content1").val($($("div.nicEdit-main")[0]).html());

    //    if ($($("div.nicEdit-main")[1]).html() != "<br>")
    //        $("#Content2").val($($("div.nicEdit-main")[1]).html());
    //});


    //$("input:file[class=upload]").change(function (e) {
    //    var FileUpload = $(this).attr('id');
    //    if (this.files && this.files[0]) {
    //        if (CheckFileTypeExtension(this.files[0].name)) {
    //            var reader = new FileReader();
    //            reader.onload = function (e) {

    //                $("#" + FileUpload).next("img").css('display', 'inline-block');
    //                $("#" + FileUpload).next("img").attr('src', e.target.result);

    //            }
    //            reader.readAsDataURL(this.files[0]);
    //        }
    //    }


    //});

    //<![CDATA[
    bkLib.onDomLoaded(function () {
        new nicEditor({ fullPanel: true, maxHeight: 200 }).panelInstance('News_Content1');
        new nicEditor({ fullPanel: true, maxHeight: 200 }).panelInstance('News_Content2');
    });
    //]]>

    //Select Current Menu
    $('.nav-li').removeClass('current');
    $('#menuItemVacancies').addClass('current');

    $('.upload_remove').on('click', function () {
        var ctrlId = $(this).attr("id");
        var fileUploadedId = String(ctrlId.split('_')[1]);
        var imgId = String(ctrlId.split('_')[2]);

        $('#' + fileUploadedId).val("");
        $('#' + imgId).hide();
        $('#' + imgId).attr("src", "");
        $(this).hide();
    });

    $('#btnBack').on('click', function () {
        var foo = getParameterByName('DepartmentId')
        window.location.href = "Index?DepartmentId=" + foo;
    });



    // Post data on save button click
    $("#btnSave").click(function (event) {

        //stop submit the form, we will post it manually.
       // event.preventDefault();

        $("#Content1").val($($("div.nicEdit-main")[0]).html());
        $("#Content2").val($($("div.nicEdit-main")[1]).html());

        // Validation is ok or not
        if (validateVacancy() == false) {
            return false;
        }

        // Get form
        var form = $('#formVacancy')[0];


        // Create an FormData object 
        var data = new FormData(form);

        // If you want to add an extra field for the FormData
        // data.append("CustomField", "This is some extra data, testing");

        // disabled the submit button
        $("#btnSave").prop("disabled", true);

        $.ajax({
            type: "POST",
            enctype: 'multipart/form-data',
            url: "/Vacancies/Add",
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

    // Validate Vacancy form
    var validateVacancy = function () {
        var isValid = true;
        var isTab1Valid = true;
        var isTab2Valid = true;
        var isTab3Valid = true;
        var flag = 0;

        // check if the form input is valid
        if (!$("#formVacancy").valid()) {
            $("#formVacancy").submit();
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
            $("span[data-valmsg-for='Content1']").text("Please enter the vacancy content.");
            //$("span[data-valmsg-for='Content2']").text("Please enter the content");
            isValid = false;
            isTab1Valid = false;
        }
        else {
            $("span[data-valmsg-for='Content1']").text("");
            //$("span[data-valmsg-for='Content2']").text("");
        }


        //// manual validation of nice editor for Content2
        //if ($($("div.nicEdit-main")[1]).text().length == 0) {
        //    //$("span[data-valmsg-for='Content1']").text("Please enter the content");
        //    $("span[data-valmsg-for='Content2']").text("Please enter the content 2");
        //    isValid = false;
        //    isTab1Valid = false;
        //}
        //else {
        //    // $("span[data-valmsg-for='Content1']").text("");
        //    $("span[data-valmsg-for='Content2']").text("");
        //}

        // Check Thumbnail image is browsed or not
        //if ($("#fileUploaderImg1").val() == '') {
        //    isTab2Valid = false;
        //    isValid = false;
        //}

        //// Check main image is browsed or not
        //if ($("#fileUploaderImg2").val() == '') {
        //    isTab2Valid = false;
        //    isValid = false;
        //}

        // Check Additional image 1 is browsed or not
        //if ($("#AdditionalImg1").val() == '') {
        //    isTab2Valid = false;
        //    isValid = false;
        //}

        //// Check Additional image 2 is browsed or not
        //if ($("#AdditionalImg2").val() == '') {
        //    isTab2Valid = false;
        //    isValid = false;
        //}

        // Check publish date is entered or not
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
            //$("#formNews").submit();
        }

        // Click Tab2 if it has incomplete inputs
        else if (!isTab2Valid) {
            // $("#tab_2").click();
            $('#tabUL a[href="#tab_2"]').trigger('click');
            //$("#formNews").submit();
        }

        // Click Tab3 if it has incomplete inputs
        else if (!isTab3Valid) {
            // $("#tab_3").click();
            $('#tabUL a[href="#tab_3"]').trigger('click');
            //$("#formNews").submit();
        }


        return isValid;


    }


});

var onBegin = function () {
    $(".loaderModal").show();
};


var onSuccess = function (context) {
    $(".loaderModal").hide();
    swal(
        'The vacancy has been created successfully.',
        '',
        'success'
    ).then(function () {
        var foo = getParameterByName('DepartmentId')
        window.location.href = "Index?DepartmentId=" + foo;

    });
};

var onFailed = function (context) {
    console.log(context);
    swal(context);
};

function readIMG1(input) {
    if (input.files && input.files[0]) {
        var checkValid = CheckFileTypeExtensionForVacancy(input.files[0].name);
        if (checkValid == true) {
            var reader = new FileReader();

            reader.onload = copyImage;

            function copyImage(ev) {
                $('#img1').attr('src', ev.target.result);
                $("#img1").show();
                $("#remove_fileUploaderImg1_img1").show();
            };

            reader.readAsDataURL(input.files[0]);
        }
        else {
            $("#fileUploaderImg1").val('');
        }
    }
}
function readIMG2(input) {
    if (input.files && input.files[0]) {
        var checkValid = CheckFileTypeExtensionForVacancy(input.files[0].name);
        if (checkValid == true) {
            var reader = new FileReader();

            reader.onload = copyImage;

            function copyImage(ev) {
                $('#img2').attr('src', ev.target.result);
                $("#img2").show();
                $("#remove_fileUploaderImg2_img2").show();
            };

            reader.readAsDataURL(input.files[0]);
        }
        else {
            $("#fileUploaderImg2").val('');
        }
    }
}
function readIMG3(input) {
    if (input.files && input.files[0]) {
        var checkValid = CheckFileTypeExtensionForVacancy(input.files[0].name);
        if (checkValid == true) {
            var reader = new FileReader();

            reader.onload = copyImage;

            function copyImage(ev) {
                $('#img3').attr('src', ev.target.result);
                $("#img3").show();
                $("#remove_fileUploaderImg3_img3").show();
            };

            reader.readAsDataURL(input.files[0]);
        }
        else {
            $("#fileUploaderImg3").val('');
        }
    }
}
function readIMG4(input) {
    if (input.files && input.files[0]) {
        var checkValid = CheckFileTypeExtensionForVacancy(input.files[0].name);
        if (checkValid == true) {
            var reader = new FileReader();

            reader.onload = copyImage;

            function copyImage(ev) {
                $('#img4').attr('src', ev.target.result);
                $("#img4").show();
                $("#remove_fileUploaderImg4_img4").show();
            };

            reader.readAsDataURL(input.files[0]);
        }
        else {
            $("#fileUploaderImg4").val('');
        }
    }
}

function CheckFileTypeExtensionForVacancy(fileName) {
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

function PutSampleContent(ctrl) {
    var value = $(ctrl.elm.parentElement.parentElement.parentElement).find("label").attr('for');

    //VacancyContent1
    if (value == "VacancyContent1") {
        $("#divValueVacancyContent1 div.nicEdit-main").html();
        $("#divValueVacancyContent1 div.nicEdit-main").html($('#hdnDivValueVacancyContent1').html());
    }
    //VacancyContent2
    else if (value == "VacancyContent2") {
        $("#divValueVacancyContent2 div.nicEdit-main").html();
        $("#divValueVacancyContent2 div.nicEdit-main").html($('#hdnDivValueVacancyContent2').html());
    }
}