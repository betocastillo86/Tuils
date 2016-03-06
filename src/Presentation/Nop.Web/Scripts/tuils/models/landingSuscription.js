define(['underscore', 'backbone'], function (_, Backbone) {
    var LandingSuscriptionModel = Backbone.Model.extend({
        baseUrl: "/api/subscriptions/",
        url: "/api/subscriptions/",
        validation: {
            'Name': {
                required: true
            },
            'Email': {
                required: true,
                pattern:'email'
            },
            'Phone': {
                required: true,
                pattern:'number'
            },
            'Company': {
                required: function (val, attr, computed) {
                    //Poner validaciones en los casos que no es necesario
                    return true;
                }
            }
        },
        labels: {
            'Name': 'nombres',
            'Email': 'correo electrónico',
            'Phone': 'Teléfono',
            'Company' : 'negocio'
        }
    });


    return LandingSuscriptionModel;
});

