define(['underscore', 'backbone'], function (_, Backbone) {
    var CategoryCollection = Backbone.Collection.extend({

        baseUrl: "/api/categories/",

        url: "/api/categories",

        //model: CategoryModel,

        getBikeReferences: function () {
            this.url = this.baseUrl + "bikereferences";
            this.fetch();
            return this;
        },
        getSubcategories: function (id) {
            this.url = '/api/categories/' + id + '/subcategories';
            this.fetch();
        },
        getServices: function () {
            this.url = this.baseUrl + 'services';
            this.fetch();
            return this;
        }
    });

    return CategoryCollection;
});

