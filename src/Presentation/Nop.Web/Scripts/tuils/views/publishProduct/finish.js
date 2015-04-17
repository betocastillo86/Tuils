define(['jquery', 'underscore', 'backbone', 'handlebars', 'accounting'], function ($, _, Backbone, Handlebars, accounting) {
    var PublishFinishedView = Backbone.View.extend({

        template : Handlebars.compile($("#templateFinishPublishProduct").html()),
        images : undefined,

        initialize: function (args) {
            this.model = args.model;
            this.images = args.images;
            this.render();
        },
        render: function () {
            debugger;
            this.$el.html(this.template(
                {
                    ImgSrc: this.images ? this.images.at(0).toJSON().src : undefined,
                    Name: this.model.get('Name'),
                    Price: accounting.formatMoney(this.model.get('Price'), { precision: 0 })
                }));
            return this;
        }
    }); 

    return PublishFinishedView;
});