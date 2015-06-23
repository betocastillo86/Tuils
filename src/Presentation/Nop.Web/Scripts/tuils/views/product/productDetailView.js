define(['jquery', 'underscore', 'baseView', 'tuils/models/order', 'resources'],
    function ($, _, BaseView, OrderModel, Resources) {

        var ProductDetailView = BaseView.extend({

            productId: 0,

            vendorUrl : undefined,

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