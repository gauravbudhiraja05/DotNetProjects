$(document).ready(function () {

    var env = "Development";
    //  var env = "Production";

    // Get the windows login user id
    var windowsUserId = $("#hdnWidnowsUserId").val();
    console.log(windowsUserId);
    debugger;
   

    if (env == "Development") {

        //windowsUserId = 'dsacker';
        windowsUserId = 'qwerty';
        debugger;
        AuthenticateWindowsUser(windowsUserId);
    }

    else {

        if (windowsUserId != '') {
            var windowsUserSplitArray = windowsUserId.split("\\");
            console.log('UserId :'+windowsUserId);
            if (windowsUserSplitArray.length != 2) {
                
                swal("Sorry, Not able to get Windows User Id, contact to system administrator!!!");
                return;
            }

            if (windowsUserSplitArray.length == 2) {
                windowsUserId = windowsUserSplitArray[1];
                AuthenticateWindowsUser(windowsUserId);
            }
        }

        else {
            console.log('windowsUserId is empty');
            // swal("Sorry, Not able to get Windows User Id, contact to system administrator!!!");
            window.location.reload();
        }

    }
    //input:file[class=upload_btn]
    $("#upload1").change(function (e) {
        e.stopImmediatePropagation();
        var FileUpload = $(this).attr('id');
        if (this.files && this.files[0]) {
            var fileNameUser = this.files[0].name;
            if (CheckFileTypeExtensionFrontEnd(this.files[0].name)) {
                var reader = new FileReader();
                reader.onload = function (e) {

                    //$("#" + FileUpload).next("img").attr('src', e.target.result);
                    $("#" + FileUpload).next("div#bg_Upload_img1").css('background-image', 'url("' + reader.result + '")');
                    //$("span[data-valmsg-for='FrontEndUserDetails.UploadImage']").text("");
                }
                reader.readAsDataURL(this.files[0]);
                $("span[data-valmsg-for='User.UploadImage']").text("");
            }
            else {
                $(this).val('');
                $('#bg_Upload_img1').css('background-image', '');
                $("span[data-valmsg-for='User.UploadImage']").text("Please upload an image.");
                
            }
        }

    });

    $("#fELoginBtn").click(function () {

        debugger;

        var emailId = $("#loginEmail").val();

        if (emailId.trim().length == 0) {
            $("#loginEmailError").text("This field is required.");
            return false;
        }
        else if (!validateEmail(emailId)) {

            $("#loginEmailError").text("Please enter a valid email address");
            return false;
        }
        else
        {
            $("#loginEmailError").text("");
        }

        //var data = new FormData();

        $.ajax({
            type: "Post",
            url: "/FrontEndHome/LoginEndUser",
            data: { emailId: emailId },
            success: function (data) {

                if (data == "success") {

                    window.location.href = '/index';
                }
                else if (data == "emailNotPresent") {
                    $('#registrationPopup').show();
                    $('body').css('overflow', 'hidden');
                    //$('.search_star_popup_overlay').show();
                    $('#User_EmailAddress').val(emailId);
                }
                else if (data == "emailEmptyOrNull") {

                }
                else if (data == "errorPage") {

                }

            },
            error: function (e) {

            }
        });

    })

    $('#registrationPopup .profile_popup_close').click(function () {
        $('#registrationPopup').hide();
        $('body').css('overflow', 'inherit');
        //$('.search_star_popup_overlay').hide();
    });


    $('#btnEditProfile').on('click', function () {

        debugger;

        // Validation is ok or not
        if (ValidateRegisterForm() == false) {
            return false;
        }

        // Create an FormData object 
        var data = new FormData();

        // If you want to add an extra field for the FormData
        data.append('file', $('#upload1')[0].files[0]);
        data.append('Id', '');
        data.append('FirstName', $('#User_FirstName').val());
        data.append('SurName', $('#User_SurName').val());
        data.append('JobTitle', $('#User_JobTitle').val());
        data.append('Location', $('#User_Location').val());
        data.append('MyDepartmentName', $("#ddlEndUser_Department option:selected").text());
        data.append('EmailAddress', $('#User_EmailAddress').val());
        data.append('Password', $('#User_Password').val());
        data.append('TelephoneNumber', $('#User_TelephoneNumber').val());
        data.append('Mobile', $('#User_Mobile').val());
        data.append('Photo', $('#User_Photo').val());
        data.append('MyDepartmentId', $('#ddlEndUser_Department').val());
        data.append('WindowsUserId', $('#User_WindowsUserId').val());
        data.append('EmployeeId', $('#hdnEmployeeId').val());


        // disabled the submit button
        $("#btnEditProfile").prop("disabled", true);

        $.ajax({
            type: "POST",
            enctype: 'multipart/form-data',
            url: "/FrontEndHome/CreateEnUserProfile",
            data: data,
            processData: false,
            contentType: false,
            cache: false,
            timeout: 600000,
            success: function (data) {

                if (data.isSuccess == false && data.isEmailValid == false) {
                    $("#lblError_EmailAddress").html("Email address already exist.");
                    $("#btnEditProfile").prop("disabled", false);
                }
                else
                {
                    //window.location.href = '/index';
                    location.reload();
                }
                
            },
            error: function (e) {
                
            }
        });
    });

});


