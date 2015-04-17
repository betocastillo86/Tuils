define(['underscore', 'backbone'], function (_, Backbone) {
    'use strict'

    var SpecificationAttributeCollection = Backbone.Collection.extend({
        baseUrl: '/api/attributes',
        url: '/api/attributes'
    });

    return SpecificationAttributeCollection;
});
