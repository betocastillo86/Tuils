define(['jquery', 'underscore', 'backbone', 'handlebars', 'tuils/views/login/login', 'tuils/views/login/createUser', 'util','tuils/views/common/topMenuView',
    'baseView', 'baseModel'],
    function ($, _, Backbone, Handlebars, LoginView, CreateUserView,  TuilsUtilities, TopMenuView,
        BaseView, BaseModel) {
    var HeaderView = BaseView.extend({

        el: ".header-links",

        viewLogin: undefined,
        viewRegister: undefined,
        viewTopMenu : undefined,

        templateUserAuthenticated :undefined,

        events : {
            'click #liLogin': 'loadLoginForm',
            'click #liRegister': 'loadRegisterForm',
            'click #nav-open-btn' : 'openMenu'
        },
        initialize: function () {
            this.loadTopMenu();
            this.render();

            //Si viene por querystring reg = 1 significa que debe mostrar el registro
            if (TuilsUtilities.getParameterByName('reg') == '1')
            {
                //Si por defecto tiene el registro de empresas lo envia
                var model = new BaseModel(
                    {
                        default_reg: TuilsUtilities.getParameterByName('reg_emp') == '1' ? 'empresas' : undefined,
                        ga_action: TuilsUtilities.getParameterByName('reg_src')
                    });
                this.showRegister(model);
            }
                
        },
        showLogin: function (model) {
            var that = this;
            if (this.viewLogin) {
                this.viewLogin.dispose();
            }
            //Debe validar si el modelo es un evento o un modelo, si es evento no lo envía
            this.viewLogin = new LoginView({ $el: this.$('#divLoginUser'), sourceModel: model });
            this.viewLogin.on("register", this.showRegister, this);
            this.viewLogin.on('close', this.closeAlert, this);
            this.viewLogin.on("user-authenticated", this.showUserAuthenticated, this);
            this.alert({
                message: this.viewLogin.$el,
                alertType: 'window',
                afterClose: function () {
                    if (!that.viewLogin.authenticated)
                        that.closeAuthentication();
                }
            });
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
        closeAuthentication: function () {
            this.trigger('close-authentication');
        },
        showRegister : function(sourceModel){

            var that = this;
            if (this.viewRegister) {
                this.viewRegister.dispose();
            }
            //Debe validar si el modelo es un evento o un modelo, si es evento no lo envía
            this.viewRegister = new CreateUserView({ $el: this.$('#divRegisterUser'), sourceModel: sourceModel });
            this.viewRegister.on("user-authenticated", this.showUserAuthenticated, this);
            this.viewRegister.on("login", this.showLogin, this);
            this.viewRegister.on("close", this.closeAlert, this);

            this.alert({
                message: this.viewRegister.$el,
                alertType : 'window',
                afterClose: function () {
                    if (!that.viewRegister.authenticated)
                        that.closeAuthentication();
                    that.viewRegister.dispose();
                }
            });
        },
        showUserAuthenticated: function (model) {
            this.refreshUserData(model);
            this.trigger("user-authenticated", model);
        },
        refreshUserData: function (model) {
            this.templateUserAuthenticated = Handlebars.compile(this.$("#templateLoggedUser").html());
            this.$(".preLogin").hide();
            this.$("ul").append(this.templateUserAuthenticated(model.toJSON()));
        },
        loadTopMenu: function () {
            this.viewTopMenu = new TopMenuView({ el: '.header-links' });
            //this.viewTopMenu.on("register", this.showRegister, this);
            //this.viewTopMenu.on("login", this.showLogin, this);
        },
        openMenu: function () {
            this.viewTopMenu.hide();
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