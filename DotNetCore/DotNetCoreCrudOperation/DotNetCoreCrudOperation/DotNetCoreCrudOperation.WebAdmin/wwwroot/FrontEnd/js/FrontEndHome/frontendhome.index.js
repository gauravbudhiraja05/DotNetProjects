var GlobalUserArray = "";
var DocumentSearchFlagOpenWindow = 0;
var FAQSearchFlagOpenWindow = 0;
$(document).ready(function () {
    var availableTags = [];
    var availableDocs = [];
    var options = null;
    var optionsDocuments = null;
    
    $.get('/FrontEndHome/GetFaqsQuestions/', function (data) {

        if (data != null) {
            availableTags = [];
            for (var i = 0; i < data.length; i++) {

                availableTags.push(data[i].faQsQuestionText);

            }

            options = {
                source: availableTags,
                minLength: 2,
                select: function (event, ui) {
                    var paramSelecter = "";
                    var searchVal = ui.item.value;
                    var inputBoxId = event.target.attributes.id.nodeValue;
                    if (inputBoxId == "tags") {
                        paramSelecter = "#topFaqSearchBtn";
                        SearchBySelectOfTopFaqAutoComplete(paramSelecter, searchVal);
                    }
                    else if (inputBoxId == "BottomFaqSearchInput") {
                        SearchBySelectOfBottomFaqAutoComplete(searchVal);
                    }
                }
            };
        }
        // availableTags = availableTags.join(", ")
        //console.log(availableTags);
    });

    $.get('/FrontEndHome/GetDocumentsTitle/', function (data) {

        if (data != null) {
            availableDocs = [];
            for (var i = 0; i < data.length; i++) {

                availableDocs.push(data[i].documentTitle);

            }

            optionsDocuments = {
                source: availableDocs,
                minLength: 2,
                select: function (event, ui) {

                    var searchVal = ui.item.value;

                    var paramSelecter = "";
                    var inputBoxId = event.target.attributes.id.nodeValue;
                    if (inputBoxId == "TopSearchDocumentInput") {
                        paramSelecter = "#TopSearchDocumentBtn";
                        SeachBySelectOfTopDocumentAutoComplete(paramSelecter, searchVal);
                    }
                    else if (inputBoxId == "BottomSearchDocInput") {
                        SearchBySelectOfBottomDocumentAutoComplete(searchVal);
                    }


                    //e.stopImmediatePropagation();
                }
            };
        }
        // availableTags = availableTags.join(", ")
        //console.log(availableTags);
    });



    // Check auto complete faqs

    var selector = '.tagsFaqAutoSearch';
    $(document).on('keydown.autocomplete', selector, function () {
        // console.log(options);

        $(this).autocomplete(options);
    });

    // Check auto complete faqs

    var selectorDocs = '.tagsDocsAutoSearch';
    $(document).on('keydown.autocomplete', selectorDocs, function () {
        // console.log(optionsDocuments);

        $(this).autocomplete(optionsDocuments);
    });

    //$("#tags").autocomplete({
    //    source: availableTags
    //});
    //console.log(availableTags);
    var date = new Date();
    var options = {
        weekday: "long", day: "numeric", month: "long",
        year: "numeric"
    };

    $('.date').html(date.toLocaleDateString("en-GB", options));

    $('#findALocation').on('click', function () {

        $("select#PostalCode").prop('selectedIndex', 0);
    });

    if ($('#FrontEndUserDetails_MyDepartmentId').val() != undefined && $('#FrontEndUserDetails_MyDepartmentId').val() != null && $('#FrontEndUserDetails_MyDepartmentId').val() != "") {
        //$('#ddlEndUser_Department').val($(this).val());
        //$("#ddlEndUser_Department").val('');
        $('#ddlEndUser_Department option[value="' + $('#FrontEndUserDetails_MyDepartmentId').val() + '"]').attr('selected', 'selected');
    }

    $('#ddlEndUser_Department').on('change', function () {
        $('#FrontEndUserDetails_MyDepartmentId').val($(this).val());
    });

    $(".edit_profile_btn").on('click', function () {

        $("span[data-valmsg-for='FrontEndUserDetails.UploadImage']").text("");
        $("span[data-valmsg-for='FrontEndUserDetails.FirstName']").text("");
        $("span[data-valmsg-for='FrontEndUserDetails.SurName']").text("");
        $("span[data-valmsg-for='FrontEndUserDetails.JobTitle']").text("");
        $("span[data-valmsg-for='FrontEndUserDetails.Department']").text("");
        $("span[data-valmsg-for='FrontEndUserDetails.Location']").text("");
        $("span[data-valmsg-for='FrontEndUserDetails.EmailAddress']").text("");
        $("span[data-valmsg-for='FrontEndUserDetails.TelephoneNumber']").text("");
        $("span[data-valmsg-for='FrontEndUserDetails.Mobile']").text("");
        $("span[data-valmsg-for='FrontEndUserDetails.MyDepartmentName']").text("");

    });

    //input:file[class=upload_btn]
    $("#upload1").change(function (e) {
        e.stopImmediatePropagation();
        var FileUpload = $(this).attr('id');
        if (this.files && this.files[0]) {
            var fileNameUser = this.files[0].name;
            if (CheckFileTypeExtensionFrontEnd(this.files[0].name)) {
                var reader = new FileReader();
                reader.onload = function (e) {

                    //$("#" + FileUpload).next("img").attr('src', e.target.result);
                    $("#" + FileUpload).next("div#bg_Upload_img1").css('background-image', 'url("' + reader.result + '")');
                    //$("span[data-valmsg-for='FrontEndUserDetails.UploadImage']").text("");
                }
                reader.readAsDataURL(this.files[0]);
            }
            else {
                //$("span[data-valmsg-for='FrontEndUserDetails.UploadImage']").text("Please browse a image.");
                //$("#" + FileUpload).next("img").attr('src', '/FrontEnd/images/defaultuser.png');
                $("#" + FileUpload).next("div#bg_Upload_img1").css('background-image', 'url(/fileserver/Uploads/Images/FrontEndUser/defaultuser.png');
                //$("#img1").attr("src", "/Uploads/Images/FrontEndUser/defaultuser.png");
                $(this).val('');

            }
        }

    });

    $('#btnEditProfile').on('click', function () {

        //stop submit the form, we will post it manually.
        //event.preventDefault();

        // Validation is ok or not
        if (CheckProfileEditFormValid() == false) {
            return false;
        }

        // Create an FormData object 
        var data = new FormData();

        // If you want to add an extra field for the FormData
        data.append('file', $('#upload1')[0].files[0]);
        data.append('Id', $('#FrontEndUserDetails_Id').val());
        data.append('FirstName', $('#FrontEndUserDetails_FirstName').val());
        data.append('SurName', $('#FrontEndUserDetails_SurName').val());
        data.append('JobTitle', $('#FrontEndUserDetails_JobTitle').val());
        data.append('Location', $('#FrontEndUserDetails_Location').val());
        data.append('MyDepartmentName', $("#ddlEndUser_Department option:selected").text());
        data.append('EmailAddress', $('#FrontEndUserDetails_EmailAddress').val());
        data.append('Password', $('#FrontEndUserDetails_Password').val());
        data.append('TelephoneNumber', $('#FrontEndUserDetails_TelephoneNumber').val());
        data.append('Mobile', $('#FrontEndUserDetails_Mobile').val());
        data.append('Photo', $('#FrontEndUserDetails_Photo').val());
        data.append('MyDepartmentId', $('#ddlEndUser_Department').val());
        data.append('WindowsUserId', $('#ddlEndUser_WindowsUserId').val());


        // disabled the submit button
        $("#btnEditProfile").prop("disabled", true);

        $.ajax({
            type: "POST",
            enctype: 'multipart/form-data',
            url: "/FrontEndHome/SaveUserProfile",
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
                $("#div_user_logged").removeClass("open_logged_drop");
                $(".user_logged_drop").hide();
                $("#btnEditProfile").prop("disabled", false);
                $('#FrontEndUserDetails_MyDepartmentId').val($('#ddlEndUser_Department').val());
                debugger;
                onSuccessEditProfile(data);
            },
            error: function (e) {
                $("#btnEditProfile").prop("disabled", false);
                onFailed(e);
            }
        });
    });

    $('#div_Location').hide();

    $('#PostalCode').on('change', function () {
        var selectedOption = this.value;
        $.ajax({
            type: "Post",
            url: "/FrontEndHome/GetLocationDetails",
            data: { Code: selectedOption },
            success: function (result) {

                $('#span_SalesCenter').html("");
                $('#span_CustomerNumber').html("");
                $('#span1_GroupSalesManager').html("");
                $('#span2_GroupSalesManager').html("");
                $('#span_OperationsLocation').html("");
                $('#span_BookingDeptCode').html("");
                $('#span1_CustomerServiceManager').html("");
                $('#span2_CustomerServiceManager').html("");
                $('#span1_AreaManager').html("");
                $('#span2_AreaManager').html("");
                $('#span1_ResourceQualtiyManager').html("");
                $('#span2_ResourceQualtiyManager').html("");
                $('#span1_OperationalContact').html("");
                $('#span2_OperationalContact').html("");

                var salesCentre = result.details.salesCentre;
                var customerNumber = result.details.customerNumber;
                var groupSalesManager = result.details.groupSalesManager;
                var operationsLocation = result.details.operationsLocation;
                var bookingDeptCode = result.details.bookingDeptCode;
                var customerServiceManager = result.details.customerServiceManager;
                var areaManager = result.details.areaManager;
                var resourceQualityManager = result.details.resourceQualityManager;
                var operationalContact = result.details.operationalContact;

                $('#div_Location').show();


                $('#span_SalesCenter').html(salesCentre);
                $('#span_CustomerNumber').html(customerNumber);
                $('#span1_GroupSalesManager').html(groupSalesManager.split('-')[0]);
                $('#span2_GroupSalesManager').html(groupSalesManager.split('-')[1]);
                $('#span_OperationsLocation').html(operationsLocation);
                $('#span_BookingDeptCode').html(bookingDeptCode);
                $('#span1_CustomerServiceManager').html(customerServiceManager.split('-')[0]);
                $('#span2_CustomerServiceManager').html(customerServiceManager.split('-')[1]);
                $('#span1_AreaManager').html(areaManager.split('-')[0]);
                $('#span2_AreaManager').html(areaManager.split('-')[1]);
                $('#span1_ResourceQualtiyManager').html(resourceQualityManager.split('-')[0]);
                $('#span2_ResourceQualtiyManager').html(resourceQualityManager.split('-')[1]);
                $('#span1_OperationalContact').html(operationalContact.split(/-(.+)/)[0]);
                $('#span2_OperationalContact').html(operationalContact.split(/-(.+)/)[1]);

            },
            error: function (response) {
                alert('eror');
            }
        });
    });

    $('.read_allnews').on('click', function () {
        //window.location.href = '/FrontEndHome/IntranetNews';
        window.location.href = '/intranet/news';
    });

    $('.our-values').on('click', function () {
        //window.location.href = '/FrontEndHome/IntranetOurValues';
        window.location.href = '/intranet/our_values';
    });

    //$('.my_department').on('click', function () {
    //    window.location.href = '/FrontEndHome/IntranetDepartment';
    //});

    $('.my-vacancies').on('click', function () {
        //window.location.href = '/FrontEndHome/IntranetVacancies';
        window.location.href = '/intranet/vacancies';
    });

    $('.my_index').on('click', function () {
        //window.location.href = '/FrontEndHome/Index';
        window.location.href = '/index';
    });

    $('.vacancy_detail').on('click', function () {
        var VacancyId = this.id;
        var VacancyTitle = $(this).attr('mytag');
        //window.location.href = '/FrontEndHome/IntranetVacanciesDetail/' + VacancyId;
        window.location.href = '/intranet/vacancies/' + VacancyId + '/' + VacancyTitle;
    });

    $('.news_detail').on('click', function () {
        var NewsId = this.id;
        var NewsTitle = $(this).attr('mytag');
        //window.location.href = '/FrontEndHome/IntranetNewsDetail/' + NewsId;
        window.location.href = '/intranet/news/' + NewsId + '/' + NewsTitle;
    });


    //Search Document region starts

    //$(".tagsDocsAutoSearch").on('select', function () {

    //    alert("doc name selected");
    //    //e.stopImmediatePropagation();
    //});


    $("#TopSearchDocumentBtn").on('click', function (e) {

        var result = WindowDocumentOpenClose(this);
        if (result == true) {
            $("#ZerosearchDocumnetTextHeading").hide();
            var searchText = $("#TopSearchDocumentInput").val().trim();
            if (searchText != "") {
                $("#searchDocumnetTextHeading").show();
                $("#BottomSearchDocInput").val(searchText);
                SearchDocument(searchText);
            }
            else {
                $("#searchDocumnetTextHeading").hide();
                SearchDocument(searchText);
            }
        }
        else {
            $("#TopSearchDocumentInput").val('');
            $("#BottomSearchDocInput").val('');
            $("#searchDocumnetTextHeading").hide();
            $("#ZerosearchDocumnetTextHeading").hide();
            SearchDocument('');
        }

        e.stopImmediatePropagation();
    });

    $("#BottomSearchDocBtn").on('click', function (e) {

        $("#ZerosearchDocumnetTextHeading").hide();
        var searchText = $("#BottomSearchDocInput").val().trim();
        if (searchText != "") {

            $("#searchDocumnetTextHeading").show();
            $("#TopSearchDocumentInput").val(searchText);
            SearchDocument(searchText);
        }
        else {
            $("#searchDocumnetTextHeading").hide();
        }
        e.stopImmediatePropagation();
    });

    $("#bottomDocumentSearchClose").on('click', function (e) {

        DocumentSearchFlagOpenWindow = 0;
        $(".search_anything_filter").slideUp();
        $('#search_anything_filter').removeClass("active_item");
        $('.top_filter_section').removeClass("show_item_overlay");

        $("#TopSearchDocumentInput").val('');
        $("#BottomSearchDocInput").val('');
        $("#searchDocumnetTextHeading").hide();
        $("#ZerosearchDocumnetTextHeading").hide();
        SearchDocument('');
        e.stopImmediatePropagation();
    });


    //Search Document region ends

    //search Faq region Starts

    $("#topFaqSearchBtn").on('click', function (e) {

        var result = WindowFAQOpenClose(this);
        if (result == true) {
            var searchText = $(".topFaqSearchInput").val().trim();
            if (searchText != "") {
                $("#BottomFaqSearchInput").val(searchText);
                SearchFaq(searchText);
            }
            else {
                SearchFaq(searchText);
            }
        }
        else {
            $(".topFaqSearchInput").val('');
            $("#BottomFaqSearchInput").val('');
            SearchFaq('');


        }

        e.stopImmediatePropagation();
    });

    $("#btnFaqsearchClose").on('click', function (e) {

        FAQSearchFlagOpenWindow = 0;
        $(".how_do_filter").slideUp();
        $('#how_do_filter').removeClass("active_item");
        $('.top_filter_section').removeClass("show_item_overlay");

        $(".topFaqSearchInput").val('');
        $("#BottomFaqSearchInput").val('');
        SearchFaq('');
        e.stopImmediatePropagation();
    });

    $("#BottomFaqSearchBtn").on('click', function (e) {

        var searchText = $("#BottomFaqSearchInput").val().trim();
        if (searchText != "") {

            $(".topFaqSearchInput").val(searchText);
            SearchFaq(searchText);
        }
        e.stopImmediatePropagation();
    });

    //search faq region Ends


    //search by person starts

    $("#find_person_filter").click(function () {

        $("#SearchByPersonValuesDiv").html("");
        $(".searchByPersonJobTxt").val("");
        $(".searchByPersonNameTxt").val("");

    });


    $(".searchByPersonJobBtn").on('click', function (e) {

        var jobTitle = $(".searchByPersonJobTxt").val();
        var personName = "";


        if (jobTitle.trim().length > 0) {
            SearchByPerson(personName, jobTitle);
        }
        else {
            $("#SearchByPersonValuesDiv").html("");
        }
        e.stopImmediatePropagation();

    });

    $(".searchByPersonNameBtn").on('click', function (e) {

        var jobTitle = "";
        var personName = $(".searchByPersonNameTxt").val();

        if (personName.trim().length > 0) {

            SearchByPerson(personName, jobTitle);
        }
        else {
            $("#SearchByPersonValuesDiv").html

        }
        e.stopImmediatePropagation();

    });

    $(".CloseSearchByPersonBtn").on('click', function () {

        $("#SearchByPersonValuesDiv").html("");
        $(".searchByPersonNameTxt").val("");
        $(".searchByPersonJobTxt").val("");

    });

    $("#SearchByPersonValuesDiv").on('click', 'div.others_person div.others_per_img a.DisplayOtherPerson', function () {

        var Userid = this.id;
        for (var i = 0; i < GlobalUserArray.length; i++) {

            if (GlobalUserArray[i].id == Userid) {
                $("#mainUserImage").css('background-image', 'url(/fileserver/Uploads/Images/FrontEndUser/' + GlobalUserArray[i].photo + ')');  //.attr('src', '/Uploads/Images/FrontEndUser/' + GlobalUserArray[i].photo);

                $("#mainUserName").text(GlobalUserArray[i].firstName + " " + GlobalUserArray[i].surName);

                $("#mainUserJob").text(GlobalUserArray[i].jobTitle);

                $("#mainUserLocation").text(GlobalUserArray[i].location);

                $("#mainUserTelephone").text(GlobalUserArray[i].telephoneNumber);

                $("#mainUserEmail").text(GlobalUserArray[i].emailAddress);

                // Check mobile info is available or not, if available then display the Mobile field else hide
                if (GlobalUserArray[i].mobile != null && GlobalUserArray[i].mobile != '') {
                    $("#labelMobile").show();
                    $("#mainUserMobile").text(GlobalUserArray[i].mobile);
                }

                else {
                    $("#labelMobile").hide();
                    $("#mainUserMobile").text('');
                }

            }
        }


    });

    // search by person ends

    $('.edit_logout_btn').on('click', function () {

        window.location.href = '/FrontEndHome/Login';
    });

    $("#openMyFavDiv").on('click', function () {

        var userId = $('#FrontEndUserDetails_Id').val();


        GetFavDocuments(userId);

    });

    $(".guidance_list").on('click', 'label.RemoveFromFavourite', function () {

        var docId = this.id;
        var userId = $('#FrontEndUserDetails_Id').val();

        $.ajax({
            type: "Post",
            url: "/FrontEndHome/RemoveFromFavouriteList",
            data: { docId: docId, userId: userId },

            success: function (Data) {

                swal("Document is successfully removed from favourite list.");
                GetFavDocuments(userId);
                var url = window.location.href;
                var deptId = url.split('/')[5];
                //var deptId = getParameterByName('DepartmentId');
                GetIntranetDepartmentDetailsByDepartmentWise(deptId);

            },
            error: function (response) {

            }
        });

    });
});


