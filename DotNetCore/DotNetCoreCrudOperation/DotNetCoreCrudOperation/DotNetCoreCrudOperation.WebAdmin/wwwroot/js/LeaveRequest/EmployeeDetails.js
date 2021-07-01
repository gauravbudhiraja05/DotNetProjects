$(document).ready(function () {
    $('#menuItemVacancies').addClass('current');
    if (parseInt($("#UserId option:selected").val()) > 0) {
        GetEmployeeDetails(parseInt($("#UserId option:selected").val()));
    }
    var leaveId = $("#LeaveID").val();
    if (leaveId === "0") {
        $("#divDateLeaveRequest").hide();
        $("#divDownloadedFileLink").hide();
    }
    else {   
        if ($('#FileName').val() === "") {
            $("#divDownloadedFileLink").hide();
        }
        else {
            $("#divDownloadedFileLink").show();
            $("#aDownloadLink").attr("href", "/fileserver/Uploads/Admin/Leave/" + $('#FileName').val());
        }
        $("#divEmployee").hide();
    }

    var disabledDays = [0, 6];
    var disabledDates = [$('#divbankholidays').html().trim().substring(0, $('#divbankholidays').html().trim().length - 1)];
    $('.LMLeaveRequestDate').datepicker({
        autoClose: true,
        format: 'dd/mm/yyyy',
        minDate: new Date(),
        position: 'top left', // Default position
        //onHide: function (inst) {
        //    inst.update('position', 'top left'); // Update the position to the default again
        //},
        onRenderCell: function (date, cellType) {
            if (cellType === 'day') {
                var day = date.getDay(),
                    formatted = getFormattedDate(date),
                    isDisabled = disabledDays.indexOf(day) !== -1 || disabledDates[0].indexOf("'" + formatted + "'") !== -1;
                return {
                    disabled: isDisabled
                }
            }
        }
    });

    $('.LMLeaveRequestEndDate').datepicker({
        autoClose: true,
        format: 'dd/mm/yyyy',
        minDate: new Date(),
        position: 'top left', // Default position
        //onHide: function (inst) {
        //    inst.update('position', 'top left'); // Update the position to the default again
        //},
        //onSelect: function (dateText, inst) {            
        //    if ($('#StartDate').val() !== "" && dateText !== "") {
        //        var strArr = $('#StartDate').val().split("/");
        //        var startdate = strArr[1] + "/" + strArr[0] + "/" + strArr[2];
        //        var endArr = dateText.split("/");
        //        var enddate = endArr[1] + "/" + endArr[0] + "/" + endArr[2];
        //        var eDate = new Date(enddate);
        //        var sDate = new Date(startdate);
        //        if (startdate !== '' && enddate !== '' && sDate > eDate) {
        //            $("span[data-valmsg-for='EndDate']").text("Returns back to Work on date must be greater than to first day of leave date.");
        //            return false;
        //        }
        //        else {
        //            $("span[data-valmsg-for='EndDate']").text("");
        //            return true;
        //        }
        //    }  
        //},
        onRenderCell: function (date, cellType) {
            if (cellType === 'day') {
                var day = date.getDay(),
                    formatted = getFormattedDate(date),
                    isDisabled = disabledDays.indexOf(day) !== -1 || disabledDates[0].indexOf("'" + formatted + "'") !== -1;
                return {
                    disabled: isDisabled
                }
            }
        }
    });

    $(".leave_reason_list ul li").each(function (index) {
        if ($("#LeaveTypeId").val() === $(this).find(".reason_radio_input").attr("id")) {
            $(this).find(".reason_radio_input").prop("checked", true);
            $(this).find(".other_reason_field").prop("style", "display:inline-block;");
            var leaveTypeId = $("#LeaveTypeId").val();
            switch (parseInt(leaveTypeId)) {
                case 2:
                    $("#Description_6").val("");
                    $("#Description_11").val("");
                    break;
                case 6:
                    $("#Description_2").val("");
                    $("#Description_11").val("");
                    break;
                case 11:
                    $("#Description_2").val("");
                    $("#Description_6").val("");
                    break;
                default:
                    $("#Description_2").val("");
                    $("#Description_6").val("");
                    $("#Description_11").val("");
            }
        }
    });

    function getFormattedDate(date) {
        var year = date.getFullYear(),
            month = date.getMonth() + 1,
            dt = date.getDate();

        return year + '.' + month + '.' + dt;
    }
});

