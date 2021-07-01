$(document).ready(function () {
    $("#btnSubmit").bind("click", function () {

        if (!ValidateDropDownValues()) {
            return false;
        }
        else {
            $('#SinUserSingDBExc').hide();
            $('#SinUserSingDBSql').hide();
            $('#SinUserSingDBOra').hide();
            $('#MulUserSingDBExc').hide();
            $('#MulUserSingDBSQL').hide();
            $('#MulUserSingDBOra').hide();
            $('#SinUserMulDB').hide();
            $('#MulUserMulDB').hide();

            if ($('#ddlDataBaseType').val() == "Single Database") {
                if ($('#ddlUserType').val() == "Single User") {
                    if ($('#chkExcel:checked')) {
                        $('#SinUserSingDBExc').show();
                    }
                    else if ($('#chkMSSQL:checked')) {
                        $('#SinUserSingDBSql').show();
                    }
                    else if ($('#chkOracle:checked')) {
                        $('#SinUserSingDBOra').show();
                    }
                }
                else if ($('#ddlUserType').val() == "Multiple User") {
                    if ($('#chkExcel:checked')) {
                        $('#MulUserSingDBExc').show();
                    }
                    else if ($('#chkMSSQL:checked')) {
                        $('#MulUserSingDBSQL').show();
                    }
                    else if ($('#chkOracle:checked')) {
                        $('#MulUserSingDBOra').show();
                    }
                }
            }
            else if ($('#ddlDataBaseType').val() == "Multiple Database")
                if ($('#ddlUserType').val() == "Single User") {
                    $('#SinUserMulDB').show();
                }
                else if ($('#ddlUserType').val() == "Multiple User") {
                    $('#MulUserMulDB').show();
                }
        }
    });
});

function ValidateDropDownValues() {

    $('#spanUserTypeError').html("");
    $('#spanDataBaseType').html("");
    $('#spanDataBaseError').html("");

    var validate = true;


    if ($('#ddlUserType').val() == "" || $('#ddlUserType').val() == null || $('#ddlUserType').val() == undefined) {
        $('#spanUserTypeError').html("Please select user type.");
        validate = false;
    }

    if ($('#ddlDataBaseType').val() == "" || $('#ddlDataBaseType').val() == null || $('#ddlDataBaseType').val() == undefined) {
        $('#spanDataBaseType').html("Please select dataBase type.");
        validate = false;
    }

    if ($('#ddlDataBaseType').val() == "Single Database") {
        if ($('.checkbox_item span .checkbox:checked').length <= 0) {
            $('#spanDataBaseError').html("Please select alteast one database.");
            validate = false;
        }
        else if ($('.checkbox_item span .checkbox:checked').length > 1) {
            $('#spanDataBaseError').html("Please select only one database.");
            validate = false;
        }
    }

    if ($('#ddlDataBaseType').val() == "Multiple Database") {
        if ($('.checkbox_item span .checkbox:checked').length <= 1) {
            $('#spanDataBaseError').html("Please select alteast more than one database.");
            validate = false;
        }
    }

    return validate;
}

function Code_click(code) {
    var productType = $('#ddlUserType').val();
    var databaseType = $('#ddlDataBaseType').val();
    var database = "";

    $('.checkbox_item span .checkbox:checked').each(function () {
        if (database == "")
            database = $(this).val();
        else
            database = database + "," + $(this).val();
    });


    window.location.href = "/User/Registration?code=" + code + "&productType=" + productType + "&databaseType=" + databaseType + "&database=" + database;
}

