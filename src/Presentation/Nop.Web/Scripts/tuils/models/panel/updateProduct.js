define(['underscore', 'backbone'],
    function (_, Backbone) {

        'use strict'

        var UpdateProductModel = Backbone.Model.extend({
            baseUrl: "/api/products",
            url: "/api/products",

            idAttribute: 'Id',

            initialize: function () {

            },
            validation: {
                Name: {
                    required: true,
                    maxLength: 50,
                    minLength: 10
                },
                ShortDescription: {
                    required: true,
                    maxLength: 500,
                    minLength: 30
                },
                Price: {
                    required: true,
                    pattern: 'number'
                }
            },
            labels: {
                Name: 'Título',
                ShortDescription: 'Descripción',
                Price: 'Precio'
            },
            update: function () {
                this.save();
            }
        });

        return UpdateProductModel;
    });



