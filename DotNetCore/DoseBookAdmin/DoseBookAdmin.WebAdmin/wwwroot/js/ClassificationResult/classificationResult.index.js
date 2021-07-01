$(document).ready(function () {

    var doctorId = getParameterByName('DoctorId');
    if (doctorId != '' && doctorId != null) {
        if (parseInt(doctorId) > 0) {
            var element = document.getElementById(doctorId);
            if (element != undefined || element != null) {
                GetClassificationResultByDoctorWise(doctorId, element.innerHTML);
            }
        }
        else {
            GetClassificationResultByDoctorWise(0, "All");
        }

    }
    else {
        GetClassificationResultByDoctorWise(0, 'Select Doctor');
    }

    $('.doctorClick').on('click', function () {
        var doctorId = parseInt(this.id);
        var doctorName = this.innerText;
        GetClassificationResultByDoctorWise(doctorId, doctorName);
    });

    var rowsCount = $('#tblClassificationResults tbody tr').length;
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

function GetClassificationResultByDoctorWise(doctorId, doctorName) {
    $.get('/ClassificationResult/GetClassificationResultByDoctorWise/' + doctorId, function (data) {
        $('#partialClassificationResult').html(data);
        $('#partialClassificationResult').show();

        $('#hdnDoctorId').val(doctorId);
        $('#hdnDoctorName').val(doctorName);

        var menuText = doctorName;
        if (menuText == "All") {
            menuText = "Select Doctor";
        }

        $(".drop_menu > ul > li > a")[0].innerText = menuText;

        $('#btnSearch').prop("disabled", false);
        $('#btnDeleteSelected').prop("disabled", false);
        $('#txtSearcClassificationResultUp').prop("disabled", false);
    });
}

function EditRedirect(id) {
    $('#hdnDoctor_Back').val($('#hdnDoctorId').val());
    window.location.href = '/ClassificationResult/EditClassificationResult?ClassificationResultId=' + parseInt(id) + '&SelectedDoctorId=' + parseInt($('#hdnDoctorId').val());
}

