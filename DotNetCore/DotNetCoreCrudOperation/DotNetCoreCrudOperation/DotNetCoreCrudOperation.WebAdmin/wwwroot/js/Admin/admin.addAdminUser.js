var selectedRoleIds = [];

$(document).ready(function () {

    // set the selectedRoleIds
    $('#RoleId option:selected').each(function (index, role) {
        selectedRoleIds.push(parseInt($(this).val()));
    });


    //Select Current Menu
    $('.nav-li').removeClass('current');
    $('#menuItemSuperAdmin').addClass('current');

    $('#RoleId').multiselect({
        buttonWidth: '550px',
        buttonHeight: '40px',
        nonSelectedText: 'Select Role Type',
        //nSelectedText: 'selected',
        //allSelectedText: 'All selected',
        numberDisplayed: 3,
        selectAllName: true,
        selectAllNumber: false,
        selectedClass: '',
        includeSelectAllOption: false,
        nSelectedText: 'selected',
        onChange: function (element, checked) {

            // empty the selected Item array
            selectedRoleIds = [];

            var selectedRoles = $('#RoleId option:selected');
            $(selectedRoles).each(function (index, role) {
                selectedRoleIds.push(parseInt($(this).val()));
            });

            // iterate through options
            $('#RoleId > option:selected').each(function () {
                //  alert($(this).text() + ' ' + $(this).val());
            });


            //console.log(selectedRoleIds);
            var currentClickedOption = element[0].label;
            var isChecked = checked;

            // If Super Admin is selcted then de-select other options
            if (currentClickedOption == 'Super Administrator' && checked) {

                // De-Select all except Super administration
                $('#RoleId').multiselect('deselect', selectedRoleIds);
                $('#RoleId').multiselect('select', [1]);

                // Empty and Push only one item in selectedRoleIds array
                selectedRoleIds = [];
                selectedRoleIds.push(1);

                // Set Department dropdown to All
                $('#DepartmentId option[value=0]').text('All');
                $('#DepartmentId').val('0');
                $("#DepartmentId").prop("disabled", true);
                $("#lblDepartmentError").html("");

                // hide the LineManagerCan dropdown
                $("#divLineManagerCan").hide()

            }

            // If Departmmental admin selected then de-select Super Admin Option
            else if (currentClickedOption == 'Departmental Administrator' && checked) {

                // De-Select all except Super administration
                var removeItem = '1';  // Super Admin Id
                $('#RoleId').multiselect('deselect', [1]);
                // selectedRoleIds.pop('1');
                selectedRoleIds = $.grep(selectedRoleIds, function (value) {
                    return value != removeItem;
                });


                // Set Department dropdown to 'Select Departments'
                $('#DepartmentId option[value=0]').text('Select Departments');
                $('#DepartmentId').val('0');
                $("#DepartmentId").prop("disabled", false);

            }

            // If Departmmental admin de-selected then Set Department dropdown to All and disable
            else if (currentClickedOption == 'Departmental Administrator' && checked == false) {

                // Set Department dropdown to All
                $('#DepartmentId option[value=0]').text('All');
                $('#DepartmentId').val('0');
                $("#DepartmentId").prop("disabled", true);
                $("#lblDepartmentError").html("");

            }

            // If Line Manager selected then de-select Super Admin Option   
            else if (currentClickedOption == 'Line Manager' && checked) {

                // De-Select all except Super administration
                var removeItem = 1 ;  // Super Admin Id
                $('#RoleId').multiselect('deselect', [1]);
                //  selectedRoleIds.pop('1');
                selectedRoleIds = $.grep(selectedRoleIds, function (value) {
                    return value != removeItem;
                });
                // Set Department dropdown to 'Select Departments'
                //$(selectedRoleIds).each(function (item, index) {

                //    if (item == "2") console.log('yes');
                //});
                if ($.inArray(2, selectedRoleIds) != '-1') {

                    $('#DepartmentId option[value=0]').text('Select Departments');
                    $('#DepartmentId').val('0');
                    $("#DepartmentId").prop("disabled", false);
                }

                else {
                    // Set Department dropdown to All
                    //$('#DepartmentId option[value=0]').text('All');
                    //$('#DepartmentId').val('0');
                    //$("#DepartmentId").prop("disabled", true);
                    //$("#lblDepartmentError").html("");
                }
            }

            else {

            }

           // If Line Manager Checkbox clicked and checked it then show the LineManagerCan dropdown
            if (currentClickedOption == 'Line Manager' && checked) {

                $("#divLineManagerCan").show()
            }

            // If Line Manager Checkbox clicked and un-checked it then hide the LineManagerCan dropdown
            if (currentClickedOption == 'Line Manager' && checked == false) {

                $("#divLineManagerCan").hide()
            }
        }

    });
    
});

