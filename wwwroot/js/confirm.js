function dltcfr(e) {
	console.log('Item removal action succeeded: ' + e);
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
				url = "/Locations/Delete/" + e;
				$.ajax({
					url: url,
					type: 'GET',
					success: function (res) {
						alert(res);
						//$('ul.cart-list').children('li').fadeOut(500);
						//listCart();
					}
				})
			};
		}
	});
};