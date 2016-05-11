define(['jquery', 'underscore', 'backbone', 'baseView', 'manufacturerModel', 'manufacturerCollection', 'storage',
    'util', 'configuration', 'specificationAttributeModel', 'categoryCollection',
     'tagit', 'validations', 'stickit'],
    function ($, _, Backbone, BaseView, ManufacturerModel, ManufacturerCollection, TuilsStorage,
        TuilsUtil, TuilsConfiguration, SpecificationAttributeModel, CategoryCollection) {
        "use strict"
        var ProductDetailView = BaseView.extend({

            events: {
                "click .btnNext": "save",
                "click .btnBack": "back",
                "change #chkIsShipEnabled": "switchDoorToDoor",
                "change #chkIncludeSupplies": "switchSupplies",
                "change #chkHasSpecialBikes": 'switchBikeReferences',
                'change #chkCallForPrice': 'switchCallForPrice'
            },

            bindings: {
                "#txtName": {
                    observe: "Name",
                    onSet: function (value, ctx) {
                        var maxSize = ctx.view.model.validation.Name.maxLength;
                        var diff = maxSize - value.length;
                        if (diff < 0)
                            ctx.view.$("#addMessageName").css('color', 'red').html("Sobran " + (diff * (-1)) + " caracteres");
                        else
                            ctx.view.$("#addMessageName").css('color', '').html("Restan " + diff + " caracteres");

                        return value;
                    }
                },
                "select#ddlManufacturerId": {
                    observe: 'ManufacturerId',
                    selectOptions: {
                        collection: 'this.manufacturersCollection',
                        labelPath: 'Name',
                        valuePath: 'Id',
                        defaultOption: { label: '-', value: undefined }
                    },
                    onSet: function (value, ctx) {
                        //Toma el texto seleccionado y se lo asigna al nombre de la marca
                        ctx.view.model.set("ManufacturerIdName", ctx.view.$(ctx.selector + " :selected").text());
                        return parseInt(value);
                    }
                },
                "#chkIsShipEnabled": "IsShipEnabled",
                "#txtAdditionalShippingCharge": {
                    observe: "AdditionalShippingCharge",
                    onSet: function (value) {
                        return parseInt(value);
                    }
                },
                "#txtPrice": {
                    observe: "Price",
                    onSet: function (value) {
                        //if (value && typeof (value) == 'string')
                        //{
                        //    if (value.indexOf(',') != -1 || value.indexOf('.') != -1)
                        //    {
                        //        value = value.replace(/(,|\.)/g, '');
                        //        this.$('#txtPrice').val(value);
                        //    }
                        //}
                        return value ? parseInt(value) : '';
                    },
                    //onGet: function (value) {
                    //    if (value && typeof (value) == 'string')
                    //        value = value.replace(/(,|\.)/g, '');
                    //    
                    //    return value ? parseInt(value) : '';
                    //},
                },

                '#chkCallForPrice': 'CallForPrice',
                "#txtBikeReferencesProduct": {
                    observe: "SpecialCategories",
                    onSet: function (value) {
                        var brands = new Array();
                        _.each(value.split(','), function (element) {
                            var category = parseInt(element);
                            if (!isNaN(category))
                                brands.push({ SpecialTypeId: TuilsConfiguration.specialCategories.bikeBrand, CategoryId: category });
                        });
                        return brands;
                    },
                    onGet: function (brands, ctx) {
                        return _.pluck(brands, 'CategoryId').toString();
                    }
                },
                //"#productHtml_textarea": {
                //    observe: "FullDescription",
                //    controlToMark: "#divProductHtml"
                //},
                "#txtFullDescription": "FullDescription",
                "#ddlCondition": {
                    observe: "Condition",
                    onSet: function (value, ctx) {
                        //Toma el texto seleccionado y se lo asigna al nombre de la condición
                        ctx.view.model.set("ConditionName", ctx.view.$(ctx.selector + " :selected").text());
                        return value;
                    }
                },
                "#txtCarriagePlate": "CarriagePlate",
                "#ddlColor": {
                    observe: "Color",
                    onSet: function (value, ctx) {
                        //Toma el texto seleccionado y se lo asigna al nombre del color
                        ctx.view.model.set("ColorName", ctx.view.$(ctx.selector + " :selected").text());
                        return value;
                    }
                },
                "#ddlYear": {
                    observe: 'Year',
                    onGet: function (value) {
                        return value != undefined ? value.toString() : undefined;
                    }
                },
                "#txtKms": {
                    observe: "Kms",
                    onSet: function (value) {
                        //if (value && typeof (value) == 'string') {
                        //    if (value.indexOf(',') != -1 || value.indexOf('.') != -1) {
                        //        value = value.replace(/(,|\.)/g, '');
                        //        this.$('#txtKms').val(value);
                        //    }
                        //}
                        return value ? parseInt(value) : '';
                    }
                },
                "[name='Negotiation']": {
                    observe: "Negotiation",
                    onSet: function (value, ctx) {
                        var names = TuilsUtil.pluckPropertiesJquery(ctx.view.$(ctx.selector + ":checked"), 'tuils-name');
                        ctx.view.model.set('NegotiationName', names);
                        return value;
                    },
                    onGet: function (value) {
                        //Convierte a enteros todos los elementos
                        return _.map(value, function (num) { return num.toString(); });
                    }
                },
                "[name='Accesories']": {
                    observe: "Accesories",
                    onSet: function (value, ctx) {
                        var names = TuilsUtil.pluckPropertiesJquery(ctx.view.$(ctx.selector + ":checked"), 'tuils-name');
                        ctx.view.model.set('AccesoriesName', names);
                        return value;
                    },
                    onGet: function (value) {
                        //Convierte a enteros todos los elementos
                        return _.map(value, function (num) { return num.toString(); });
                    }
                },
                "#ddlIsNew": {
                    observe: 'IsNew',
                    onGet: function (value) {
                        //Se agrega validación para que almacene el valor real como string y haga binding bien en el combo
                        return value != undefined ? ((value == 'true' || value === true) ? 'true' : 'false') : undefined;
                    },
                },
                "#ddlStateProvince": {
                    observe: 'StateProvince',
                    onGet: function (value) {
                        //Se agrega validación para que almacene el valor real como string y haga binding bien en el combo
                        return value != undefined ? value.toString() : undefined;
                    }
                },
                "#txtDetailShipping": "DetailShipping",
                "#chkIncludeSupplies": "IncludeSupplies",
                "#txtSupplies": {
                    observe: "Supplies",
                    controlToMark: '[tuils-for="no-supplies"] .tagit-new .ui-widget-content',
                    onSet: function (value, ctx) {
                        var names = "";
                        if (value.length > 0)
                            _.each(value.split(','), function (element) {
                                //Carga los nombres de los insumos
                                var name = _.findWhere(ctx.view.suppliesCollection.get('Options'), { Id: parseInt(element) }).Name;
                                names += names != "" ? ("," + name) : name;
                            });
                        ctx.view.model.set("SuppliesName", names);

                        return value.length > 0 ? value.split(',') : undefined;
                    }
                },
                "#txtSuppliesValue": {
                    observe: 'SuppliesValue',
                    onSet: function (value) {
                        return value ? parseInt(value) : '';
                    }
                },
                '#PhoneNumber' : 'PhoneNumber'
            },

            //views
            viewHtmlEditor: undefined,
            //views

            productType: undefined,

            selectedCategory: undefined,

            manufacturersCollection: undefined,

            suppliesCollection: undefined,

            initialize: function (args) {
                this.isPreproduct = args.isPreproduct;
                this.productType = args.productType;
                this.selectedCategory = args.selectedCategory;
                this.loadControls();
                this.render();
            },
            render: function () {
                Backbone.Validation.bind(this);
                return this;
            },
            loadControls: function () {
                this.loadManufacturersByCategory();
                //El llamado de las marcas se realiza dos segundos despues para que no se curce con los llamados importantes
                var that = this;
                setTimeout(function () { that.loadBikeReferences.call(that) }, 2000);

                this.loadSupplies();

                if (this.productType != TuilsConfiguration.productBaseTypes.product)
                    this.myStickit();
                else
                {
                    //Realiza la validacion para mostrar o ocultar el campo de nuevo usado
                    //En los casos como aceites o lubricantes no lo muestra ya que no aplica
                    var disabledCats = TuilsConfiguration.catalog.disabledCategoriesForUsedProducts;
                    if (disabledCats && _.contains(disabledCats.split(/,/g), this.selectedCategory.toString())) {
                        this.$('div[data-field="IsNew"]').hide();
                        this.model.set('IsNew', true);
                    }
                    else
                        this.$('div[data-field="IsNew"]').show();
                }

                if (this.model.get('CallForPrice'))
                    this.$('#divPrice').hide();

               
                    
            },
            //loadHtmlEditor: function () {
            //    //el HTML no está habilitado para las motos
            //    if (this.productType != TuilsConfiguration.productBaseTypes.bike)
            //        this.viewHtmlEditor = new HtmlEditorView({ el: this.el, prefix: 'productHtml' });
            //},
            loadManufacturersByCategory: function () {
                //Las marcas solo aplican para productos
                if (this.productType == TuilsConfiguration.productBaseTypes.product) {
                    this.manufacturersCollection = new ManufacturerCollection();
                    this.manufacturersCollection.on("sync", this.myStickit, this);
                    this.manufacturersCollection.getByCategoryId(this.selectedCategory);
                }
            },
            myStickit: function () {
                //Le pega el teléfono al modelo
                this.model.set('PhoneNumber', this.$('#PhoneNumber').val());
                this.stickThem();
            },
            loadSupplies: function () {
                if (this.productType == TuilsConfiguration.productBaseTypes.service) {
                    this.suppliesCollection = new SpecificationAttributeModel();
                    this.suppliesCollection.on("sync", this.tagSupplies, this);
                    this.suppliesCollection.getSupplies();
                }
            },
            loadBikeReferences: function () {
                //Los tags no se cargan para las motocicletas
                if (!this.bikeReferences && this.productType != TuilsConfiguration.productBaseTypes.bike) {
                    this.bikeReferences = new CategoryCollection();
                    this.bikeReferences.on('sync', this.tagBikeReferences, this);
                    this.bikeReferences.getBikeReferences();
                }
            },
            tagBikeReferences: function () {

                var tagReferences = [];

                var addTag = function (element) {
                    tagReferences.push({ label: element.Name, value: element.Id });
                };
                _.each(this.bikeReferences.toJSON(), function (element, index) {
                    _.each(element.ChildrenCategories, function (child, index) {
                        addTag(child);
                    });
                });

                this.$("#txtBikeReferencesProduct")
                    .tagit({
                        availableTags: tagReferences,
                        allowOnlyAvailableTags: true,
                        tagLimit: TuilsConfiguration.catalog.limitOfSpecialCategories,
                        autocomplete: {
                            source: TuilsUtil.tagItAutocomplete
                        },
                        allowSpaces: true
                    });

                //Si tiene categorias especiales muestra el combo abierto
                if (this.model.get('SpecialCategories')) {
                    this.$('#chkHasSpecialBikes').click();
                }
                //Si es preproduct carga las cosas adicionales
                if (this.isPreproduct)
                {
                    this.model.set("ManufacturerIdName", this.$("#ddlManufacturerId :selected").text());
                    if (this.model.get('IsShipEnabled')) this.$('[tuils-for="no-doorToDoor"]').show();
                }
                    
            },
            tagSupplies: function () {
                this.$("#txtSupplies")
                    .tagit({
                        availableTags: TuilsUtil.tagitAvailableTags(this.suppliesCollection.get('Options')),
                        allowOnlyAvailableTags: true,
                        tagLimit: 5,
                        autocomplete: {
                            source: TuilsUtil.tagItAutocomplete
                        }
                    });
            },
            //switchShipping: function (obj) {
            //    if (this.$("#chkIsShipEnabled").prop("checked"))
            //        this.$("[tuils-for='shipping']").show();
            //    else
            //        this.$("[tuils-for='shipping']").hide();
            //},
            switchSupplies: function (obj) {
                this.$("[tuils-for='no-supplies']").css('display', !obj.currentTarget.checked ? 'block' : 'none');
            },
            switchDoorToDoor: function (obj) {
                this.$("[tuils-for='no-doorToDoor']").css('display', obj.currentTarget.checked ? 'block' : 'none');
            },
            switchBikeReferences: function (obj) {
                this.$("#divBikeReferences").css("display", obj.currentTarget.checked ? "block" : "none");
            },
            switchCallForPrice: function (obj) {
                this.$("#divPrice").css('display', obj.currentTarget.checked ? 'none' : 'block');
            },
            save: function () {
                //Salva el preproducto sin importar las validaciones
                this.trigger('save-preproduct', this.model);
                this.validateControls();
                if (this.model.isValid()) {
                    this.trigger("detail-product-finished", this.model);
                }
            },
            cleanView: function () {
                //Limpia la vis
                this.unstickit();
                this.undelegateEvents();
            },
            back: function () {
                this.trigger("detail-product-back");
            }

        });

        return ProductDetailView;
    });

