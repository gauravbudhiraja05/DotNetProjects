$(document).ready(function () {

    var doseMetaTypeId = getParameterByName('DoseMetaTypeId');
    if (doseMetaTypeId != '' && doseMetaTypeId != null) {
        if (parseInt(doseMetaTypeId) > 0) {
            var element = document.getElementById(doseMetaTypeId);
            if (element != undefined || element != null) {
                GetDoseDataByDoseMetaTypeWise(doseMetaTypeId, element.innerHTML);
            }
        }
        else {
            GetDoseDataByDoseMetaTypeWise(0, "All");
        }

    }
    else {
        GetDoseDataByDoseMetaTypeWise(0, 'Select DoseMetaType');
    }

    $('.doseMetaTypeClick').on('click', function () {
        var doseMetaTypeId = parseInt(this.id);
        var doseMetaTypeName = this.innerText;
        GetDoseDataByDoseMetaTypeWise(doseMetaTypeId, doseMetaTypeName);

    });

    var rowsCount = $('#tblDoseDataList tbody tr').length;
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

function GetDoseDataByDoseMetaTypeWise(doseMetaTypeId, doseMetaTypeName) {
    $.get('/DoseData/GetDoseDataByDoseMetaTypeWise/' + doseMetaTypeId, function (data) {
        $('#partialDoseData').html(data);
        $('#partialDoseData').show();

        $('#hdnDoseMetaTypeId').val(doseMetaTypeId);
        $('#hdnDoseMetaTypeName').val(doseMetaTypeName);

        var menuText = doseMetaTypeName;
        if (menuText == "All") {
            menuText = "Select DoseMetaTypes";
        }

        //if (roleType == 'SA') {
        $(".drop_menu > ul > li > a")[0].innerText = menuText;
        //}

        $('#btnSearch').prop("disabled", false);
        $('#btnDeleteSelected').prop("disabled", false);
        $('#txtSearchDoseMetaUp').prop("disabled", false);
    });
}

function EditRedirect(id) {
    $('#hdnDoseMetaType_Back').val($('#hdnDoseMetaTypeId').val());

    window.location.href = '/DoseData/Edit?DoseMetaTypeId=' + parseInt(id) + '&SelectedDoseMetaTypeId=' + parseInt($('#hdnDoseMetaTypeId').val());
}

