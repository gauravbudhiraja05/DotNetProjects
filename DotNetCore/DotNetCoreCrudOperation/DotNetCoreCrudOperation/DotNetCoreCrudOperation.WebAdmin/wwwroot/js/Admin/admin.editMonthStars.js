$(document).ready(function () {


    $(".popup_close_btn").click(function () {

        $("#search_image_text").val('');
        $("#SearchByNameStarListForImage").html('');
        $("#Search_image_popup").fadeOut(200);
        $(".search_star_popup_overlay").fadeOut(200);
    })

    $(".OpenImagesPopUpForStar").click(function () {

        $("#search_image_text").val('');
        $("#SearchByNameStarListForImage").html('');
        var imgId = $(this).siblings("img").attr("id");
        $("#SeacrhImagePopupHiddenTabId").val(imgId);
        $("#Search_image_popup").fadeIn(200);
        $(".search_star_popup_overlay").fadeIn(200);
    });

    //$(".upload_remove").click(function () {

    //    var imgId = $(this).siblings("img").attr("id");

    //    $('#' + imgId + '').attr('src', '#');
    //    $('#' + imgId + '').hide();
    //    $(this).siblings('.hiddenFrontEndUserImageForStar').val('');
    //    $(this).siblings('input.upload').val('');
    //    $(this).siblings('input.hiddenStarPhotoName').val('');
    //})
    $('.upload_remove').on('click', function () {
        var ctrlId = $(this).attr("id");
        var fileUploadedId = String(ctrlId.split('_')[1]);
        var imgId = String(ctrlId.split('_')[2]);

        $('#' + fileUploadedId).val("");
        $('#' + imgId).hide();
        $('#' + imgId).attr("src", "");
        $(this).hide();
    });


    $("#search_image_btn").click(function () {

        var personName = $("#search_image_text").val();
        var imageBox = $("#SeacrhImagePopupHiddenTabId").val();
        personName = personName.trim();
        if (personName.length > 0) {
            SearchPersonForStarsImage(personName, imageBox);
        }


    });

    $("#SearchByNameStarListForImage").on('click', 'div.search_star_list a.search_star_link', function () {


        var imageTagVal = $("#SeacrhImagePopupHiddenTabId").val();

        var bgImage = $(this).siblings("div.search_star_img").css('background-image');
        bgImage = bgImage.replace('url(', '').replace(')', '').replace(/\"/gi, "");
        bgImage = bgImage.substring(bgImage.lastIndexOf("/") + 1, bgImage.length);

        $('#' + imageTagVal + '').attr('src', '/Uploads/Images/FrontEndUser/' + bgImage + '');

        $('#' + imageTagVal + '').siblings('.hiddenFrontEndUserImageForStar').val(bgImage);

        $("#Search_image_popup").fadeOut(200);
        $(".search_star_popup_overlay").fadeOut(200);
        $('#' + imageTagVal + '').show();
    });

    //Select Current Menu
    $('.nav-li').removeClass('current');
    $('#menuItemSuperAdmin').addClass('current');

    //$("#img1").hide();
    //$("#img2").hide();
    //$("#img3").hide();
    //$("#img4").hide();
    //$("#img5").hide();
    //$("#img6").hide();
    //$("#img7").hide();
    //$("#img8").hide();
    //$("#img9").hide();
    //$("#img10").hide();
    //$("#img11").hide();
    //$("#img12").hide();


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

    $("#fileUploaderImg5").change(function () {
        readIMG5(this);
    });

    $("#fileUploaderImg6").change(function () {
        readIMG6(this);
    });

    $("#fileUploaderImg7").change(function () {
        readIMG7(this);
    });

    $("#fileUploaderImg8").change(function () {
        readIMG8(this);
    });

    $("#fileUploaderImg9").change(function () {
        readIMG9(this);
    });

    $("#fileUploaderImg10").change(function () {
        readIMG10(this);
    });

    $("#fileUploaderImg11").change(function () {
        readIMG11(this);
    });

    $("#fileUploaderImg12").change(function () {
        readIMG12(this);
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


    //$("input:file[class=upload]").change(function (e) {
    //    var FileUpload = $(this).attr('id');
    //    if (this.files && this.files[0]) {
    //        var reader = new FileReader();
    //        reader.onload = function (e) {

    //            $("#" + FileUpload).next("img").attr('src', e.target.result);

    //        }
    //        reader.readAsDataURL(this.files[0]);
    //    }
    //});


    //// Select month year in datepicker
    //var datepicker = $('#MonthYear').datepicker().data('datepicker');
    //var year = $("#Year").val();
    //var month = $("#MonthNumber").val() - 1;
    //datepicker.selectDate(new Date(year, month, 1));

    //// Dropdown
    //$('select.selectpicker').selectpicker({
    //    caretIcon: 'glyphicon glyphicon-menu-down'
    //});


});

// Post data on save button click

$("#btnUpdate").click(function (event) {
    debugger;
    // Set month information into hidden field
    //var datepicker = $('#MonthYear').datepicker().data('datepicker');
    //$("#Year").val(datepicker.selectedDates[0].getFullYear());
    //$("#MonthNumber").val(datepicker.selectedDates[0].getMonth() + 1);

    //var months = ["January", "February", "March", "April", "May", "June",
    //    "July", "August", "September", "October", "November", "December"];
    //var selectedMonthName = months[datepicker.selectedDates[0].getMonth()];
    //$("#MonthName").val(selectedMonthName);

    //stop submit the form, we will post it manually.
    //event.preventDefault();

    // Validation is ok or not
    if (validateMonthStars() == false) {
        return false;
    }

    if ($('#NewMonthName-error').html() == "This month already exists, please enter another one.") {
        return false
    }

    // Get form
    var form = $('#formMonthStars')[0];


    // Create an FormData object 
    var data = new FormData(form);

    // If you want to add an extra field for the FormData
    // data.append("CustomField", "This is some extra data, testing");

    // disabled the submit button
    $("#btnUpdate").prop("disabled", true);

    $.ajax({
        type: "POST",
        enctype: 'multipart/form-data',
        url: "/SuperAdmin/EditMonthStars",
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
            $("#btnUpdate").prop("disabled", false);
            onSuccess(data);
        },
        error: function (e) {
            $("#btnUpdate").prop("disabled", false);
            onFailed(e);
        }
    });

});



// Validate Month Stars form
var validateMonthStars = function () {
    var isValid = true;
    var isTab1Valid = true;

    // check if the form input is valid
    if (!$("#formMonthStars").valid()) {
        $("#formMonthStars").submit();
        isValid = false;
    }


    // check month is entered or not
    if ($("#NewMonthName").val().trim() == '') {
        isTab1Valid = false;
        isValid = false;
    }

    if ($("#NewMonthName-error").text().length > 0) {
        isTab1Valid = false;
        isValid = false;
    }

    // Click Tab1 if it has incomplete inputs
    if (!isTab1Valid) {
        $("#tab1").click();
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
        'The Stars have been updated successfully.',
        '',
        'success'
    ).then(function () {
        window.location.href = "/SuperAdmin/MonthStars";

    });
};

// On failed/error of ajax 
var onFailed = function (context) {
    return false;
};

function readIMG1(input) {
    if (input.files && input.files[0]) {
        var checkValid = CheckFileTypeExtension(input.files[0].name);
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
            $(input).val('');
        }
    }
}

function readIMG2(input) {
    if (input.files && input.files[0]) {
        var checkValid = CheckFileTypeExtension(input.files[0].name);
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
            $(input).val('');
        }
    }
}

