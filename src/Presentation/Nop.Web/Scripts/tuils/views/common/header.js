define(['jquery', 'underscore', 'backbone', 'handlebars', 'tuils/views/login/login', 'tuils/views/login/createUser',
    'baseView', 'baseModel'],
    function ($, _, Backbone, Handlebars, LoginView, CreateUserView,
        BaseView, BaseModel) {
    var HeaderView = BaseView.extend({

        el: ".header-links",

        viewLogin: undefined,
        viewRegister: undefined,
        viewTopMenu : undefined,

        templateUserAuthenticated :undefined,

        events : {
            'click #liLogin': 'loadLoginForm',
            'click #liRegister': 'loadRegisterForm'
        },
        initialize: function () {
            this.render();
        },
        showLogin: function (model) {
            var that = this;
            if (!this.viewLogin) {
                //Debe validar si el modelo es un evento o un modelo, si es evento no lo envía
                that.viewLogin = new LoginView({ $el: that.$('#divLoginUser'), sourceModel: model });
                that.viewLogin.on("register", that.showRegister, that);
                that.viewLogin.on("user-authenticated", that.showUserAuthenticated, that);
                that.viewLogin.on("close-menu-responsive", that.closeMenuResponsive, that);
            }
            else {
                this.viewLogin.sourceModel = model;
                this.viewLogin.show();
            }
        },
        getNewSourceModel: function () {
            var sourceModel = new BaseModel();
            sourceModel.set('ga_action', 'Registro');
            return sourceModel;
        },
        loadLoginForm : function()
        {
            this.showLogin(this.getNewSourceModel());
        },
        loadRegisterForm : function(e){
            this.showRegister(this.getNewSourceModel());
        },
        showRegister : function(sourceModel){
            var that = this;

            if (!this.viewRegister) {
                that.viewRegister = new CreateUserView({ $el: that.$('#divRegisterUser'), sourceModel : sourceModel });
                that.viewRegister.on("user-authenticated", that.showUserAuthenticated, that);
                that.viewRegister.on("login", that.showLogin, that);
                that.viewRegister.on("close-menu-responsive", that.closeMenuResponsive, that);
            }
            else {
                this.viewRegister.sourceModel = sourceModel;
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
            this.$("ul").append(this.templateUserAuthenticated(model.toJSON()));
            //if (this.isMinSize())
            //{
            //    var templateLoggedInResponsive = Handlebars.compile(this.$("#templateLoggedInResponsive").html());
            //    $("#nav ul").parent().prepend(templateLoggedInResponsive(model.toJSON()));
            //    $("#nav ul").hide();
            //}
        },
        //loadTopMenu: function () {
        //    this.viewTopMenu = new TopMenuView({ el: '.header-menu' });
        //    this.viewTopMenu.on("register", this.showRegister, this);
        //    this.viewTopMenu.on("login", this.showLogin, this);
        //},
        closeMenuResponsive: function () {
            this.viewTopMenu.hideMenuResponsive();
        },
        loadControls: function () {
            //this.loadTopMenu();
        },
        render: function () {
            this.loadControls();
            return this;
        }
    });
    return HeaderView;
});