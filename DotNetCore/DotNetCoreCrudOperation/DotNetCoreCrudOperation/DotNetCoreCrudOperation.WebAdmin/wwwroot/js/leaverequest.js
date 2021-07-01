
$("#abtn1Continue").click(function (event) {
    debugger;
    event.preventDefault();
    var isValid = true;
    if (!$("#formRequestLeave").valid()) {
        $("#formRequestLeave").submit();
        isValid = false;
    }
    if ($('#LeaveReq_FirstDayOfLeave').val().trim() == '') {
        isValid = false;
    }
    if (isValid) {    
        var h1 = $(".header_top_area").height();
        var h2 = $(".top_filter_section").outerHeight();
        var h3 = h1 + h2;
        $('.leave_step_btn_1, .leave_step_1 .error_msg').hide();
        $(".leave_step_2, .leave_step_btn_2, .update_date").fadeIn(200);
        $('html, body').stop().animate({
            scrollTop: $("#leavestep2").offset().top - h3
        }, 1000);
    }
    return isValid;
});
// Post data on save button click
$("#aLeaveConfirm").click(function (event) {
    console.log("Save Click");   
    if (validateLeaveRequest() == false) {
        return false;
    }  
    var form = $('#formRequestLeave')[0];
    var data = new FormData(form);     
    $("#aLeaveConfirm").prop("disabled", true);

    //$.ajax({
    //    type: "POST",
    //    enctype: 'multipart/form-data',
    //    url: "/News/Add",
    //    beforeSend: function (xhr) {
    //        xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val());
    //        onBegin(xhr);
    //    },
    //    data: data,
    //    processData: false,
    //    contentType: false,
    //    cache: false,
    //    timeout: 600000,
    //    success: function (data) {
    //        $("#btnSave").prop("disabled", false);
    //        onSuccess(data);
    //    },
    //    error: function (e) {
    //        $("#btnSave").prop("disabled", false);
    //        onFailed(e);
    //    }
    //});

});

// Validate News form
var validateLeaveRequest = function () {
    var isValid = true;
    if (!$("#formRequestLeave").valid()) {
        isValid = false;
    }
    if ($('#LeaveReq_FirstDayOfLeave').val().trim() == '') {
        isValid = false;
    }
    return isValid;
};

var onBegin = function () {
    $(".loaderModal").show();
};


var onSuccess = function (context) {
    $(".loaderModal").hide();
    swal(
        'The leave request has been successfully added. ',
        '',
        'success'
    ).then(function () {
        var foo = getParameterByName('DepartmentId');
       // window.location.href = "Index?DepartmentId=" + foo;

    });
};

var onFailed = function (context) {
    console.log(context);
    swal(context);
};