function SearchDocumentPressTop(e) {
    var key = e.keyCode || e.which;
    if (key == 13) {

        var element = document.getElementById("TopSearchDocumentBtn");
        var result = WindowDocumentOpenClose(element);
        if (result == true) {

            $("#ui-id-2").css('display', 'none');
            $("#ZerosearchDocumnetTextHeading").hide();
            var searchText = $("#TopSearchDocumentInput").val().trim();
            if (searchText != "") {
                $("#searchDocumnetTextHeading").show();
                $("#BottomSearchDocInput").val(searchText);
                SearchDocument(searchText);
            }
            else {
                $("#searchDocumnetTextHeading").hide();
                SearchDocument(searchText);
            }
        }
        else {
            $("#TopSearchDocumentInput").val('');
            $("#BottomSearchDocInput").val('');
            $("#searchDocumnetTextHeading").hide();
            $("#ZerosearchDocumnetTextHeading").hide();
            SearchDocument('');
        }

        e.stopImmediatePropagation();

    }
}

function SearchDocumentPressBottom(e) {
    var key = e.keyCode || e.which;
    if (key == 13) {

        $("#ui-id-13").css('display', 'none');
        $("#ZerosearchDocumnetTextHeading").hide();
        var searchText = $("#BottomSearchDocInput").val().trim();
        if (searchText != "") {

            $("#searchDocumnetTextHeading").show();
            $("#TopSearchDocumentInput").val(searchText);
            SearchDocument(searchText);
        }
        else {
            $("#searchDocumnetTextHeading").hide();
        }
        e.stopImmediatePropagation();
    }
}

