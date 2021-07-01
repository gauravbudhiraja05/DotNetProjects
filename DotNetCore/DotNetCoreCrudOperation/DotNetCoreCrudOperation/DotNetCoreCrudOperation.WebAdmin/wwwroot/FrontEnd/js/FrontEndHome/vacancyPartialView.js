$(document).ready(function () {

    //$(".dropdown-menu > ul > li > a")[0].innerText = "Show: All News";

    GetVacancyDetailsByDepartmentWise(0);

    $("#ddlVacanciesDepartment").on('change', function () {
        var departmentId = $(this).val();
        GetVacancyDetailsByDepartmentWise(departmentId);
    });

    $('#PartialVacancyDiv').on('click','ul li div.all_case_detail div.case_detail a.vacancy_detail', function () {
        var VacancyId = this.id;
        var VacancyTitle = $(this).attr('mytag');
        //window.location.href = '/FrontEndHome/IntranetVacanciesDetail/' + NewsId;
        window.location.href = '/intranet/vacancies/' + VacancyId + '/' + VacancyTitle;
    });

    $('#PartialVacancyDiv').on('click' ,'.vacancy_detail', function () {
        var VacancyId = this.id;
        var VacancyTitle = $(this).attr('mytag');
        //window.location.href = '/FrontEndHome/IntranetVacanciesDetail/' + NewsId;
        window.location.href = '/intranet/vacancies/' + VacancyId + '/' + VacancyTitle;
    });

});



function GetVacancyDetailsByDepartmentWise(id) {
    $.get('/FrontEndHome/GetVacancyDetailsByDepartmentWise/' + id, function (data) {

        $('#PartialVacancyDiv').html(data);
        $('#PartialVacancyDiv').show();
        setTimeout(function () {
            equalheight('.case_studies_detail > ul > li');
        }, 300);
        size_li = $(".case_studies_detail > ul > li").size();

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