var _empId = "";
$("#UserId").change(function () {
    _empId = $(this).val();
    GetEmployeeDetails(parseInt(_empId));

});
function GetEmployeeDetails(empId) {
    $.get('/LeaveManagement/GetEmployeeDetailsById?empId=' + empId, function (data) {
        $('#partialEmployeeDetails').html(data);
        if (empId === "") {
            $('#partialEmployeeDetails').hide();
        }
        else {
            $('#partialEmployeeDetails').show();
        }
    });
}

$("#btnCreateLMLeaves").click(function (event) {
    event.preventDefault();
    if (validateSaveLeave() === false) {
        return false;
    }
    else if (validateSaveLeave() === true) {
        checkdatediff();
    }
    else {
        return false;
    }
});

// Validate Featured Message form
var validateSaveLeave = function () {

    var isValid = true;
    if ($("#UserId option:selected").text() === "Please Select") {
        $(".error_msg").show();
        $("span[data-valmsg-for='UserId']").text("Please select employee");
        isValid = false;
    }
    else {
        $("span[data-valmsg-for='EmployeeId']").text("");
    }

    if ($("#LeaveTypeId").val() === "0") {
        $(".error_msg").show();
        $("span[data-valmsg-for='LeaveTypeId']").text("Please select leave type");
        isValid = false;
    }
    else {
        $("span[data-valmsg-for='LeaveTypeId']").text("");
    }
    if ($('#FileName').val() === "") {
        var leavetype = parseInt($("#LeaveTypeId").val());
        if (leavetype === 5 || leavetype === 2 || leavetype === 6 || leavetype === 3 || leavetype === 7 || leavetype === 9 || leavetype === 11) {
            var fileName = $("#upload1").val();
            if (fileName) {
                $("span[data-valmsg-for='FileNameData']").text("");
                isValid = true;
            } else { // no file was selected
                $("span[data-valmsg-for='FileNameData']").text("Please upload a file for this field.");
                isValid = false;
            }
        }
        else {
            $("span[data-valmsg-for='FileNameData']").text("");
            isValid = true;
        }
    }


    if ($("#StartDate").val().trim().length === 0) {
        $(".error_msg").show();
        $("span[data-valmsg-for='StartDate']").text("Please enter the date of your first day of leave");
        isValid = false;
    }
    else {
        $("span[data-valmsg-for='StartDate']").text("");
    }

    var radioValuestarttime = $("input[name='StartTime']:checked").val();
    if (!radioValuestarttime) {
        $(".error_msg").show();
        $("span[data-valmsg-for='StartTime']").text("Please select If you are leaving in the morning or afternoon");
        isValid = false;

        movetotop();
    }
    else {
        $("span[data-valmsg-for='StartTime']").text("");
        $(".error_msg").hide();
    }

    if ($("#EndDate").val().trim().length === 0) {
        $(".error_msg").show();
        $("span[data-valmsg-for='EndDate']").text("Please enter the date that you return back to work on");
        isValid = false;
    }
    else {
        if (IsValidToDate()) {
            $("span[data-valmsg-for='EndDate']").text("");
        }
    }

    var radioValueendtime = $("input[name='EndTime']:checked").val();
    if (!radioValueendtime) {
        $(".error_msg").show();
        $("span[data-valmsg-for='EndTime']").text("Please select If you are returning in the morning or afternoon");
        isValid = false;

        movetotop();
    }
    else {
        $("span[data-valmsg-for='EndTime']").text("");
        $(".error_msg").hide();
    }


    if ($("#Status option:selected").text() === "Please Select") {
        $(".error_msg").show();
        $("span[data-valmsg-for='Status']").text("Please tell us your reason for booking this leave");
        isValid = false;
    }
    else {
        $("span[data-valmsg-for='Status']").text("");
    }
    return isValid;
};

// On begin ajax request this function will be call
var onBegin = function (xhr) {
    $(".loaderModal").show();
};


// On success of ajax callback this function will be call
var onLeaveSuccess = function (data) {
    //console.log(data);
    $(".loaderModal").hide();
    if (data.isSuccess) {
        swal(
            data.message,
            '',
            'success'
        ).then(function () {
            window.location.href = "/LeaveManagement/Index";

        });
    }
    else {
        swal(
            data.message,
            '',
            'error'
        );
    }
};

