define(['underscore', 'backbone'], function (_, Backbone) {
    var UserRegisterModel = Backbone.Model.extend({
        url: '/api/auth',
        baseUrl: '/api/auth',
        intialize: function () {
            this.set('IsRegister', false);
        },
        validation: {
            VendorType: {

            },
            Name: {
                required: function (val, attr, computed) {
                    return computed.IsRegister;
                }
            },
            LastName: {
                required: function (val, attr, computed) {
                    return computed.IsRegister;
                }
            },
            Email: {
                required: true,
                pattern : 'email'
            },
            Password: {
                required: true,
                minLength : 5
            },
            CompanyName: {
                required: function (val, attr, computed) {
                    return computed.VendorType != 0 && computed.IsRegister;
                }
            }

        },
        labels: {
            Name: "Nombres",
            LastName: "Apellidos",
            Email: "Correo electrónico",
            Password: "Clave",
            CompanyName : "Nombre establecimiento"
        },
        login: function () {
            this.url = this.baseUrl;
            this.save();
        },
        register: function () {
            this.url = this.baseUrl + '/register';
            this.save();
        }
    });

    return UserRegisterModel;

});