function WindowDocumentOpenClose(param) {

    if (DocumentSearchFlagOpenWindow == 0) {
        $(".search_anything_filter").slideDown();
        $(param).parent('.search_filter_field').parent('.filter_select_item').addClass("active_item");
        $('.top_filter_section').addClass("show_item_overlay");
        $('html, body').animate({ scrollTop: $('.content_area').offset().top - 150 }, 500);
        DocumentSearchFlagOpenWindow = 1
        return true;
    }
    else {
        $(".search_anything_filter").slideUp();
        $(param).parent('.search_filter_field').parent('.filter_select_item').removeClass("active_item");
        $('.top_filter_section').removeClass("show_item_overlay");
        $('html, body').animate({ scrollTop: $('.content_area').offset().top - 150 }, 500);
        DocumentSearchFlagOpenWindow = 0
        return false;
    }
}

function SearchFAQPressTop(e) {
    var key = e.keyCode || e.which;
    if (key == 13) {

        var element = document.getElementById("topFaqSearchBtn");
        var result = WindowFAQOpenClose(element);
        if (result == true) {

            $("#ui-id-1").css('display', 'none');
            var searchText = $(".topFaqSearchInput").val().trim();
            if (searchText != "") {
                $("#BottomFaqSearchInput").val(searchText);
                SearchFaq(searchText);
            }
            else {
                SearchFaq(searchText);
            }
        }
        else {
            $(".topFaqSearchInput").val('');
            $("#BottomFaqSearchInput").val('');
            SearchFaq('');
        }

        e.stopImmediatePropagation();

    }
}

