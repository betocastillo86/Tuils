define(['jquery', 'underscore', 'backbone'], function ($, _, Backbone) {
    var HeaderView = Backbone.View.extend({

        el: ".header-links",

        viewLogin: undefined,
        viewRegister: undefined,

        events : {
        
        },
        initialize: function () {
            
        },
        showLogin: function () {
            var that = this;
            if (!this.viewLogin) {
                require(['tuils/views/login/login'], function (LoginView) {
                    that.viewLogin = new LoginView({ $el: that.$('#divLoginUser') });
                    that.viewLogin.on("register", that.showRegister, that);
                    that.viewLogin.on("user-authenticated", that.showUserAuthenticated, that);
                });
            }
            else {
                this.viewLogin.show();
            }
            
        },
        showRegister : function(){
            var that = this;

            if (!this.viewRegister) {
                require(['tuils/views/login/createUser'], function (CreateUserView) {
                    that.viewRegister = new CreateUserView({ $el: that.$('#divRegisterUser') });
                    that.viewRegister.on("user-authenticated", that.showUserAuthenticated, that);
                });
            }
            else {
                this.viewRegister.show();
            }
        },
        showUserAuthenticated : function(){
            this.trigger("user-authenticated");
        },
        render: function () {
            return this;
        }
    });
    return HeaderView;
});