/*  ---------------------------------------------------
    Template Name: Fashi
    Description: Fashi eCommerce HTML Template
    Author: Colorlib
    Author URI: https://colorlib.com/
    Version: 1.0
    Created: Colorlib
---------------------------------------------------------  */

'use strict';

(function ($) {

    /*------------------
        Preloader
    --------------------*/
    //$(window).on('load', function () {
    //    $(".loader").fadeOut();
    //    $("#preloder").delay(200).fadeOut("slow");
    //});
        $(".loader").fadeOut();
        $("#preloder").delay(200).fadeOut("slow");

    $(".depart-btn").on("click", function () {
        $(".depart-hover").toggleClass("activeCategory")
    })
    /*------------------
        Background Set
    --------------------*/
    $('.set-bg').each(function () {
        var bg = $(this).data('setbg');
        $(this).css('background-image', 'url(' + bg + ')');
    });

    /*------------------
		Navigation
	--------------------*/
    $(".mobile-menu").slicknav({
        label: RESOURCE_MENULABEL,
        prependTo: '#mobile-menu-wrap',
    });
    $(".slicknav_nav").append(`
    <div class="slickIcon-close"><i class="fas fa-times fa-2x"></i> </div>
    `)
    $(".slickIcon-close").click(function () {
        $(".slicknav_btn ").removeClass("slicknav_open").addClass("slicknav_collapsed");
        $(".slicknav_nav").addClass("slicknav_hidden")
        $(".slicknav_nav").slideUp(300)
    })
    $(".slicknav_menutxt").html("")

    /*------------------
        Hero Slider
    --------------------*/
    $(".hero-items").owlCarousel({
        rtl: ISRTL,
        loop: true,
        margin: 0,
        nav: true,
        items: 1,
        dots: false,
        animateOut: 'fadeOut',
        animateIn: 'fadeIn',
        navText: ['<i class="fas fa-chevron-left"></i>', '<i class="fas fa-chevron-right"></i>'],
        smartSpeed: 1200,
        autoHeight: false,
        autoplay: true
    });

    /*------------------
        Product Slider
    --------------------*/
    $(".product-slider").owlCarousel({
        rtl: ISRTL,
        loop: true,
        margin: 25,
        nav: false,
        items: 4,
        dots: true,
        navText: ['<i class="fas fa-chevron-left"></i>', '<i class="fas fa-chevron-right"></i>'],
        smartSpeed: 1200,
        autoHeight: false,
        autoplay: true,
        responsive: {
            0: {
                items: 2,
            },
            576: {
                items: 2,
            },
            992: {
                items: 3,
            },
            1200: {
                items: 4,
            }
        }
    });

    /*------------------
       logo Carousel
    --------------------*/
    $(".logo-carousel").owlCarousel({
        rtl: ISRTL,
        loop: false,
        margin: 30,
        nav: false,
        items: 6,
        dots: false,
        navText: ['<i class="fas fa-chevron-left"></i>', '<i class="fas fa-chevron-right"></i>'],
        smartSpeed: 1200,
        autoHeight: false,
        mouseDrag: false,
        autoplay: true,
        responsive: {
            0: {
                items: 3,
            },
            768: {
                items: 6,
            }
        }
    });

    /*-----------------------
       Product Single Slider
    -------------------------*/
    $(".ps-slider").owlCarousel({
        rtl: ISRTL,
        loop: false,
        margin: 10,
        nav: true,
        items: 3,
        dots: false,
        navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
        smartSpeed: 1200,
        autoHeight: false,
        autoplay: true,
    });

    $(document).ready(function (e) {
        
    });

    /*-------------------
		Radio Btn
	--------------------- */
    $(".fw-size-choose .sc-item label, .pd-size-choose .sc-item label").on('click', function () {
        $(".fw-size-choose .sc-item label, .pd-size-choose .sc-item label").removeClass('active');
        $(this).addClass('active');
    });

    /*-------------------
		Nice Select
    --------------------- */
    $('.sorting, .p-show').niceSelect();

    /*------------------
		Single Product
	--------------------*/
    $('.product-thumbs-track .pt').on('click', function () {
        $('.product-thumbs-track .pt').removeClass('active');
        $(this).addClass('active');
        var imgurl = $(this).data('imgbigurl');
        var bigImg = $('.product-big-img').attr('src');
        if (imgurl !== bigImg) {
            $('.product-big-img').attr({ src: imgurl });
            $('.zoomImg').attr({ src: imgurl });
        }
    });

    $('.product-pic-zoom').zoom();
})(jQuery);