$(document).ready(function () {
    debugger;
    var prescriptionMetaDataTypeName = getParameterByName('prescriptionMetaDataType');
    if (prescriptionMetaDataTypeName != '' && prescriptionMetaDataTypeName != null) {
        if (prescriptionMetaDataTypeName != "All")
            GetPrescriptionMetaDataByPrescriptionMetaTypeWise(prescriptionMetaDataTypeName);
        else
            GetPrescriptionMetaDataByPrescriptionMetaTypeWise('All');
    }
    else {
        GetPrescriptionMetaDataByPrescriptionMetaTypeWise('Select PrescriptionType');
    }

    $('.prescriptionMetaTypeClick').on('click', function () {
        var prescriptionMetaType = this.id;
        GetPrescriptionMetaDataByPrescriptionMetaTypeWise(prescriptionMetaType);
    });

    var rowsCount = $('#tblDoseDataList tbody tr').length;
    if (rowsCount <= 1) {
        $("#btnDeleteSelected").hide();
    }
    else {
        $("#btnDeleteSelected").show();
    }
});

function GetPrescriptionMetaDataByPrescriptionMetaTypeWise(prescriptionMetaDataType) {
    $.get('/PrescriptionMeta/GetPrescriptionMetaDataByPrescriptionMetaTypeWise?prescriptionMetaDataType= ' + prescriptionMetaDataType, function (data) {
        $('#partialPrescriptionMetaData').html(data);
        $('#partialPrescriptionMetaData').show();

        $('#hdnPrescriptionMetaTypeId').val(prescriptionMetaDataType);
        $('#hdnPrescriptionMetaTypeName').val(prescriptionMetaDataType);

        var menuText = prescriptionMetaDataType;
        if (menuText == "All") {
            menuText = "Select PrescriptionType";
        }

        $(".drop_menu > ul > li > a")[0].innerText = menuText;

        $('#btnSearchPrescriptionMetaDataUp').prop("disabled", false);
        $('#btnDeleteSelected').prop("disabled", false);
        $('#txtSearchPrescriptionMetaDataUp').prop("disabled", false);
    });
}

