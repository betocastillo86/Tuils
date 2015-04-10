define(['underscore', 'backbone'], function (_, Backbone) {

    var FileCollection = Backbone.Collection.extend({
        baseUrl: '/api/files',

        url: '/api/files'
    });

    return FileCollection;
});