// On failed/error of ajax 
var onFailed = function (context) {
    return false;
};

$("ul li .reason_radio_input").change(function () {
    var reg1 = $(this).prop("checked");
    if (reg1) {
        $(".other_reason_field").css('display', 'none');
        $(this).parent().parent().children(".other_reason_field").css('display', 'inline-block');
    }
});

function getId(ref) {
    $("#LeaveTypeId").val(ref.id);
    $("#LeaveTypeDescriptionId").val("Description_" + ref.id);
    if ($("#LeaveTypeId").val() !== "") {
        $(".leave_reason_list ul li").each(function (index) {
            if ($("#LeaveTypeId").val() === $(this).find(".reason_radio_input").attr("id")) {
                $(this).find(".reason_radio_input").prop("checked", true);
                $(this).find(".other_reason_field").prop("style", "display:inline-block;");
            }
        });
    }
}

function getYesterdaysDate(d) {
    var date = new Date(d);
    date.setDate(date.getDate() - 1);
    return date.getDate() + '/' + (date.getMonth() + 1) + '/' + date.getFullYear();
}

function getendDate(d) {
    var date = new Date(d);
    return date.getDate() + '/' + (date.getMonth() + 1) + '/' + date.getFullYear();
}

function deductDays(date, days) {
    //alert(date + '||' + days);
    var result = new Date(date);
    result.setDate(result.getDate() - days);
    return result;
}

function parseDate(str) {
    var mdy = str.split('/');
    return new Date(mdy[2], mdy[1] - 1, mdy[0]);
}

function datediff(first, second) {
    // Take the difference between the dates and divide by milliseconds per day.
    // Round to nearest whole number to deal with DST.
    return Math.round((second - first) / (1000 * 60 * 60 * 24));
}

function checkdatediff() {
    var radioValuestart = $("input[name='StartTime']:checked").val();
    var radioValueend = $("input[name='EndTime']:checked").val();
    var isvalid = true;
    var startdate = $('#StartDate').val();
    var enddate = $('#EndDate').val();
    var date = new Date();
    var currentDate;
    var sd;
    var month = date.getMonth();
    currentDate = new Date(date.getFullYear(), month, date.getDate());
    sd = new Date(parseDate(startdate));
    var datedifference = datediff(parseDate(startdate), parseDate(enddate));

    var ed;
    var etime;
    ed = new Date(parseDate(enddate));
    var newed;
    if (radioValueend.toLowerCase() === "morning") {
        etime = "AFTERNOON";
        newed = getYesterdaysDate(ed);
    }
    else {
        etime = "MORNING";
        newed = getendDate(ed);
    }
    var weekenddays = calcBusinessDays(parseDate(startdate), parseDate(newed));
    $.get("/LeaveManagement/CountBankHolidays", { Startdate: startdate, Enddate: newed, StartTime: radioValuestart.toLowerCase(), EndTime: radioValueend.toLowerCase() }, function (data) {
        $("#BankHolidayCount").val(data);
        var quantitywithhalfday = datedifference;
        if (radioValuestart.toLowerCase() === "morning" && radioValueend.toLowerCase() === "afternoon")
            quantitywithhalfday = datedifference + 0.5;
        if (radioValuestart.toLowerCase() === "afternoon" && radioValueend.toLowerCase() === "morning")
            quantitywithhalfday = datedifference - 0.5;
        var totalbankholidays = $("#BankHolidayCount").val();
        quantitywithhalfday = quantitywithhalfday - weekenddays - totalbankholidays;
        $("#LeaveDuration").val(quantitywithhalfday);
        if (GetWeekDayName(parseDate(enddate)).toLowerCase() === 'monday' && radioValueend.toLowerCase() === "morning") {
            newed = getendDate(deductDays(parseDate(enddate), 3));
            $.get("/LeaveManagement/CheckBankHolidayByDate", { Date: newed }, function (data) {
                if (data) {
                    newed = getYesterdaysDate(parseDate(newed));
                    ValidateDateFields(quantitywithhalfday, datedifference, sd, currentDate, startdate, newed, etime, radioValuestart, radioValueend);
                }
                else {
                    ValidateDateFields(quantitywithhalfday, datedifference, sd, currentDate, startdate, newed, etime, radioValuestart, radioValueend);
                }
            });
            //if ($('#divbankholidays').html().trim().replace('.','/'))
        }
        else if (radioValueend.toLowerCase() === "morning") {
            newed = getYesterdaysDate(parseDate(enddate));
            $.get("/LeaveManagement/CheckBankHolidayByDate", { Date: newed }, function (data) {
                if (data) {
                    newed = getYesterdaysDate(parseDate(newed));
                    if (GetWeekDayName(parseDate(newed)).toLowerCase() === 'sunday') {
                        newed = getendDate(deductDays(parseDate(newed), 2));
                        $.get("/LeaveManagement/CheckBankHolidayByDate", { Date: newed }, function (data1) {
                            if (data1) {
                                newed = getYesterdaysDate(parseDate(newed));
                                ValidateDateFields(quantitywithhalfday, datedifference, sd, currentDate, startdate, newed, etime, radioValuestart, radioValueend);
                            }
                            else {
                                ValidateDateFields(quantitywithhalfday, datedifference, sd, currentDate, startdate, newed, etime, radioValuestart, radioValueend);
                            }
                        });
                    }

                }
                else {
                    ValidateDateFields(quantitywithhalfday, datedifference, sd, currentDate, startdate, newed, etime, radioValuestart, radioValueend);
                }


            });
        }
        else {
            ValidateDateFields(quantitywithhalfday, datedifference, sd, currentDate, startdate, newed, etime, radioValuestart, radioValueend);
        }
    });
}

