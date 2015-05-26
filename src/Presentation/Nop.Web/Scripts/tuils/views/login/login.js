define(['underscore', 'backbone', 'text!/Customer/FastLogin', 'handlebars', 'tuils/models/userRegister', 'baseView', 'resources'],
    function (_, Backbone, template, Handlebars, UserRegisterModel, BaseView, Resources) {
    var LoginView = BaseView.extend({

        viewCreateUser: undefined,

        events: {
            'click #btnLogin' : 'login',
            'click #btnRegister': 'register'
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