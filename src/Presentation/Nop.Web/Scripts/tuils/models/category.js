define(['underscore', 'backbone'], function (_, Backbone) {
    var CategoryModel = Backbone.Model.extend({

        baseUrl: "/api/categories/",

        url: "/api/categories/",

        getCategory: function (id) {
            this.url = this.baseUrl + id;
            this.fetch();
            return this;
        },
        getManufacturers: function (id) {
            this.url = this.baseUrl + id + '/manufacturers';
            this.fetch();
            return this;
        }

    });

 
    return CategoryModel;
});

