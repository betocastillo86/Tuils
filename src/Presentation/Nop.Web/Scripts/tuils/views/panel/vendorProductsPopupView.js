define(['jquery', 'underscore', 'baseView', 'handlebars', 'tuils/collections/products'],
    function ($, _, BaseView, Handlebars, ProductCollection) {

        var VendorProductsPopupView = BaseView.extend({
            events: {

            },
            vendorId :0,
            initialize: function (args) {
                this.template = Handlebars.compile($("#templateVendorProductsPopup").html());
                this.vendorId = args.id;
                this.collection = new ProductCollection();
                this.collection.on('sync', this.showProducts, this);
            },
            show: function (filter) {
                this.collection.getProductsByVendor(this.vendorId, filter == 'home', filter == 'sliders', filter == 'SN');
            },
            showProducts: function () {
                this.$el.html(this.template(this.collection.toJSON()));
                this.$el.dialog(this.dialogBasicOptions);
                this.render();
            },
            render: function () {
                return this;
            }
        });

        return VendorProductsPopupView;
    });