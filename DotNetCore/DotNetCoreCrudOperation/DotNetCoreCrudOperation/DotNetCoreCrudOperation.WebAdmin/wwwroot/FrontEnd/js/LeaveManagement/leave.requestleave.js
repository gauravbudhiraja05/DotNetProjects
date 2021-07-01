var selectedDocuments = "";
$(document).ready(function () {

    $(".error_msg").hide();
    var currentTab = location.hash;
    if (currentTab) {
        $(".tab_list li").removeClass("current");
        //$(".tab_list li>a[href=" + currentTab + "]").closest("li").addClass("current");
        $('.tab_list li a[href="' + currentTab + '"]').closest("li").addClass("current");
        $(".tab_content").hide();
        $(currentTab).show();
        $(currentTab + "_btn").show();
    }
    $("input[name='LeaveTypeId']").each(function () {
       
        if ($('#hdnLeaveTypeId').val() === "0") {
            if ($(this).siblings('label#lblleavetype').html().toLowerCase() === "paid annual holiday") {
                $(this).prop("checked", true);
                $('#hdnLeaveType').val("Paid annual holiday");
            } 
        }
        else {
            if ($(this).val() === $('#hdnLeaveTypeId').val()) {
                $(this).prop("checked", true);
                $('#hdnLeaveType').val(toTitleCase($(this).siblings('label#lblleavetype').html().toLowerCase()));
                if ($(this).siblings('label#lblleavetype').html().toLowerCase() === "paid medical") {
                    $("#divbriefdesc").css('display', 'inline-block');
                }
                else {
                    $("#divbriefdesc").css('display', 'none');
                }
                if ($(this).siblings('label#lblleavetype').html().toLowerCase() === "other") {
                    $("#divother").css('display', 'inline-block');
                }
                else {
                    $("#divother").css('display', 'none');
                }
                if ($(this).siblings('label#lblleavetype').html().toLowerCase() === "paid sickness") {
                    $("#divpaidsickness").css('display', 'inline-block');
                }
                else {
                    $("#divpaidsickness").css('display', 'none');
                }
            }
        }

        $('.leave_step_btn_2 > a').on("click", function (e) {
            e.preventDefault();
            var h1 = $(".header_top_area").height();
            var h2 = $(".top_filter_section").outerHeight();
            var h3 = h1 + h2;
            $('.leave_step_btn_2').hide();
            $(".leave_step_3, .leave_step_btn_3, .view_more_leave").fadeIn(200);
            $(".leave_step_btn_3").addClass("active_btn3");

            $('html, body').stop().animate({
                scrollTop: $("#leavestep3").offset().top - h3
            }, 1000);

        });

        //$(".leave_step_btn_3 a").click(function (event) {
        //    event.preventDefault();
        //    $(".confirmation_popup, .confirmation_popup_overlay").fadeIn(200);
        //});

        $(".confirmation_popup .rq_cancel_btn").click(function (event) {
            event.preventDefault();
            $(".confirmation_popup, .confirmation_popup_overlay").fadeOut(200);
        });
    });
    var disabledDays = [0, 6];
    var disabledDates = [$('#divbankholidays').html().trim().substring(0, $('#divbankholidays').html().trim().length - 1)];
  // alert(disabledDates.length);
    $('.date-pick').datepicker({

        autoClose: true,
        format: 'mm/dd/yyyy',
        minDate: new Date(),
        position: 'top left', // Default position
        //onHide: function (inst) {
        //    inst.update('position', 'top left'); // Update the position to the default again
        //},
        onRenderCell: function (date, cellType) {
           // alert(getFormattedDate(date) + ':' + disabledDates[0] +':' + disabledDates[0].indexOf(getFormattedDate(date)));
            if (cellType === 'day') {
                var day = date.getDay(),
                    formatted = getFormattedDate(date),
                    isDisabled = disabledDays.indexOf(day) !== -1 || disabledDates[0].indexOf("'"+formatted+"'") !== -1;
                return {
                    disabled: isDisabled
                }
            }
            //if (cellType === 'day') {
            //    var disabled = false,
            //    day = date.getDay(),
            //        formatted = getFormattedDate(date);

            //    disabled = disabledDates.filter(function (date) {
            //        return date === formatted || disabledDays.indexOf(day) !== -1;
            //    }).length;

            //    return {
            //        disabled: disabled
            //    }
            //}
        },
       
        onShow: function (inst, animationComplete) {
            // Just before showing the datepicker
            if (!animationComplete) {
                var iFits = false;
                // Loop through a few possible position and see which one fits
                $.each(['top left', 'left bottom', 'bottom left', 'top center', 'bottom center'], function (i, pos) {
                    if (!iFits) {
                        inst.update('position', pos);
                        var fits = isElementInViewport(inst.$datepicker[0]);
                        if (fits.all) {
                            iFits = true;
                        }
                    }
                });
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

// Save FAQs using ajax

// Post data on save button click
function GetAllUserDuringtheRequestedLeavePeriod(startdate, enddate) {
   
    $.get("/FrontEndHome/GetAllBookedLeaves", { Startdate: startdate, Enddate: enddate }, function (data) {
        //if (data !== null) {
        //    console.log(data);
        //    //var news = data.result.newsList;
        //}
        //console.log(data);
        var htmlData = "";
        $("#checkavailibityheadertxt").hide();
        if (data.length !== 0) {
            htmlData = "<table> <thead><tr><th>Name</th><th>Start Date</th> <th>End Date</th></tr> </thead> <tbody>";
            for (var i = 0; i < data.length; i++) {
                htmlData += " <tr><td>" + data[i].employeeName + "</td>";
                htmlData += " <td>" + data[i].leaveStartDate + "</td>";
                htmlData += " <td>" + data[i].leaveEndDate + "</td></tr>";
            }
            htmlData += "</<table>";
            // alert(htmlData);
            $("#booked_leave_table").html(htmlData);
            $("#checkavailibityheadertxt").show();
        }
        else {
            $("#checkavailibityheadertxt").hide();
            $("#booked_leave_table").html("");
        }

    });
}
function CountBankHolidays(startdate, enddate, starttime,endtime) {
   
}
function GetLeaveBalance(quantity) {
    $.get("/FrontEndHome/ValidateLeaveBalanceExistOrNot", { Quantity : quantity } , function (data) {
        var d = data;
        console.log(d);
        if (d)
        {
            $('#divwithconfirm').show();
            $('#divwithcancel').hide();
        }
        else {
            $('#divwithconfirm').hide();
            $('#divwithcancel').show();
        }
    });
}
$("#btnContinue").click(function (event) {
    // Validation is ok or not
    if (validateSaveLeave(false) === false) {
        return false;
    }

        checkdatediff();
   
    
   
});
$("#btnupdatedate").click(function (event) {
    // Validation is ok or not
    if (validateSaveLeave(false) === false) {
        return false;
    }

    checkdatediff();

    var quantity = $('#hdnQuantity').val();
    GetLeaveBalance(quantity);

});
$("#btnConfirm").click(function (event) {
    $(".confirmation_popup, .confirmation_popup_overlay").fadeOut(200);
    // $("#AnswerText").val($($("div.nicEdit-main")[0]).html());
    // // Get form
    var form = $('#frmRequestLeave')[0];


    // Create an FormData object 
    var data = new FormData(form);
    var selectedLeaveTypeId = $("input[name='LeaveTypeId']:checked").val();
    data.append('SelectedLeaveTypeId', selectedLeaveTypeId);

    var quantity = $('#hdnQuantity').val();
    data.append('Quantity', quantity);

    var leaveid = $('#hdnLeaveId').val();
    data.append('LeaveID', leaveid);
    var filename = $('#hdnFilename').val();
    data.append('FileName', filename);
    var newenddate = $('#hdnNewEnddate').val();
    data.append('NewEndDate', newenddate);

    // disabled the submit button
    $("#btnSave").prop("disabled", true);

    $.ajax({
        type: "POST",
        enctype: 'multipart/form-data',
        url: "/FrontEndHome/AddLeaveRequest",
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
            //$("#btnSave").prop("disabled", false);
            onFailed(e);
        }
    });
});
$("#btnCancel").click(function (event) {
    $(".confirmation_popup, .confirmation_popup_overlay").fadeOut(200);
});
$("#btnCancel1").click(function (event) {
    $(".confirmation_popup, .confirmation_popup_overlay").fadeOut(200);
});
$("#btnRequest").click(function (event) {
  
    // Validation is ok or not
    if (validateSaveLeave(true) === false) {
        return false;
    }
    else {
        $('#lblleavetypepopup').text($('#hdnLeaveType').val());
        $(".confirmation_popup, .confirmation_popup_overlay").fadeIn(200);
    }
  

});



var movetotop = function () {
    var h1 = $(".header_top_area").height();
    var h2 = $(".top_filter_section").outerHeight();
    var h3 = h1 + h2;
    $('html, body').stop().animate({
        scrollTop: 0
    }, 1000);
};

// Validate Featured Message form
var validateSaveLeave = function (status) {
    var isValid = true;
    //// check if the form input is valid
    //if (!$("#frmRequestLeave").valid()) {
    //    $("#frmRequestLeave").submit();
    //    isValid = false;
    //}
    if (status === false) {
        if ($("#StartingFrom").val().trim().length === 0) {
            $(".error_msg").show();
            $("span[data-valmsg-for='LeaveReq.LeaveStartDate']").text("Please enter the date of your first day of leave");
            isValid = false;

            movetotop();
        }
        else {
            $("span[data-valmsg-for='LeaveReq.ReturnBackDate']").text("");
            $(".error_msg").hide();
        }
        if ($("#EndingOn").val().trim().length === 0) {
            $(".error_msg").show();
            $("span[data-valmsg-for='LeaveReq.ReturnBackDate']").text("Please enter the date that you return back to work on");
            isValid = false;

            movetotop();
        }
        else {
            $("span[data-valmsg-for='LeaveReq.ReturnBackDate']").text("");
            $(".error_msg").hide();
        }

        var radioValuestarttime = $("input[name='LeaveReq.StartTime']:checked").val();

        if (!radioValuestarttime) {
            $(".error_msg").show();
            $("span[data-valmsg-for='LeaveReq.StartTime']").text("Please select If you are leaving in the morning or afternoon");
            isValid = false;

            movetotop();
        }
        else {
            $("span[data-valmsg-for='LeaveReq.StartTime']").text("");
            $(".error_msg").hide();
        }

        var radioValueendtime = $("input[name='LeaveReq.EndTime']:checked").val();
        if (!radioValueendtime) {
            $(".error_msg").show();
            $("span[data-valmsg-for='LeaveReq.EndTime']").text("Please select If you are returning in the morning or afternoon");
            isValid = false;

            movetotop();
        }
        else {
            $("span[data-valmsg-for='LeaveReq.EndTime']").text("");
            $(".error_msg").hide();
        }
    }
    else {
        //var selectedLeaveTypeId = $("input[name='LeaveTypeId']:checked").val();
        //if (selectedLeaveTypeId.length === 0)
        //{
        //    $(".error_msg").show();
        //    $("span[data-valmsg-for='LeaveReq.fk_LeaveTypeId']").text("Please tell us your reason for booking this leave");
        //    isValid = false;
        //}
        //else {
        //    $("span[data-valmsg-for='LeaveReq.fk_LeaveTypeId']").text("");
        //}
        var leavetype = $('#hdnLeaveType').val().toLowerCase();

        if (leavetype === 'unpaid leave' || leavetype === 'paid sickness' || leavetype === 'paid medical' || leavetype === 'unpaid sickness' ||
            leavetype === 'unpaid medical' || leavetype === 'training' || leavetype === 'other') {
            var fileName = $("#upload11").val();
            if (fileName !== '') {
                $("span[data-valmsg-for='LeaveReq.FileNameData']").text("");
                var exts = ['doc', 'docx', 'xls', 'xlsx', 'pdf', 'jpg', 'jpeg', 'png'];
                var get_ext = fileName.split('.');
                get_ext = get_ext.reverse();
                if ($.inArray(get_ext[0].toLowerCase(), exts) > -1) {
                    $("span[data-valmsg-for='LeaveReq.FileNameData']").text("");
                    isValid = true;
                }
                else {
                    $("span[data-valmsg-for='LeaveReq.FileNameData']").text('Please select a valid file in (PDF, MS Word, MS Excel,JPEG, PNG) format');
                    isValid = false;
                }

            } else { // no file was selected
                $(".error_msg").show();
                $("span[data-valmsg-for='LeaveReq.FileNameData']").text("Please upload a file for this field.");
                isValid = false;
            }
        }
        else {
            $("span[data-valmsg-for='LeaveReq.FileNameData']").text("");
            isValid = true;
        }
    }
    return isValid;

};
//$("#upload11").on('change', function () {
//    isValid = false;
//    var fileName = $("#upload11").val();
//    if (fileName) {
//        var exts = ['doc', 'docx', 'xls', 'xlsx', 'pdf', 'jpge', 'png'];
//        var get_ext = fileName.split('.');
//        get_ext = get_ext.reverse();
//        if ($.inArray(get_ext[0].toLowerCase(), exts) > -1) {
//            $("span[data-valmsg-for='LeaveReq.FileNameData']").text("");
//            isValid = true;
//        }
//        else {
//            $("span[data-valmsg-for='LeaveReq.FileNameData']").text('Please select a valid file in (PDF, MS Word, MS Excel,JPEG, PNG) format');
//            isValid = false;
//        }
//    }
//    return isValid;
//});

// On begin ajax request this function will be call
var onBegin = function (xhr) {
    $(".loaderModal").show();
};


// On success of ajax callback this function will be call
var onSuccess = function (data) {

    //console.log(data);
    $(".loaderModal").hide();
    if ($('#hdnLeaveId').val() === "0") {
        window.location.href = "/intranet/whathappensnext";
        //if (data.isSuccess) {
        //    swal(
        //        'Your Leave Request has been sent.',
        //        '',
        //        'success'
        //    ).then(function () {
        //        window.location.href = "/intranet/whathappensnext";

        //    });
        //}
        //else {
        //    swal(
        //        data.message,
        //        '',
        //        'error'
        //    );
        //}
    }
    else {
        window.location.href = "/intranet/whathappensnext";
        //if (data.isSuccess) {
        //    swal(
        //        'Your leave request has been updated.',
        //        '',
        //        'success'
        //    ).then(function () {
        //        window.location.href = "/intranet/whathappensnext";

        //    });
        //}
        //else {
        //    swal(
        //        data.message,
        //        '',
        //        'error'
        //    );
        //}
    }
};

// On failed/error of ajax 
var onFailed = function (context) {
    return false;
};

//$(".confirmation_popup .rq_cancel_btn").click(function (event) {
//    event.preventDefault();
//    $(".confirmation_popup, .confirmation_popup_overlay").fadeOut(200);
//});

//$(".table_wrap .table .cancel_item").click(function (event) {
//    event.preventDefault();
//    $(".confirmation_popup, .confirmation_popup_overlay").fadeIn(200);
//});
function toTitleCase(str) {
    return str.replace(/\w\S*/g, function (txt) {
        return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase();
    });
}
$("ul li .reason_radio_input").change(function () {
    var reg1 = $(this).prop("checked");
    //alert($(this).siblings('label#lblleavetype').html().toLowerCase());
    $('#hdnLeaveType').val(toTitleCase($(this).siblings('label#lblleavetype').html().toLowerCase()));
    if ($(this).siblings('label#lblleavetype').html().toLowerCase() === "paid medical") {
        $("#divbriefdesc").css('display', 'inline-block');
        //$(this).parent().parent().children(".other_reason_field").css('display', 'inline-block');
    }
    else {
        $("#divbriefdesc").css('display', 'none');
    }
    if ($(this).siblings('label#lblleavetype').html().toLowerCase() === "other") {
        $("#divother").css('display', 'inline-block');
        //$(this).parent().parent().children(".other_reason_field").css('display', 'inline-block');
    }
    else {
        $("#divother").css('display', 'none');
    }
    if ($(this).siblings('label#lblleavetype').html().toLowerCase() === "paid sickness") {
      
        $("#divpaidsickness").css('display', 'inline-block');
        //$(this).parent().parent().children(".other_reason_field").css('display', 'inline-block');
    }
    else {
        $("#divpaidsickness").css('display', 'none');
    }
});
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
function checkdatediff() {
    var radioValuestart = $("input[name='LeaveReq.StartTime']:checked").val();
    var radioValueend = $("input[name='LeaveReq.EndTime']:checked").val();
   
    var startdate = $('#StartingFrom').val();
    var enddate = $('#EndingOn').val();
   
   
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
    //alert(getYesterdaysDate(parseDate(enddate)));
    var weekenddays = calcBusinessDays(parseDate(startdate), parseDate(newed));
    //CountBankHolidays(startdate, newed, radioValuestart.toLowerCase(), radioValueend.toLowerCase());
    $.get("/FrontEndHome/CountBankHolidays", { Startdate: startdate, Enddate: newed, StartTime: radioValuestart.toLowerCase(), EndTime: radioValueend.toLowerCase()}, function (data) {
       //debugger;
        $("#hdnBankHolidayCount").val(data);

        var quantitywithhalfday = datedifference;
        if (radioValuestart.toLowerCase() === "morning" && radioValueend.toLowerCase() === "afternoon")
            quantitywithhalfday = datedifference + 0.5;
        if (radioValuestart.toLowerCase() === "afternoon" && radioValueend.toLowerCase() === "morning")
            quantitywithhalfday = datedifference - 0.5;
        var totalbankholidays = $("#hdnBankHolidayCount").val();
        //if (radioValueend.toLowerCase() === "afternoon")
        //    weekenddays = weekenddays - 0.5;
        quantitywithhalfday = quantitywithhalfday - weekenddays - totalbankholidays;

        if (GetWeekDayName(parseDate(enddate)).toLowerCase() === 'monday' && radioValueend.toLowerCase() === "morning") {
            newed = getendDate(deductDays(parseDate(enddate), 3));
            $.get("/FrontEndHome/CheckBankHolidayByDate", { Date: newed }, function (data) {
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

            $.get("/FrontEndHome/CheckBankHolidayByDate", { Date: newed }, function (data) {

                if (data) {
                    newed = getYesterdaysDate(parseDate(newed));
                    if (GetWeekDayName(parseDate(newed)).toLowerCase() === 'sunday') {
                        newed = getendDate(deductDays(parseDate(newed), 2));
                        $.get("/FrontEndHome/CheckBankHolidayByDate", { Date: newed }, function (data1) {

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
            //if ($('#divbankholidays').html().trim().replace('.','/'))
        }
        else {
            ValidateDateFields(quantitywithhalfday, datedifference, sd, currentDate, startdate, newed, etime, radioValuestart, radioValueend);
        }
        //alert(newed);
        //alert(addDays(parseDate(startdate), quantitywithhalfday));

      
    });
    //alert(etime);
    //alert(newed);
  
 
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
function GetRequiredDateFormat(date) {
    var reqdateformat;
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
    var dateno = d.getDate();

    var month = new Array();
    month[0] = "January";
    month[1] = "February";
    month[2] = "March";
    month[3] = "April";
    month[4] = "May";
    month[5] = "June";
    month[6] = "July";
    month[7] = "August";
    month[8] = "September";
    month[9] = "October";
    month[10] = "November";
    month[11] = "December";


    var monthfullname = month[d.getMonth()];

    reqdateformat = weekdayname + " " + dateno + " " + monthfullname;
    return reqdateformat;
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
function ValidateDateFields(quantitywithhalfday, datedifference, sd, currentDate, startdate, newed, etime, radioValuestart, radioValueend) {    
    var isvalid = true;
    if (quantitywithhalfday.toString() === '0') {
        isvalid = false;
        $(".error_msg").show();
        $("span[data-valmsg-for='LeaveReq.LeaveStartDate']").text("You have selected bank holidays");

        movetotop();
    }
    if (datedifference === 0 && radioValuestart.toLowerCase() === "morning" && radioValueend.toLowerCase() === "morning") {
        isvalid = false;
        $(".error_msg").show();
        $("span[data-valmsg-for='LeaveReq.LeaveStartDate']").text("First day of leave cannot be equal to the date that you return back to work on");

        movetotop();
    }
    if (datedifference === 0 && radioValuestart.toLowerCase() === "afternoon" && radioValueend.toLowerCase() === "afternoon") {
        isvalid = false;
        $(".error_msg").show();
        $("span[data-valmsg-for='LeaveReq.LeaveStartDate']").text("First day of leave cannot be equal to the date that you return back to work on");

        movetotop();
    }
    if (datedifference === 0 && radioValuestart.toLowerCase() === "afternoon" && radioValueend.toLowerCase() === "morning") {
        isvalid = false;
        $(".error_msg").show();
        $("span[data-valmsg-for='LeaveReq.LeaveStartDate']").text("First day of leave cannot be greater than the date that you return back to work on");

        movetotop();
    }
    if (datedifference <= -1) {
        isvalid = false;
        $(".error_msg").show();
        $("span[data-valmsg-for='LeaveReq.LeaveStartDate']").text("First day of leave cannot be greater thanthe date that you return back to work on");

        movetotop();
    }
    if (sd < currentDate) {
        isvalid = false;
        $(".error_msg").show();
        $("span[data-valmsg-for='LeaveReq.LeaveStartDate']").text("First day of leave cannot be smaller than current date");

        movetotop();
    }

    if (isvalid) {
        $("span[data-valmsg-for='LeaveReq.LeaveStartDate']").text("");
        var h1 = $(".header_top_area").height();
        var h2 = $(".top_filter_section").outerHeight();
        var h3 = h1 + h2;
        // $('.leave_step_btn_1, .leave_step_1 .error_msg').hide();
        $(".leave_step_2, .leave_step_btn_2, .update_date").fadeIn(200);


        $('html, body').stop().animate({
            scrollTop: $("#leavestep2").offset().top - h3
        }, 1000);
    }
    else {
        $(".leave_step_3, .leave_step_btn_3, .view_more_leave").hide();
        $(".leave_step_2, .leave_step_btn_2, .update_date").hide(200);
    }

    var day = ' days';
    if (quantitywithhalfday <= 1)
        day = ' day';

    // alert(quantitywithhalfday - weekenddays);

    $('#lblnoofdays').text(quantitywithhalfday + day);
    $('#hdnQuantity').val(quantitywithhalfday);
    $('#lblnoofdayspopup').text(quantitywithhalfday + day);

    $('#lblleavefrom').text(GetRequiredDateFormat(parseDate(startdate)));
    $('#lblleaveto').text(GetRequiredDateFormat(parseDate($('#EndingOn').val())));
    $('#lblleavefrompopup').text(GetRequiredDateFormat(parseDate(startdate)));
    $('#lblleavetopopup').text(GetRequiredDateFormat(parseDate($('#EndingOn').val())));
    $('#hdnNewEnddate').val(newed);

    $('#lblstarttime').text(radioValuestart.toLowerCase());
    $('#lblstarttimepopup').text(radioValuestart.toLowerCase());

    $('#lblendtime').text(radioValueend.toLowerCase());
    $('#lblendtimepopup').text(radioValueend.toLowerCase());

    GetAllUserDuringtheRequestedLeavePeriod(startdate, newed);
    var quantity = $('#hdnQuantity').val();
    GetLeaveBalance(quantity);
}