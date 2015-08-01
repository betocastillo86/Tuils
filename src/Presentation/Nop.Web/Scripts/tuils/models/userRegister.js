define(['underscore', 'backbone', 'backbone.basicauth'], function (_, Backbone) {
    var UserRegisterModel = Backbone.Model.extend({
        url: '/api/auth',
        baseUrl: '/api/auth',
        intialize: function () {
            this.set('IsRegister', false);
        },
        validation: {
            VendorType: {

            },
            Bike: {
                required: function (val, attr, computed) {
                    return computed.IsRegister;
                }
            },
            Email: {
                required: true,
                pattern : 'email'
            },
            Password: {
                required: false,
                minLength : 5
            },
            CompanyName: {
                required: function (val, attr, computed) {
                    return computed.VendorType != 0 && computed.IsRegister;
                }
            },
            TermsOfUse: {
                acceptance: true
            }
        },
        labels: {
            Name: "Nombres",
            Email: "Correo electrónico",
            Password: "Clave",
            CompanyName : "Nombre establecimiento",
            TermsOfUse: "Terminos y condiciones"
        },
        login: function () {
            
            this.credentials = {
                username: this.get('Email'),
                password: this.get('Password')
            };

            this.set('Password', this.credentials.password.replace(/./gi, "*"));

            this.url = this.baseUrl;
            this.save();
        },
        register: function () {
            this.url = this.baseUrl + '/register';
            this.save();
        },
        isSessionActive: function () {
            this.url = this.baseUrl + '/verify'
            this.save();
        }
    });

    return UserRegisterModel;

});