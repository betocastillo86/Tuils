define(['underscore', 'backbone', 'manufacturerModel', 'util'], function (_, Backbone, ManufacturerModel, TuilsUtilities) {

    
    var ManufacturerCollection = Backbone.Collection.extend({
        model: ManufacturerModel,
        baseUrl: "/api/manufacturers",
        url: "/api/manufacturers",

        getByCategoryId: function (categoryId) {
            this.url = '/api/categories/' + categoryId + '/manufacturers';
            this.fetch({ beforeSend: TuilsUtilities.addMinHeader });
            return this;
        }
    });

    return ManufacturerCollection;
});

