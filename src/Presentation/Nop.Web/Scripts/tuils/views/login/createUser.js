define(['jquery', 'underscore', 'backbone', 'text!/Customer/CreateUser', 'handlebars', 'jqueryui', 'baseView', 'tuils/models/userRegister', 'validations', 'stickit'],
    function ($, _, Backbone, template, Handlebars, jqueryui, BaseView, UserRegisterModel) {
    var CreateUserView = BaseView.extend({

        userType : undefined,

        events : {
            "click #step1 div": "selectType",
            "click #btnCreateUser": "createUser",
            "click #btnBack": "back"
        },

        bindings: {
            "#txtName": "Name",
            "#txtLastName": "LastName",
            "#txtCompanyName": "CompanyName",
            "#txtEmail": "Email",
            "#txtPassword" : "Password"
        },

        template : Handlebars.compile(template),

        initialize: function (args) {
            this.model = new UserRegisterModel();
            this.model.on("error", this.errorCreating, this);
            this.model.on("sync", this.userCreated, this);
            this.model.set('IsRegister', true);
            
            this.render();
        },
        selectType: function (obj) {
            obj = $(obj.target);
            this.userType = obj.attr("tuils-action");
            this.showForm();
        },
        createUser: function () {
            this.validateControls();
           
            if (this.model.isValid())
            {
                this.model.register();
            }
        },
        userCreated : function(model){
            this.trigger("user-authenticated", this.model);
            this.close();
        },
        errorCreating: function (model, exception) {
            alert(exception.responseJSON.Message);
        },
        showForm : function(){
            this.$("#step1").hide();
            this.$("#step2").show();

            this.model.set('VendorType', parseInt(this.userType));

            //Si es de tipo almacen muestra los datos
            if (this.userType != 0) {
                this.$("[tuils-for='establecimiento']").show();
            }
            else {
                this.$("[tuils-for='establecimiento']").hide();
            }
        },
        back: function () {
            this.$("#step2").hide();
            this.$("#step1").show();
        },
        show: function () {
            this.$el.dialog();
        },
        close: function () {
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

    return CreateUserView;
});