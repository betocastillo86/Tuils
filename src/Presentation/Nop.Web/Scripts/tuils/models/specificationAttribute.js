define(['underscore', 'backbone'], function (_, Backbone) {
    'use strict'

    var SpecificationAttributeModel = Backbone.Model.extend({
        baseUrl: '/api/attributes',
        url: '/api/attributes',

        getSupplies: function () {
            this.url = this.baseUrl + '/supplies';
            this.fetch();
            return this;
        }
    });

    return SpecificationAttributeModel;
});