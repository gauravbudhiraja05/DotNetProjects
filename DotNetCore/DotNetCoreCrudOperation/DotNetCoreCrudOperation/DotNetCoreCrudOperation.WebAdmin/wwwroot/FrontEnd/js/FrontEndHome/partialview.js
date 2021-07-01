$(document).ready(function () {

    
    $(".accordion_title").click(function () {
        var thisChildren = $(this).parent().children(".accordion_content");
        var allChildren = $(".accordion_content");
        if ($(thisChildren).css('display') == 'block') {
            $(thisChildren).slideUp("fast");
            $(".accordion_title").removeClass("active");
        } else {
            $(allChildren).slideUp("fast");
            $(".accordion_title").removeClass("active");
            $(thisChildren).slideDown("fast");
            $(this).addClass("active");
        }
    });

    $(".select_picker_area").appendTo(".append_select_pk");

    $("div[class='accordion_wapper']").filter(function (i, v) { return $.trim($(v).text()).length > 0; }).css('display', 'block');

    $("div[class='accordion_wapper']").filter(function (i, v) { return $.trim($(v).children('.SubFolderArea').text()).length == 0; }).css('display', 'none');


    $($("div[class='accordion_wapper']").children('.SubFolderArea').children('.accordion_list_area').children('.accordion_section')).filter(function (i, v) { return $.trim($(v).children('.accordion_content').children('.guidance_list').text()).length > 0; }).css('display', 'block');

    //$("div[class='accordion_wapper']").filter(function(i, v) { return $.trim($(v).text()).length > 0; }).css('display', 'block');

    $("div[class='accordion_wapper']").filter(function(i, v) {

    //    return $(v).children('.SubFolderArea').children('.accordion_list_area').length == 1 ;
        return $.trim($(v).children('.SubFolderArea').children('.accordion_list_area').children('.accordion_section').children('.accordion_content').children('.guidance_list').text()).length == 0;

    }).css('display', 'none');

    $(".AddMyFavIcon").on("click", function () {
        debugger;
        $(this).css('background-position', 'left top');

        var docId = this.id;
        var userId = $('#FrontEndUserDetails_Id').val();
        $.ajax({
            type: "Post",
            url: "/FrontEndHome/AddMyFavouriteDocument",
            data: { docId: docId, userId: userId },
            success: function(Data) {

                onSuccess(Data);
            },
            error: function(response) {

            }
        });
    })

    
});


function onSuccess(data) {
    var d = data;
    if (d.result.isSuccess) {

        swal({
            title : d.result.message,
            width: '500px'
        });
    }
    else
    {

    }

}


