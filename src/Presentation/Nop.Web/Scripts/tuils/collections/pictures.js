define(['jquery', 'underscore', 'backbone', 'tuils/models/picture'],
    function ($, _, Backbone, PictureModel) {

        var PictureCollection = Backbone.Collection.extend({

            baseUrl: "/api/pictures",
            url: "/api/pictures",

            model : PictureModel,

            getPicturesByProductId: function (id) {
                this.url = '/api/products/' + id + '/pictures';
                this.fetch();
            }
        });

        return PictureCollection;
    });