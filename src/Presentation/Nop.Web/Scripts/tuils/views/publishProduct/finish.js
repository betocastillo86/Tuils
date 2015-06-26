define(['jquery', 'underscore', 'backbone', 'handlebars', 'accounting'],
    function ($,_, Backbone, Handlebars, accounting) {
    var PublishFinishedView = Backbone.View.extend({

        images : undefined,

        initialize: function (args) {
            this.template = Handlebars.compile($("#templateFinishPublishProduct").html());
            this.model = args.model;
            this.images = args.images;
            this.render();
        },
        render: function () {
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