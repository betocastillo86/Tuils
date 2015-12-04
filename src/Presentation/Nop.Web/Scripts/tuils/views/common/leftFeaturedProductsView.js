define(['jquery', 'underscore', 'backbone', 'baseView', 'tuils/models/newsletter', 'jqueryui', 'carousel'],
    function ($, _, Backbone, BaseView, NewsletterModel) {
    var LeftFeaturedProductsView = BaseView.extend({
        initialize: function () {
            this.render();
        },
        loadControls: function () {
            this.$("#owl-home-page-bestsellers").owlCarousel({
                loop: true,
                margin: 10,
                items:1
                //nav:true,
                /*responsive: {
                    0: {
                        items: 1
                    },
                    480: {
                        items: 2
                    },
                    600: {
                        items: 3
                    },
                    768: {
                        items: 1
                    }
                }*/
            });
        },
        render: function () {
            this.loadControls();
            return this;
        }
    });
    return LeftFeaturedProductsView;
});