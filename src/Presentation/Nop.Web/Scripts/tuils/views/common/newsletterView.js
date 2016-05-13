define(['jquery', 'underscore', 'backbone', 'baseView', 'tuils/models/newsletter'],
    function ($, _, Backbone, BaseView, NewsletterModel) {
    var NewsletterView = BaseView.extend({
        el : '.block-newsletter',
        subscribeProgress: undefined,
        
        events: {
            'click #newsletter-subscribe-button' : 'suscribe'
        },
        bindings: {
            '#newsletter-email' : 'Email'
        },
        initialize: function () {
            this.model = new NewsletterModel();
            this.model.on("sync", this.suscribed, this);
            this.model.on("Error", this.errorSuscription, this);
            this.render();
        },
        loadControls : function(){
            this.subscribeProgress = this.$("#subscribe-loading-progress");
        },
        suscribe : function()
        {
            this.validateControls();
            if (this.model.isValid())
            {
                this.subscribeProgress.show();
                this.model.suscribe();
            }
        },
        suscribed: function (model) {
            this.subscribeProgress.hide();
            this.$("#newsletter-result-block").html(model.toJSON().Result);
            if (model.toJSON().Success) {
                this.$('#newsletter-subscribe-block').hide();
                this.$('#newsletter-result-block').show();
            }
            else {
                this.$('#newsletter-result-block').fadeIn("slow").delay(2000).fadeOut("slow");
            }
        },
        errorSuscription : function(){
            this.alertError('Failed to subscribe.');
            this.subscribeProgress.hide();
        },
        render: function () {
            this.loadControls();
            this.bindValidation();
            this.stickThem();
            return this;
        }
    });
    return NewsletterView;
});