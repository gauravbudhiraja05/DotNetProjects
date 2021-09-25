$(document).ready(function () {

    //Select Current Menu
    $('.nav-li').removeClass('current');
    $('#menuItemSuperAdmin').addClass('current');

    // Select creation date as today
    $('#CreationDate').val(getFormattedDate());

    $("#Save_Admin_Btn").click(function () {
        debugger;
        if (validateAddAdminMessage() == false) {
            return false;
        }
        else if (ValidateEmailAddress() == false) {
            return false;
        }
        else {
            
        }
    });

});


function ValidateEmailAddress() {
    debugger;
    if ($('#EmailAddress').val().trim() != "") {
        var testEmail = /^[A-Z0-9._%+-]+@([A-Z0-9-]+\.)+[A-Z]{2,4}$/i;
        var emailAddress = $('#EmailAddress').val().trim();
        if (testEmail.test(emailAddress)) {
            $.ajax({
                type: "POST",
                url: "/SuperAdmin/CheckEmailExists",
                data: { emailAddress: emailAddress },
                async: false,
                success: function (data) {
                    debugger;
                    if (data.message =="Email Not Exists") {
                        //return true;
                        $.ajax({
                            type: "POST",
                            url: "/SuperAdmin/AddAdminUser",
                            async: false,
                            beforeSend: function (xhr) {
                                onBegin(xhr);
                            },
                            data: { FullName: $('#FullName').val(), EmailAddress: $('#EmailAddress').val(), Password: $('#Password').val() },
                            success: function (data) {
                                onSuccess(data);
                            },
                            error: function (e) {
                                onFailed(e);
                            }
                        });
                    }
                    else {
                        $("span[id='EmailAddress_Error']").text("Email address already exists. Please try with different one");
                        return false;
                    }
                },
                error: function (e) {
                    onFailed(e);
                    return false;
                }
            });
        }
        else {
            $("span[id='EmailAddress_Error']").text("Please enter the valid email address.");
            return false;
        }
    }
}

var onBegin = function () {
    $(".loaderModal").show();
};


var onComplete = function () {
    //alert("onComplete");
};

var onSuccess = function (context) {
    $(".loaderModal").hide();
    swal(
        'The admin user was successfully created.',
        '',
        'success'
    ).then(function () {
        window.location.href = "/SuperAdmin/AdminUsers";
    });
};

var onFailed = function (context) {
    //alert("Failed");
};



// Validate Featured Message form
var validateAddAdminMessage = function () {
    var isValid = true;

    // check if the form input is valid
    if (!$("#formAdmin").valid()) {
        $("#formAdmin").submit();
        isValid = false;
    }


    if ($("#FullName").val().trim().length == 0) {
        $("span[id='FullName_Error']").text("Please enter the name.");
        isValid = false;

    }
    else {
        $("span[id='FullName_Error']").text("");
    }


    if ($("#EmailAddress").val().trim().length == 0) {
        $("span[id='EmailAddress_Error']").text("Please enter the email address.");
        isValid = false;
    }
    else {
        $("span[id='EmailAddress_Error']").text("");
    }


    if ($("#Password").val().trim().length == 0) {
        $("span[id='Password_Error']").text("Please enter the password.");
        isValid = false;

    }
    else {
        $("span[id='Password_Error']").text("");
    }



    return isValid;


}

