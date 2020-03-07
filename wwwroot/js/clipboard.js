window.clipboardFunctions = {	
	set: function (content) {
		navigator.clipboard.writeText(content).then(function () {
			console.log('clipboard.set', content);
			return "true";
		}, function () {
			console.log('clipboard.set failed');
			return "false";
		});
	},
	get: function () {
		return navigator.clipboard.readText();
	}
}