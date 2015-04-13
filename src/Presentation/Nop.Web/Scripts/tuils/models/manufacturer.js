define(['underscore', 'backbone'], function (_, Backbone) {

    
    var ManufacturerModel = Backbone.Model.extend({
        url: "/api/manufacturers",
    });

    return ManufacturerModel;
});

