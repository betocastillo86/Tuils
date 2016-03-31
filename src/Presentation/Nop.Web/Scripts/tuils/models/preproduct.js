define(['underscore', 'backbone', '_authenticationModel'],
    function (_, Backbone, AuthenticationModel) {
        var PreproductModel = AuthenticationModel.extend({

            idAttribute: "Id",

            url: '/api/preproducts',

            getByProductType: function (productType) {
                this.fetch({ data: { productType: productType } });
            },
        });

        return PreproductModel;
    });