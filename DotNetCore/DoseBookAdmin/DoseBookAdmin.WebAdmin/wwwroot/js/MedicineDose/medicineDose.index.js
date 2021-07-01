$(document).ready(function () {

    var doctorId = getParameterByName('DoctorId');
    if (doctorId != '' && doctorId != null) {
        if (parseInt(doctorId) > 0) {
            var element = document.getElementById(doctorId);
            if (element != undefined || element != null) {
                GetMedicineDoseByDoctorWise(doctorId, element.innerHTML);
            }
        }
        else {
            GetMedicineDoseByDoctorWise(0, "All");
        }

    }
    else {
        GetMedicineDoseByDoctorWise(0, 'Select Doctor');
    }

    $('.doctorClick').on('click', function () {
        var doctorId = parseInt(this.id);
        var doctorName = this.innerText;
        GetMedicineDoseByDoctorWise(doctorId, doctorName);

    });

    var rowsCount = $('#tblMedicineDoses tbody tr').length;
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

function GetMedicineDoseByDoctorWise(doctorId, doctorName) {
    $.get('/MedicineDose/GetMedicineDoseByDoctorWise/' + doctorId, function (data) {
        $('#partialMedicineDose').html(data);
        $('#partialMedicineDose').show();

        $('#hdnDoctorId').val(doctorId);
        $('#hdnDoctorName').val(doctorName);

        var menuText = doctorName;
        if (menuText == "All") {
            menuText = "Select Doctor";
        }

        $(".drop_menu > ul > li > a")[0].innerText = menuText;

        $('#btnSearch').prop("disabled", false);
        $('#btnDeleteSelected').prop("disabled", false);
        $('#txtSearchMedicineDoseUp').prop("disabled", false);
    });
}

function EditRedirect(id) {
    $('#hdnDoctor_Back').val($('#hdnDoctorId').val());
    window.location.href = '/MedicineDose/EditMedicineDose?MedcineId=' + parseInt(id) + '&SelectedDoctorId=' + parseInt($('#hdnDoctorId').val());
}

