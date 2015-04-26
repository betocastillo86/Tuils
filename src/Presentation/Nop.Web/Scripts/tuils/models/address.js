define(['underscore', 'backbone'], function (_, Backbone) {
    var AddressModel = Backbone.Model.extend({

        idAttribute : "Id",

        baseUrl: "/api/addresses",

        url: "/api/addresses",

        validation :{
            Name: {
                required : true
            },
            Email: {
                required:  false,
                pattern: 'email'
            },
            PhoneNumber: {
                pattern : 'number',
                required: true
            },
            FaxNumber: {
                pattern: 'number',
                required: false
            },
            StateProvinceId: {
                pattern: 'number',
                required: true
            },
            Address: {
                required: true
            },
            Schedule: {
                required: true
            },
            Latitude: {
                required: true
            },
            Longitude: {
                required: true
            }
        },
        labels: {
            Name: 'Nombre',
            Email: 'Correo',
            PhoneNumber: 'Teléfono',
            FaxNumber: 'Teléfono 2',
            StateProvinceId: 'Ciudad',
            Address: 'Dirección',
            Schedule: 'Horario',
            Latitude: 'Latitud',
            Longitude: 'Longitud'
        },
        validateLnLg : function(value)
        {
            return value != undefined && value != 0;
        },
        getAddress: function (id) {
            this.url = this.baseUrl + '/' +  id;
            this.fetch();
        },
        insert: function (id) {
            this.url = this.baseUrl;
            this.save();
        },
        deleteById: function (id)
        {
            this.url = this.baseUrl + '/' + id;
            this.set('Id', id);
            this.destroy();
        }
    });

    return AddressModel;
});