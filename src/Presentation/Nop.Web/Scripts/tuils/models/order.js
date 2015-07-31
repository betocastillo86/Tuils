define(['underscore', 'backbone', '_authenticationModel', 'resources'],
    function (_, Backbone, AuthenticationModel, TuilsResources) {
    var OrderModel = AuthenticationModel.extend({

        baseUrl: "/api/orders",

        url: "/api/orders",

        newOrder: function () {
            this.set('message_login', TuilsResources.loginMessages.showVendor);
            this.url = this.baseUrl;
            this.save();
        }
    });

    return OrderModel;
});