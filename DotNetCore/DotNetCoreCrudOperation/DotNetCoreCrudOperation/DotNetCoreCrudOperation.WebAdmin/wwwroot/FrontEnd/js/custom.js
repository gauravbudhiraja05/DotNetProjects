$(document).ready(function () {

    equalheight = function (container) {

        var currentTallest = 0,
            currentRowStart = 0,
            rowDivs = new Array(),
            $el,
            topPosition = 0;
        $(container).each(function () {

            $el = $(this);
            $($el).height('auto')
            topPostion = $el.position().top;

            if (currentRowStart != topPostion) {
                for (currentDiv = 0; currentDiv < rowDivs.length; currentDiv++) {
                    rowDivs[currentDiv].height(currentTallest);
                }
                rowDivs.length = 0; // empty the array
                currentRowStart = topPostion;
                currentTallest = $el.height();
                rowDivs.push($el);
            } else {
                rowDivs.push($el);
                currentTallest = (currentTallest < $el.height()) ? ($el.height()) : (currentTallest);
            }
            for (currentDiv = 0; currentDiv < rowDivs.length; currentDiv++) {
                rowDivs[currentDiv].height(currentTallest);
            }
        });
    }

    $(window).load(function () {
        equalheight('.case_studies_detail > ul > li');
        equalheight('.news_slide_item > .inner');
        equalheight('.md_message > .md_message_inner, .month_stars > .stars_inner');
    });

    $(window).resize(function () {
        equalheight('.case_studies_detail > ul > li');
        equalheight('.news_slide_item > .inner');
        equalheight('.md_message > .md_message_inner, .month_stars > .stars_inner');
    });


    /*-----------------------------------
            THE PICKFORDS INTRANET
    -----------------------------------*/

    $(".my_favrt a.title").click(function (event) {
        event.preventDefault();
        $(this).parent('.my_favrt').toggleClass("open_drop")
        /*$(".my_favrt_drop").slideToggle(200);*/
        $(".my_fav_popup").slideToggle(200);
    });

    $(".user_logged a.title").click(function (event) {
        event.preventDefault();
        $(this).parent('.user_logged').toggleClass("open_logged_drop");
        $(".user_logged_drop").slideToggle(200);
    });

    $(".stars_item .star_icon").click(function () {
        $(".stars_item").removeClass("open_star_hover");
        $(this).parent('.star_text').parent('.front').parent('.stars_item').addClass("open_star_hover");
    });

    $(".star_hover .flip_btn").click(function (event) {
        event.preventDefault();
        $(".stars_item").removeClass("open_star_hover");
    });

    $('body').click(function (e) {
        var container = $(".star_hover, .stars_item");

        if (!container.is(e.target) // if the target of the click isn't the container...
            && container.has(e.target).length === 0) // ... nor a descendant of the container
        {
            $('.stars_item').removeClass("open_star_hover");
        }
    });

    $(".user_logged_drop .edit_profile_btn").click(function (event) {
        event.preventDefault();
        $(".edit_profile_popup").slideDown(200);
    });

    $(".edit_profile_popup .profile_popup_close").click(function (event) {
        event.preventDefault();
        $(".edit_profile_popup").slideUp(200);
    });

	/*$(".my_favrt_drop .see_more_fav_btn").click(function (event) {	
		event.preventDefault();
		$(".my_fav_popup").slideDown(200);
    });*/

    $(".my_fav_popup .fav_popup_close").click(function (event) {
        event.preventDefault();
        $(".my_fav_popup").slideUp(200);
        $('.my_favrt').removeClass("open_drop")
    });


    /*------------------
Upload Image
------------------*/
    $("input:file[class=upload_btn]").change(function (e) {
        var FileUpload = $(this).attr('id');
        if (this.files && this.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {

                $("#" + FileUpload).next("img").attr('src', e.target.result);

            }
            reader.readAsDataURL(this.files[0]);
        }


    });

	/*------------------
    Find Location Filter
    ------------------*/

    $("#find_location_filter .select_item_btn").click(function (event) {
        event.preventDefault();
        $(".find_location_filter").slideToggle();
        $(this).parent('.filter_select_item').toggleClass("active_item");
        $('.top_filter_section').toggleClass("show_item_overlay");

        $('html, body').animate({ scrollTop: $('.content_area').offset().top - 150 }, 500);

        setTimeout(function () {
            $('.location_center').css('display', 'none');
            //$('.find_location_search select').val('');
        }, 500);

    });

    $(".find_location_filter .location_close_btn").click(function (event) {
        event.preventDefault();
        $(".find_location_filter").slideToggle();
        $('#find_location_filter').toggleClass("active_item");
        $('.top_filter_section').toggleClass("show_item_overlay");

        setTimeout(function () {
            $('.location_center').css('display', 'none');
            //$('.find_location_search select').val('');
        }, 500);

    });


    $('.find_location_search select').change(function () {
        if ($(this).val() == "Select...") {
            $('.location_center').fadeOut();
        } else {
            $('.location_center').fadeIn();
        }
    });


	/*------------------
    Find Person Filter
    ------------------*/

    $("#find_person_filter .select_item_btn").click(function (event) {
        event.preventDefault();
        $(".find_person_filter").slideToggle();
        $(this).parent('.filter_select_item').toggleClass("active_item");
        $('.top_filter_section').toggleClass("show_item_overlay");
        $('html, body').animate({ scrollTop: $('.content_area').offset().top - 150 }, 500);

        setTimeout(function () {
            $('.location_center').css('display', 'none');
            $('.find_location_search select').val('');
        }, 500);

    });

    $(".find_person_filter .location_close_btn").click(function (event) {
        event.preventDefault();
        $(".find_person_filter").slideToggle();
        $('#find_person_filter').toggleClass("active_item");
        $('.top_filter_section').toggleClass("show_item_overlay");
        setTimeout(function () {
            $('.location_center').css('display', 'none');
            $('.find_location_search select').val('');
        }, 500);

    });

    $('.find_person_field .search_filter_btn').click(function () {
        $('.location_center').fadeIn()
    });

	/*------------------
    Search Anything Filter
    ------------------*/

   

	/*------------------
    How do Filter
    ------------------*/

    $("#how_do_filter .search_filter_btn").click(function (event) {
        event.preventDefault();
        

    });

    $(".how_do_filter .location_close_btn").click(function (event) {
        event.preventDefault();
        

    });

	/*------------------
    Header Sticky
    ------------------*/

    var stickyTop = $('.content_area').offset().top - 40;

    $(window).on('scroll', function () {

        if ($(window).scrollTop() >= stickyTop) {
            $('.top_filter_section').addClass('stickyfilter');

        } else {
            $('.top_filter_section').removeClass('stickyfilter');
        }
    });



    $('.navMegaMenu > li').each(function (index) {
        $(this).addClass('drop_down');
    });

    $(".iphone_menu_icon").click(function () {
        $(".iphone_header").toggleClass("open_iphone_menu");
        $(".iphone_menu_area").fadeToggle();
    });
    $(".iphone_collpsed_menu .mm-next").click(function (event) {
        event.preventDefault();
        $(this).parent().toggleClass("active_li");
        $(this).parent().children(".iphone_drop_menu").slideToggle("swing");
        $(this).toggleClass("active_mm");
    });


    if ($('.navMegaMenu li div.dropdown_menu').size() > 0) {
        $('.navMegaMenu li div.dropdown_menu').parent('.navMegaMenu > li').addClass("has_sub");
    }
    $(".navMegaMenu > li").mouseenter(function () {
        $(this).children("div.dropdown_menu").fadeIn();
        $(this).addClass("open_dropdown");
    });
    $(".navMegaMenu > li").mouseleave(function () {
        $(this).children("div.dropdown_menu").hide();
        $(this).removeClass("open_dropdown");
    });

    $(".navMegaMenu > li").click(function () {
        $(this).children("div.dropdown_menu").fadeToggle();
        $(this).toggleClass("open_dropdown");
    });

	/*------------------
    Accordion 
    ------------------*/

    $(".accordion_title").click(function () {
        var thisChildren = $(this).parent().children(".accordion_content");
        var allChildren = $(".accordion_content");
        if ($(thisChildren).css('display') == 'block') {
            $(thisChildren).slideUp("fast");
            $(".accordion_title").removeClass("active");
        } else {
            $(allChildren).slideUp("fast");
            $(".accordion_title").removeClass("active");
            $(thisChildren).slideDown("fast");
            $(this).addClass("active");
        }
    });

    $(".select_picker_area").appendTo(".append_select_pk");

	/*------------------
    News Page
    ------------------*/

    //size_li = $(".case_studies_detail > ul > li").size();

    //if (size_li <= 12) {
    //    $(".more_case_studies").hide()
    //}

    //if (size_li <= 8) {
    //    $(".more_case_studies a").hide();
    //}

    //if (size_li <= 4) {
    //    $(".deatils_btn a").hide();
    //}

    //x = 12
    //$('.news_article > ul > li:lt(' + x + ')').show();

    //$('.more_case_studies a').click(function (event) {
    //    event.preventDefault();
    //    x = (x + 4 <= size_li) ? x + 4 : size_li;
    //    $('.news_article > ul > li:lt(' + x + ')').slideDown();

    //    setTimeout(function () {
    //        equalheight('.case_studies_detail > ul > li')
    //    }, 200)

    //    if (x == size_li) {
    //        $(this).hide();
    //    }

    //});

    z = 4
    //$('.details_article > ul > li:lt(' + z + ')').show();
    ////$('.deatils_btn a').click(function (event) {
    ////    event.preventDefault();
    ////    z = (z + 4 <= size_li) ? z + 4 : size_li;
    ////    $('.details_article > ul > li:lt(' + z + ')').slideDown();

    ////    setTimeout(function () {
    ////        equalheight('.case_studies_detail > ul > li')
    ////    }, 500)

    ////    if (z == size_li) {
    ////        $(this).hide();
    ////    }

    ////});

/*------------------
	Stars JS
	------------------*/
	star_len = $(".stars_grid .stars_grid_item").length;

	if (star_len <= 12) {
		$(".our_values_btn .load_more_star").hide()
	}

	st = 12
	$('.stars_grid .stars_grid_item:lt(' + st + ')').show();

	$('.our_values_btn .load_more_star').click(function (event) {
		event.preventDefault();
		st = (st + 4 <= star_len) ? st + 4 : star_len;
		$('.stars_grid .stars_grid_item:lt(' + st + ')').fadeIn(300);
      
    setTimeout(function () {
			equalheight('.md_message > .md_message_inner, .month_stars > .stars_inner');
		}, 600)
      

		if (st == star_len) {
			$('.our_values_btn .load_more_star').hide();
			$(".show_less").show();
		}

	});

	$('.news_item_grid .show_less a').click(function () {
		st = (st - 3 < 0) ? 3 : st - 3;
		$('.news_item_grid .grid_item').not(':lt(' + st + ')').slideUp();
		if (st <= 12) {
			$(".show_less").hide();
			$(".show_more").show();
			setTimeout(function () {
				$(".show_more>a").focus();
			}, 200);
		}
	});

    $('[data-toggle="tooltip"]').tooltip();


    $(".baggage_form_btn #pnlcallmebacksection .call_u_btn").click(function (event) {
        event.preventDefault();
        $(this).toggleClass("open_baggage_form");
        $("#call_me_back_area").slideToggle()
    });
    $(".baggage_form_btn #pnlbaggageCallToActionSection .call_u_btn").click(function (event) {
        event.preventDefault();
        $(this).toggleClass("open_baggage_form");
        $("#baggage_form_area").slideToggle()
    });

    $(".search_icon").click(function (event) {
        event.preventDefault();
        $(".header_local_pickfords, .iphone_search_area").addClass("open_search");
        $(".header_top_menu").addClass("hide_top_menu");
    });
    $(".search_close").click(function (event) {
        event.preventDefault();
        $(".header_local_pickfords, .iphone_search_area").removeClass("open_search");
        $(".header_top_menu").removeClass("hide_top_menu");
    });


    var items = $('.news_items_slider .news_slide_item');
    if (items.length > 1) {
        $(".news_items_slider").owlCarousel({
            autoplayTimeout: 4500,
            autoplayHoverPause: true,
            autoplay: false,
            items: 4,
            margin: 20,
            dots: false,
            nav: true,
            loop: false,
            responsive: {
                0: {
                    items: 2
                },
                768: {
                    items: 3
                },
                979: {
                    items: 3
                },
                1199: {
                    items: 4
                }
            }

        });
    } else {
    }
    var items = $('.pickfords_gold_slider .item');
    if (items.length > 1) {
        $(".pickfords_gold_slider").owlCarousel({
            autoplayTimeout: 4500,
            autoplayHoverPause: true,
            autoplay: false,
            items: 1,
            dots: true,
            nav: false,
            loop: true,
            responsive: {
                0: {
                    items: 1
                },
                768: {
                    items: 1
                },
                979: {
                    items: 1
                },
                1199: {
                    items: 1
                }
            }

        });
    } else {
    }

    var items = $('.twitter_post_slider .item');
    if (items.length > 1) {
        $(".twitter_post_slider").owlCarousel({
            autoplayTimeout: 4500,
            autoplayHoverPause: true,
            autoplay: true,
            items: 1,
            dots: false,
            nav: false,
            loop: true,
            responsive: {
                0: {
                    items: 1
                },
                768: {
                    items: 1
                },
                979: {
                    items: 1
                },
                1199: {
                    items: 1
                }
            }

        });
    } else {
    }

    var items = $('.stars_text_slider .item');
    if (items.length > 1) {
        $(".stars_text_slider").owlCarousel({
            autoplayTimeout: 10000,
            autoplayHoverPause: true,
            autoplay: true,
            items: 1,
            autoHeight: true,
            dots: false,
            nav: true,
            loop: true,
            responsive: {
                0: {
                    items: 1
                },
                768: {
                    items: 1
                },
                979: {
                    items: 1
                },
                1199: {
                    items: 1
                }
            }

        });
    } else {
    }


   

    //$(".update_date a").click(function (event) {
    //    event.preventDefault();
    //    var h1 = $(".header_top_area").height();
    //    var h2 = $(".top_filter_section").outerHeight();
    //    var h3 = h1 + h2;
    //    $(".leave_step_2, .leave_step_btn_2, .booked_leave_list").fadeIn(200);
    //    $(".leave_step_3, .leave_step_btn_3, .view_more_leave").hide();

    //    $('html, body').stop().animate({
    //        scrollTop: $("#leavestep2").offset().top - h3
    //    }, 1000);
    //});

   
    
});



$(function () {
    $('a[href*=#]:not([href=#])').click(function () {
        if (location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '') && location.hostname == this.hostname) {
            var target = $(this.hash);
            target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
            if (target.length) {
                $('html,body').animate({
                    scrollTop: target.offset().top - 40
                }, 1000);
                return false;
            }
        }
    });
});






