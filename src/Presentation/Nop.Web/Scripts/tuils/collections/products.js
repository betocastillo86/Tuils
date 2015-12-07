define(['underscore', 'backbone', 'productModel'],
    function (_, Backbone, AuthenticationModel, ProductModel) {

        var ProductCollection = Backbone.Collection.extend({
            url: "/api/products",

            getProductsByVendor: function (vendorId, onHome, onSliders, onSN) {
                this.url = '/api/vendors/' + vendorId + '/products';
                this.fetch({ data: $.param({ OnHome : onHome, OnSliders :onSliders, OnSN : onSN })});
            }

        });

        return ProductCollection;
    });