function SearchFAQPressBottom(e) {
    var key = e.keyCode || e.which;
    if (key == 13) {

        $("#ui-id-3").css('display', 'none');
        var searchText = $("#BottomFaqSearchInput").val().trim();
        if (searchText != "") {

            $(".topFaqSearchInput").val(searchText);
            SearchFaq(searchText);
        }
        e.stopImmediatePropagation();
    }
}

function WindowFAQOpenClose(param) {

    if (FAQSearchFlagOpenWindow == 0) {
        $(".how_do_filter").slideDown();
        $(param).parent('.search_filter_field').parent('.filter_select_item').addClass("active_item");
        $('.top_filter_section').addClass("show_item_overlay");
        $('html, body').animate({ scrollTop: $('.content_area').offset().top - 150 }, 500);
        FAQSearchFlagOpenWindow = 1
        return true;
    }
    else {
        $(".how_do_filter").slideUp();
        $(param).parent('.search_filter_field').parent('.filter_select_item').removeClass("active_item");
        $('.top_filter_section').removeClass("show_item_overlay");
        $('html, body').animate({ scrollTop: $('.content_area').offset().top - 150 }, 500);
        FAQSearchFlagOpenWindow = 0
        return false;
    }
}

function GetFavDocuments(userId) {
    $.ajax({
        type: "Post",
        url: "/FrontEndHome/GetFavDocumentList",
        data: { userId: userId },
        success: function (Data) {
            //debugger;
            $("#myFavCollectionDiv").html('');

            var htmlData = "";

            if (Data != null) {
                if (Data.length <= 0) {

                    $(".my_fav_popup").slideUp(200);
                    $('.my_favrt').removeClass("open_drop")



                }

                for (var i = 0; i < Data.length; i++) {
                    htmlData += '<div class="guidance_item">';
                    htmlData += '<input type="checkbox" id="item_1" checked />';
                    htmlData += '<label style="background-position: left top;" class="item_star_icon RemoveFromFavourite" id=' + Data[i].documentId + '  for="item_1" data-toggle="tooltip" data-placement="right" title="Remove from My Favourites"></label>';
                    htmlData += '<div class="doc_icon"><img src="/Uploads/IconImages/' + Data[i].fileTypeName + '_icon.png" alt="icon" /> </div>';
                    //htmlData += '<a href="/Uploads/Documents/' + Data[i].documentName + '" target="_blank" class="doc_name">' + Data[i].documentTitle + '<span>(' + Data[i].fileSize + ')</span></a>';

                    // Check Doc Type is Document 
                    if (Data[i].type == 'Document') {
                        htmlData += '<a href="/fileserver/Uploads/Documents/' + Data[i].documentName + '" target="_blank" class="doc_name">' + Data[i].documentTitle + '</a>';
                    }

                    // Check Doc Type is Link 
                    if (Data[i].type == 'Link') {
                        htmlData += '<a href="' + Data[i].linkDestination + '" target="_blank" class="doc_name">' + Data[i].documentTitle + '</a>';
                    }

                    htmlData += ' </div>';

                }


            }


            $("#myFavCollectionDiv").html(htmlData);

        },
        error: function (response) {

        }
    });

}

