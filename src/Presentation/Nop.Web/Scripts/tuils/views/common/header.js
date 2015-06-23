define(['jquery','underscore', 'backbone', 'handlebars', 'tuils/views/login/login', 'tuils/views/login/createUser'],
    function ($,_, Backbone, Handlebars, LoginView, CreateUserView) {
    var HeaderView = Backbone.View.extend({

        el: ".header-links",

        viewLogin: undefined,
        viewRegister: undefined,

        templateUserAuthenticated :undefined,

        events : {
            'click #liLogin': 'showLogin',
            'click #liRegister': 'showRegister'
        },
        initialize: function () {
            
        },
        showLogin: function () {
            var that = this;
            if (!this.viewLogin) {
                //require(['tuils/views/login/login'], function (LoginView) {
                    that.viewLogin = new LoginView({ $el: that.$('#divLoginUser') });
                    that.viewLogin.on("register", that.showRegister, that);
                    that.viewLogin.on("user-authenticated", that.showUserAuthenticated, that);
               // });
            }
            else {
                this.viewLogin.show();
            }
        },
        showRegister : function(){
            var that = this;

            if (!this.viewRegister) {
               // require(['tuils/views/login/createUser'], function (CreateUserView) {
                    that.viewRegister = new CreateUserView({ $el: that.$('#divRegisterUser') });
                    that.viewRegister.on("user-authenticated", that.showUserAuthenticated, that);
                    that.viewRegister.on("login", that.showLogin, that);
              //  });
            }
            else {
                this.viewRegister.show();
            }
        },
        showUserAuthenticated: function (model) {
            this.refreshUserData(model);
            this.trigger("user-authenticated", model);
        },
        refreshUserData: function (model) {
            this.templateUserAuthenticated = Handlebars.compile(this.$("#templateLoggedUser").html());
            this.$(".preLogin").hide();
            this.$("ul").prepend(this.templateUserAuthenticated(model.toJSON()));
        },
        render: function () {
            return this;
        }
    });
    return HeaderView;
});