define(['underscore', 'backbone'], function (_, Backbone) {
    var VendorModel = Backbone.Model.extend({

        baseUrl: "/api/vendors",

        url: "/api/vendors",

        getOffices: function (id) {
            this.url = this.baseUrl + '/' + id + '/offices';
            this.fetch();
        }
    });

    return VendorModel;
});