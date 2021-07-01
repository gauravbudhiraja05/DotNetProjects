$(document).ready(function () {

    $("#RecipientByNamesearchBtn").click(function () {

        var userName = $("#News_Title").val();
       
        if (userName.trim() != "")
        {
            GetUserListByName(userName);
        }
    });

    $('.rewards_testi-1 textarea').on('keyup change', function () {
        var text2 = $('.rewards_testi-2 textarea').val();
        var selectedValueoption = $("#ValueId option:selected").text();
        var selectedAwardoption = $("#AwardId option:selected").text();
        var selectedUserId = $("div.reward_rept_item").children("div.rept_item_img").children('#hiddenIdSelectedEndUser').attr('val');
        if (selectedUserId == undefined) selectedUserId = "";

        if (text2.trim() != "" && $(this).val().trim() != "" && selectedUserId != "" && selectedValueoption != "Select Values" && selectedAwardoption != "Select Reward type") {
            $('.form_field .btn_field').prop("disabled", false);
            $('#btnSave').prop("disabled", false);
        }

        else {
            $('.form_field .btn_field').prop("disabled", true);
            $('#btnSave').prop("disabled", true);
        }

    });
    $('.rewards_testi-2 textarea').on('keyup change', function () {
        var text2 = $('.rewards_testi-1 textarea').val();
        var selectedValueoption = $("#ValueId option:selected").text();
        var selectedAwardoption = $("#AwardId option:selected").text();
        var selectedUserId = $("div.reward_rept_item").children("div.rept_item_img").children('#hiddenIdSelectedEndUser').attr('val');
        if (selectedUserId == undefined) selectedUserId = "";
        if (text2.trim() != "" && $(this).val().trim() != "" && selectedUserId != "" && selectedValueoption != "Select Values" && selectedAwardoption != "Select Reward type") {
            $('.send_award .btn_field').prop("disabled", false);
            $('#btnSave').prop("disabled", false);
        } else {
            $('.send_award .btn_field').prop("disabled", true);
            $('#btnSave').prop("disabled", true);
        }

    });

    $("#ValueId").on('change', function () {

        var selectedValueoption = $("#ValueId option:selected").text();
        var selectedAwardoption = $("#AwardId option:selected").text();
        var text2 = $('.rewards_testi-2 textarea').val();
        var text1 = $('.rewards_testi-1 textarea').val();
        var selectedUserId = $("div.reward_rept_item").children("div.rept_item_img").children('#hiddenIdSelectedEndUser').attr('val');
        if (selectedUserId == undefined) selectedUserId = "";

        if (text2.trim() != "" && text1.trim() != "" && selectedUserId != "" && selectedValueoption != "Select Values" && selectedAwardoption != "Select Reward type") {
            $('.form_field .btn_field').prop("disabled", false);
            $('#btnSave').prop("disabled", false);
        } else {
            $('.form_field .btn_field').prop("disabled", true);
            $('#btnSave').prop("disabled", true);
        }
    });
    
    $("#AwardId").on('change', function () {

        var selectedValueoption = $("#ValueId option:selected").text();
        var selectedAwardoption = $("#AwardId option:selected").text();
        var text2 = $('.rewards_testi-2 textarea').val();
        var text1 = $('.rewards_testi-1 textarea').val();
        var selectedUserId = $("div.reward_rept_item").children("div.rept_item_img").children('#hiddenIdSelectedEndUser').attr('val');
        if (selectedUserId == undefined) selectedUserId = "";

        if (text2.trim() != "" && text1.trim() != "" && selectedUserId != "" && selectedValueoption != "Select Values" && selectedAwardoption != "Select Reward type") {
            $('.form_field .btn_field').prop("disabled", false);
            $('#btnSave').prop("disabled", false);
        } else {
            $('.form_field .btn_field').prop("disabled", true);
            $('#btnSave').prop("disabled", true);
        }

        // Show and hide the Reward Amount options
        switch (selectedAwardoption) {
            case 'Love 2 Shop Gift Voucher':
            case 'Amazon Gift Voucher':
            case 'iTunes Voucher':
            case 'Experience of your choice':
                $("#divRewardAmountDropDown").show();
                $("#divRewardAmountTextBox").hide();
                break;
            case 'Team Meal':
            case 'Commission Amount':
                $("#divRewardAmountDropDown").hide();
                $("#divRewardAmountTextBox").show();

                // Disable "Save" and "Send Award" button if Amount is not valid
                var rewardId = $("#AwardId option:selected").val();
                var rewardData = MakeDataObjectOfFormValues(rewardId);
                if (validateReward(rewardData) == false) {
                    $('.form_field .btn_field').prop("disabled", true);
                    $('#btnSave').prop("disabled", true);
                }
                break;
            case 'Recognise Only':
            case 'An extra day\'s holiday':
                $("#divRewardAmountDropDown").hide();
                $("#divRewardAmountTextBox").hide();
            default:
                $("#divRewardAmountDropDown").hide();
                $("#divRewardAmountTextBox").hide();
        }
        

    });

    $("#RewardAmountVariable").on("keyup", function () {
       
        var selectedValueoption = $("#ValueId option:selected").text();
        var selectedAwardoption = $("#AwardId option:selected").text();
        var text2 = $('.rewards_testi-2 textarea').val();
        var text1 = $('.rewards_testi-1 textarea').val();
        var selectedUserId = $("div.reward_rept_item").children("div.rept_item_img").children('#hiddenIdSelectedEndUser').attr('val');
        if (selectedUserId == undefined) selectedUserId = "";
        var isRewardAmtValid = checkRewarAmtValidOrNot();

        if (text2.trim() != "" && text1.trim() != "" && selectedUserId != "" && selectedValueoption != "Select Values" && selectedAwardoption != "Select Reward type" && isRewardAmtValid) {
            $('.form_field .btn_field').prop("disabled", false);
            $('#btnSave').prop("disabled", false);
        } else {
            $('.form_field .btn_field').prop("disabled", true);
            $('#btnSave').prop("disabled", true);
        }

    });

    

    $(".reward_rept__list_wrap").on('click', 'div.reward_rept_list a.reward_star_link', function () {

        debugger;
        var bgImage = $(this).siblings("div.reward_star_img").css('background-image');
        bgImage = bgImage.replace('url(', '').replace(')', '').replace(/\"/gi, "");
        bgImage = bgImage.substring(bgImage.lastIndexOf("/") + 1, bgImage.length);

        // Get EmailId of Recipient
        var $item = $(this).siblings()[1]
        var recipientEmailAdddress = $($item).find("div.reward_star_emailAddress").text()

        var name = post = place = email = "";

        name = $(this).siblings("div.reward_star_text").children("div.reward_star_name").text();
        post = $(this).siblings("div.reward_star_text").children("div.reward_star_post").text();
        place = $(this).siblings("div.reward_star_text").children("div.reward_star_loc").text();
        var UserId = $(this).siblings("div.reward_star_text").children("input.hiddenIdEndUser").attr('val');

        $(".reward_rept_item").html('');

        var htmlData = '<div class="rept_item_img">';
        htmlData += '<img src="/fileserver/Uploads/Images/FrontEndUser/' + bgImage + '" alt="" />';
        htmlData += '<input id="hiddenIdSelectedEndUser" type="hidden" val="' + UserId + '" />';
        htmlData += '</div>';
        htmlData += '<div class="rept_item_text">';
        htmlData += '<div><strong>Reward Recipient: <span class="recipientName">' + name +'</span></strong></div>';
        htmlData += ' <div>' + post +'</div>';
        htmlData += '<div style="margin-bottom: 10px;">' + place +'</div>';
        htmlData += '<div style="font-size: 14px;"><a href="#">' + recipientEmailAdddress + '</a></div>';
        htmlData += '</div>';

        $(".reward_rept_item").html(htmlData);
        $(".reward_rept_item").show();

      

        $(".reward_rept__list_wrap").fadeOut(200);
        $(".reward_rept_item").fadeIn(200);


        var selectedValueoption = $("#ValueId option:selected").text();
        var selectedAwardoption = $("#AwardId option:selected").text();
        var text2 = $('.rewards_testi-2 textarea').val();
        var text1 = $('.rewards_testi-1 textarea').val();
        var selectedUserId = $("div.reward_rept_item").children("div.rept_item_img").children('#hiddenIdSelectedEndUser').attr('val');
        if (selectedUserId == undefined) selectedUserId = "";

        if (text2.trim() != "" && text1.trim() != "" && selectedUserId != "" && selectedValueoption != "Select Values" && selectedAwardoption != "Select Reward type") {
            $('.form_field .btn_field').prop("disabled", false);
            $('#btnSave').prop("disabled", false);
        } else {
            $('.form_field .btn_field').prop("disabled", true);
            $('#btnSave').prop("disabled", true);
        }

    });

    $("#btnSendEmail").on('click', function () {

        var selectedAward = $("#AwardId option:selected").text();

        if (selectedAward == 'Recognise Only') {
            swal({
                title: 'Are you sure you want to recognise this team member? If so, they will receive their thank you email shortly.',
                text: "",
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes',
                cancelButtonText: 'No',
                width: '500px'
            }).then(function (result) {

                if (result.value) {

                    var rewardId = 0;
                    UpSertRewardWithMail(rewardId);
                }
            });
        }

        else {
            swal({
                title: 'Are you sure you want to send this reward? If so, the Reward Recipient will receive their thank you email and HR will be notified to fulfil this award.',
                text: "",
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes',
                cancelButtonText: 'No',
                width: '500px'
            }).then(function (result) {

                if (result.value) {

                    var rewardId = 0;
                    UpSertRewardWithMail(rewardId);
                }
            });
        }
        
    });


    $("#btnSave").on('click', function () {

        var rewardId = 0;
        UpSertRewardInDB(rewardId);

    });
    
});


function SearchUserListOnEnterForRewardAdd(e) {
    var key = e.keyCode || e.which;
    if (key == 13) {

        //var userName = $("#News_Title").val();

        //if (userName.trim() != "") {
        //    GetUserListByName(userName);
        //}

        //e.stopImmediatePropagation();
        $("#RecipientByNamesearchBtn").trigger("click");

    }
}


function UpSertRewardWithMail(rewardId) {

    $(".loaderModal").show();
    var rewardData = MakeDataObjectOfFormValues(rewardId);

    if (validateReward(rewardData) == false) {
        $(".loaderModal").hide();
        return false;
    }
    else {

        $.ajax({
            type: "Post",
            url: "/Reward/UpsertRewardAndsendMail",
            data: rewardData,
            success: function (result) {

                $(".loaderModal").hide();

                if (result.isSuccess == true) {
                    onSuccess(result);
                }

                else {
                    onFailed(result.message);
                }
            },
            error: function (response) {

                $(".loaderModal").hide();
                onFailed(result.message);
            }
        });
    }
}

function UpSertRewardInDB(rewardId) {

    $(".loaderModal").show();
    var rewardData = MakeDataObjectOfFormValues(rewardId);

    $.ajax({
        type: "Post",
        url: "/Reward/UpsertReward",
        data: rewardData,
        success: function (result) {

            $(".loaderModal").hide();

            if (result.isSuccess = true) {
                onSuccessSave(result);
            }

            else {
                onFailed(response);
            }
        },
        error: function (response) {

            $(".loaderModal").hide();
            onFailed(response);
        }
    });

}

function MakeDataObjectOfFormValues(rewardId) {


    var recipientId = $("div.reward_rept_item").children("div.rept_item_img").children('#hiddenIdSelectedEndUser').attr('val');
    var recipientImage = $("div.reward_rept_item").children("div.rept_item_img").children('img').attr('src');
    //var recipientPosition = $("div.reward_rept_item").children("div.rept_item_img").children('#hiddenIdSelectedEndUser').attr('val');;
    //var recipientPlace = $("div.reward_rept_item").children("div.rept_item_img").children('#hiddenIdSelectedEndUser').attr('val');;
    var ValueName = $("#ValueId option:selected").text();
    var AwardId = $("#AwardId option:selected").attr('value');
    var testimonial = $("#Testimonial").val();
    var thankYouMsg = $("#ThankYouMsg").val();
    var recipientName = $(".recipientName").text();
    var rewardAmount = getRewardAmount();

    

    var Data = {

        Id: rewardId,
        RecipientId: recipientId,
        ValueName: ValueName,
        AwardId: AwardId,
        Testimonial: testimonial,
        ThankYouMsg: thankYouMsg,
        RecipientImage: recipientImage,
        RecipientName: recipientName,
        RewardAmount: rewardAmount
    }

    return Data;

}

function getRewardAmount() {
    var selectedAwardoption = $("#AwardId option:selected").text();
    var rewardAmount = 0;
    switch (selectedAwardoption) {
        case 'Love 2 Shop Gift Voucher':
        case 'Amazon Gift Voucher':
        case 'iTunes Voucher':
        case 'Experience of your choice':
            rewardAmount = $("#RewardAmount option:selected").val();
            break;
        case 'Team Meal':
        case 'Commission Amount':
            rewardAmount = $("#RewardAmountVariable").val();
            break;
        case 'Recognise Only':
        case 'An extra day\'s holiday':
            rewardAmount = 0;
        default:
            rewardAmount = 0;
    }

    return rewardAmount;
}

function validateReward(rewardData) {
    var isValid = true;
    if (rewardData.AwardId == "0") {

        isValid = false;
        $("#lblAwardError").html("Please Select Reward type");
    }

    else {
        $("#lblAwardError").html("");
    }

    // Check Reward Amt and validate it
    var isRewardAmtTextboxDisable = $("#RewardAmountVariable").prop("disabled");

    if (isRewardAmtTextboxDisable == false) {
        var isRewardAmtValid = checkRewarAmtValidOrNot();
        isValid = isRewardAmtValid;
    }

    return isValid;
}

function GetUserListByName(userName) {

   // alert(userName);

    $.ajax({
        type: "Post",
        url: "/Reward/GetUserListByName",
        data: { userName: userName.trim()},
        success: function (result) {

            for (var i = 0; i < result.length; i++) {

                if (result[i].photo == "" || result[i].photo == null) {

                    result[i].photo = "defaultuser.png";
                }

            }



            var htmlData = "";
            if (result != null) {
                if (result.length > 0) {

                    $('.send_award .btn_field').prop("disabled", true);
                    $('#btnSave').prop("disabled", true);
                    $("#hiddenIdSelectedEndUser").val('');
                    $(".reward_rept_item").html('');
                    $(".reward_rept_item").hide();
                    $(".reward_rept__list_wrap").html('');
                }

                for (var i = 0; i < result.length; i++) {

                    htmlData += '<div class="reward_rept_list">';
                    htmlData += '<a href="#" class="reward_star_link"></a>';
                    htmlData += '<div class="reward_star_img" style="background-image: url(/fileserver/Uploads/Images/FrontEndUser/' + result[i].photo + ');"></div>';
                    htmlData += '<div class="reward_star_text">';
                    htmlData += '<div class="reward_star_name">' + result[i].firstName + " " + result[i].surName + '</div>';
                    htmlData += '<div class="reward_star_post">' + result[i].jobTitle + '</div>';
                    htmlData += '<input class="hiddenIdEndUser" type="hidden" val="' + result[i].id + '" />';
                    htmlData += '<div class="reward_star_loc">' + result[i].myDepartmentName + '</div>';
                    htmlData += '<div class="reward_star_emailAddress">' + result[i].emailAddress + '</div>';
                    htmlData += '</div>';
                    htmlData += '</div>';

                }
                $(".reward_rept__list_wrap").html(htmlData);
            }


        },
        error: function (response) {

        }
    });
}

// Check Reward Amount is valid or not
function checkRewarAmtValidOrNot() {
    debugger;
    var isValid = true;
    var isRewardTextBoxVisible = $('#divRewardAmountTextBox:visible').length > 0 ? true : false;

    // If amount is enter through text box
    if (isRewardTextBoxVisible == true) {
        var rewardAmt = $('#RewardAmountVariable').val().trim();

        if (rewardAmt == '') {
            isValid = false;
            $("#lblRewardAmountTextBoxError").html("Please enter amount.");
        }
        else if (parseInt(rewardAmt) <= 0 || parseInt(rewardAmt) > 100) {
            isValid = false;
            var amount = parseInt(rewardAmt);
            if (amount <= 0) {
                $("#lblRewardAmountTextBoxError").html("Please enter valid amount.");
            }

            if (amount > 100) {
                $("#lblRewardAmountTextBoxError").html("Please enter less than or equal to £100.");
            }
        }

        else {
            $("#lblRewardAmountTextBoxError").html("");
        }
    }

    return isValid;
}

// On begin ajax request this function will be call
var onBegin = function (xhr) {
    //$(".loaderModal").show();
};


// On success of ajax callback this function will be call
var onSuccess = function (context) {
    //$(".loaderModal").hide();
    swal(
        'The reward has been submitted successfully.',
        '',
        'success'
    ).then(function () {
        window.location.href = "/Reward/Index";

    });
};

var onSuccessSave = function (context) {
    //$(".loaderModal").hide();
    swal({
        title: 'The reward has been saved successfully but will not be sent until you click the "Send Award" button',
        text: '',
        type: 'success',
        width: '500px'
    }).then(function () {
        window.location.href = "/Reward/Index";

    });
};

// On failed/error of ajax 
var onFailed = function (msg) {
    swal(
        msg,
        '',
        'error'
    ).then(function () {
        window.location.href = "/Reward/Index";

    });
};
