define(['jquery', 'underscore', 'backbone', 'router'], function ($, _, Backbone, TuilsRouter) {
    var TuilsApp = {
        router: undefined,

        init: function () {
            TuilsApp.router = new TuilsRouter();
            Backbone.history.start({ pushState: true });

            $(document).ajaxError(this.navigateFastLogin);
        }
    }
    
    return TuilsApp;
});


