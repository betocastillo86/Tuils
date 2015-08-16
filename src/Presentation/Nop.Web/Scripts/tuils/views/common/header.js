define(['jquery', 'underscore', 'backbone', 'handlebars', 'tuils/views/login/login', 'tuils/views/login/createUser', 'tuils/views/common/topMenuView', 'baseView'],
    function ($,_, Backbone, Handlebars, LoginView, CreateUserView, TopMenuView, BaseView) {
    var HeaderView = BaseView.extend({

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
        showLogin: function (model) {
            var that = this;
            if (!this.viewLogin) {
                //Debe validar si el modelo es un evento o un modelo, si es evento no lo envía
                that.viewLogin = new LoginView({ $el: that.$('#divLoginUser'), sourceModel: model && !model.type ? model : undefined });
                that.viewLogin.on("register", that.showRegister, that);
                that.viewLogin.on("user-authenticated", that.showUserAuthenticated, that);
                that.viewLogin.on("close-menu-responsive", that.closeMenuResponsive, that);
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
                that.viewRegister.on("close-menu-responsive", that.closeMenuResponsive, that);
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
            if (this.isMinSize())
            {
                var templateLoggedInResponsive = Handlebars.compile(this.$("#templateLoggedInResponsive").html());
                $(".main-registro").parent().prepend(templateLoggedInResponsive(model.toJSON()));
                $(".main-registro").hide();
            }
        },
        loadTopMenu: function () {
            this.viewTopMenu = new TopMenuView({ el: '.header-menu' });
            this.viewTopMenu.on("register", this.showRegister, this);
            this.viewTopMenu.on("login", this.showLogin, this);
        },
        closeMenuResponsive: function () {
            this.viewTopMenu.hideMenuResponsive();
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