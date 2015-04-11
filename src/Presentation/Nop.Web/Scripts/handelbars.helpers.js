define(['handlebars'], function (Handlebars) {
	Handlebars.registerHelper('toLowerCase', function (str) {
		return str.toLowerCase();
	});

	Handlebars.registerHelper('debugger', function (str) {
		debugger;
	});

	Handlebars.registerHelper('get', function (str) {
		debugger;
		return str;
	});
});