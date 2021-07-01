$(document).ready(function () {

    var doctorId = getParameterByName('doctorId');
    if (doctorId != '' && doctorId != null) {
        if (parseInt(doctorId) > 0) {
            var element = document.getElementById(doctorId);
            if (element != undefined || element != null) {
                GetAdviceListByDoctorWise(doctorId, element.innerHTML);
            }
        }
        else {
            GetAdviceListByDoctorWise(0, "All");
        }

    }
    else {
        GetAdviceListByDoctorWise(0, 'Select Doctor');
    }

    $('.doctorClick').on('click', function () {
        var doctorId = parseInt(this.id);
        var doctorName = this.innerText;
        GetAdviceListByDoctorWise(doctorId, doctorName);
    });

    var rowsCount = $('#tblDoctorAdvices tbody tr').length;
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

function GetAdviceListByDoctorWise(id, doctorName) {
    $.get('/Advice/GetAdviceListByDoctorWise/' + id, function (data) {
        $('#partialAdvice').html(data);
        $('#partialAdvice').show();

        $('#hdnDoctorId').val(id);
        $('#hdnDoctorName').val(doctorName);

        var menuText = doctorName;
        if (menuText == "All") {
            menuText = "Select Doctor";
        }

        $(".drop_menu > ul > li > a")[0].innerText = menuText;

        $('#btnSearch').prop("disabled", false);
        $('#btnDeleteSelected').prop("disabled", false);
        $('#txtSearchAdviceUp').prop("disabled", false);
    });
}

