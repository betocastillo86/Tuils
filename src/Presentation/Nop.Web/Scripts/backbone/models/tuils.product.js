var ProductModel = Backbone.Model.extend({
    baseUrl : "/api/products",
    url: "/api/products"
});

var ProductCollection = Backbone.Collection.extend({
    url : "/api/products"
});