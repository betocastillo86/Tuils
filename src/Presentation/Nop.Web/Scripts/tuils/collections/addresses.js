define(['underscore', 'backbone'], function (_, Backbone) {
    var AddressCollection = Backbone.Collection.extend({

        baseUrl: "/api/addresses/",

        url: "/api/addresses"
    });

    return AddressCollection;
});