function onBegin(xhr) {

}

//function onSuccess(data) {
//    var d = data;
//    if (d.result.isSuccess) {
//        swal("Your profile was successfully updated.");
//        if (d.user.photo != null) {
//            $('#imgUpload').attr('src', '/Uploads/Images/FrontEndUser/' + d.user.photo);
//        }
//        $('#span_FullName').html(d.user.firstName + ' ' + d.user.surName);
//        //$(".edit_profile_popup").slideUp(200);
//        //$('#div_user_logged').toggleClass("open_logged_drop");
//        //$(".user_logged_drop").slideToggle(200);
//    }
//    //alert('Success');
//}

function onSuccessEditProfile(data) {
    var d = data;
    if (d.result.isSuccess) {
        swal("Your profile was successfully updated.");
        if (d.user != null) {
            if (d.user.photo != null) {
                //$('#imgUpload').attr('src', '/Uploads/Images/FrontEndUser/' + d.user.photo);
                $('#div_endUserImg').css('background-image', 'url(/fileserver/Uploads/Images/FrontEndUser/' + d.user.photo + ')');
            }
            $('#span_FullName').html(d.user.firstName + ' ' + d.user.surName);
            $('#FrontEndUserDetails_FirstName').val(d.user.firstName);
            $('#FrontEndUserDetails_SurName').val(d.user.surName);
            $('#FrontEndUserDetails_JobTitle').val(d.user.jobTitle);
            $('#FrontEndUserDetails_Location').val(d.user.location);
            $('#FrontEndUserDetails_Department').val(d.user.department);
            $('#FrontEndUserDetails_TelephoneNumber').val(d.user.telephoneNumber);
            $('#FrontEndUserDetails_Mobile').val(d.user.mobile);
            //$('#ddlEndUser_Department').val($('#FrontEndUserDetails_MyDepartmentId').val());
            //$("#ddlEndUser_Department").prop('selectedIndex', -1);
            $('#ddlEndUser_Department option[value="' + $('#FrontEndUserDetails_MyDepartmentId').val() + '"]').attr('selected', 'selected');
            $("#myDepartmentlnk").attr('onClick', "window.location.href='/intranet/department/" + $('#FrontEndUserDetails_MyDepartmentId').val() + "/" + d.user.myDepartmentName.replace(/ /g, "_") + "'");
            $("#myDepartmentlnk").removeClass("isDisabled");
            //<a href="#" onClick="window.location.href='/intranet/department/@Model.FrontEndUserDetails.MyDepartmentId/@Model.FrontEndUserDetails.MyDepartmentName'">My Department</a>
            $(".profile_popup_close").trigger('click');
        }
        //$(".edit_profile_popup").slideUp(200);
        //$('#div_user_logged').toggleClass("open_logged_drop");
        //$(".user_logged_drop").slideToggle(200);
    }

    if (d.isEmailValid == false) {
        $("#lblError_EmailAddress").html("Email address already exist.");
    }
    //alert('Success');
}