function GetWeekDayName(date) {
    var d = new Date(date);
    var weekday = new Array(7);
    weekday[0] = "Sunday";
    weekday[1] = "Monday";
    weekday[2] = "Tuesday";
    weekday[3] = "Wednesday";
    weekday[4] = "Thursday";
    weekday[5] = "Friday";
    weekday[6] = "Saturday";

    var weekdayname = weekday[d.getDay()];
    return weekdayname;
}

function calcBusinessDays(start, end) {
    var s = new Date(start);
    var e = new Date(end);

    var addOneMoreDay = 0;
    if (s.getDay() === 0 || s.getDay() === 6) {
        addOneMoreDay = 1;
    }

    // Set time to midday to avoid dalight saving and browser quirks
    s.setHours(12, 0, 0, 0);
    e.setHours(12, 0, 0, 0);

    // Get the difference in whole days
    var totalDays = Math.round((e - s) / 8.64e7);

    // Get the difference in whole weeks
    var wholeWeeks = totalDays / 7 | 0;

    // Estimate business days as number of whole weeks * 5
    var days = wholeWeeks * 5;

    // If not even number of weeks, calc remaining weekend days
    if (totalDays % 7) {
        s.setDate(s.getDate() + wholeWeeks * 7);

        while (s < e) {
            s.setDate(s.getDate() + 1);

            // If day isn't a Sunday or Saturday, add to business days
            if (s.getDay() != 0 && s.getDay() != 6) {
                ++days;
            }
            //s.setDate(s.getDate() + 1);
        }
    }
    var weekEndDays = totalDays - days + addOneMoreDay;
    return weekEndDays;
}

var movetotop = function () {
    var h1 = $(".header_top_area").height();
    var h2 = $(".top_filter_section").outerHeight();
    var h3 = h1 + h2;
    $('html, body').stop().animate({
        scrollTop: 0
    }, 1000);
};

//$('#EndDate').change(function () {

//});

function IsValidToDate() {
    if ($('#StartDate').val() !== "" && $('#EndDate').val() !== "") {
        var strArr = $('#StartDate').val().split("/");
        var startdate = strArr[1] + "/" + strArr[0] + "/" + strArr[2];
        var endArr = $('#EndDate').val().split("/");
        var enddate = endArr[1] + "/" + endArr[0] + "/" + endArr[2];
        var eDate = new Date(enddate);
        var sDate = new Date(startdate);
        if (startdate !== '' && enddate !== '' && sDate > eDate) {
            $("span[data-valmsg-for='EndDate']").text("Returns back to Work on date must be greater than to first day of leave date.");
            return false;
        }
        else {
            $("span[data-valmsg-for='EndDate']").text("");
            return true;
        }
    }
    else {
        return false;
    }
}


