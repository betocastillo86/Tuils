define(['underscore', 'backbone', 'tuils/app'], function (_, Backbone, TuilsApp) {
    var CategoryModel = Backbone.Model.extend({

        baseUrl: "/api/categories/",

        url: "/api/categories/",

        getCategory: function (id, showImage) {
            this.url = this.baseUrl + id;

            if (showImage)
                this.fetch({ beforeSend: function (xhr) { xhr.setRequestHeader('image', true) } });
            else
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

