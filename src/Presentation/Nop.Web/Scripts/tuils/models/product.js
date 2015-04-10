
define(['underscore', 'backbone', '_authenticationModel'], 
    function (_, Backbone, AuthenticationModel) {

        'use strict'
        
        var ProductModel = AuthenticationModel.extend({
            baseUrl: "/api/products",
            url: "/api/products",
            //defaults: {
            //    'CategoryId': undefined,
            //    'Name': undefined,
            //    'FullDescription': undefined,
            //    'IsShipEnabled': undefined,
            //    'ManufacturerId': undefined,
            //    'AdditionalShippingCharge': undefined,
            //    'Price': undefined
            //},
            validation: {

                CategoryId: {
                    required: true
                },
                Name: {
                    required: true
                },
                FullDescription: {
                    required: true
                },
                IsShipEnabled: {
                    required: false
                },
                ManufacturerId: {
                    required: true,
                    pattern: 'number'
                },
                AdditionalShippingCharge: {
                    required: function (val, attr, computed) {
                        return computed.IsShipEnabled;
                    },
                    pattern: 'number'
                },
                Price: {
                    required: true,
                    pattern: 'number'
                }
            },
            publish: function () {
                this.save();
            }
        });
        
        return ProductModel;
});



