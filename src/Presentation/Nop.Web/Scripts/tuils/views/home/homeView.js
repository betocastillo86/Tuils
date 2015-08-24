
define(['jquery', 'underscore', 'baseView','tuils/views/home/categoriesHomeView', 'jqueryui', 'slide'],
    function ($, _, BaseView, CategoriesHomeView) {

    var HomeView = BaseView.extend({
        initialize: function (args) {
            this.loadSlider();
            this.loadCategories();
        },

        viewCategories : undefined,
        loadSlider : function()
        {
            var totalSlides = $(".rslides li").length;
            var that = this;
            $(".rslides").responsiveSlides({
                timeout: 7000,
                pager: true,
                nav: true,
                prevText: '<span class="icon-prev" title="Anterior"></span>',
                nextText: '<span class="icon-next" title="Siguiente"></span>',
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
        loadCategories: function () {
            if (this.isMobile())
            {
                this.viewCategories = new CategoriesHomeView({ el: '.conte_categories' });
            }
        },
        resizeBanner: function () {
            //Si es pantalla pequeña recalcula el alto del banner
            $(".rslides").css("height", this.isMinSizeMobile() ? (window.innerWidth / 2.5) + "px" : "");
        },
        render: function ()
        {
            return this;
        }
    });

    return HomeView;
});



