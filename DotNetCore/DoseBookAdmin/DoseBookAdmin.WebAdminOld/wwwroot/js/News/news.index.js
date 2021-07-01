$(document).ready(function () {

    //onclick = "window.location.href='/News/Edit?NewsId=@n.Id'"

    if ($('#hdnUserRoleType').val() == 'DA') {
        var deptId = parseInt($('#hdnDepartmentId').val());
        var deptName = $('#hdnDepartmentName').val();
        GetNewsByDepartmentWise(deptId, deptName, $('#hdnUserRoleType').val());
    }
    else {
        var departmentId = getParameterByName('DepartmentId');
        if (departmentId != '' && departmentId != null) {
            if (parseInt(departmentId) > 0) {
                var element = document.getElementById(departmentId);
                if (element != undefined || element != null) {
                    GetNewsByDepartmentWise(departmentId, element.innerHTML, $('#hdnUserRoleType').val());
                }
            }
            else {
                GetNewsByDepartmentWise(0, "All", $('#hdnUserRoleType').val());
            }
        }
        else if ($('#hdnUserRoleType').val() == 'SA') {
            GetNewsByDepartmentWise(0, 'Select Departments', $('#hdnUserRoleType').val());
        }
        $('.deptClick').on('click', function () {
            var departmentId = parseInt(this.id);
            var departmentName = this.innerText;
            GetNewsByDepartmentWise(departmentId, departmentName, $('#hdnUserRoleType').val());
        });
    }
    var rowsCount = $('#tblNewsList tbody tr').length;
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
    $('#menuItemNews').addClass('current');

    $('#btnAddNews').on('click', function () {
        if ($('#hdnDepartmentId').val() != undefined) {
            if (parseInt($('#hdnDepartmentId').val()) > 0) {
                window.location.href = "/News/Add?DepartmentId=" + $('#hdnDepartmentId').val();
            }
            else {
                swal("Please select a department for this news item.",'','error').then(function () {
                    $(".drop_menu").find(".drop_menu_sub").show();
                });
            }
        }
    });
});

function GetNewsByDepartmentWise(departmentId, departmentName, roleType) {
    $.get('/News/GetNewsByDepartmentWise/' + departmentId, function (data) {
        $('#partialNews').html(data);
        $('#partialNews').show();

        $('#hdnDepartmentId').val(departmentId);
        $('#hdnDepartmentName').val(departmentName);

        $('#hdnDepartmentId_Back').val(departmentId);
        //$('#hdnDepartmentName_Back').val(departmentName);

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

    window.location.href = '/News/Edit?NewsId=' + parseInt(id) + '&SelectedDepartmentId=' + parseInt($('#hdnDepartmentId').val());
}
//function GetNewsByDepartmentForDA(departmentId, departmentName) {
//    $.get('/News/GetNewsByDepartmentWise/' + departmentId, function (data) {
//        $('#partialNews').html(data);
//        $('#partialNews').show();

//        $('#hdnDepartmentId').val(departmentId);
//        $('#hdnDepartmentName').val(departmentName);

//        //$(".drop_menu > ul > li > a")[0].innerText = departmentName;

//        $('#btnSearch').prop("disabled", false);
//        $('#btnDeleteSelected').prop("disabled", false);
//        $('#txtSearchNewsUp').prop("disabled", false);
//    });
//}

