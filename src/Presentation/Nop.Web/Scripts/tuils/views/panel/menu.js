define(['underscore', 'backbone'], function (_, Backbone) {
    var MenuPanelView = Backbone.View.extend({

        viewLogin: undefined,
        viewRegister: undefined,

        events: {
            'click .nav-with-sub': 'openMenu'
        },
        initialize: function (args) {

        },
        openMenu : function(ev)
        {
            $(ev.target).parent().find("ul").show();
        },
        render: function () {
            return this;
        }
    });
    return MenuPanelView;
});