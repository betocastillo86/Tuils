define(['jquery', 'underscore', 'backbone', 'tuils/models/userRegister', 'baseView'],
    function ($, _, Backbone, UserRegisterModel, BaseView, Resources) {
        var StaticLoginView = BaseView.extend({

            //Modelo que desencadeno el error
            sourceModel: undefined,

            events: {
                'click #createNewUser': 'register',
            },
            initialize: function (args) {
                this.model = new UserRegisterModel({ TermsOfUse: true });
                this.model.on("sync", this.userAuthenticated, this);
                this.model.on("error", this.errorAuthenticated, this);
                this.model.set('ga_action', 'Registro');
                

                this.render();
            },
            register: function () {
                this.validateAuthorization();
                this.showRegister(this.model);
                /*this.trigger("register", this.sourceModel);
                this.close();*/
            },
            userAuthenticated: function (model) {
                //Si el origen del registro es por darle clic en el boton registro
                //cuando termine lo redirecciona al panel de control
                if (model.get('ga_action') == 'Registro')
                    document.location.href = '/mi-cuenta';

            },
            errorAuthenticated: function (model, error) {
                this.alert(error.responseJSON.ModelState ? error.responseJSON.ModelState.errorMessage : error.responseJSON.Message);
            },
            render: function () {
                return this;
            }
        });
        return StaticLoginView;
    });