function readIMG3(input) {
    if (input.files && input.files[0]) {
        var checkValid = CheckFileTypeExtension(input.files[0].name);
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
            $(input).val('');
        }
    }
}

function readIMG4(input) {
    if (input.files && input.files[0]) {
        var checkValid = CheckFileTypeExtension(input.files[0].name);
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
            $(input).val('');
        }
    }
}

function readIMG5(input) {
    if (input.files && input.files[0]) {
        var checkValid = CheckFileTypeExtension(input.files[0].name);
        if (checkValid == true) {
            var reader = new FileReader();

            reader.onload = copyImage;

            function copyImage(ev) {
                $('#img5').attr('src', ev.target.result);
                $("#img5").show();
                $("#remove_fileUploaderImg5_img5").show();
            };

            reader.readAsDataURL(input.files[0]);
        }
        else {
            $(input).val('');
        }
    }
}

function readIMG6(input) {
    if (input.files && input.files[0]) {
        var checkValid = CheckFileTypeExtension(input.files[0].name);
        if (checkValid == true) {
            var reader = new FileReader();

            reader.onload = copyImage;

            function copyImage(ev) {
                $('#img6').attr('src', ev.target.result);
                $("#img6").show();
                $("#remove_fileUploaderImg6_img6").show();
            };

            reader.readAsDataURL(input.files[0]);
        }
        else {
            $(input).val('');
        }
    }
}

function readIMG7(input) {
    if (input.files && input.files[0]) {
        var checkValid = CheckFileTypeExtension(input.files[0].name);
        if (checkValid == true) {
            var reader = new FileReader();

            reader.onload = copyImage;

            function copyImage(ev) {
                $('#img7').attr('src', ev.target.result);
                $("#img7").show();
                $("#remove_fileUploaderImg7_img7").show();
            };

            reader.readAsDataURL(input.files[0]);
        }
        else {
            $(input).val('');
        }
    }
}

