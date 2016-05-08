define(['underscore', 'backbone', '_authenticationModel'],
    function (_, Backbone, AuthenticationModel) {
        var PreproductModel = AuthenticationModel.extend({

            idAttribute: "Id",

            urlRoot: '/api/preproducts/id',

            url: '/api/preproducts',

            getByProductType: function (productType) {
                this.fetch({ data: { productType: productType } });
            },
            //Envia el comando de eliminación de preproductos para el usuario
            deleteById: function (productType) {
                this.url = '/api/preproducts?productType=' + productType;
                this.destroy();
            },
        });

        return PreproductModel;
    });