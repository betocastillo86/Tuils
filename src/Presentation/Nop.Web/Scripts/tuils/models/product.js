
define(['underscore', 'backbone', '_authenticationModel', 'configuration'], 
    function (_, Backbone, AuthenticationModel, TuilsConfiguration) {

        'use strict'
        
        var ProductModel = AuthenticationModel.extend({
            baseUrl: "/api/products",
            url: "/api/products",

            initialize : function(){
                this.on('change:IsShipEnabled', function (value) {
                    this.set('IsShipEnabledName', this.get('IsShipEnabled') ?  'Si': 'No');
                }, this);
                this.on('change:IncludeSupplies', function (){
                    this.set('IncludeSuppliesName', this.get('IncludeSupplies') ? 'Si' : 'No')
                }, this);
            },

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
                    pattern: 'number'
                },  
                Price: {
                    required: true,
                    pattern: 'number'
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
                    required: function (val, attr, computed) {
                        return computed.ProductTypeId != TuilsConfiguration.productBaseTypes.service;
                    },
                    pattern: /^(true|false)$/
                },
                StateProvince: {
                    required: true,
                    pattern: 'number'
                },
                DetailShipping: {
                    required: function (val, attr, computed) {
                        return computed.IsShipEnabled;
                    },
                    maxLength: 300
                },
                Supplies: {
                    required: function (val, attr, computed) {
                        return computed.IncludeSupplies;
                    }
                },
                SuppliesValue: {
                    required: function (val, attr, computed) {
                        return !computed.IncludeSupplies && computed.ProductTypeId == TuilsConfiguration.productBaseTypes.service;
                    },
                    pattern:'number'
                }
            },
            labels: {
                Name: 'Título',
                CategoryId : 'Categoría',
                FullDescription :'Descripción',
                IsShipEnabled : 'Realiza Envios/Domicilios',
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
                StateProvinceId: 'Ubicación',
                DetailShipping: 'Cobertura',
                IncludeSupplies : 'Incluye los insumos',
                SuppliesValue: 'Valor de Insumos',
                Supplies: 'Insumos'

            },
            publish: function () {
                this.save();
            }
        });
        
        return ProductModel;
});



