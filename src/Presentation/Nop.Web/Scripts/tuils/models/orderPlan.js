define(['underscore', 'backbone', '_authenticationModel'],
    function (_, Backbone, AuthenticationModel, TuilsResources) {
        var OrderPlanModel = AuthenticationModel.extend({

            baseUrl : '/api/orderplans',

            url: "/api/orderplans",

            validation :{
                ProductId: {
                    required : false
                },
                PlanId: {
                    required: true
                },
                Address: {
                    required : true
                },
                StateProvinceId: {
                    required : true
                },
                City: {
                    required: true
                },
                PhoneNumber: {
                    required:true
                },
                AddressId: {
                    required:false
                }
            },

            labels :{
                ProductId : 'Producto',
                PlanId : 'Plan',
                Address : 'Dirección',
                StateProvinceId: 'Departamento',
                City: 'City',
                PhoneNumber : 'Teléfono'
            },

            newOrder: function () {
                this.url = this.baseUrl;
                this.save();
            }
        });

        return OrderPlanModel;
    });