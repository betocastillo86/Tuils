
define(['jquery', 'underscore', 'baseView', 'tuils/views/home/categoriesHomeView', 'tuils/views/home/vendorsHomeView', 'jqueryui', 'slide'],
    function ($, _, BaseView, CategoriesHomeView, VendorsHomeView) {

        var HomeView = BaseView.extend({

            events: {
                'click .rslides_container a': 'clickOnBanner',
                'click .conte_marcas a': 'clickOnManufacturer',
                'click .product-grid a': 'clickOnProduct',
                'click .home-tiendas_grid a': 'clickOnStore',
                'click .conte_categories a': 'clickOnCategory',
                'click .prods-me a': 'clickOnMyProducts'
            },

            initialize: function (args) {
                this.loadSlider();
                this.loadCategories();
                this.loadVendors();
            },

            viewCategories: undefined,
            viewVendors: undefined,
            loadSlider: function () {
                var totalSlides = $(".rslides li").length;
                var that = this;
                $(".rslides").responsiveSlides({
                    timeout: 7000,
                    pager: true,
                    nav: true,
                    prevText: '<span class="icon-prev" title="Anterior"></span>',
                    nextText: '<span class="icon-next" title="Siguiente"></span>',
                    before: function (numSlide) {
                        if (numSlide + 1 <= totalSlides) {
                            var image = $($(".rslides li img").get(numSlide + 1));
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
                if (this.isMobile()) {
                    this.viewCategories = new CategoriesHomeView({ el: '.conte_categories' });
                }
            },
            clickOnLink: function (obj) {
                var target = $(obj.currentTarget);
                debugger;
            },
            loadVendors: function () {
                this.viewVendors = new VendorsHomeView({ el: '#vendorCarousel' });
            },
            resizeBanner: function () {
                //Si es pantalla pequeña recalcula el alto del banner
                $(".rslides").css("height", this.isMinSizeMobile() ? (window.innerWidth / 2.5) + "px" : "");
            },
            render: function () {
                return this;
            },
            clickOnBanner: function (obj) {
                return this.trackClickHome('Banner', obj);
            },
            clickOnManufacturer: function (obj) {
                return this.trackClickHome('Manufacturer', obj);
            },
            clickOnProduct: function (obj) {
                return this.trackClickHome('Product', obj);
            },
            clickOnStore: function (obj) {
                return this.trackClickHome('Store', obj);
            },
            clickOnCategory: function (obj) {
                return this.trackClickHome('Category', obj);
            },
            clickOnMyProducts: function (obj) {
                return this.trackClickHome('MyProducts', obj);
            },
            trackClickHome: function (section, obj) {
                this.trackGAEvent('HomeClick', section, $(obj.currentTarget).attr('href'));
                return true;
            }
        });

        return HomeView;
    });



