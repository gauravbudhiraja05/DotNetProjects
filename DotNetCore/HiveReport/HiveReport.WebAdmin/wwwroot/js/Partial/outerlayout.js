$(document).ready(function () {

    $("#btnLogin").bind("click", function () {

        if (!ValidateUserDetails()) {
            return false;
        }
        else {

            var authuserdto = {};

            authuserdto.UserName = $('#txtUsername').val().trim();
            authuserdto.Password = $('#txtPassword').val().trim();


            $.ajax({
                type: "POST",
                url: '/Account/Login/',
                data: { authuserdto: authuserdto },
                success: function (loggedInUserDto) {
                    if (loggedInUserDto.isSuccess) {
                        window.location.href = "/Home/Dashboard";
                    }
                    else {
                        alert(loggedInUserDto.message);
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

function ValidateUserDetails() {

    var valid = true;

    if ($('#txtUsername').val().trim() == undefined || $('#txtUsername').val().trim() == "") {
        $('#lblUserNameError').show();
        $('#lblUserNameError').html("Please enter the username.")
        valid = false;
    }

    if ($('#txtPassword').val().trim() == undefined || $('#txtPassword').val().trim() == "") {
        $('#lblPasswordError').show();
        $('#lblPasswordError').html("Please enter the password.")
        valid = false;
    }

    return valid;
}

function Code_click(code) {

    window.location.href = "/Home/Registration?code" +code;
}