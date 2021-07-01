$(document).ready(function () {

    $("#img1").hide();

    $("#HeaderImage").change(function () {
        readIMG1(this);
    });
    //Select Current Menu
    $('.nav-li').removeClass('current');
    $('#menuItemSuperAdmin').addClass('current');

    // Select creation date as today
    $('#datetimepicker1').val(getFormattedDate());

    $("#Save_Depart_Btn").click(function (event) {

        $("#DepartmentHead").val($($("div.nicEdit-main")[0]).html());
        // Get form
        var form = $('#formDepartment')[0];

        if (validateAddDepartmentMessage() == false) {
            //$("#btnSave").prop("disabled", true);
            return false;
        }

        // Create an FormData object
        var data = new FormData(form);

        $.ajax({
            type: "POST",
            enctype: 'multipart/form-data',
            url: "/Department/AddDepartment",
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
                //$("#btnSave").prop("disabled", false);
                onSuccess(data);
            },
            error: function (e) {
                // $("#btnSave").prop("disabled", false);
                onFailed(e);
            }
        });

    });

});



var onBegin = function () {
    $(".loaderModal").show();
};



var onComplete = function () {
    //alert("onComplete");
};

var onSuccess = function (context) {
    $(".loaderModal").hide();
    swal(
        'The department was successfully created.',
        '',
        'success'
    ).then(function () {
        window.location.href = "/Department/Index";

    });
};

var onFailed = function (context) {
    //alert("Failed");
};

function readIMG1(input) {
    
    if (input.files && input.files[0]) {
        var checkValid = CheckFileTypeExtensionForDepartment(input.files[0].name);
        if (checkValid == true) {
            var reader = new FileReader();

            reader.onload = copyImage;

            function copyImage(ev) {
                $('#img1').attr('src', ev.target.result);
                $("#img1").show();
            };

            reader.readAsDataURL(input.files[0]);
            $("#HeaderImage").text("");
            //$("#btnSave").prop("disabled", false);
            $("#img1").show();

        }
        else {
            $("#HeaderImage").text("Please upload a Header Image.");
            // $("#btnSave").prop("disabled", true);
            $(input).val('');
            $("#img1").hide();
        }
    }
}

function CheckFileTypeExtensionForDepartment(fileName) {
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
        swal({
            type: 'error',
            //title: "Sorry, " + fileName + " is invalid, allowed extensions are: " + _validFileExtensions.join(", "),
            title: 'The image file types allowed .jpg, .jpeg, .bmp, .gif, .png.',
            text: '',
            width: '500px'
        })
        return false;
    }
    else { return true; }
}

// Validate Featured Message form
var validateAddDepartmentMessage = function () {
    var isValid = true;
    debugger;

    // check if the form input is valid
    if (!$("#formDepartment").valid()) {
        $("#formDepartment").submit();
        isValid = false;
    }

    if ($("#DepartmentName").val().trim().length == 0) {
        $("span[data-valmsg-for='DepartmentName']").text("Please enter the department name.");
        isValid = false;
        
    }
    else {
        $("span[data-valmsg-for='DepartmentName']").text("");
    }

    if ($("#TelephoneNumber").val().trim().length == 0) {
        $("span[data-valmsg-for='TelephoneNumber']").text("Please enter the telephone number.");
        isValid = false;

    }
    else {
        $("span[data-valmsg-for='TelephoneNumber']").text("");
    }

    var ExistImageName = $("#img1").attr("src");
    if (ExistImageName == "#") { ExistImageName = ""; }
    if (ExistImageName != "") {
        var SubStringExistingName = ExistImageName.substring(ExistImageName.lastIndexOf("/") + 1, ExistImageName.length);

        if (SubStringExistingName.trim() == '') {
           
            isValid = false;
            $("#lblHeaderImage").text("Please upload a header image.");
        }
    }
    else
    {
        isValid = false;
        $("#lblHeaderImage").text("Please upload a header image.");
    }
   

    if ($($("div.nicEdit-main")[0]).text().trim().length == 0) {
        $("span[data-valmsg-for='DepartmentHead']").text("Please enter the department head.");
        isValid = false;
        
    }
    else {
        $("span[data-valmsg-for='DepartmentHead']").text("");
    }

   
    return isValid;


}

function PutSampleContent(ctrl) {
    var value = $(ctrl.elm.parentElement.parentElement.parentElement).find("label").attr('for');

    //DepartmentHead
    if (value == "DepartmentHead") {
        $("#divValueDepartmentHeadContent div.nicEdit-main").html();
        $("#divValueDepartmentHeadContent div.nicEdit-main").html($('#hdnDivValueDepartmentHeadContent').html());
    }
}

