$(document).ready(function () {

    var doctorId = getParameterByName('DoctorId');
    if (doctorId != '' && doctorId != null) {
        if (parseInt(doctorId) > 0) {
            var element = document.getElementById(doctorId);
            if (element != undefined || element != null) {
                GetDiagnosticTestByDoctorWise(doctorId, element.innerHTML);
            }
        }
        else {
            GetDiagnosticTestByDoctorWise(0, "All");
        }

    }
    else {
        GetDiagnosticTestByDoctorWise(0, 'Select Doctor');
    }

    $('.doctorClick').on('click', function () {
        var doctorId = parseInt(this.id);
        var doctorName = this.innerText;
        GetDiagnosticTestByDoctorWise(doctorId, doctorName);
    });

    var rowsCount = $('#tblDiagnosticTests tbody tr').length;
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

    $('#btnAddDiagnosticTest').on('click', function () {
        if ($('#hdnDoctorId').val() != undefined) {
            if (parseInt($('#hdnDoctorId').val()) > 0) { window.location.href = "/DiagnosticTest/AddDiagnosticTest?DoctorId=" + $('#hdnDoctorId').val(); }
            else {
                swal("Please select a doctor to add the diagnostic test.", '', 'error').then(function () {
                    $(".drop_menu").find(".drop_menu_sub").show();
                });
            }
        }
    });
});

function GetDiagnosticTestByDoctorWise(doctorId, doctorName) {
    $.get('/DiagnosticTest/GetDiagnosticTestByDoctorWise/' + doctorId, function (data) {
        $('#partialDiagnosticTest').html(data);
        $('#partialDiagnosticTest').show();

        $('#hdnDoctorId').val(doctorId);
        $('#hdnDoctorName').val(doctorName);

        var menuText = doctorName;
        if (menuText == "All") {
            menuText = "Select Doctor";
        }

        $(".drop_menu > ul > li > a")[0].innerText = menuText;

        $('#btnSearch').prop("disabled", false);
        $('#btnDeleteSelected').prop("disabled", false);
        $('#txtSearchDiagnosticTestUp').prop("disabled", false);
    });
}

function EditRedirect(id) {
    $('#hdnDoctor_Back').val($('#hdnDoctorId').val());
    window.location.href = '/DiagnosticTest/EditDiagnosticTest?TestId=' + parseInt(id) + '&SelectedDoctorId=' + parseInt($('#hdnDoctorId').val());
}

