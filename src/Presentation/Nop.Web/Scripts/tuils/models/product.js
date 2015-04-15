
define(['underscore', 'backbone', '_authenticationModel', 'configuration'], 
    function (_, Backbone, AuthenticationModel, TuilsConfiguration) {

        'use strict'
        
        var ProductModel = AuthenticationModel.extend({
            baseUrl: "/api/products",
            url: "/api/products",
            validation: {
                ProductTypeId: {
                    
                },
                CategoryId: {
                    required: true
                },
                Name: {
                    required: true,
                    maxLength : 50
                },
                FullDescription: {
                    required: true
                },
                IsShipEnabled: {
                    required: false
                },
                ManufacturerId: {
                    required: function (val, attr, computed) {
                        return computed.ProductTypeId == TuilsConfiguration.productBaseTypes.product;
                    },
                    pattern: 'number'
                },
                AdditionalShippingCharge: {
                    required: function (val, attr, computed) {
                        return computed.IsShipEnabled;
                    },
                    pattern: 'number',
                    maxLength : 10
                },
                Price: {
                    required: true,
                    pattern: 'number',
                    maxLength: 10
                },
                Color: {
                    required: function (val, attr, computed) {
                        return computed.ProductTypeId == TuilsConfiguration.productBaseTypes.bike;
                    },
                    pattern: 'number'
                },
                Condition: {
                    required: function (val, attr, computed) {
                        return computed.ProductTypeId == TuilsConfiguration.productBaseTypes.bike;
                    },
                    pattern: 'number'
                },
                CarriagePlate: {
                    required: function (val, attr, computed) {
                        return computed.ProductTypeId == TuilsConfiguration.productBaseTypes.bike;
                    },
                    maxLength: 7
                },
                Year: {
                    required: function (val, attr, computed) {
                        return computed.ProductTypeId == TuilsConfiguration.productBaseTypes.bike;
                    },
                    pattern: 'number'
                },
                Kms: {
                    required: function (val, attr, computed) {
                        return computed.ProductTypeId == TuilsConfiguration.productBaseTypes.bike;
                    },
                    pattern: 'number',
                    maxLength :10
                },
                IsNew: {
                    required:true,
                    pattern: /^(true|false)$/
                },
                StateProvince: {
                    required: function (val, attr, computed) {
                        return computed.ProductTypeId != TuilsConfiguration.productBaseTypes.service;
                    },
                    pattern: 'number'
                }
            },
            labels: {
                Name: 'Título',
                CategoryId : 'Categoría',
                FullDescription :'Descripción',
                IsShipEnabled : 'Realiza envíos',
                ManufacturerId: 'Marca',
                AdditionalShippingCharge: 'Costo por envío',
                Price: 'Precio',
                Condition: 'Condición',
                CarriagePlate: 'Placa',
                Year: 'Año/Modelo',
                Kms: 'Recorrido',
                Negotiation: 'Condiciones de Negociación',
                Accesories: 'Accesorios',
                IsNew: 'Estado',
                StateProvinceId : 'Ubicación',
            },
            publish: function () {
                this.save();
            }
        });
        
        return ProductModel;
});



