define(['underscore', 'backbone'], function (_, Backbone) {
    var AddressCollection = Backbone.Collection.extend({

        baseUrl: "/api/addresses",

        url: "/api/addresses",

        getOfficesByVendor: function (id)
        {
            this.url = '/api/vendors/' + id + '/offices';
            this.fetch();
        },
        getPictures: function (id) {
            this.url = this.baseUrl + '/' + id + '/pictures';
            this.fetch();
        }
    });

    return AddressCollection;
});