var onBegin = function () {
    var actionName = $('#formAdminUser').attr('action');
    if (actionName == "/SuperAdmin/AddAdminUserWithSendEmail") {
        $(".loaderModal").show()
    }
};




// Post data on save button click

$("#btnSave").click(function (event) {


    //stop submit the form, we will post it manually.
   // event.preventDefault();

    // Validation is ok or not
    if (validateAdminUser() == false) {
        return false;
    }

    // Get form
    var form = $('#formAdminUser')[0];


    // Create an FormData object 
    var data = new FormData(form);
    data.append("AllSelectedRoleTypes", selectedRoleIds.join("|"));

    // disabled the submit button
    $("#btnSave").prop("disabled", true);

    $.ajax({
        type: "POST",
        enctype: 'multipart/form-data',
        url: "/SuperAdmin/AddAdminUser",
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


$("#btnSendEmail").click(function (event) {


    //stop submit the form, we will post it manually.
    //event.preventDefault();

    // Validation is ok or not
    if (validateAdminUser() == false) {
        return false;
    }

    // Get form
    var form = $('#formAdminUser')[0];

    // Create an FormData object 
    var data = new FormData(form);
    data.append("AllSelectedRoleTypes", selectedRoleIds.join("|"));

    // If you want to add an extra field for the FormData
    // data.append("CustomField", "This is some extra data, testing");

    // disabled the submit button
    $("#btnSave").prop("disabled", true);

    $.ajax({
        type: "POST",
        enctype: 'multipart/form-data',
        url: "/SuperAdmin/AddAdminUserWithSendEmail",
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

// Validate Featured Message form
var validateAdminUser = function () {
    var isValid = true;
    // check if the form input is valid
    if (!$("#formAdminUser").valid()) {
        $("#formAdminUser").submit();
        isValid = false;
    }


    // Check any role is checked or not
    if (selectedRoleIds.length == 0) {
        $("#lblRoletypeError").html("Please choose Role.");
        isValid = false;
    }

    else {
        $("#lblRoletypeError").html("");
    }


    if ($("#DepartmentId").val() == 0 && $.inArray(2, selectedRoleIds) > -1) {
        $("#lblDepartmentError").html("Please select a department.");
        isValid = false;
    }

    else {
        $("#lblDepartmentError").html("");
    }



   
   
    return isValid;


}

$("#DepartmentId").on("change", function () {

    if ($("#DepartmentId").val() == 0) {
        $("#lblDepartmentError").html("Please select the department");
        isValid = false;
    }

    else {
        $("#lblDepartmentError").html("");
    }
});


// On begin ajax request this function will be call
var onBegin = function (xhr) {
    $(".loaderModal").show();
};


// On success of ajax callback this function will be call
var onSuccess = function (context) {
    $(".loaderModal").hide();
    swal(
        'The admin user has been created successfully.',
        '',
        'success'
    ).then(function () {
        window.location.href = "/SuperAdmin/AdminUsers";

    });
};

// On failed/error of ajax 
var onFailed = function (context) {
    return false;
};
