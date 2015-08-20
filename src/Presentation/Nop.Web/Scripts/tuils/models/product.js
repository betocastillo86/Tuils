
define(['underscore', 'backbone', '_authenticationModel', 'configuration', 'resources'], 
    function (_, Backbone, AuthenticationModel, TuilsConfiguration, TuilsResources) {

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
                    maxLength: 50,
                    minLength: 10
                },
                FullDescription: {
                    required: true,
                    maxLength: 500,
                    minLength: 30
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
                        //Como está deshabilitado cargar el precio del en´vío para productos, solo se tiene cuenta la obligatoriedad si es un servicio
                        return computed.IsShipEnabled && computed.ProductTypeId == TuilsConfiguration.productBaseTypes.service;
                    },
                    pattern: 'number'
                },  
                Price: {
                    required: true,
                    pattern: 'number'
                },
                Color: {
                    required: function (val, attr, computed) {
                        //return computed.ProductTypeId == TuilsConfiguration.productBaseTypes.bike;
                        return false;
                    },
                    pattern: 'number'
                },
                PhoneNumber: {
                    required: false,
                    minLength : 6
                },
                Condition: {
                    required: function (val, attr, computed) {
                        //return computed.ProductTypeId == TuilsConfiguration.productBaseTypes.bike;
                        return false;
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
                        return computed.ProductTypeId == TuilsConfiguration.productBaseTypes.product;
                    },
                    pattern: /^(true|false)$/
                },
                StateProvince: {
                    required: true,
                    pattern: 'number'
                },
                DetailShipping: {
                    required: function (val, attr, computed) {
                        return computed.IsShipEnabled && computed.ProductTypeId == TuilsConfiguration.productBaseTypes.service;
                    },
                    maxLength: 300
                },
                Supplies: {
                    required: function (val, attr, computed) {
                        return !computed.IncludeSupplies && computed.ProductTypeId == TuilsConfiguration.productBaseTypes.service;
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
                IsShipEnabled : 'Realiza Envíos/Domicilios',
                ManufacturerId: 'Marca',
                //AdditionalShippingCharge: 'Costo por envío',
                Price: 'Precio',
                Condition: 'Condición',
                CarriagePlate: 'Placa',
                Year: 'Año/Modelo',
                Kms: 'Recorrido',
                Negotiation: 'Condiciones de Negociación',
                Accesories: 'Accesorios',
                IsNew: 'Estado',
                StateProvinceId: 'Ubicación',
                StateProvince: 'Ubicación',
                DetailShipping: 'Cobertura',
                IncludeSupplies : 'Incluye los insumos',
                SuppliesValue: 'Valor de Insumos',
                Supplies: 'Insumos',
                PhoneNumber : 'Número de contacto'

            },
            publish: function () {
                this.set('message_login', TuilsResources.loginMessages.publishProduct);
                this.set('ga_action', 'Publicacion');
                this.save();
            }
        });
        
        return ProductModel;
});



