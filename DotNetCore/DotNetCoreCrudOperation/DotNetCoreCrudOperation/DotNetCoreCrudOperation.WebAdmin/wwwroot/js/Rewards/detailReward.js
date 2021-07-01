$(document).ready(function () {

    $(".reward_rept_item").show();
    
    $('.send_award .btn_field').prop("disabled", false);

    $("#RecipientByNamesearchBtn").click(function () {

        var userName = $("#News_Title").val();

        if (userName.trim() != "") {
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
        } else {
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


    });

    $(".reward_rept__list_wrap").on('click', 'div.reward_rept_list a.reward_star_link', function () {


        var bgImage = $(this).siblings("div.reward_star_img").css('background-image');
        bgImage = bgImage.replace('url(', '').replace(')', '').replace(/\"/gi, "");
        bgImage = bgImage.substring(bgImage.lastIndexOf("/") + 1, bgImage.length);

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
        htmlData += '<div><strong>Reward Recipient:<span class="recipientName"> ' + name + '</span></strong></div>';
        htmlData += ' <div>' + post + '</div>';
        htmlData += '<div style="margin-bottom: 10px;">' + place + '</div>';
        htmlData += '<div style="font-size: 14px;"><a href="#">' + email + '</a></div>';
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

    $("#btnSave").on('click', function () {

        var rewardId = $("#Id").val();
        UpSertRewardInDB(rewardId);

    });

    $("#popUpSaveFormAndMail").on('click', function () {

        var rewardId = $("#Id").val();
        UpSertRewardWithMail(rewardId);
    });

});

function SearchUserListOnEnterForRewardEdit(e) {
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

function UpSertRewardInDB(rewardId) {

    var rewardData = MakeDataObjectOfFormValues(rewardId);

    $.ajax({
        type: "Post",
        url: "/Reward/UpsertReward",
        data: rewardData,
        success: function (result) {
            onSuccess(result);
        },
        error: function (response) {
            onFailed(response);
        }
    });

}

function MakeDataObjectOfFormValues(rewardId) {


    var recipientId = $("div.reward_rept_item").children("div.rept_item_img").children('#hiddenIdSelectedEndUser').attr('val');
    var recipientImage = $("div.reward_rept_item").children("div.rept_item_img").children('img').attr('src');
    var ValueName = $("#ValueId option:selected").text();
    var AwardId = $("#AwardId option:selected").attr('value');
    var testimonial = $("#Testimonial").val();
    var thankYouMsg = $("#ThankYouMsg").val();
    var recipientName = $(".recipientName").text();

    var Data = {

        Id: rewardId,
        RecipientId: recipientId,
        ValueName: ValueName,
        AwardId: AwardId,
        Testimonial: testimonial,
        ThankYouMsg: thankYouMsg,
        RecipientImage: recipientImage,
        RecipientName:recipientName
    }

    return Data;

}


function GetUserListByName(userName) {

    // alert(userName);

    $.ajax({
        type: "Post",
        url: "/Reward/GetUserListByName",
        data: { userName: userName.trim() },
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


// On begin ajax request this function will be call
var onBegin = function (xhr) {
    //$(".loaderModal").show();
};


// On success of ajax callback this function will be call
var onSuccess = function (context) {
    //$(".loaderModal").hide();
    swal(
        'The reward has been edited successfully.',
        '',
        'success'
    ).then(function () {
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