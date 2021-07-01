$(document).ready(function () {

    $(function () {
        $(".drag_list").sortable({
            placeholder: "ui-state-highlight"
        });
        $(".drag_list").disableSelection();

        if ($('.ui-state-default .drag_list').size() > 0) {
            $('.ui-state-default .drag_list').parent('.ui-state-default').addClass("has_sub");
        }
    });

    if ($(".hiddenUserType").val() == "SA") {
        // GetDocumentsByDepartmentWise(0, "Select Departments");
        $("#txtSearchNewsUp").hide();
        $("#btnSearch").hide();
        $(".drop_menu > ul > li > a")[0].innerText = "Select Departments";
    }
    else {
        var departmentId = $("#hiddenDepartmentIdIndex").val();
        GetDocumentsByDepartmentWise(departmentId, "");
    }


    // Select Current Menu
    $('.nav-li').removeClass('current');
    $('#menuItemDocuments').addClass('current');

    var url = window.location.href;
    var departmentId = url.split('/')[5];
    //var departmentId = getParameterByName('DepartmentId');
    if (departmentId != '') {

        var element = document.getElementById(departmentId);
        if (element != undefined || element != null) {
            //element.click();
            GetDocumentsByDepartmentWise(departmentId, element.innerHTML);
        }
    }
    $('.deptClick').on('click', function () {
        var departmentId = parseInt(this.id);
        var departmentName = this.innerText;
        var menuText = departmentName;
        $('#hdnDepartmentId').val(departmentId);
        $('#hdnDepartmentName').val(departmentName);
        if (departmentName == "All") {
            menuText = "Select Departments"
        }
        $(".drop_menu > ul > li > a")[0].innerText = menuText;

        GetDocumentsByDepartmentWise(departmentId, departmentName);
        $("#txtSearchNewsUp").show();
        $("#btnSearch").show();

    });

    $('#btnSearch').on('click', function () {

        var departmentId = $('#hdnDepartmentId').val();
        var searchText = $('#txtSearchNewsUp').val();
        var departmentName= $('#hdnDepartmentName').val();
        //$('#hdnDepartmentName').val(departmentName);
        GetDocumentsByDepartmentWise_Search(departmentId, departmentName,searchText);
        //newsListTable.search($(this).val()).draw();
    });
});

function GetDocumentsByDepartmentWise_Search(departmentId, departmentName, searchText) {

    $.get('/Documents/GetDocumentsByDepartmentWise_Search' ,{ DepartmentId: departmentId, SeachValue: '' + searchText+'' }, function (data) {
        $('#hdnDepartmentId').val(departmentId);
        $('#hdnDepartmentName').val(departmentName);

        $('#partialDocumentList').html(data);
        $('#partialDocumentList').show();

        //if (departmentId == 0) {
        //    $('#btnSearch').prop("disabled", true);
        //    $('#txtSearchNewsUp').prop("disabled", true);
        //}

        //if (departmentId > 0) {
        //    $('#btnSearch').prop("disabled", false);
        //    $('#txtSearchNewsUp').prop("disabled", false);
        //}
    });
}

function GetDocumentsByDepartmentWise(departmentId, departmentName) {
    $.get('/Documents/GetDocumentsByDepartmentWise/' + departmentId, function (data) {
        $('#hdnDepartmentId').val(departmentId);
        $('#hdnDepartmentName').val(departmentName);

        $('#partialDocumentList').html(data);
        $('#partialDocumentList').show();

        if (departmentId == 0) {
            $('#btnSearch').prop("disabled", true);
            $('#txtSearchNewsUp').prop("disabled", true);
        }

        if (departmentId > 0) {
            $('#btnSearch').prop("disabled", false);
            $('#txtSearchNewsUp').prop("disabled", false);
        }
    });
}