function onFailed(ex) {
    alert(ex);
}
function CheckProfileEditFormValid() {

    var isValid = true;

    if ($('#FrontEndUserDetails_FirstName').val().trim().length == 0) {
        $("span[data-valmsg-for='FrontEndUserDetails.FirstName']").text("This field is required.");
        isValid = false;
    }
    else {
        //$("span[data-valmsg-for='FrontEndUserDetails.FirstName']").text("");
        if ($('#FrontEndUserDetails_FirstName').val().trim().length > 40) {
            $("span[data-valmsg-for='FrontEndUserDetails.FirstName']").text("The first name should not be greater than 40 characters..");
            isValid = false;
        }
        else {
            $("span[data-valmsg-for='FrontEndUserDetails.FirstName']").text("");
        }
    }




    if ($('#FrontEndUserDetails_SurName').val().trim().length == 0) {
        $("span[data-valmsg-for='FrontEndUserDetails.SurName']").text("This field is required.");
        isValid = false;
    }
    else {
        // $("span[data-valmsg-for='FrontEndUserDetails.SurName']").text("");
        if ($('#FrontEndUserDetails_SurName').val().trim().length > 40) {
            $("span[data-valmsg-for='FrontEndUserDetails.SurName']").text("The sur name should not be greater than 40 characters.");
            isValid = false;
        }
        else {
            $("span[data-valmsg-for='FrontEndUserDetails.SurName']").text("");
        }

    }



    if ($('#FrontEndUserDetails_JobTitle').val().trim().length == 0) {
        $("span[data-valmsg-for='FrontEndUserDetails.JobTitle']").text("This field is required.");
        isValid = false;
    }
    else {
        // $("span[data-valmsg-for='FrontEndUserDetails.JobTitle']").text("");
        if ($('#FrontEndUserDetails_JobTitle').val().trim().length > 200) {
            $("span[data-valmsg-for='FrontEndUserDetails.JobTitle']").text("The job title should not be greater than 200 characters.");
            isValid = false;
        }
        else {
            $("span[data-valmsg-for='FrontEndUserDetails.JobTitle']").text("");
        }
    }


    if ($('#ddlEndUser_Department').val() == "0") {
        $("span[data-valmsg-for='FrontEndUserDetails.MyDepartmentName']").text("This field is required.");
        isValid = false;
    }
    else {
        $("span[data-valmsg-for='FrontEndUserDetails.MyDepartmentName']").text("");
    }


    if ($('#FrontEndUserDetails_Location').val().trim().length == 0) {
        $("span[data-valmsg-for='FrontEndUserDetails.Location']").text("This field is required.");
        isValid = false;
    }
    else {
        //$("span[data-valmsg-for='FrontEndUserDetails.Location']").text("");
        if ($('#FrontEndUserDetails_Location').val().trim().length > 200) {
            $("span[data-valmsg-for='FrontEndUserDetails.Location']").text("The location should not be greater than 200 characters.");
            isValid = false;
        }
        else {
            $("span[data-valmsg-for='FrontEndUserDetails.Location']").text("");
        }
    }


    if ($('#FrontEndUserDetails_EmailAddress').val().trim().length == 0) {
        $("span[data-valmsg-for='FrontEndUserDetails.EmailAddress']").text("This field is required.");
        isValid = false;
    }
    else {
        //$("span[data-valmsg-for='FrontEndUserDetails.EmailAddress']").text("");
        if ($('#FrontEndUserDetails_EmailAddress').val().trim().length > 300) {
            $("span[data-valmsg-for='FrontEndUserDetails.EmailAddress']").text("The email address should not be greater than 200 characters.");
            isValid = false;
        }
        else {
            $("span[data-valmsg-for='FrontEndUserDetails.EmailAddress']").text("");
        }
    }


    if ($('#FrontEndUserDetails_TelephoneNumber').val().trim().length == 0) {
        $("span[data-valmsg-for='FrontEndUserDetails.TelephoneNumber']").text("This field is required.");
        isValid = false;
    }
    else {
        $("span[data-valmsg-for='FrontEndUserDetails.TelephoneNumber']").text("");
    }

    //if ($('#FrontEndUserDetails_TelephoneNumber').val().trim().length > 0) {
    //    if ($('#FrontEndUserDetails_TelephoneNumber').val().trim().length > 20) {
    //        $("span[data-valmsg-for='FrontEndUserDetails.TelephoneNumber']").text("The telephone number should not be greater than 20.");
    //        isValid = false;
    //    }
    //    else {
    //        $("span[data-valmsg-for='FrontEndUserDetails.TelephoneNumber']").text("");
    //    }

    //}


    //if ($('#FrontEndUserDetails_Mobile').val().trim().length == 0) {
    //    $("span[data-valmsg-for='FrontEndUserDetails.Mobile']").text("Please enter the mobile number.");
    //    isValid = false;
    //}
    //else {
    //    $("span[data-valmsg-for='FrontEndUserDetails.Mobile']").text("");
    //}

    //if ($('#FrontEndUserDetails_Mobile').val().trim().length > 0) {
    //    if ($('#FrontEndUserDetails_Mobile').val().trim().length > 10) {
    //        $("span[data-valmsg-for='FrontEndUserDetails.Mobile']").text("The mobile number should not be greater than 10.");
    //        isValid = false;
    //    }
    //    else {
    //        $("span[data-valmsg-for='FrontEndUserDetails.Mobile']").text("");
    //    }

    //}

    return isValid;

}

//Search Document region starts

function SearchDocument(searchText) {

    var text = '"' + searchText + '"';
    $("#searchKeywordSpan").text(text);
    $("#ZerosearchKeywordSpan").text(text);

    $.ajax({
        type: "Post",
        url: "/FrontEndHome/GetDocumentList",
        data: { searchKeyword: searchText },
        success: function (data) {


            var htmlData = "";
            if (data != null) {

                if (text != '""') {
                    if (data.length == 0) {
                        $("#ZerosearchDocumnetTextHeading").show();
                        $("#searchDocumnetTextHeading").hide();
                    }
                    else {
                        $("#ZerosearchDocumnetTextHeading").hide();
                        $("#searchDocumnetTextHeading").show();
                    }

                }
                for (var i = 0; i < data.length; i++) {
                    htmlData += '<tr class="result_list_item">';
                    htmlData += '<td>';

                    // Check  Doc Type is Document
                    if (data[i].type == 'Document') {
                        htmlData += '<a href="/fileserver/Uploads/Documents/' + data[i].documentName + '"" target="_blank">';
                    }

                    // Check  Doc Type is  Link
                    if (data[i].type == 'Link') {
                        htmlData += '<a href="' + data[i].linkDestination + '"" target="_blank">';
                    }

                    //if (data[i].fileTypeName == "PDF") {
                    //    htmlData += '<span class="file_icon" ><img src="/Uploads/IconImages/big_pdf_icon.png" /></span>'
                    //}
                    //else if (data[i].fileTypeName == "Excel") {
                    //    htmlData += '<span class="file_icon" ><img src="/Uploads/IconImages/big_excel_icon.png" /></span >'
                    //}
                    htmlData += '<span class="file_icon" ><img src="/fileserver/Uploads/IconImages/' + data[i].fileTypeName + '_icon.png" /></span >';
                    htmlData += '<div class="result_text">';

                    //var FileSizeCalc = bytesToSize(data[i].fileSize);  
                    // Check  Doc Type is Document
                    if (data[i].type == 'Document') {
                        htmlData += '<span>' + data[i].documentTitle + '<cite>(' + data[i].fileSize + ' )</cite> </span>';
                    }

                    // Check  Doc Type is  Link
                    if (data[i].type == 'Link') {
                        htmlData += '<span>' + data[i].documentTitle + '</span>';
                    }

                    if (data[i].documentDescription != null) {
                        if (data[i].documentDescription.length > 50) {
                            // earlier limit was applied on description as lengthy description was looking bad, but client now wants full description
                            htmlData += '<small>' + data[i].documentDescription + ' </small>';
                        }
                        else {
                            htmlData += '<small>' + data[i].documentDescription + ' </small>';
                        }
                    }
                    else {
                        htmlData += '<small> </small>';
                    }
                    htmlData += "</div>"
                    htmlData += '</a>';
                    htmlData += '</td>';
                    htmlData += '</tr >'

                }
            }



            $("#searchDocumentListhtml").html(htmlData);
            getPagination('#searchDocumentListhtml');
            //Pagination();
            //$('#searchDocumentListhtml').paging({ limit: 4 });
            $('#0').attr('class', 'selected-page');

        },
        error: function (response) {

        }
    });

}

