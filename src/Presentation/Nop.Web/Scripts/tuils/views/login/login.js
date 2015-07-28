define(['jquery', 'underscore', 'backbone', 'handlebars', 'tuils/models/userRegister', 'baseView', 'resources'],
    function ($, _, Backbone, Handlebars, UserRegisterModel, BaseView, Resources) {
    var LoginView = BaseView.extend({

        viewCreateUser: undefined,

        events: {
            'click #btnLogin' : 'login',
            'click #btnRegister': 'register',
            'click .fb-btn': 'externalAuthentication',
            'keypress form input' : 'validateEnter'
        },

        bindings: {
            "#txtEmail": "Email",
            "#txtPassword": "Password"
        },

        template : undefined,

        initialize: function (args) {

            this.model = new UserRegisterModel({ TermsOfUse: true });
            this.model.on("sync", this.userAuthenticated, this);
            this.model.on("error", this.errorAuthenticated, this);
            this.$el.fixedDialog(this.dialogBasicOptions);

            var that = this;
            require(['text!/Customer/FastLogin'], function (template) {
                that.template = Handlebars.compile(template);
                that.render();
            });
        },
        register: function () {
            this.trigger("register");
            this.close();
        },
        validateEnter: function (e) {
            if(e.keyCode==13)
                this.login();
        },
        login: function () {
            this.validateControls();

            if (this.model.isValid()) {
                this.model.login();
            }
        },
        userAuthenticated : function(model){
            this.trigger("user-authenticated", model);
            this.$el.dialog('close');
        },
        errorAuthenticated: function (model, error) {
            alert(error.responseJSON.ModelState ? error.responseJSON.ModelState.errorMessage : error.responseJSON.Message);
        },
        show: function () {
            this.$el.dialog({
                width: 365,
                title : Resources.account.login,
                modal : true
            });
        },
        close : function(){
            this.$el.dialog('close');
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
        render: function () {
            this.$el.html(this.template());
            this.show();

            Backbone.Validation.bind(this);
            this.stickit();
            return this;
        }
    });
    return LoginView;
});