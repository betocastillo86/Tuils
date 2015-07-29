﻿
define(['jquery', 'underscore', 'baseView','jqueryui', 'slide'],
    function ($, _, BaseView) {

    var HomeView = BaseView.extend({
        initialize: function (args) {
            this.loadSlider();
        },
        loadSlider : function()
        {
            var totalSlides = $(".rslides li").length;
            var that = this;
            $(".rslides").responsiveSlides({
                timeout: 7000,
                pager: true,
                nav: true,
                prevText: '<span class="icon-prev"></span>',
                nextText: '<span class="icon-next"></span>',
                before: function (numSlide) {
                    if(numSlide+1 <= totalSlides)
                    {
                        var image = $($(".rslides li img").get(numSlide+1));
                        if (image.attr('src') === '')
                            image.attr('src', image.attr('data-src'));
                    }

                }
            });

            this.handleResize();
            this.on("window-resized-max", this.resizeBanner, this);
            this.on("window-resized-min", this.resizeBanner, this);
            this.resizeBanner();
        },
        resizeBanner: function () {
            //Si es pantalla pequeña recalcula el alto del banner
            $(".rslides_container ul").css("height", this.isMinSize() ? (window.innerWidth / 3.5)+"px" : "");
        },
        render: function ()
        {
            return this;
        }
    });

    return HomeView;
});



