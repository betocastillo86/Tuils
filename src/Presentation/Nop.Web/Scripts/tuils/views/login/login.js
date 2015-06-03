define(['underscore', 'backbone', 'text!/Customer/FastLogin', 'handlebars', 'tuils/models/userRegister', 'baseView', 'resources', 'css!/Plugins/ExternalAuth.Facebook/Content/facebookstyles'],
    function (_, Backbone, template, Handlebars, UserRegisterModel, BaseView, Resources) {
    var LoginView = BaseView.extend({

        viewCreateUser: undefined,

        events: {
            'click #btnLogin' : 'login',
            'click #btnRegister': 'register',
            'click .facebook-login-block a' : 'externalAuthentication'
        },

        bindings: {
            "#txtEmail": "Email",
            "#txtPassword": "Password"
        },

        template : Handlebars.compile(template),

        initialize: function (args) {
            this.model = new UserRegisterModel({TermsOfUse : true});
            this.model.on("sync", this.userAuthenticated, this);
            this.model.on("error", this.errorAuthenticated, this);
            this.render();
        },
        register: function () {
            this.trigger("register");
            this.close();
        },
        login: function () {
            this.validateControls();

            if (this.model.isValid()) {
                this.model.login();
            }
        },
        userAuthenticated : function(){
            this.trigger("user-authenticated");
            this.$el.dialog('close');
        },
        errorAuthenticated: function (model, error) {
            alert(error.responseJSON.Message);
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
            model = model.toJSON();
            if (model.Active)
            {
                if (this.intervalAuthentication)
                    clearInterval(this.intervalAuthentication);

                this.userAuthenticated();
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