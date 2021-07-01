$(document).ready(function () {

    BindDropDownValues();

    $("#txtEmpId").keypress(function (e) {
        //if the letter is not digit then display error and don't type anything
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            //display error message
            $("#lblEmpId").html("Enter Numerics Only!").show().fadeOut("slow");
            return false;
        }
    });

    $('#ddlDepartmentName').bind("change", function () {

        if (this.val() == "0") {
            $('#ddlClientName').empty();
            $('#ddlLOBName').empty();
        }
        else {
            BindClient(this.val());
        }
    });


    $('#ddlClientName').bind("change", function () {

        if (this.val() == "0") {
            $('#ddlLOBName').empty();
        }
        else {
            BindLOB($('#ddlDepartmentName').val(), this.val());
        }
    });

    $("#btnAvailableEmployeeId").bind("click", function () {

        if ($('#txtEmpId').val().trim() == undefined || $('#txtEmpId').val().trim() == "") {
            $('#lblEmailError').html("Please enter the user id.")
            return;
        }
        else {

            $.ajax({
                type: "GET",
                url: '/Account/CheckAvailableEmployeeId/',
                data: '{employeeId: ' + $('#txtEmpId').val() + '}',
                success: function (isAvailable) {
                    if (isAvailable == true) {
                        $('#lblEmailError').html("Employee Id does not exist,You may Proceed!!!");
                    }
                    else {
                        $('#lblEmailError').html("Employee Id already exists,Please select another!!!");
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


    $("#btnAvailableUserId").bind("click", function () {

        if ($('#txtUserId').val().trim() == undefined || $('#txtUserId').val().trim() == "") {
            $('#lblUserIdError').html("Please enter the user id.")
            return;
        }
        else {

            $.ajax({
                type: "GET",
                url: '/Account/CheckAvailableUserId/',
                data: '{emailAddress: ' + $('#txtUserId').val() + '}',
                success: function (isAvailable) {
                    if (isAvailable == true) {
                        $('#lblEmailError').html("Emailid does not exist,You may Proceed!!!");
                    }
                    else {
                        $('#lblEmailError').html("Emailid already exists,Please select another!!!");
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

    $("#btnSave").bind("click", function () {

        if (!ValidateRegistrationDetails()) {
            return false;
        }
        else {

            var registeredUser = {};

            registeredUser.Prefix = $('#ddlPrefix').val();
            registeredUser.Name = $('#txtName').val();
            registeredUser.EmployeeId = $('#txtEmpId').val();

            if ($('#txtDesignation').val() != "" && $('#txtDesignation').val() != undefined) {
                registeredUser.Designation = $('#txtDesignation').val();
                registeredUser.IsNewDesignation = true;
            }
            else {
                registeredUser.Designation = $('#ddlDesignation').val();
                registeredUser.IsNewDesignation = false;
            }
            registeredUser.DepartmentId = $('#ddlDepartmentName').val();
            registeredUser.ClientId = $('#ddlClientName').val();
            registeredUser.LOBId = $('#ddlLOBName').val();
            registeredUser.EmailAddress = $('#txtEmail').val();
            if ($('#chkSelectscope').attr('checked')) {
                registeredUser.Scope = 'Local';
            }
            else {
                registeredUser.Scope = 'Non Local';
            }
            registeredUser.UserId = $('#txtUserId').val();
            registeredUser.Password = $('#txtPwd').val();

            if ($('#ddlDepartmentName').val() != 0)
                registeredUser.DepartmentName = $('#ddlDepartmentName option:selected').text();

            if ($('#ddlClientName').val() != 0)
                registeredUser.ClientName = $('#ddlClientName option:selected').text();

            if ($('#ddlLOBName').val() != 0)
                registeredUser.LOBName = $('#ddlLOBName option:selected').text();


            $.ajax({
                type: "POST",
                url: '/Account/Registration/',
                data: { registeredUser: registeredUser },
                success: function (message) {
                    alert(message);

                    $('#txtName').val("");
                    $('#txtEmpId').val("");
                    $('#txtDesignation').val("");
                    $('#txtEmail').val("");
                    $('#txtUserId').val("");
                    $('#txtPwd').val("");
                    $('#txtConfirmPwd').val("");

                    $('#ddlDesignation').prop('selectedIndex', 0);
                    $('#ddlDepartmentName').prop('selectedIndex', 0);
                    $('#ddlClientName').empty();
                    $('#ddlLOBName').empty();
                    $('#ddlBU').prop('selectedIndex', 0);
                    $("#chkSelectscope").prop('checked', false);
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

function BindDropDownValues() {

    BindDesignation();
    BindDepartment();
}

function ValidateRegistrationDetails() {

    var valid = true;

    if ($('#txtName').val().trim() == undefined || $('#txtName').val().trim() == "") {
        $('#lblNameError').html("Please enter the name.")
        valid = false;
    }

    if ($('#txtName').val().trim() != undefined && $('#txtName').val().trim() != "") {
        var nameformat = /([a-zA-Z][a-zA-Z.][a-zA-Z.])[a-z A-Z.]*/;
        if (!nameformat.test($('#txtName').val().trim())) {
            $('#lblNameError').html("Enter Alphabet,Dot Only(Min 3 character)");
            valid = false;
        }
    }

    if ($('#txtDesignation').val().trim() == "" && $('#ddlDesignation').val().trim() == "None") {
        $('#lblDesignationError').html("Please select the designation or enter the designation.")
        valid = false;
    }

    if ($('#txtDesignation').val().trim() != "" && $('#ddlDesignation').val().trim() == "None") {
        var designationformat = /([a-z A-Z][a-z A-Z _][a-z A-Z _])[a-z A-Z _]*/;
        if (!designationformat.test($('#txtDesignation').val().trim())) {
            $('#lblDesignationError').html("Enter Alphabets,Underscore and Space Only(Min 3 character)");
            valid = false;
        }
    }

    if ($('#txtDesignation').val().trim() != "" && $("#ddlDesignation option[value='" + $('#txtDesignation').val() + "']").length > 0) {
        $('#lblDesignationError').html("Designation Already Exists. Please enter new one");
        valid = false;
    }

    //if ($('#ddlDepartmentName').val().trim() == "0") {
    //    $('#lblDepartmentNameError').html("Please select department");
    //    valid = false;
    //}

    //if ($('#ddlClientName').val().trim() == "0") {
    //    $('#lblClientNameError').html("Please select client");
    //    valid = false;
    //}

    //if ($('#ddlLOBName').val().trim() == "0") {
    //    $('#lblLOBNameError').html("Please select lob");
    //    valid = false;
    //}

    if ($('#txtEmail').val().trim() == undefined || $('#txtEmail').val().trim() == "") {
        $('#lblEmailError').html("Please enter the email.")
        valid = false;
    }

    if ($('#txtEmail').val().trim() != undefined && $('#txtEmail').val().trim() != "") {
        var emailformat = /^[\w-\.]+@@([\w-]+\.)+[\w-]{2,4}$/;
        if (!emailformat.test($('#txtEmail').val().trim())) {
            $('#lblEmailError').html("Fill Valid Email Id");
            valid = false;
        }
    }

    if ($('#txtUserId').val().trim() == undefined || $('#txtUserId').val().trim() == "") {
        $('#lblEmailError').html("Please enter the user id.")
        valid = false;
    }


    if ($('#txtUserId').val().trim() != undefined && $('#txtUserId').val().trim() != "") {
        var userIdformat = /^[\w-\.]+@@([\w-]+\.)+[\w-]{2,4}$/;
        if (!userIdformat.test($('#txtUserId').val().trim())) {
            $('#lblUserIdError').html("Fill Valid Email Id");
            valid = false;
        }
    }

    if ($('#txtPwd').val().trim() == undefined || $('#txtPwd').val().trim() == "") {
        $('#lblPwdError').html("Please enter the password.")
        valid = false;
    }


    if ($('#txtPwd').val().trim() != undefined && $('#txtPwd').val().trim() != "") {
        var passwordformat = /^.*(?=.{8,})(?=.*\d)(?=.*[a-zA-Z])(?=.*[0-9])(?=.*[!*,@@#$%\(\)\{\}\[\]\\\/|^&+=:;]).*$/;
        if (!passwordformat.test($('#txtPwd').val().trim())) {
            $('#lblPwdError').html("Enter 8-15 Characters in Length (Mix Of Alphabetic, Non Alphabetic & Special Characters)");
            valid = false;
        }
    }

    if ($('#txtConfirmPwd').val().trim() == undefined || $('#txtConfirmPwd').val().trim() == "") {
        $('#lblPwdError').html("Please enter the confirm password.")
        valid = false;
    }


    if ($('#txtConfirmPwd').val().trim() != $('#txtPwd').val().trim()) {
        $('#lblPwdError').html("Password and confirm password should be same")
        valid = false;
    }

    return valid;
}

function BindDesignation() {

    $.ajax({
        type: "GET",
        url: '/Account/GetDesignationList/',
        success: function (designationList) {
            if (designationList != null && designationList.length > 0) {
                $.each(designationList, function (value) {
                    $('#ddlDesignation').append('<option value="' + value + '">' + value + '</option>');
                });

                $('#ddlDesignation').append('<option value="None">Select Designation</option>');
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

function BindDepartment() {

    $.ajax({
        type: "GET",
        url: '/Account/GetDepartmentList/',
        success: function (departmentList) {
            if (departmentList != null && departmentList.length > 0) {
                $.each(departmentList, function (key, value) {
                    $('#ddlDepartmentName').append('<option value="' + key + '">' + value + '</option>');
                });

                $('#ddlDepartmentName').append('<option value="0">Select Department</option>');
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

function BindClient(departmentId) {

    if (departmentId != "0") {

        $.ajax({
            type: "GET",
            url: '/Account/GetClientList/',
            data: '{departmentId: ' + departmentId + '}',
            success: function (clientList) {
                if (clientList != null && clientList.length > 0) {
                    $.each(clientList, function (key, value) {
                        $('#ddlClientName').append('<option value="' + key + '">' + value + '</option>');
                    });

                    $('#ddlClientName').append('<option value="0">Select Client</option>');
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
}

function BindLOB(departmentId, clientId) {

    if (clientId != "0") {

        $.ajax({
            type: "GET",
            url: '/Account/GetLOBList/',
            data: '{departmentId: ' + departmentId + ', clientId: ' + clientId + '}',
            success: function (lobList) {
                if (lobList != null && lobList.length > 0) {
                    $.each(lobList, function (key, value) {
                        $('#ddlLOBName').append('<option value="' + key + '">' + value + '</option>');
                    });

                    $('#ddlLOBName').append('<option value="0">Select LOB</option>');
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
}