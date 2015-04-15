define(['underscore', 'backbone'], function (_, Backbone) {
    var CategoryCollection = Backbone.Collection.extend({

        baseUrl: "/api/categories/",

        url: "/api/categories",

        //model: CategoryModel,

        getBikeReferences: function () {
            this.url = this.baseUrl + "bikereferences";
            this.fetch();
            return this;
        }
    });

    return CategoryCollection;
});

