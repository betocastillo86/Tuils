define(['underscore', 'backbone', 'productModel'],
    function (_, Backbone, AuthenticationModel, ProductModel) {

        var ProductCollection = Backbone.Collection.extend({
            url: "/api/products"
        });

        return ProductCollection;
    });