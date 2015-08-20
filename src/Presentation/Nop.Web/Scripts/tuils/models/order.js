define(['underscore', 'backbone', '_authenticationModel', 'resources'],
    function (_, Backbone, AuthenticationModel, TuilsResources) {
    var OrderModel = AuthenticationModel.extend({

        baseUrl: "/api/orders",

        url: "/api/orders",

        newOrder: function () {
            this.set('message_login', TuilsResources.loginMessages.showVendor);
            //Agrega la llave de Google Analytics para registrar el evento de registro
            this.set('ga_action', 'Compra');
            this.url = this.baseUrl;
            this.save();
        }
    });

    return OrderModel;
});