function bytesToSize(bytes) {
    var sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB'];
    if (bytes == 0) return 'n/a';
    var i = parseInt(Math.floor(Math.log(bytes) / Math.log(1024)));
    if (i == 0) return bytes + ' ' + sizes[i];
    return (bytes / Math.pow(1024, i)).toFixed(1) + ' ' + sizes[i];
};
//Seach Document region ends

//Search Faq region starts

function SearchFaq(searchText) {

    $.ajax({
        type: "Post",
        url: "/FrontEndHome/GetFaqsList",
        data: { searchKeyword: searchText },
        success: function (Data) {

            $("#searchFaqDocumentHtml").html('');
            $("#FAQsQuestion").text("");
            $("#FAQsAnswer").text("");
            $("#searchFaqDocumentHead").hide();

            if (Data.faQsAttachedDocumentList != null) {

                if (Data.faQsAttachedDocumentList.length < 1) {
                    $("#searchFaqDocumentHead").hide();
                }
                else {
                    $("#searchFaqDocumentHead").show();
                }


                for (var i = 0; i < Data.faQsList.length; i++) {

                    $("#FAQsQuestion").text(Data.faQsList[i].faQsQuestionText);
                    $("#FAQsAnswer").html(Data.faQsList[i].faQsAnswerText);
                }


                var htmlData = "";

                if (Data.faQsAttachedDocumentList != null) {
                    for (var i = 0; i < Data.faQsAttachedDocumentList.length; i++) {
                        htmlData += '<div class="result_list_item">';
                        //htmlData += '<a href="#">';
                        htmlData += '<a href="/fileserver/Uploads/Documents/' + Data.faQsAttachedDocumentList[i].documentName + '"" target="_blank">';
                        //if (Data.faQsAttachedDocumentList[i].fileTypeName == "PDF") {
                        //    htmlData += '<span class="file_icon"><img src="/FrontEnd/images/big_pdf_icon.png" /> </span>';
                        //}
                        //else if (Data.faQsAttachedDocumentList[i].fileTypeName == "Excel") {
                        //    htmlData += '<span class="file_icon" ><img src="/Uploads/IconImages/big_excel_icon.png" /></span >'
                        //}
                        htmlData += '<span class="file_icon" ><img src="/fileserver/Uploads/IconImages/' + Data.faQsAttachedDocumentList[i].fileTypeName + '_icon.png" /></span >'
                        htmlData += '<div class="result_text">';
                        htmlData += '<span>' + Data.faQsAttachedDocumentList[i].documentTitle + ' <cite>(' + Data.faQsAttachedDocumentList[i].fileSize + ')</cite> </span>';
                        if (Data.faQsAttachedDocumentList[i].documentDescription == null) { Data.faQsAttachedDocumentList[i].documentDescription = ""; }
                        htmlData += '<small>' + Data.faQsAttachedDocumentList[i].documentDescription + ' </small>';
                        htmlData += '</div>';
                        htmlData += '</a>';
                        htmlData += '</div>';
                    }
                }
            }

            $("#searchFaqDocumentHtml").html(htmlData);

        },
        error: function (response) {

        }
    });

}

//Seach Faq region ends

// search by person starts

