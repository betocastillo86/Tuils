define(['jquery', 'underscore', 'backbone', 'handlebars', 'baseView', 'tuils/models/userRegister',
    'resources', 'storage', 'util'],
    function ($, _, Backbone, Handlebars, BaseView, UserRegisterModel,
        Resources, TuilsStorage, TuilsUtil) {
        var CreateUserView = BaseView.extend({

            userType: undefined,

            events: {
                "click #step1 a": "selectType",
                "click #btnCreateUser": "createUser",
                'click .fb-btn': 'externalAuthentication',
                "click #btnBack": "back"
            },

            template: undefined,

            sourceModel: undefined,

            bindings: {
                "#txtName": "Name",
                'input[name="rbVendorSubType"]': 'VendorSubType',
                "#txtCompanyName": "CompanyName",
                "#txtEmail": "Email",
                '#txtBike': {
                    observe: 'Bike',
                    controlToMark: '.tagit-new input[type="text"]'
                },
                "#txtPassword": "Password",
                //"#chkTerms": "TermsOfUse"
            },
            intervalAuthentication: undefined,
            initialize: function (args) {
                this.template = Handlebars.compile($("#templateCreateUser").html());
                //var that = this;
                this.loadModel();

                if (args.sourceModel)
                    this.sourceModel = args.sourceModel;

                this.render();
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
            trackGA: function () {
                //Valida que tenga source Model
                if (this.sourceModel && this.sourceModel.get && this.sourceModel.get('ga_action')) {
                    this.trackGAEvent('Registro', this.sourceModel.get('ga_action'));
                }
                else {
                    //Si no hay un source model registrado debe mostrar una alerta para programarla
                    //ya que la idea es registrar exactamente desde donde se registró un usuario
                    this.alert('No hay source model registrado para este evento');
                }
            },
            userAuthenticated: function (model) {
                this.trigger("user-authenticated", model);
                this.authenticated = true;
                this.validateRedirect();
                this.close();
            },
            validateRedirect: function () {
                //Si el origen del registro es por darle clic en el boton registro
                //cuando termine lo redirecciona al panel de control
                if (this.sourceModel.get('ga_action') == 'Registro')
                    document.location.href = '/mi-cuenta';
            },
            selectType: function (obj) {

                obj = $(obj.currentTarget);
                this.$("#step1 a").removeClass('active');
                obj.addClass('active');
                this.userType = obj.attr("tuils-action");
                this.showForm();
            },
            createUser: function () {
                this.validateControls(undefined, false, true);

                if (this.model.isValid()) {
                    this.showLoadingAll(this.model);
                    this.model.register();
                }
            },
            userCreated: function (model) {
                this.trackGA();
                this.alert({ message: Resources.confirm.userRegistered, afterClose: this.validateRedirect, ctx : this});
                this.trigger("user-authenticated", this.model);
                this.authenticated = true;
                this.close();
                //this.validateRedirect();
            },
            errorCreating: function (model, exception) {
                this.alert(exception.responseJSON.Message);
            },
            showForm: function () {

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
                this.isChangingLogin = true;
                this.trigger("login", this.sourceModel);
            },
            tagBikeReferences: function () {

                if (TuilsStorage.bikeReferences) {
                    var tagReferences = [];

                    var addTag = function (element) {
                        tagReferences.push({ label: element.Name, value: element.Id });
                    };
                    _.each(TuilsStorage.bikeReferences, function (element, index) {
                        _.each(element.ChildrenCategories, function (child, index) {
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
                            placeholderText: this.$("#txtBike").attr('placeholder'),
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
                console.log('lanza cierre');
                this.trigger('close');
            },
            render: function () {
                this.$el.html(this.template());

                //Valida si debe seleccionar por defecto alguno de los tipos de usuarios
                if (this.sourceModel.get('default_reg')) {
                    var active = this.sourceModel.get('default_reg') == 'empresas' ? 1 : 0;
                    this.$("#step1 a[tuils-action='" + active + "']").click();
                }

                this.tagBikeReferences();

                Backbone.Validation.bind(this);
                this.stickit();
                return this;
            }
        });

        return CreateUserView;
    });