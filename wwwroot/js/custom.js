	$(".js-height-full").height($(window).height());
	$(".js-height-parent").each(function() {
	    $(this).height($(this).parent().first().height());
	});


	// Fun Facts
	function count($this) {
	    var current = parseInt($this.html(), 10);
	    current = current + 1; /* Where 50 is increment */

	    $this.html(++current);
	    if (current > $this.data('count')) {
	        $this.html($this.data('count'));
	    } else {
	        setTimeout(function() {
	            count($this)
	        }, 50);
	    }
	}

	$(".stat-timer").each(function() {
	    $(this).data('count', parseInt($(this).html(), 10));
	    $(this).html('0');
	    count($(this));
	});



	$('.header').affix({
	    offset: {
	        top: 100,
	        bottom: function() {
	            return (this.bottom = $('.footer').outerHeight(true))
	        }
	    }
	})

	$(window).load(function() {
	    $("#preloader").on(500).fadeOut();
	    $(".preloader").on(600).fadeOut("slow");
	});

$('.selectpicker').on('changed.bs.select', function (e) {
	var sort = this.options[this.selectedIndex].value;
	
	//alert(sort);
	var dir = $('itemsorter');
	var url = dir.attr('action');
	var addr = "/items/sort/"
	var Sortby = 'Sortby='+sort;
	$.ajax(
		{
			url: '/items/sort/',
			type: 'POST',
			data: Sortby,//.replace(/\?/g, '/'),
			success: function (result) {
				$('#shop-grid').html(result);
			}

		});
	$('#loader').hide();
	$(document).ajaxStart(function (e) {
		$('div.row.blog-grid.shop-grid').empty();
		$('#loader').show();
	}).ajaxStop(function () {
		$('#loader').hide();
	});

});

$("#sendReview").click(function () {
	var data = {};
	data.ItemId =$("#ItemId").val();
	data.rating = $("#rating").val();
	data.CommentBody = $("#CommentBody").val();
	data.CommentTitle = $("#CommentTitle").val();
	data.__RequestVerificationToken = $('input[name="__RequestVerificationToken"]').val();;
	//data.publishdate = 'PublishTime=' + Date.now().toString;
	//alert(data.publishdate);
		alert(data.__RequestVerificationToken);
	var url = $("#send").attr("formaction");


	$.ajax(
		{
			url: url,
			type: 'POST',
			data: data,//.replace(/\?/g, '/'),
			success: function (result) {
				
				alert(result);
				$("#menu1").hide();
				getitemcomments();
			}

		});
		
});
/*
	$('#loader').hide();
	$(document).ajaxStart(function () {
		$('div.row.blog-grid.shop-grid').empty();
		$('#loader').show();
	}).ajaxStop(function () {
		$('#loader').hide();
	});
	*/
$('.ratings_stars').hover(
	// Handles the mouseover
	function () {
		$(this).prevAll().andSelf().addClass('ratings_over');
		$(this).nextAll().removeClass('ratings_vote');
	},
	// Handles the mouseout
	function () {
		$(this).prevAll().andSelf().removeClass('ratings_over');

	}
).click(function () {
	$('.ratings_stars').removeClass('selected'); // Removes the selected class from all of them
	$(this).prevAll().andSelf().addClass('selected').removeClass('ratings_over'); // Adds the selected class to just the one you clicked

	var rating = $(this).data('rating');
	//alert(rating);
	// Get the rating from the selected star
	$('#rating').val(rating); // Set the value of the hidden rating form element
});
$(document).ready( function () {
	$('.nav-tabs a[href="#menu1"]').on('show.bs.tab',getitemcomments = function (e) {


		var url = $("#getComments").attr("formaction");
		$.ajax(
			{
				url: url,
				type: 'GET',
				//data: itemId,
				success: function (comments) {
					$('#comment-section').html(comments);
				}


			});
		$('#loader').hide();
		$(document).ajaxStart(function (e) {
			$('div.comment-section').empty();
			$('#loader').show();
		}).ajaxStop(function () {
			$('#loader').hide();
		});

	});
});
$(window).scroll(function () {
	if ($(this).scrollTop() > 100) {
		$('.scrolltop').fadeIn();
	} else {
		$('.scrolltop').fadeOut();
	}
});

$('.scrolltop').click(function () {
	$("html, body").animate({
		scrollTop: 0
	}, 600);
	return false;
});


$("nav.navbar.bootsnav .attr-nav").each(function () {
	$("li.search > a", this).on("click", function (e) {
		e.preventDefault();
		$(".top-search").slideToggle();
	});
});
$(".input-group-addon.close-search").on("click", function () {
	$(".top-search").slideUp();
});

// ------------------------------------------------------------------------------ //
// Toggle Side Menu .attr-nav
// ------------------------------------------------------------------------------ //
$("nav.navbar.bootsnav ").each(function () {
	$("li.side-menu > a", this).on("click", function (e) {
		e.preventDefault();
		$("nav.navbar.bootsnav > .side").toggleClass("on");
		$("body").toggleClass("on-side");
	});
});
$(".side .close-side").on("click", function (e) {
	e.preventDefault();
	$("nav.navbar.bootsnav > .side").removeClass("on");
	$("body").removeClass("on-side");
});
