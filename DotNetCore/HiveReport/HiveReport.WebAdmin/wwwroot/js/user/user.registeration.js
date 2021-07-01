$(document).ready(function () {

    $("#txtMobile").keypress(function (e) {
        //if the letter is not digit then display error and don't type anything
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            //display error message
            return false;
        }
    });

    $('#txtEmail').bind("change", function () {

        $.ajax({
            type: "POST",
            url: '/User/IsEmailExists',
            data: { emailAddress: $('#txtEmail').val() },
            success: function (data) {

            },
            error: function (response) {
                alert(response.responseText);
            },
            failure: function (response) {
                alert(response.responseText);
            }
        });
    });

    $("#btnSubmit").bind("click", function () {

        if (!ValidateRegisterationForm()) {
            return false;
        }
        else {

            var registeredUser = {}

            if (
                ($('#hdnProductType').val() == "Single User" && $('#hdnDatabaseType').val() == "Single Database") ||
                ($('#hdnProductType').val() == "Single User" && $('#hdnDatabaseType').val() == "Multiple Database")
            ) {
                registeredUser.UserType = 1;
            }
            else {
                registeredUser.UserType = 3;
            }
            registeredUser.Name = $('#txtName').val();
            registeredUser.CompanyName = $('#txtCompany').val();
            registeredUser.EmailAddress = $('#txtEmail').val();
            registeredUser.MobileNumber = $('#txtMobile').val();
            registeredUser.ProductType = $('#hdnProductType').val();
            registeredUser.DatabaseType = $('#hdnDatabaseType').val();
            registeredUser.ProductCode = $('#hdnProductCode').val();
            registeredUser.Database = $('#hdnDatabase').val();
            registeredUser.CreatorId = 3;
            registeredUser.Scope = "NonLocal";
            registeredUser.DepartmentId = 0;
            registeredUser.ClientId = 0;
            registeredUser.LOBId = 0;
            registeredUser.Password = $('#txtEmail').val().substring(0,3) + "@1234";

            $.ajax({
                type: "POST",
                url: '/User/Registration',
                data: { registeredUser: registeredUser },
                success: function (data) {
                    if (data.isSuccess == true && data.message == "Record Saved Successfully") {
                        $('#mailMsg').html("Your Userid and Password has send to your given E-mail Id");
                        $('#txtName').val("");
                        $('#txtCompany').val("");
                        $('#txtEmail').val("");
                        $('#txtMobile').val("");
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
    });
});

function ValidateRegisterationForm() {

    $('#txtNameError').html("");
    $('#txtCompanyError').html("");
    $('#txtEmailError').html("");
    $('#txtMobileError').html("");

    var validate = true;

    if ($('#txtName').val() == "" || $('#txtName').val() == null || $('#txtName').val() == undefined) {
        $('#txtNameError').html("Please enter the name.");
        validate = false;
    }

    if ($('#txtCompany').val() == "" || $('#txtCompany').val() == null || $('#txtCompany').val() == undefined) {
        $('#txtCompanyError').html("Please enter the company name.");
        validate = false;
    }

    if ($('#txtEmail').val() == "" || $('#txtEmail').val() == null || $('#txtEmail').val() == undefined) {
        $('#txtEmailError').html("Please enter the email address.");
        validate = false;
    }

    if ($('#txtMobile').val() == "" || $('#txtMobile').val() == null || $('#txtMobile').val() == undefined) {
        $('#txtMobileError').html("Please enter the mobile number.");
        validate = false;
    }

    if ($('#txtEmail').val() != null && !(/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/.test($('#txtEmail').val()))) {
        $('#txtEmailError').html("Please enter the valid email address.");
        validate = false;
    }

    if ($('#txtMobile').val() != null && $('#txtMobile').val().trim().length != 10) {
        $('#txtEmailError').html("Please enter the valid mobile number.");
        validate = false;
    }

    return validate;
}



