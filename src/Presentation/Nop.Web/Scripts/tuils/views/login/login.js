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
            "#txtEmail": "Email",
            "#txtPassword": "Password"
        },

        initialize: function (args) {


            this.template = Handlebars.compile($("#templateLogin").html());

            this.model = new UserRegisterModel({ TermsOfUse: true });
            this.model.on("sync", this.userAuthenticated, this);
            this.model.on("error", this.errorAuthenticated, this);

            if (args.sourceModel)
                this.sourceModel = args.sourceModel;

            if (this.isMobile())
            {
                this.$el.dialog(this.dialogBasicOptions);
                this.trigger('close-menu-responsive');
            }
            else
                this.$el.fixedDialog(this.dialogBasicOptions);

            //var that = this;
            //require(['text!/Customer/FastLogin'], function (template) {
                //that.template = Handlebars.compile(template);
                this.render();
            //});
        },
        register: function () {
            this.trigger("register", this.sourceModel);
            this.close();
        },
        validateEnter: function (e) {
            if(e.keyCode==13)
                this.login();
        },
        login: function () {
            this.validateControls(undefined, false);

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
                width: window.innerWidth < 365 ? 300 : 365,
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
            this.$el.html(this.template({ MessageLogin: this.sourceModel ? this.sourceModel.get('message_login') : '' }));
            this.show();

            Backbone.Validation.bind(this);
            this.stickit();
            return this;
        }
    });
    return LoginView;
});