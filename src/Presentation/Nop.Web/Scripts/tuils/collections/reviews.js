define(['underscore', 'backbone'],
    function (_, Backbone) {

    var ReviewsCollection = Backbone.Collection.extend({
        url: "/api/products",

        getReviewsByVendor: function (id, page) {
            this.url = '/api/vendors/' + id + '/reviews';
            this.fetch({ data: $.param({page : page})});
        }
    });

    return ReviewsCollection;
});