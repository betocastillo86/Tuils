define(['underscore', 'backbone', '_authenticationModel'],
    function (_, Backbone, AuthenticationModel) {
        var PreproductModel = AuthenticationModel.extend({

            idAttribute: "Id",

            url: '/api/preproducts',

            
        });

        return PreproductModel;
    });