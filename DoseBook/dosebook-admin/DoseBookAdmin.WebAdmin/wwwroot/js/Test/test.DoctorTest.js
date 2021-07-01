$(document).ready(function () {

    var doctorId = getParameterByName('doctorId');
    if (doctorId != '' && doctorId != null) {
        if (parseInt(doctorId) > 0) {
            var element = document.getElementById(doctorId);
            if (element != undefined || element != null) {
                GetTestListByDoctorWise(doctorId, element.innerHTML);
            }
        }
        else {
            GetTestListByDoctorWise(0, "All");
        }

    }
    else {
        GetTestListByDoctorWise(0, 'Select Doctor');
    }

    $('.doctorClick').on('click', function () {
        var doctorId = parseInt(this.id);
        var doctorName = this.innerText;
        GetTestListByDoctorWise(doctorId, doctorName);
    });

    var rowsCount = $('#tblDoctorTests tbody tr').length;
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

function GetTestListByDoctorWise(doctorId, doctorName) {
    $.get('/Test/GetTestListByDoctorWise/' + doctorId, function (data) {
        $('#partialTest').html(data);
        $('#partialTest').show();

        $('#hdnDoctorId').val(doctorId);
        $('#hdnDoctorName').val(doctorName);

        var menuText = doctorName;
        if (menuText == "All") {
            menuText = "Select Doctor";
        }

        $(".drop_menu > ul > li > a")[0].innerText = menuText;

        $('#btnSearch').prop("disabled", false);
        $('#btnDeleteSelected').prop("disabled", false);
        $('#txtSearchTestUp').prop("disabled", false);
    });
}
