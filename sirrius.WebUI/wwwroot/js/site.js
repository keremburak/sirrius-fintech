var config = {
    date_catcher: $(".current-date")
}

var initApp = (function (app) {
	if (config.date_catcher.length) {
		var months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
			day = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'],
			now = new Date(),
			formatted = day[now.getDay()] + ', ' +
				months[now.getMonth()] + ' ' +
				now.getDate() + ', ' +
				now.getFullYear();
		config.date_catcher.text(new Date(formatted).toLocaleDateString('en-GB'));
	}
})({});