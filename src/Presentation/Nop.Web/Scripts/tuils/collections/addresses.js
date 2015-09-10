define(['underscore', 'backbone', 'tuils/models/address'], function (_, Backbone, AddressModel) {
    var AddressCollection = Backbone.Collection.extend({

        baseUrl: "/api/addresses",

        url: "/api/addresses",

        model : AddressModel,

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