function readIMG8(input) {
    if (input.files && input.files[0]) {
        var checkValid = CheckFileTypeExtension(input.files[0].name);
        if (checkValid == true) {
            var reader = new FileReader();

            reader.onload = copyImage;

            function copyImage(ev) {
                $('#img8').attr('src', ev.target.result);
                $("#img8").show();
                $("#remove_fileUploaderImg8_img8").show();
            };

            reader.readAsDataURL(input.files[0]);
        }
        else {
            $(input).val('');
        }
    }
}

function readIMG9(input) {
    if (input.files && input.files[0]) {
        var checkValid = CheckFileTypeExtension(input.files[0].name);
        if (checkValid == true) {
            var reader = new FileReader();

            reader.onload = copyImage;

            function copyImage(ev) {
                $('#img9').attr('src', ev.target.result);
                $("#img9").show();
                $("#remove_fileUploaderImg9_img9").show();
            };

            reader.readAsDataURL(input.files[0]);
        }
        else {
            $(input).val('');
        }
    }
}

function readIMG10(input) {
    if (input.files && input.files[0]) {
        var checkValid = CheckFileTypeExtension(input.files[0].name);
        if (checkValid == true) {
            var reader = new FileReader();

            reader.onload = copyImage;

            function copyImage(ev) {
                $('#img10').attr('src', ev.target.result);
                $("#img10").show();
                $("#remove_fileUploaderImg10_img10").show();
            };

            reader.readAsDataURL(input.files[0]);
        }
        else {
            $(input).val('');
        }
    }
}

function readIMG11(input) {
    if (input.files && input.files[0]) {
        var checkValid = CheckFileTypeExtension(input.files[0].name);
        if (checkValid == true) {
            var reader = new FileReader();

            reader.onload = copyImage;

            function copyImage(ev) {
                $('#img11').attr('src', ev.target.result);
                $("#img11").show();
                $("#remove_fileUploaderImg11_img11").show();
            };

            reader.readAsDataURL(input.files[0]);
        }
        else {
            $(input).val('');
        }
    }
}

function readIMG12(input) {
    if (input.files && input.files[0]) {
        var checkValid = CheckFileTypeExtension(input.files[0].name);
        if (checkValid == true) {
            var reader = new FileReader();

            reader.onload = copyImage;

            function copyImage(ev) {
                $('#img12').attr('src', ev.target.result);
                $("#img12").show();
                $("#remove_fileUploaderImg12_img12").show();
            };

            reader.readAsDataURL(input.files[0]);
        }
        else {
            $(input).val('');
        }
    }
}

function SearchPersonWithImageOnEnter(e) {
    var key = e.keyCode || e.which;
    if (key == 13) {

        var personName = $("#search_image_text").val();
        var imageBox = $("#SeacrhImagePopupHiddenTabId").val();
        personName = personName.trim();
        if (personName.length > 0) {
            SearchPersonForStarsImage(personName, imageBox);
        }

        e.stopImmediatePropagation();

    }
}

function SearchPersonForStarsImage(personName, imageBox) {

    //alert(personName + " " + imageBox);

    $.ajax({
        type: "Post",
        url: "/SuperAdmin/GetStarsImageList",
        data: { personName: personName.trim(), JobTitle: "" },
        success: function (Data) {

            for (var i = 0; i < Data.length; i++) {

                if (Data[i].photo == "" || Data[i].photo == null) {

                    Data[i].photo = "defaultuser.png";
                }

            }



            var htmlData = "";
            if (Data != null) {
                if (Data.length > 0) {
                    $("#SearchByNameStarListForImage").html('');
                }

                for (var i = 0; i < Data.length; i++) {

                    htmlData += '<div class="search_star_list">';
                    htmlData += '<a href="#" class="search_star_link"></a>';
                    htmlData += '<div class="search_star_img" style="background-image: url(/Uploads/Images/FrontEndUser/' + Data[i].photo + ');"></div>';
                    htmlData += '<div class="search_star_text">';
                    htmlData += '<div class="search_star_name">' + Data[i].firstName + " " + Data[i].surName + '</div>';
                    htmlData += '<div class="search_star_post">' + Data[i].jobTitle + '</div>';
                    htmlData += '<div class="search_star_loc">' + Data[i].myDepartmentName + '</div>';
                    htmlData += '</div>';
                    htmlData += '</div>';

                }
                $("#SearchByNameStarListForImage").html(htmlData);
            }


        },
        error: function (response) {

        }
    });
}