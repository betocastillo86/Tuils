define(['jquery', 'underscore', 'backbone', 'handlebars', 'tuils/models/userRegister', 'baseView', 'resources'],
    function ($, _, Backbone, Handlebars, UserRegisterModel, BaseView, Resources) {
    var LoginView = BaseView.extend({

        viewCreateUser: undefined,

        //Modelo que desencadeno el error
        sourceModel :undefined,

        events: {
            'click #btnLogin' : 'login',
            'click #btnRegister': 'register',
            'click .fb-btn': 'externalAuthentication',
            'keypress form input' : 'validateEnter'
        },
        template : undefined,

        bindings: {
            "#txtEmail": {
                observe: 'Email',
                onSet: function (value) {
                    return value.replace(/ /g, '');
                }
            },
            "#txtPassword": "Password"
        },

        initialize: function (args) {


            this.template = Handlebars.compile($("#templateLogin").html());

            this.model = new UserRegisterModel({ TermsOfUse: true });
            this.model.on("sync", this.userAuthenticated, this);
            this.model.on("error", this.errorAuthenticated, this);

            if (args.sourceModel)
                this.sourceModel = args.sourceModel;

            this.render();
        },
        register: function () {
            //this.close();
            this.trigger("register", this.sourceModel);
            
        },
        validateEnter: function (e) {
            if(e.keyCode==13)
                this.login();
        },
        login: function () {
            this.validateControls(undefined, false, true);

            if (this.model.isValid()) {
                this.showLoadingAll(this.model);
                this.model.login();
            }
        },
        userAuthenticated : function(model){
            this.trigger("user-authenticated", model);
            this.authenticated = true;

            //Si el origen del registro es por darle clic en el boton registro
            //cuando termine lo redirecciona al panel de control
            if (this.sourceModel && this.sourceModel.get('ga_action') == 'Registro')
                document.location.href = '/mi-cuenta';

            this.close();
        },
        errorAuthenticated: function (model, error) {
            this.alertError(error.responseJSON.ModelState ? error.responseJSON.ModelState.errorMessage : error.responseJSON.Message);
        },
        intervalAuthentication :undefined,
        externalAuthentication : function()
        {
            var that = this;
            var modelValidation = new UserRegisterModel();
            modelValidation.on("sync", that.validateActiveSession, that);
            //Realiza la validacion de sesion cada N segundos cuando la autenticación es externa
            this.intervalAuthentication = setInterval(function () {
                modelValidation.isSessionActive();
            }, 2000);
        },
        validateActiveSession : function(model)
        {
            if (model.toJSON().Active)
            {
                if (this.intervalAuthentication)
                    clearInterval(this.intervalAuthentication);

                this.userAuthenticated(model);
            }
        },
        close: function () {
            this.trigger('close');
            //NO PONER:this.dispose();
        },
        render: function () {
            this.$el.html(this.template({ MessageLogin: this.sourceModel ? this.sourceModel.get('message_login') : '' }));
            Backbone.Validation.bind(this);
            this.stickit();
            return this;
        }
    });
    return LoginView;
});