
define(['jquery', 'underscore', 'baseView','jqueryui', 'slide'],
    function ($, _, BaseView) {

    var HomeView = BaseView.extend({
        initialize: function (args) {
            this.loadSlider();
        },
        loadSlider : function()
        {
            var totalSlides = $(".rslides li").length;
            $(".rslides").responsiveSlides({
                timeout: 7000,
                pager: true,
                nav: true,
                before: function (numSlide) {
                    if(numSlide+1 <= totalSlides)
                    {
                        var image = $($(".rslides li img").get(numSlide+1));
                        if (image.attr('src') === '')
                            image.attr('src', image.attr('data-src'));
                    }
                }
            });
        },
        render: function ()
        {
            return this;
        }
    });

    return HomeView;
});



