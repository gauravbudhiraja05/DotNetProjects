$(document).ready(function () {

    $('.datepicker-here').datepicker({
        autoClose: true,
        format: 'mm/dd/yyyy',
        minDate: new Date(),
        position: 'top left', // Default position
        onHide: function (inst) {
            inst.update('position', 'top left'); // Update the position to the default again
        },
        onShow: function (inst, animationComplete) {
            // Just before showing the datepicker
            if (!animationComplete) {
                var iFits = false;
                // Loop through a few possible position and see which one fits
                $.each(['top left', 'left bottom', 'bottom left', 'top center', 'bottom center'], function (i, pos) {
                    if (!iFits) {
                        inst.update('position', pos);
                        var fits = isElementInViewport(inst.$datepicker[0]);
                        if (fits.all) {
                            iFits = true;
                        }
                    }
                });
            }
        },

    });

    $('[data-toggle="tooltip"]').tooltip();

    $(".table .table_title").click(function (e) {
        $(".table .table_title").not($(this)).removeClass("active");
        $(this).addClass("active");
        var iconClass = $(this).children(".icon").hasClass("desc") ? 'asc' : 'desc';
        $(this).children(".icon").removeClass("asc desc");
        $(this).children(".icon").addClass(iconClass);
    });

    $(".check_all").change(function () {  //"select all" change
        var status = this.checked; // "select all" checked status
        $(this).closest("table").find(".check_item").each(function () { //iterate all listed checkbox items
            this.checked = status; //change ".checkbox" checked status
        });
    });

    $('.check_item').change(function () {
        var st = false;
        $(this).closest("table").find(".check_item").each(function () {
            if ($(this).prop("checked")) {
                st = true;
            } else {
                st = false;
                return false;
            }
        });
        if (st) {
            $(this).closest("table").find(".check_all").prop("checked", true);
        } else {
            $(this).closest("table").find(".check_all").prop("checked", false);
        }
    });

});