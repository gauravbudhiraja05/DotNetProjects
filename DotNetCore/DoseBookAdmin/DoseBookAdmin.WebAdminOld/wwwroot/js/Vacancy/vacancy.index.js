$(document).ready(function () {
    if ($('#hdnUserRoleType').val() == 'DA') {
        var deptId = parseInt($('#hdnDepartmentId').val());
        var deptName = $('#hdnDepartmentName').val();
        GetVacancyByDepartmentWise(deptId, deptName, $('#hdnUserRoleType').val());
    }
    else {

        var departmentId = getParameterByName('DepartmentId');
        if (departmentId != '' && departmentId != null) {
            if (parseInt(departmentId) > 0) {
                var element = document.getElementById(departmentId);
                if (element != undefined || element != null) {
                    GetVacancyByDepartmentWise(departmentId, element.innerHTML, $('#hdnUserRoleType').val());
                }
            }
            else {
                GetVacancyByDepartmentWise(0, "All", $('#hdnUserRoleType').val());
            }

        }
        else if ($('#hdnUserRoleType').val() == 'SA') {
            GetVacancyByDepartmentWise(0, 'Select Departments', $('#hdnUserRoleType').val());
        }
        $('.deptClick').on('click', function () {
            var departmentId = parseInt(this.id);
            var departmentName = this.innerText;
            GetVacancyByDepartmentWise(departmentId, departmentName, $('#hdnUserRoleType').val());

        });
    }
    var rowsCount = $('#tblVacancyList tbody tr').length;
    if (rowsCount <= 1) {
        $("#btnDeleteSelected").hide();
        // $(".table_checkbox").hide();
    }
    else {
        $("#btnDeleteSelected").show();
        //$(".table_checkbox").show();
    }
    //Select Current Menu
    $('.nav-li').removeClass('current');
    $('#menuItemVacancies').addClass('current');
});

function GetVacancyByDepartmentWise(departmentId, departmentName, roleType) {
    $.get('/Vacancies/GetVacancyByDepartmentWise/' + departmentId, function (data) {
        $('#partialVacancy').html(data);
        $('#partialVacancy').show();

        $('#hdnDepartmentId').val(departmentId);
        $('#hdnDepartmentName').val(departmentName);

        var menuText = departmentName;
        if (menuText == "All") {
            menuText = "Select Departments";
        }

        if (roleType == 'SA') {
            $(".drop_menu > ul > li > a")[0].innerText = menuText;
        }

        $('#btnSearch').prop("disabled", false);
        $('#btnDeleteSelected').prop("disabled", false);
        $('#txtSearchNewsUp').prop("disabled", false);
    });
}

function EditRedirect(id) {
    $('#hdnDepartmentId_Back').val($('#hdnDepartmentId').val());

    window.location.href = '/Vacancies/Edit?VacancyId=' + parseInt(id) + '&SelectedDepartmentId=' + parseInt($('#hdnDepartmentId').val());
}

//function GetVacancyByDepartmentForDA(departmentId, departmentName) {
//    $.get('/Vacancies/GetVacancyByDepartmentWise/' + departmentId, function (data) {
//        $('#partialVacancy').html(data);
//        $('#partialVacancy').show();

//        $('#hdnDepartmentId').val(departmentId);
//        $('#hdnDepartmentName').val(departmentName);

//        //$(".drop_menu > ul > li > a")[0].innerText = departmentName;

//        $('#btnSearch').prop("disabled", false);
//        $('#btnDeleteSelected').prop("disabled", false);
//        $('#txtSearchNewsUp').prop("disabled", false);
//    });
//}

