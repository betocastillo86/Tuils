define(['underscore', 'backbone', 'manufacturerModel', 'tuils/app'], function (_, Backbone, ManufacturerModel, TuilsApp) {

    
    var ManufacturerCollection = Backbone.Collection.extend({
        model: ManufacturerModel,
        baseUrl: "/api/manufacturers",
        url: "/api/manufacturers",

        getByCategoryId: function (categoryId) {
            this.url = '/api/categories/' + categoryId + '/manufacturers';
            this.fetch({ beforeSend: TuilsApp.addMinHeader });
            return this;
        }
    });

    return ManufacturerCollection;
});

