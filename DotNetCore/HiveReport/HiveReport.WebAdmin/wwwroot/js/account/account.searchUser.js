$(document).ready(function () {
    $('#imgBtnSearch').bind("click", function () {

        if (ValidateSearchCriteria()) {

            $.ajax({
                type: "GET",
                url: '/Account/GetSearchedResult/',
                data: '{dropdownValue: ' + $('#ddlSearchCriteria').val() + ', txtValue: ' + $('#txtSearch').val() + '}',
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
});

function ValidateSearchCriteria() {

    var validate = true;

    if ($('#ddlSearchCriteria').val() == undefined || $('#ddlSearchCriteria').val() == "" || $('#ddlSearchCriteria').val() == "--Select--") {
        $('#lblMsgBox').html("Please select search criteria");
        validate = false;
    }
    if ($('#txtSearch').val == undefined || $('#txtSearch').val().trim() == "") {
        $('#lblLink').html("Please enter some value");
        validate = false;
    }

    return validate;
}
