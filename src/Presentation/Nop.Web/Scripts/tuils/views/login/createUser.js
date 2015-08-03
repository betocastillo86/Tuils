define(['jquery', 'underscore', 'backbone', 'handlebars', 'baseView', 'tuils/models/userRegister',
    'resources', 'storage', 'util'],
    function ($, _, Backbone, Handlebars, BaseView, UserRegisterModel,
        Resources, TuilsStorage, TuilsUtil) {
    var CreateUserView = BaseView.extend({

        userType : undefined,

        events : {
            "click #step1 a": "selectType",
            "click #btnCreateUser": "createUser",
            'click .fb-btn': 'externalAuthentication',
            "click #btnBack": "back"
        },

        bindings: {
            //"#txtName": "Name",
            "#txtCompanyName": "CompanyName",
            "#txtEmail": "Email",
            '#txtBike': {
                observe: 'Bike',
                controlToMark: '.tagit-new input[type="text"]'
            },
            //"#txtPassword": "Password",
            "#chkTerms": "TermsOfUse"
        },

        initialize: function (args) {

            var that = this;
            this.loadModel();

            if (this.isMobile()) {
                this.$el.dialog(this.dialogBasicOptions);
                this.trigger('close-menu-responsive');
            }
            else
                this.$el.fixedDialog(this.dialogBasicOptions);

            require(['text!/Customer/CreateUser'], function (template) {
                that.template = Handlebars.compile(template);
                that.render();
            });
            
            
        },
        loadModel: function () {
            this.model = new UserRegisterModel();
            this.model.on("error", this.errorCreating, this);
            this.model.on("sync", this.userCreated, this);
            this.model.set('IsRegister', true);
            this.model.set('VendorType', 0);
        },
        externalAuthentication: function () {
            var that = this;
            var modelValidation = new UserRegisterModel();
            modelValidation.on("sync", that.validateActiveSession, that);
            //Realiza la validacion de sesion cada N segundos cuando la autenticación es externa
            this.intervalAuthentication = setInterval(function () {
                modelValidation.isSessionActive();
            }, 2000);
        },
        validateActiveSession: function (model) {
            
            if (model.toJSON().Active) {
                if (this.intervalAuthentication)
                    clearInterval(this.intervalAuthentication);
                this.userAuthenticated(model);
            }
        },
        userAuthenticated: function (model) {
            this.trigger("user-authenticated", model);
            this.$el.dialog('close');
        },
        selectType: function (obj) {
            
            obj = $(obj.currentTarget);
            this.$("#step1 a").removeClass('active');
            obj.addClass('active');
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

            this.model.set('VendorType', parseInt(this.userType));

            //Si es de tipo almacen muestra los datos
            if (this.userType == 0) {
                this.$("[tuils-for='company']").hide();
                this.$("[tuils-for='user']").show();
            }
            else {
                this.$("[tuils-for='company']").show();
                this.$("[tuils-for='user']").hide();
            }
        },
        back: function () {
            this.close();
            this.trigger("login");
        },
        show: function () {
            this.$el.dialog({
                width: 365,
                title: Resources.account.newCustomer,
                modal:true
            });
        },
        tagBikeReferences: function () {

            if (TuilsStorage.bikeReferences) {
                var tagReferences = [];

                var addTag = function (element) {
                    tagReferences.push({ label: element.Name, value: element.Id });
                };
                _.each(TuilsStorage.bikeReferences, function (element, index) {
                    //addTag(element);
                    _.each(element.ChildrenCategories, function (child, index) {
                        child.Name = element.Name + ' ' + child.Name;
                        addTag(child);
                    });
                });

                var that = this;
                this.$("#txtBike")
                    .tagit({
                        availableTags: tagReferences,
                        allowOnlyAvailableTags: true,
                        tagLimit: 1,
                        autocomplete: {
                            source: TuilsUtil.tagItAutocomplete
                        },
                        placeholderText : this.$("#txtBike").attr('placeholder'),
                        allowSpaces: true,
                        afterTagAdded: function () {
                            //Cuando se agrega el tag se oculta la caja de texto
                            that.$('.tagit-new input[type="text"]').hide();
                        },
                        afterTagRemoved: function () {
                            //Muestra la caja de texto nuevamente
                            that.$('.tagit-new input[type="text"]').show();
                        }
                    });
            }
            else {
                TuilsStorage.loadBikeReferences(this.tagBikeReferences, this);
            }
        },
        close: function () {
            this.$el.dialog('close');
        },
        render: function () {
            this.$el.html(this.template());
            this.show();
            this.tagBikeReferences();

            Backbone.Validation.bind(this);
            this.stickit();
            return this;
        }
    });

    return CreateUserView;
});