var CategoryModel = Backbone.Model.extend({

    baseUrl: "/api/categories/",

    url: "/api/categories/",

    get: function (id)
    {
        this.url = this.baseUrl +  id;
        this.fetch();
        return this;
    },
    getManufacturers: function (id)
    {
        this.url = this.baseUrl + id + '/manufacturers';
        this.fetch();
        return this;
    }

});

var CategoryCollection = Backbone.Collection.extend({
    
    baseUrl: "/api/categories/",

    url: "/api/categories",

    //model: CategoryModel,

    getBikeReferences: function ()
    {
        this.url = this.baseUrl + "bikereferences";
        this.fetch();
        return this;
    }
});