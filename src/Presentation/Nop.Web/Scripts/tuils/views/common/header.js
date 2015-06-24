define(['jquery', 'underscore', 'backbone', 'handlebars', 'tuils/views/login/login', 'tuils/views/login/createUser', 'tuils/views/common/topMenuView'],
    function ($,_, Backbone, Handlebars, LoginView, CreateUserView, TopMenuView) {
    var HeaderView = Backbone.View.extend({

        el: ".header-links",

        viewLogin: undefined,
        viewRegister: undefined,
        viewTopMenu : undefined,

        templateUserAuthenticated :undefined,

        events : {
            'click #liLogin': 'showLogin',
            'click #liRegister': 'showRegister'
        },
        initialize: function () {
            this.render();
        },
        showLogin: function () {
            var that = this;
            if (!this.viewLogin) {
                that.viewLogin = new LoginView({ $el: that.$('#divLoginUser') });
                that.viewLogin.on("register", that.showRegister, that);
                that.viewLogin.on("user-authenticated", that.showUserAuthenticated, that);
            }
            else {
                this.viewLogin.show();
            }
        },
        showRegister : function(){
            var that = this;

            if (!this.viewRegister) {
                that.viewRegister = new CreateUserView({ $el: that.$('#divRegisterUser') });
                that.viewRegister.on("user-authenticated", that.showUserAuthenticated, that);
                that.viewRegister.on("login", that.showLogin, that);
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
        loadTopMenu: function () {
            this.viewTopMenu = new TopMenuView({ el: '.header-menu' });
            this.viewTopMenu.on("register", this.showRegister, this);
            this.viewTopMenu.on("login", this.showLogin, this);
        },
        loadControls: function () {
            this.loadTopMenu();
        },
        render: function () {
            this.loadControls();
            return this;
        }
    });
    return HeaderView;
});