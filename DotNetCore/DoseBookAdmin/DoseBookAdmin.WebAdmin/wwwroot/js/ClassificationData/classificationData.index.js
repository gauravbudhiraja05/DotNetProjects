$(document).ready(function () {

    $('.ClassificationTypeClick').on('click', function () {
        var classificationTypeId = parseInt(this.id);
        var classificationTypeName = this.innerText;
        GetClassificationDataByClassificationTypeWise(classificationTypeId, classificationTypeName);
    });

    //Select Current Menu
    $('.nav-li').removeClass('current');
    $('#menuItemVacancies').addClass('current');
});

function GetClassificationDataByClassificationTypeWise(classificationTypeId, classificationTypeName) {

    $.get('/ClassificationData/GetClassificationDataByClassificationTypeWise/' + classificationTypeId, function (data) {
        $('#partialClassificationData').html(data);
        $('#partialClassificationData').show();

        $('#hdnClassificationTypeId').val(classificationTypeId);
        $('#hdnClassificationTypeName').val(classificationTypeName);

        var menuText = classificationTypeName;
        if (menuText == "Select Classification Type") {
            menuText = "Select Classification Types";
        }

        $(".drop_menu > ul > li > a")[0].innerText = menuText;

        $('#btnSearch').prop("disabled", false);
        $('#txtSearchClassificationDataUp').prop("disabled", false);
    });
}


