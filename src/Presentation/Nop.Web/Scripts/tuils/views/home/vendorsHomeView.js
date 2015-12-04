define(['jquery', 'underscore', 'baseView', 'jqueryui'],
    function ($, _, BaseView) {

        var VendorsHomeView = BaseView.extend({
            initialize: function (args) {
                this.loadCarousel();
            },
            loadCarousel: function (obj) {
                $("#vendorCarousel").owlCarousel({
                    loop: true,
                    items: 6, //10 items above 1000px browser width
                    itemsDesktop: [600, 3], //5 items between 1000px and 901px
                    itemsDesktopSmall: [600, 3], // betweem 900px and 601px
                    itemsTablet: [600, 3], //2 items between 600 and 0
                    itemsMobile: false // itemsMobile disabled - inherit from itemsTablet option
                });
                
            }
        });

        return VendorsHomeView;
    });