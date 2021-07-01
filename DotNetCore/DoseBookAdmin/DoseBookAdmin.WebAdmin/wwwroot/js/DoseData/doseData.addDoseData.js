$(document).ready(function () {

    //Select Current Menu
    $('.nav-li').removeClass('current');
    $('#menuItemVacancies').addClass('current');

    $('#btnBack').on('click', function () {
        var foo = getParameterByName('DoseMetaTypeId')
        window.location.href = "Index?DoseMetaTypeId=" + foo;
    });



    // Post data on save button click
    $("#btnSave").click(function () {

        // Validation is ok or not
        if (validateDoseData() == false) {
            return false;
        }

        // Get form
        var form = $('#formDoseData')[0];


        // Create an FormData object 
        var data = new FormData(form);

        // If you want to add an extra field for the FormData
        // data.append("CustomField", "This is some extra data, testing");

        // disabled the submit button
        $("#btnSave").prop("disabled", true);

        $.ajax({
            type: "POST",
            enctype: 'multipart/form-data',
            url: "/DoseData/Add",
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

    // Validate DoseData form
    var validateDoseData = function () {
        var isValid = true;
        var flag = 0;

        // check if the form input is valid
        if (!$("#formDoseData").valid()) {
            $("#formDoseData").submit();
            isValid = false;
        }

        if ($('#Title').val() == '') {
            isValid = false;
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
        'The dose data has been created successfully.',
        '',
        'success'
    ).then(function () {
        var foo = getParameterByName('DoseMetaTypeId')
        window.location.href = "Index?DoseMetaTypeId=" + foo;

    });
};

var onFailed = function (context) {
    console.log(context);
    swal(context);
};