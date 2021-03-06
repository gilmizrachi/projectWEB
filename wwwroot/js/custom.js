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
//Sort function for mainshop page-> fully working + tested
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
			data: Sortby,
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
//add comment for item -> fully working + tested 
$("#sendReview").click(function () {
	var data = {};
	data.ItemId =$("#ItemId").val();
	data.rating = $("#rating").val();
	data.CommentBody = $("#CommentBody").val();
	data.CommentTitle = $("#CommentTitle").val();
	data.__RequestVerificationToken = $('input[name="__RequestVerificationToken"]').val();;

	//	alert(data.__RequestVerificationToken);
	var url = $("#send").attr("formaction");


	$.ajax(
		{
			url: url,
			type: 'POST',
			data: data,
			success: function (result) {
				
				//alert(result);
				$("#menu1").hide();
				getitemcomments();
			}

		});
		
});
//animation for rating stars
$('.ratings_stars').hover(
	
	function () {
		$(this).prevAll().andSelf().addClass('ratings_over');
		$(this).nextAll().removeClass('ratings_vote');
	},
	
	function () {
		$(this).prevAll().andSelf().removeClass('ratings_over');

	}
).click(function () {
	$('.ratings_stars').removeClass('selected'); // Removes the selected class from all of them
	$(this).prevAll().andSelf().addClass('selected').removeClass('ratings_over'); // Adds the selected class to just the one you clicked

	var rating = $(this).data('rating');
	//alert(rating);
	// Get the rating from the selected star
	$('#rating').val(rating); // Set the value of the hidden rating form element - need to update at DB
});
/*$(document).ready(function () {
	alert(document.cookie);
});*/
//pulls comment section on every item single view -> fully working  
//TODO: static representation of item rating at item details page 
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


//side bar cart on shop layout
$("nav.navbar.bootsnav ").each(function () {
	$("li.side-menu > a", this).on("click", function (e) {
		e.preventDefault();
		listCart();
		//removeitem();
		$("nav.navbar.bootsnav > .side").toggleClass("on");
		$("body").toggleClass("on-side");
	});
});
$(".side .close-side").on("click", function (e) {
	e.preventDefault();
	$("nav.navbar.bootsnav > .side").removeClass("on");
	$("body").removeClass("on-side");
});
//pulls list of items for current user ---not yet finished--- 
 function listCart() {
	var cart = $("#GetCart").attr("href");
	$.ajax(
		{
			url: cart,
			type: 'GET',
			success: function (items) {
				$('ul.cart-list').html(items);
			}
		});
};

$("#confirmpayment").on("click", function (e) {
	e.preventDefault();
	var card = {};
	card.CreditCardNo = $("#cardno").val();
	card.__RequestVerificationToken = $('input[name="__RequestVerificationToken"]').val();

	//	alert(card.__RequestVerificationToken);
	//var url = $("#addpay").attr("href");
	var url = "/RegisteredUsers/AddPayment";
	$.ajax(
		{
			url: url,
			type: 'POST',
			data: card,
			success: function (result) {
				$.ajax({
					url: "/Transactions/CommitToBuy",
					type: 'GET'
				});
				console.log('Payment succeeded:' + result);
			}
		});
});
//react to "add to cart" buttons both on mainshop or single item view
$("#AddToCart").click(function (e) {
	e.preventDefault();
	carturl = $("#AddToCart").attr("formaction");
	$.ajax({
		url: carturl,
		type: 'GET',
		success: function () { console.log('Item added to cart via item page'); }
	})
});
// /Transactions/Addtocart/  #AddCartLink
again = $('.list-inline a[href="#"]').on('click', function (e) {
	e.preventDefault();
	$(this).parent().addClass('animated bounceOutUp');
	url = "/Transactions/Addtocart/" + $(this).attr('value');
	$.ajax({
		url: url,
		type: 'GET',
		success: function () { console.log('Item added to cart via main page'); }
	})
});


