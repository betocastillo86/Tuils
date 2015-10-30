define(['jquery', 'underscore', 'baseView', 'handlebars', 'tuils/views/panel/vendorProductsPopupView'],
    function ($, _, BaseView, Handlebars, VendorProductsPopupView) {

        var MyOrdersView = BaseView.extend({
            events: {
                'click .filterFeatured' : 'showProductsPopup'
            },

            vendorId: 0,

            productsView : undefined,

            initialize: function (args) {
                this.vendorId = parseInt(this.$("#VendorId").val());
                this.productsView = new VendorProductsPopupView({ el: '#productsViewPopup', id : this.vendorId });
                this.render();
            },
            showProductsPopup: function (obj) {

                var filter = $(obj.target).attr('data-filter');
                this.productsView.show(filter);
            },
            render: function () {
                return this;
            }
        });

        return MyOrdersView;
    });