function SearchByPerson(personName, JobTitle) {

    $.ajax({
        type: "Post",
        url: "/FrontEndHome/GetPersonsList",
        data: { personName: personName.trim(), JobTitle: JobTitle.trim() },
        success: function (Data) {

            if (Data != null && Data.length > 0) {

                for (var i = 0; i < Data.length; i++) {

                    if (Data[i].photo == "" || Data[i].photo == null) {

                        Data[i].photo = "defaultuser.png";
                    }

                }

                GlobalUserArray = Data;

                $("#SearchByPersonValuesDiv").html("");

                var htmlData = "";



                htmlData += ' <div class="find_person_deatils">';
                htmlData += '<div id="mainUserImage" class="person_img" style="background-image: url(/fileserver/Uploads/Images/FrontEndUser/' + Data[0].photo + ');">';
                //htmlData += '<img id="mainUserImage" src="/Uploads/Images/FrontEndUser/' + Data[0].photo + '" alt="image" />';
                htmlData += ' </div>';
                htmlData += '<div class="person_detail">';
                htmlData += '<div id="mainUserName" class="person_name">' + Data[0].firstName + " " + Data[0].surName + '</div>';
                htmlData += '<div id="mainUserJob" class="person_post">' + Data[0].jobTitle + '</div>';
                htmlData += '<div id="mainUserLocation" class="person_location">' + Data[0].location + '</div>';
                htmlData += '</div>';
                htmlData += '</div>';
                htmlData += '<div class="full_width">';
                htmlData += '<div class="center_numbers_wrap">';
                htmlData += '<div class="center_numbers">';
                htmlData += '<small>TELEPHONE: </small>';
                htmlData += '<span id="mainUserTelephone">' + Data[0].telephoneNumber + '</span>';
                htmlData += '</div>';
                htmlData += '</div>';
                htmlData += '<div class="center_numbers_wrap">';
                htmlData += '<div class="center_numbers">';
                htmlData += '<small>EMAIL ADDRESS:</small>';
                htmlData += '<span id="mainUserEmail">' + Data[0].emailAddress + '</span>';
                htmlData += '</div>';
                htmlData += '</div>';
                htmlData += '<div class="center_numbers_wrap">';
                htmlData += '<div class="center_numbers">';
                if (Data[0].mobile != null && Data[0].mobile != '') {
                    htmlData += '<small id="labelMobile">MOBILE:</small>';
                    htmlData += '<span id="mainUserMobile">' + Data[0].mobile + '</span>';
                }
                else {
                    htmlData += '<small id="labelMobile" style="display:none;">MOBILE:</small>';
                    htmlData += '<span id="mainUserMobile">' + Data[0].mobile + '</span>';
                }
                htmlData += '</div>';
                htmlData += '</div>';
                htmlData += '</div>';


                if (Data.length > 1) {
                    htmlData += ' <div class="others_person">';
                    if (personName == "") {
                        htmlData += '<div class="text">Looking for someone else? We have more than one <span>' + JobTitle + '…</span></div>';
                    }
                    else {
                        htmlData += '<div class="text">Looking for someone else? We have more than one <span>' + personName + '…</span></div>';
                    }


                    for (var i = 1; i < Data.length; i++) {

                        htmlData += '<div class="others_per_img">';
                        htmlData += '<a href="#" id=' + Data[i].id + ' class="DisplayOtherPerson"  data-toggle="tooltip" data-placement="top" title="' + Data[i].firstName + " " + Data[i].surName + ' - ' + Data[i].myDepartmentName + " " + Data[i].jobTitle + '" style="background-image: url(/fileserver/Uploads/Images/FrontEndUser/' + Data[i].photo + ');"> </a>';
                        htmlData += '</div>';

                    }

                    htmlData += '</div>';
                }


                $("#SearchByPersonValuesDiv").html(htmlData);
            }
            else {
                $("#SearchByPersonValuesDiv").html("<p class='no_result_person'>We couldn't find anyone matching your search</p>");
            }
        },
        error: function (response) {

        }
    });

}

// search by person ends

function CheckFileTypeExtensionFrontEnd(fileName) {
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
        swal("Sorry, " + fileName + " is invalid, allowed extensions are: " + _validFileExtensions.join(", "));
        return false;
    }
    else {
        return true;
    }
}

function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}


function SearchPersonByNamePressBtn(e) {
    var key = e.keyCode || e.which;
    if (key == 13) {

        var jobTitle = "";
        var personName = $(".searchByPersonNameTxt").val();

        if (personName.trim().length > 0) {

            SearchByPerson(personName, jobTitle);
            $("#SearchByPersonValuesDiv").show();

        }
        else {
            $("#SearchByPersonValuesDiv").html("");
            $("#SearchByPersonValuesDiv").hide();



        }


        //e.stopImmediatePropagation();
    }
}


function SearchPersonByJobPressBtn(e) {
    var key = e.keyCode || e.which;
    if (key == 13) {

        var jobTitle = $(".searchByPersonJobTxt").val();
        var personName = "";


        if (jobTitle.trim().length > 0) {
            SearchByPerson(personName, jobTitle);
            $("#SearchByPersonValuesDiv").show();

        }
        else {
            $("#SearchByPersonValuesDiv").html("");
            $("#SearchByPersonValuesDiv").hide();

        }
        // e.stopImmediatePropagation();
    }
}

function SeachBySelectOfTopDocumentAutoComplete(paramSelector, searchVal) {


    var result = WindowDocumentOpenClose(paramSelector);
    if (result == true) {
        $("#ZerosearchDocumnetTextHeading").hide();
        var searchText = searchVal; //$("#TopSearchDocumentInput").val().trim();
        if (searchText != "") {
            $("#searchDocumnetTextHeading").show();
            $("#BottomSearchDocInput").val(searchText);
            SearchDocument(searchText);
        }
        else {
            $("#searchDocumnetTextHeading").hide();
            SearchDocument(searchText);
        }
    }
    else {
        $("#TopSearchDocumentInput").val('');
        $("#BottomSearchDocInput").val('');
        $("#searchDocumnetTextHeading").hide();
        $("#ZerosearchDocumnetTextHeading").hide();
        SearchDocument('');
    }
}

function SearchBySelectOfBottomDocumentAutoComplete(searchVal) {

    $("#ZerosearchDocumnetTextHeading").hide();
    var searchText = searchVal; //$("#BottomSearchDocInput").val().trim();
    if (searchText != "") {

        $("#searchDocumnetTextHeading").show();
        $("#TopSearchDocumentInput").val(searchText);
        SearchDocument(searchText);
    }
    else {
        $("#searchDocumnetTextHeading").hide();
    }

}

function SearchBySelectOfTopFaqAutoComplete(paramSelector, searchVal) {
    var result = WindowFAQOpenClose(paramSelector);
    if (result == true) {
        var searchText = searchVal; //$(".topFaqSearchInput").val().trim();
        if (searchText != "") {
            $("#BottomFaqSearchInput").val(searchText);
            SearchFaq(searchText);
        }
        else {
            SearchFaq(searchText);
        }
    }
    else {
        $(".topFaqSearchInput").val('');
        $("#BottomFaqSearchInput").val('');
        SearchFaq('');


    }

}

function SearchBySelectOfBottomFaqAutoComplete(searchVal) {
    var searchText = searchVal;//$("#BottomFaqSearchInput").val().trim();
    if (searchText != "") {

        $(".topFaqSearchInput").val(searchText);
        SearchFaq(searchText);
    }


}