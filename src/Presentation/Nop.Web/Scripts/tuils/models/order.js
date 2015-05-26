define(['underscore', 'backbone', '_authenticationModel'], function (_, Backbone, AuthenticationModel) {
    var OrderModel = AuthenticationModel.extend({

        baseUrl: "/api/orders",

        url: "/api/orders",

        newOrder: function () {
            this.url = this.baseUrl;
            this.save();
        }
    });

    return OrderModel;
});