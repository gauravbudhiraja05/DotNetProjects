// Write your JavaScript code.
$(document).ready(function () {



    /*-----------------------------------
            Check All
    -----------------------------------*/
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


	/*-----------------------------------
       Menu
-----------------------------------*/
    $(".top_menu > ul > li").mouseenter(function () {
        $(this).children("div.dropdown_menu").fadeIn();
        $(this).addClass("open_dropdown");
    });
    $(".top_menu > ul > li").mouseleave(function () {
        $(this).children("div.dropdown_menu").hide();
        $(this).removeClass("open_dropdown");
    });



	/*-----------------------------------
       Tab Menu
-----------------------------------*/
    $(".tab_list a").click(function (event) {
        //event.preventDefault();
        $(this).parent().addClass("current");
        $(this).parent().siblings().removeClass("current");
        var taby = $(this).attr("href");
        $(".tab_right_section .add_new_list_item, .tab_menu .top_filter_right, .displaynone").hide();
        $(taby + "_btn").show();
        $(".tab_content").not(taby).css("display", "none");
        $(taby).fadeIn();


    });

    $(window).scrollTop(0);

	/*-----------------------------------
       Table sort
-----------------------------------*/

    $(".table .table_title").click(function (e) {
        $(".table .table_title").not($(this)).removeClass("active");
        $(this).addClass("active");
        var iconClass = $(this).children(".icon").hasClass("desc") ? 'asc' : 'desc';
        $(this).children(".icon").removeClass("asc desc");
        $(this).children(".icon").addClass(iconClass);
    });

    $(".top_filter_right .search_icon").click(function (e) {
        $('.search_filter').slideToggle(1000);
    });

    $(".tab_menu .tab_save, .light_box .light_cancel").click(function () {
        $('.light_box, .light_box_overlay').fadeToggle(100);
    });

    $('[data-toggle="tooltip"]').tooltip();

    $(".drop_menu > ul > li > a").click(function () {
        $('.drop_menu_sub').slideToggle(500);
        $(this).toggleClass("drop_menu_sub_active");
    });

    $('body').click(function (e) {
        var container = $(".drop_menu > ul > li > a");

        if (!container.is(e.target) // if the target of the click isn't the container...
            && container.has(e.target).length === 0) // ... nor a descendant of the container
        {
            $(".drop_menu_sub").fadeOut();
        }
    });

    $(".folder_area .create_folder").click(function () {
        $(this).parent().parent().parent(".folder_area").children(".crate_folder_box").fadeToggle(200);
    });

    $(".folder-container li.folder-item").click(function () {
        $(this).parent(".folder-container").children(".folder-wrapper").fadeToggle(200);
    });

    $(".folder_name").click(function () {
        $(this).parent().children(".drag_list").slideToggle(300);
    });

    $(".folder_popup, .create_folder").click(function (event) {
        //event.preventDefault();
        $("#add_folder_popup, .popup_window_overlay").fadeToggle(200);
    });

    $(".doc_popup").click(function () {
        $("#add_doc_popup, .popup_window_overlay").fadeToggle(200);
    });

    $(".popup_close_btn").click(function () {
        $(".popup_window, .popup_window_overlay").fadeOut(200);
    });


    /**NEW change Rewards**/
    $(".search_award_recipt .search_filter_btn").click(function (event) {
        event.preventDefault();
        $(".reward_rept__list_wrap").fadeIn(200);
    });

    //$(".reward_rept_list a").click(function (event) {
    //    event.preventDefault();
    //    $(".reward_rept__list_wrap").fadeOut(200);
    //    $(".reward_rept_item").fadeIn(200);
    //});

    //$('.rewards_testi-1 textarea').on('keyup change', function () {
    //    var text2 = $('.rewards_testi-2 textarea').val();
    //    var selectedUserId = $("div.reward_rept_item").children("div.rept_item_img").children('#hiddenIdSelectedEndUser').attr('val');
    //    if (text2.trim() != "" && $(this).val().trim() != "" && selectedUserId != "") {
    //        $('.form_field .btn_field').prop("disabled", false);
    //    } else {
    //        $('.form_field .btn_field').prop("disabled", true);
    //    }

    //});
    //$('.rewards_testi-2 textarea').on('keyup change', function () {
    //    var text2 = $('.rewards_testi-1 textarea').val();
    //    var selectedUserId = $("div.reward_rept_item").children("div.rept_item_img").children('#hiddenIdSelectedEndUser').attr('val');
    //    if (text2.trim() != "" && $(this).val().trim() != "" && selectedUserId != "") {
    //        $('.send_award .btn_field').prop("disabled", false);
    //    } else {
    //        $('.send_award .btn_field').prop("disabled", true);
    //    }

    //});

    $(".send_award .btn_field").click(function (event) {
        event.preventDefault();
        $(".confirm_popup, .confirm_popup_overlay").fadeIn(200);
    });

    $(".confirm_popup .confclose_btn, .confirm_popup_inner input[type='button']").click(function (event) {
        event.preventDefault();
        $(".confirm_popup, .confirm_popup_overlay").fadeOut(200);
    });


    // A function to check wether the element fits inside the viewport:
    function isElementInViewport(el) {
        var rect = el.getBoundingClientRect();
        var fitsLeft = (rect.left >= 0 && rect.left <= $(window).width());
        var fitsTop = (rect.top >= 0 && rect.top <= $(window).height());
        var fitsRight = (rect.right >= 0 && rect.right <= $(window).width());
        var fitsBottom = (rect.bottom >= 0 && rect.bottom <= $(window).height());
        return {
            top: fitsTop,
            left: fitsLeft,
            right: fitsRight,
            bottom: fitsBottom,
            all: (fitsLeft && fitsTop && fitsRight && fitsBottom)
        };
    }

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

    //$('.date-picker').datepicker({
    //    autoClose: true,
    //    format: 'mm/dd/yyyy',
    //    minDate: new Date(),
    //    position: 'top left', // Default position
    //    onHide: function (inst) {
    //        inst.update('position', 'top left'); // Update the position to the default again
    //    },
    //    onShow: function (inst, animationComplete) {
    //        // Just before showing the datepicker
    //        if (!animationComplete) {
    //            var iFits = false;
    //            // Loop through a few possible position and see which one fits
    //            $.each(['top left', 'left bottom', 'bottom left', 'top center', 'bottom center'], function (i, pos) {
    //                if (!iFits) {
    //                    inst.update('position', pos);
    //                    var fits = isElementInViewport(inst.$datepicker[0]);
    //                    if (fits.all) {
    //                        iFits = true;
    //                    }
    //                }
    //            });
    //        }
    //    },

    //});

  


});

$(window).on('load', function () {
    $(window).scrollTop(0);
});

