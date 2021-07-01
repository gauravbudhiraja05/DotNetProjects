$(document).ready(function () {


    $("#fileUploaderImg1").change(function () {
        readIMG1(this);
    });

    //Select Current Menu
    $('.nav-li').removeClass('current');
    $('#menuItemSuperAdmin').addClass('current');

    $("#Edit_Department_Btn").click(function (event) {

        $("#DepartmentHead").val($($("div.nicEdit-main")[0]).html());
        // Validation is ok or not
        if (validateEditDepartmentMessage() == false) {
            //$("#btnSave").prop("disabled", true);
            return false;
        }
        
        // Get form
        var form = $('#EditformDepartment')[0];


        // Create an FormData object 
        var data = new FormData(form);

        $.ajax({
            type: "POST",
            enctype: 'multipart/form-data',
            url: "/Department/EditDepartment",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val());
                onUpdateBegin(xhr);
            },
            data: data,
            processData: false,
            contentType: false,
            cache: false,
            timeout: 600000,
            success: function (data) {
                //$("#btnSave").prop("disabled", false);
                onUpdateSuccess(data);
            },
            error: function (e) {
                // $("#btnSave").prop("disabled", false);
                onUpdateFailed(e);
            }
        });

    });

});
var onUpdateBegin = function () {
    //alert("onBegin");
};


var onUpdateComplete = function () {
    //alert("onComplete");
};

var onUpdateSuccess = function (data) {
    if (data.isSuccess == true) {
        swal({
            title: data.message,
            text: "",
            type: 'success'
        }).then(function () {
            window.location.href = "/Department/Index";
        });
    }
    else {
        swal({
            title: data.message,
            text: "",
            type: 'error',
            width:'500px'
        });
    }
};

var onUpdateFailed = function (context) {
    //alert("Failed");
};


function readIMG1(input) {
    // debugger;
    if (input.files && input.files[0]) {
        var checkValid = CheckFileTypeExtensionForEditDocument(input.files[0].name);
        if (checkValid == true) {
            var reader = new FileReader();

            reader.onload = copyImage;

            function copyImage(ev) {
                $('#img1').attr('src', ev.target.result);
                $("#img1").show();
            };

            reader.readAsDataURL(input.files[0]);
            $("#lblErrorMessageImage").text("");
            //$("#btnSave").prop("disabled", false);
            $("#img1").show();

        }
        else {
            // $("#lblErrorMessageImage").text("Please browse the image");
            // $("#btnSave").prop("disabled", true);
            //var prevImageName = $("#img1").attr("src");
            //prevImageName = prevImageName.substring(prevImageName.lastIndexOf("/")+1, prevImageName.length);
            $(input).val("");
            $("#img1").hide();
            $("#img1").attr("src", "");
        }
    }
}

function CheckFileTypeExtensionForEditDocument(fileName) {
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
var validateEditDepartmentMessage = function () {
    var isValid = true;
    

    // check if the form input is valid
    if (!$("#EditformDepartment").valid()) {
        $("#EditformDepartment").submit();
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
    var SubStringExistingName = ExistImageName.substring(ExistImageName.lastIndexOf("/")+1, ExistImageName.length);

    if (SubStringExistingName.trim() == '') {
       
        isValid = false;
        $("#lblErrorMessageImage").text("Please upload a header image.");
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