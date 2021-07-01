$(document).ready(function () {

    //$(".dropdown-menu > ul > li > a")[0].innerText = "Show: All News";

    GetNewsDetailsByDepartmentWise(0);

    $("#ddlNewsDepartment").on('change', function () {
        var departmentId = $(this).val();
        GetNewsDetailsByDepartmentWise(departmentId);
    });

    $('#PartialNewsDiv').on('click', 'ul li div.all_case_detail div.case_detail a.news_detail', function () {
        var NewsId = this.id;
        var NewsTitle = $(this).attr('mytag');
        //window.location.href = '/FrontEndHome/IntranetVacanciesDetail/' + NewsId;
        window.location.href = '/intranet/news/' + NewsId + '/' + NewsTitle;
        //window.location.href = '/FrontEndHome/IntranetNewsDetail/' + NewsId;
    });

    $('#PartialNewsDiv').on('click', '.news_detail', function () {
        var NewsId = this.id;
        var NewsTitle = $(this).attr('mytag');
        //window.location.href = '/FrontEndHome/IntranetVacanciesDetail/' + NewsId;
        window.location.href = '/intranet/news/' + NewsId + '/' + NewsTitle;
    });
});



function GetNewsDetailsByDepartmentWise(id) {
    $.get('/FrontEndHome/GetNewsDetailsByDepartmentWise/' + id, function (data) {

        var news = "";
        if (data != null) {
            var news = data.result.newsList;
        }

        $("#PartialNewsDiv").html("");
        var htmlData = "";
        if (news != "") {
            htmlData = "<ul>"
            for (var i = 0; i < news.length; i++) {

                var title = String(news[i].title).trim().replace(/ /g, "_");
                //<input type="hidden" id="hdnNewsTitle" value="@title" />

                htmlData += '<li>';
                htmlData += '<div class="all_case_detail">';
                htmlData += '<div class="case_img">';
                htmlData += '<span class="case_title">' + news[i].departmentName + '</span>';
                if (news[i].thumbnailImagePath != null) {
                    news[i].thumbnailImagePath = '/fileserver/Uploads/Images/News/' + news[i].thumbnailImagePath;
                }
                else {
                    news[i].thumbnailImagePath = '/fileserver/Uploads/Images/News/thumbnail_no_image.jpg';
                }

                htmlData += '<a href="#" mytag=' + title + ' id=' + news[i].id + ' class="news_detail item_bg" style="background-image:url(' + news[i].thumbnailImagePath + ')"></a>';
                htmlData += '</div>';
                htmlData += '<div class="case_detail">';
                htmlData += '<span class="up_date">' + news[i].publishDateDisplay + '</span>';
                htmlData += '<p>';
                htmlData += '<a href="#" mytag=' + title + ' id=' + news[i].id + ' class="news_detail">' + news[i].title + '</a>';
                htmlData += '</p>';
                htmlData += '<a href="#" mytag=' + title + ' id=' + news[i].id + ' class="read_more_btn news_detail">Read More</a>';
                htmlData += '</div>';
                htmlData += '</div>';
                htmlData += '</li>';
            }
            htmlData += "</ul>"

            $("#PartialNewsDiv").html(htmlData);
        }

        size_li = $(".case_studies_detail > ul > li").size();
        setTimeout(function () {
            equalheight('.case_studies_detail > ul > li');
        }, 300);
        if (size_li <= 12) {
            $(".more_case_studies").hide()
        }

        if (size_li <= 8) {
            $(".more_case_studies a").hide();
        }

        if (size_li <= 4) {
            $(".deatils_btn a").hide();
        }

        x = 12
        $('.news_article > ul > li:lt(' + x + ')').show();

        $('.more_case_studies a').click(function (event) {
            event.preventDefault();
            x = (x + 4 <= size_li) ? x + 4 : size_li;
            $('.news_article > ul > li:lt(' + x + ')').slideDown();

            setTimeout(function () {
                equalheight('.case_studies_detail > ul > li')
            }, 200)

            if (x == size_li) {
                $(this).hide();
            }

        });



    });


}