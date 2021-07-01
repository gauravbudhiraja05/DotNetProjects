$(document).ready(function () {

    var doctorId = getParameterByName('DoctorId');
    if (doctorId != '' && doctorId != null) {
        if (parseInt(doctorId) > 0) {
            var element = document.getElementById(doctorId);
            if (element != undefined || element != null) {
                GetMiscSuggestionByDoctorWise(doctorId, element.innerHTML);
            }
        }
        else {
            GetMiscSuggestionByDoctorWise(0, "All");
        }

    }
    else {
        GetMiscSuggestionByDoctorWise(0, 'Select Doctor');
    }

    $('.doctorClick').on('click', function () {
        var doctorId = parseInt(this.id);
        var doctorName = this.innerText;
        GetMiscSuggestionByDoctorWise(doctorId, doctorName);

    });

    var rowsCount = $('#tblMiscSuggestions tbody tr').length;
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

function GetMiscSuggestionByDoctorWise(doctorId, doctorName) {
    $.get('/MiscSuggestion/GetMiscSuggestionByDoctorWise/' + doctorId, function (data) {
        $('#partialMiscSuggestion').html(data);
        $('#partialMiscSuggestion').show();

        $('#hdnDoctorId').val(doctorId);
        $('#hdnDoctorName').val(doctorName);

        var menuText = doctorName;
        if (menuText == "All") {
            menuText = "Select Doctor";
        }

        $(".drop_menu > ul > li > a")[0].innerText = menuText;

        $('#btnSearch').prop("disabled", false);
        $('#btnDeleteSelected').prop("disabled", false);
        $('#txtSearchMiscSuggestionUp').prop("disabled", false);
    });
}

function EditRedirect(id) {
    $('#hdnDoctor_Back').val($('#hdnDoctorId').val());
    window.location.href = '/MiscSuggestion/EditMiscSuggestion?TestId=' + parseInt(id) + '&SelectedDoctorId=' + parseInt($('#hdnDoctorId').val());
}

