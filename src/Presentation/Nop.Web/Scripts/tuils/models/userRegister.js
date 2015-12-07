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
                //required: function (val, attr, computed) {
                //    return computed.VendorType == 0;
                //}
                required : false
            },
            Name: {
                required: function (val, attr, computed) {
                    return computed.IsRegister;
                },
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
            //TermsOfUse: {
            //    acceptance: true
            //}
        },
        labels: {
            Name: "Nombre",
            Email: "Correo electrónico",
            Password: "Contraseña",
            CompanyName : "Nombre establecimiento",
            TermsOfUse: "Terminos y condiciones",
            Bike : 'con tu Motocicleta'
        },
        oAuth: function () {
            this.credentials = {
                username: this.get('Email'),
                password: this.get('Password')
            };

            this.set('Password', this.credentials.password.replace(/./gi, "*"));
        },
        login: function () {
            
            this.oAuth();

            this.url = this.baseUrl;
            this.save();
        },
        register: function () {
            this.oAuth();
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