function AuthenticateWindowsUser(userId) {

    $.ajax({
        type: "Post",
        url: "/FrontEndHome/LoginEndUser",
        data: { windowsUserId: userId },
        success: function (data) {

            if (data == "success") {

                window.location.href = '/index';
            }
            else  {
                $('#registrationPopup').show();
                $('body').css('overflow', 'hidden');
                //$('.search_star_popup_overlay').show();
                $('#User_WindowsUserId').val(userId);
                //$('#User_TelephoneNumber').val(data.TelephoneNo);
                $('#hdnEmployeeId').val(data.EmployeeId);
            }
            //else if (data == "emailEmptyOrNull") {

            //}
            //else if (data == "errorPage") {

            //}

        },
        error: function (e) {

        }
    });
}

function validateEmail($email) {
    var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
    return emailReg.test($email);
}


function ValidateRegisterForm() {

    var isValid = true;

    // Check Profile Image is browsed or not
    if ($("#upload1").val().trim() == '') {
        isValid = false;
        $("span[data-valmsg-for='User.UploadImage']").text("Please upload an image.");
    }

    else {
        $("span[data-valmsg-for='User.UploadImage']").text("");
    }
    

    if ($('#User_FirstName').val().trim().length == 0) {
        $("span[data-valmsg-for='User.FirstName']").text("This field is required.");
        isValid = false;
    }
    else {
        //$("span[data-valmsg-for='FrontEndUserDetails.FirstName']").text("");
        if ($('#User_FirstName').val().trim().length > 40) {
            $("span[data-valmsg-for='User.FirstName']").text("The first name should not be greater than 40 characters..");
            isValid = false;
        }
        else {
            $("span[data-valmsg-for='User.FirstName']").text("");
        }
    }




    if ($('#User_SurName').val().trim().length == 0) {
        $("span[data-valmsg-for='User.SurName']").text("This field is required.");
        isValid = false;
    }
    else {
        // $("span[data-valmsg-for='FrontEndUserDetails.SurName']").text("");
        if ($('#User_SurName').val().trim().length > 40) {
            $("span[data-valmsg-for='User.SurName']").text("The sur name should not be greater than 40 characters.");
            isValid = false;
        }
        else {
            $("span[data-valmsg-for='User.SurName']").text("");
        }

    }



    if ($('#User_JobTitle').val().trim().length == 0) {
        $("span[data-valmsg-for='User.JobTitle']").text("This field is required.");
        isValid = false;
    }
    else {
        // $("span[data-valmsg-for='FrontEndUserDetails.JobTitle']").text("");
        if ($('#User_JobTitle').val().trim().length > 200) {
            $("span[data-valmsg-for='User.JobTitle']").text("The job title should not be greater than 200 characters.");
            isValid = false;
        }
        else {
            $("span[data-valmsg-for='User.JobTitle']").text("");
        }
    }


    if ($('#ddlEndUser_Department').val() == "0") {
        $("span[data-valmsg-for='User.MyDepartmentName']").text("This field is required.");
        isValid = false;
    }
    else {
        $("span[data-valmsg-for='User.MyDepartmentName']").text("");
    }


    if ($('#User_Location').val().trim().length == 0) {
        $("span[data-valmsg-for='User.Location']").text("This field is required.");
        isValid = false;
    }
    else {
        //$("span[data-valmsg-for='FrontEndUserDetails.Location']").text("");
        if ($('#User_Location').val().trim().length > 200) {
            $("span[data-valmsg-for='User.Location']").text("The location should not be greater than 200 characters.");
            isValid = false;
        }
        else {
            $("span[data-valmsg-for='User.Location']").text("");
        }
    }


    if ($('#User_EmailAddress').val().trim().length == 0) {
        $("span[data-valmsg-for='User.EmailAddress']").text("This field is required.");
        isValid = false;
    }
    else {
        //$("span[data-valmsg-for='FrontEndUserDetails.EmailAddress']").text("");
        if ($('#User_EmailAddress').val().trim().length > 300) {
            $("span[data-valmsg-for='User.EmailAddress']").text("The email address should not be greater than 200 characters.");
            isValid = false;
        }
        else {
            $("span[data-valmsg-for='User.EmailAddress']").text("");
        }
    }


    if ($('#User_TelephoneNumber').val().trim().length == 0) {
        $("span[data-valmsg-for='User.TelephoneNumber']").text("This field is required.");
        isValid = false;
    }
    else {
        $("span[data-valmsg-for='User.TelephoneNumber']").text("");
    }

   

    return isValid;

}

function CheckFileTypeExtensionFrontEnd(fileName) {
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
        swal("Sorry, " + fileName + " is invalid, allowed extensions are: " + _validFileExtensions.join(", "));
        return false;
    }
    else {
        return true;
    }
}