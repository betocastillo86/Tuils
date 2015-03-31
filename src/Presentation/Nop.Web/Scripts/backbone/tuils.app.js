var TuilsApp = {
	router : undefined,

	init: function ()
	{
		this.router = new TuilsRouter();
		Backbone.history.start({ pushState: true });
        
	}
}

$(document).on("ready", TuilsApp.init);