define(['jquery', 'underscore', 'baseView', 'tuils/models/order', 'resources','tuils/models/review', 'tuils/views/product/reviewView', 'jpopup', 'jtabs'],
    function ($, _, BaseView, OrderModel, Resources, ReviewModel, ReviewView) {

        var ProductDetailView = BaseView.extend({

            productId: 0,

            vendorUrl: undefined,

            viewReviews : undefined,

            events: {
                'click #btnShowVendor': 'confirmShowVendor'
            },

            initialize: function (args) {

                this.loadControls();

                this.model = new OrderModel();
                this.model.on('sync', this.redirectToVendor, this);
                this.model.set('ProductId', this.productId);

                this.validateAuthorization();
            },
            loadControls: function () {
                this.productId = parseInt(this.$("#ProductId").val());
                this.loadGallery();
                this.loadTabs();
                this.loadReviews();
            },
            loadGallery: function () {
                //$('.jqzoom').jqzoom({
                //    zoomType: 'standard',
                //    title: true,
                //    lens: _lens,
                //    preloadImages: true,
                //    alwaysOn: false,
                //    xOffset: 70,
                //    position: 'left',
                //    showEffect: 'fadein',
                //    hideEffect: 'fadeout'
                //});
                $('#main-product-img-lightbox-anchor-'+this.productId).magnificPopup(
                   {
                       type: 'image',
                       removalDelay: 300,
                       gallery: {
                           enabled: true
                       }
                   });

                $('.thumb-popup-link').magnificPopup(
                {
                    type: 'image',
                    removalDelay: 300,
                    gallery: {
                        enabled: true
                    }
                });
                
            },
            loadTabs : function(){
                $('#tab-container').easytabs();
            },
            loadReviews : function(){
                this.viewReviews = new ReviewView({ el: '#product-reviews-page' });
            },
            confirmShowVendor: function (obj) {
                obj = $(obj.target);
                if (confirm(Resources.products.confirmBuy)) {
                    this.vendorUrl = obj.attr('data-vendorUrl');
                    this.createOrder();
                }
            },
            createOrder: function () {
                this.model.newOrder();
            },
            userAuthenticated: function () {
                this.createOrder();
            },
            redirectToVendor: function () {
                document.location.href = this.vendorUrl;
            }
        });

        return ProductDetailView;
    });