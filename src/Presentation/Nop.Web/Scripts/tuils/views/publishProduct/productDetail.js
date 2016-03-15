define(['jquery', 'underscore', 'backbone', 'baseView', 'manufacturerModel', 'manufacturerCollection', 'storage',
    'util', 'configuration', 'specificationAttributeModel', 'tagit', 'validations', 'stickit'],
    function ($, _, Backbone, BaseView, ManufacturerModel, ManufacturerCollection, TuilsStorage,
        TuilsUtil, TuilsConfiguration, SpecificationAttributeModel) {
    "use strict"
    var ProductDetailView = BaseView.extend({

        events: {
            "click .btnNext": "save",
            "click .btnBack": "back",
            "change #chkIsShipEnabled": "switchDoorToDoor",
            "change #chkIncludeSupplies": "switchSupplies",
            "change #chkHasSpecialBikes": 'switchBikeReferences',
            'change #chkCallForPrice' : 'switchCallForPrice'
        },

        bindings: {
            "#txtName": {
                observe: "Name",
                onSet: function (value, ctx) {
                    var maxSize = ctx.view.model.validation.Name.maxLength;
                    var diff = maxSize - value.length;
                    if(diff < 0)
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
                    return value;
                }
            },
            "#chkIsShipEnabled": "IsShipEnabled",
            "#txtAdditionalShippingCharge": {
                observe : "AdditionalShippingCharge",
                onSet : function(value){
                    return parseInt(value);
                }
            } ,
            "#txtPrice": {
                observe: "Price",
                onSet: function (value) {
                    return value ? parseInt(value) : '';
                },
                onGet: function (value) {
                    return value ? parseInt(value) : '';
                },
            },
            '#chkCallForPrice': 'CallForPrice',
            "#txtBikeReferencesProduct": {
                observe: "SpecialCategories",
                onSet: function (value) {
                    var brands = new Array();
                    _.each(value.split(','), function (element) {
                        brands.push({ SpecialTypeId: TuilsConfiguration.specialCategories.bikeBrand, CategoryId: parseInt(element) });
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
                observe : "Condition",
                onSet: function (value, ctx) {
                    //Toma el texto seleccionado y se lo asigna al nombre de la condición
                    ctx.view.model.set("ConditionName", ctx.view.$(ctx.selector + " :selected").text());
                    return value;
                }
            },
            "#txtCarriagePlate": "CarriagePlate",
            "#ddlColor": {
                observe : "Color" ,
                onSet: function (value,ctx) {
                    //Toma el texto seleccionado y se lo asigna al nombre del color
                    ctx.view.model.set("ColorName", ctx.view.$(ctx.selector + " :selected").text());
                    return value;
                }
            },
            "#ddlYear": "Year",
            "#txtKms": "Kms",
            "[name='Negotiation']": {
                observe: "Negotiation",
                onSet: function (value, ctx) {
                    var names = TuilsUtil.pluckPropertiesJquery(ctx.view.$(ctx.selector + ":checked"), 'tuils-name');
                    ctx.view.model.set('NegotiationName', names);
                    return value;
                }
            },
            "[name='Accesories']": {
                observe: "Accesories",
                onSet: function (value, ctx) {
                    var names = TuilsUtil.pluckPropertiesJquery(ctx.view.$(ctx.selector + ":checked"), 'tuils-name');
                    ctx.view.model.set('AccesoriesName', names);
                    return value;
                }
            },
            "#ddlIsNew" : "IsNew",
            "#ddlStateProvince": "StateProvince",
            "#txtDetailShipping": "DetailShipping",
            "#chkIncludeSupplies": "IncludeSupplies",
            "#txtSupplies": {
                observe: "Supplies",
                controlToMark: '[tuils-for="no-supplies"] .tagit-new .ui-widget-content',
                onSet: function (value, ctx) {
                    var names = "";
                    if(value.length > 0)
                        _.each(value.split(','), function (element) {
                            //Carga los nombres de los insumos
                            var name = _.findWhere(ctx.view.suppliesCollection.get('Options'), { Id: element }).Name;
                            names += names!="" ? ("," + name) : name;
                        });
                    ctx.view.model.set("SuppliesName", names);

                    return value.split(',');
                }
            },
            "#txtSuppliesValue": {
                observe: 'SuppliesValue',
                onSet: function (value) {
                    return parseInt(value);
                }
            }
        },

        //views
        viewHtmlEditor: undefined,
        //views

        productType: undefined,

        selectedCategory: undefined,

        manufacturersCollection: undefined,

        suppliesCollection : undefined,

        initialize: function (args) {
            this.model = args.model;
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
            this.tagBikeReferences();
            this.loadSupplies();
            //this.loadHtmlEditor();
            //this.switchShipping();
            if(this.productType != TuilsConfiguration.productBaseTypes.product)
                this.myStickit();

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
        loadSupplies: function () {
            if (this.productType == TuilsConfiguration.productBaseTypes.service) {
                this.suppliesCollection = new SpecificationAttributeModel();
                this.suppliesCollection.on("sync", this.tagSupplies, this);
                this.suppliesCollection.getSupplies();
            }
        },
        myStickit: function () {
            this.stickThem();
        },
        tagBikeReferences: function () {


            //Los tags no se cargan para las motocicletas
            if (this.productType != TuilsConfiguration.productBaseTypes.bike)
            {
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
                }
                else {
                    TuilsStorage.loadBikeReferences(this.tagBikeReferences, this);
                }

                
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
            //this.model.set({ FullDescription: this.$("#productHtml_textarea").val() });
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

