var CategoryModel = Backbone.Model.extend({

    baseUrl: "/api/categories",

    url: "/api/categories",

    get: function (id)
    {
        this.url = this.baseUrl + '/' + id;
        this.fetch();
        return this;
    }
});

var CategoryCollection = Backbone.Collection.extend({
    url: "/api/categories",

    model : CategoryModel
});