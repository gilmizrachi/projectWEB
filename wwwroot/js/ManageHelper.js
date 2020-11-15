function dltcfr(e) {
	url = "/Transactions/Delete/" + e;
	$.ajax({
		url: url,
		type: 'GET',
		success: function (res) {
			nxt(res);
		}
	})
	console.log('Item removal action succeeded: ' + e);
	function nxt(i) {
		bootbox.confirm({
			message: i,
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
					$("div.modal-body > div > div > form > input.btn.btn-default").submit();

				};
			}
		});
	};
};