//#rmv-from-cart
function removeitem() {
	$("#rmv-from-cart").on("click", function (e) {
		alert("Pressed");
		e.preventDefault();
		var remove = $(this).parent();
		var removepanel = remove.next();
		$.get(remove.attr("formaction"), function (data, status) {
			if (data) {
				//var id = $(this).val();
				removepanel.fadeOut(3000);
			}
		});
	});
};
if($('.recomendation')[0]){
	url = "/AlsoTries/Recomended"
	$.ajax(
		{
			url: url,
			type: 'GET',
			success: function (items) {
				$('div.related-products').html(items);
			}
		});

};
function rfc(e) {
	//bootbox.alert("test rfc "+e);
	bootbox.confirm({
		message: "Are you sure?",
		buttons: {
			confirm: {
				label: 'Yes',
				className: 'btn-success'
			},
			cancel: {
				label: 'No',
				className: 'btn-danger'
			}
		},
		callback: function (result) {
			console.log('Item removal action succeeded: ' + result);
			if (result == false) { console.log('No changes were made'); }
			else {
				url = "/Transactions/rmvfrmcart/" + e;
				$.ajax({
					url: url,
					type: 'GET',
					success: function () {
						$('ul.cart-list').children('li').fadeOut(500);
						listCart();
					}
				})
			};
		}
	});
};
function nxpg(dx) {
	var data = $("#ItemsVal").val();
	data = JSON.parse(data);
	//console.log(data);
	const len = data.length;
$("#shop-grid").html("");
	for (var it = 12 * (dx-1); it < len && it < 12*dx ;it++) {
		//console.log(data[it]);
		//console.log(data[it].ItemName);
		//console.log(data[it].id);
		var ItemCard = '<div class="col-md-3">' +
			'<div class="course-box shop-wrapper">' +
			'<div class="image-wrap entry">' +
			'<img src="/upload/items/' + data[it].id + '-0.jpg" alt="" style="height:30vh" class="img-responsive">' +
			'<div class="magnifier">' +
			'<a href"item_details/' + data[it].id + '"  title=""><i class="flaticon-add"></i></a>' +
			'</div>' +
			'</div>' +
			'<!-- end image-wrap -->' +
			'<div class="course-details shop-box text-center">' +
			'<h4>' +
			'<a href="item_details/' + data[it].id + '"  title="">' + data[it].ItemName + '</a>' +
			'<small>' + data[it].ItemDevision + '</small>' +
			'</h4>' +
			'</div>' +
			'<div class="course-footer clearfix">' +
			'<div class="pull-left">' +
			'<ul class="list-inline">' +
			'<li><a href="#" id="AddCartLink" value="' + data[it].id + '"><i class="fa fa-shopping-basket"></i> Add Cart</a><input type="hidden" style="display:none" href="/Transactions/Addtocart/' + data[it].id + '" id="AddToCart" /></li>'+
/* @if (item.amount > 0)
			 {

			 }
			 else
			 {
			 <a> <i class="fa fa-truck">This item is currntly out of stock</i> </a>
			 }*/
			'</ul>' +
			'</div><!-- end left -->' +
			// @if (ViewBag.membertype != "BasicUser")
			//{<a asp-action="Edit" asp-route-id="@item.id">Edit</a>}
			'<div class="pull-right">' +
			'<ul class="list-inline">' +
			'<li><a href="#">$' + data[it].price + '</a></li>' +
			'</ul>' +
			'</div><!-- end left -->' +
			'</div><!-- end footer -->' +
			'</div><!-- end box -->' +
			'</div><!-- end col -->'

		
		$("#shop-grid").append(ItemCard);

	}
	$(".pagination > .active").removeClass("active");
	var spec = '<p> Showing ' + ((12 * (dx - 1))+1) + '-' + (12 * dx) + ' of ' + len + ' results</p>';
	$("#showing").html(spec);
	//$('li > #'+dx+'.pg').addClass("active");
	again;
	

}
$(".pagination  li  a").on("click", function () {
	
	$(this).parent().addClass("active");
});