function ValidateDateFields(quantitywithhalfday, datedifference, sd, currentDate, startdate, newed, etime, radioValuestart, radioValueend) {
    var isvalid = true;
    if (quantitywithhalfday.toString() === '0') {
        isvalid = false;
        $(".error_msg").show();
        $("span[data-valmsg-for='StartDate']").text("You have selected bank holidays");
        movetotop();
    }
    if (datedifference === 0 && radioValuestart.toLowerCase() === "morning" && radioValueend.toLowerCase() === "morning") {
        isvalid = false;
        $(".error_msg").show();
        $("span[data-valmsg-for='StartDate']").text("First day of leave cannot be equal to the date that you return back to work on");
        movetotop();
    }
    if (datedifference === 0 && radioValuestart.toLowerCase() === "afternoon" && radioValueend.toLowerCase() === "afternoon") {
        isvalid = false;
        $(".error_msg").show();
        $("span[data-valmsg-for='StartDate']").text("First day of leave cannot be equal to the date that you return back to work on");
        movetotop();
    }
    if (datedifference === 0 && radioValuestart.toLowerCase() === "afternoon" && radioValueend.toLowerCase() === "morning") {
        isvalid = false;
        $(".error_msg").show();
        $("span[data-valmsg-for='StartDate']").text("First day of leave cannot be greater than the date that you return back to work on");
        movetotop();
    }
    if (datedifference <= -1) {
        isvalid = false;
        $(".error_msg").show();
        $("span[data-valmsg-for='StartDate']").text("First day of leave cannot be greater thanthe date that you return back to work on");
        movetotop();
    }
    if (sd < currentDate) {
        isvalid = false;
        $(".error_msg").show();
        $("span[data-valmsg-for='StartDate']").text("First day of leave cannot be smaller than current date");
        movetotop();
    }
    if (isvalid) {
        var leaveId = $("#LeaveID").val();
        var form = $('#formAddLeaveRequestLM')[0];

        var leavedes = "";
        var leaveTypeId = $("#LeaveTypeId").val();
        switch (parseInt(leaveTypeId)) {
            case 2:
                leavedes = $("#Description_2").val();
                break;
            case 6:
                leavedes = $("#Description_6").val();
                break;
            case 11:
                leavedes = $("#Description_11").val();
                break;
            default:
                leavedes = "";
        }
        $("#Description_2").val(leavedes);
        $("#Description_6").val(leavedes);
        $("#Description_11").val(leavedes);

        // Create an FormData object 
        var formdata = new FormData(form);

        var quantity = $('#LeaveDuration').val();
        formdata.append('LeaveDuration', quantity);

        var firstName = $('#FirstName').val();
        formdata.append('FirstName', firstName);

        var emailAddress = $('#EmailAddress').val();
        formdata.append('EmailAddress', emailAddress);

        formdata.append('NewEndDate', newed);

        var filename = $('#FileName').val();
        formdata.append('FileName', filename);

        // disabled the submit button
        $("#btnCreateLMLeaves").prop("disabled", true);
        $.ajax({
            type: "POST",
            enctype: 'multipart/form-data',
            url: "/LeaveManagement/AddLMLeaveRequest/" + leaveId,
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val());
                onBegin(xhr);
            },
            data: formdata,
            processData: false,
            contentType: false,
            cache: false,
            timeout: 600000,
            success: function (data) {
                $("#btbtnCreateLMLeavesnSave").prop("disabled", false);
                onLeaveSuccess(data);
            },
            error: function (e) {
                //$("#btnCreateLMLeaves").prop("disabled", false);
                onFailed(e);
            }
        });
    }
}

$("#upload1").on('change', function () {
    $(this).next(".file_path").text($(this).val());
    isValid = false;
    var fileName = $("#upload1").val();
    if (fileName) {
        var exts = ['doc', 'docx', 'xls', 'xlsx', 'pdf', 'jpg', 'jpeg', 'png'];
        var get_ext = fileName.split('.');
        get_ext = get_ext.reverse();
        if ($.inArray(get_ext[0].toLowerCase(), exts) > -1) {
            $("span[data-valmsg-for='FileNameData']").text("");
            isValid = true;
        }
        else {
            $("span[data-valmsg-for='FileNameData']").text('Please select a valid file in (PDF, MS Word, MS Excel,JPEG, PNG) format');
            isValid = false;
        }
    }
    return isValid;
});