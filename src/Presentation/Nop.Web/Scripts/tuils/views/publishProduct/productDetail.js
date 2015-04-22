define(['jquery', 'underscore', 'backbone', 'baseView', 'manufacturerModel', 'manufacturerCollection', 'storage', 'util', 'htmlEditorView', 'configuration', 'specificationAttributeModel','tagit', 'validations', 'stickit'],
    function ($, _, Backbone, BaseView, ManufacturerModel, ManufacturerCollection, TuilsStorage, TuilsUtil, HtmlEditorView, TuilsConfiguration, SpecificationAttributeModel) {
    "use strict"
    var ProductDetailView = BaseView.extend({

        events: {
            "click .btnNext": "save",
            "click .btnBack": "back",
            "change #chkIsShipEnabled": "switchShipping",
            "change #chkIncludeSupplies" : "switchSupplies"
        },

        bindings: {
            "#txtName": {
                observe: "Name",
                onSet: function (value, ctx) {
                    var maxSize = ctx.view.model.validation.Name.maxLength;
                    ctx.view.$("#addMessageName").html("Restan " + (maxSize - value.length) + " caracteres");
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
                    return parseInt(value);
                }
            },
            "#txtBikeReferencesProduct": {
                observe: "SpecialCategories",
                onSet: function (value) {
                    var brands = new Array();
                    _.each(value.split(','), function (element) {
                        brands.push({ SpecialTypeId: TuilsConfiguration.specialCategories.bikeBrand, CategoryId: parseInt(element) });
                    });
                    return brands;
                }
            },
            "#productHtml_textarea": {
                observe: "FullDescription",
                controlToMark: "#divProductHtml"
            },
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
                onSet: function (value, ctx) {
                    var names = "";
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
            this.loadHtmlEditor();
            this.switchShipping();
            if(this.productType != TuilsConfiguration.productBaseTypes.product)
                this.myStickit();
        },
        loadHtmlEditor: function () {
            //el HTML no está habilitado para las motos
            if (this.productType != TuilsConfiguration.productBaseTypes.bike)
                this.viewHtmlEditor = new HtmlEditorView({ el: this.el, prefix: 'productHtml' });
        },
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
                var tagReferences = [];

                var addTag = function (element) {
                    tagReferences.push({ label: element.Name, value: element.Id });
                };
                _.each(TuilsStorage.bikeReferences, function (element, index) {
                    addTag(element);
                    _.each(element.ChildrenCategories, function (child, index) {
                        child.Name = element.Name + ' ' + child.Name;
                        addTag(child);
                    });
                });

                this.$("#txtBikeReferencesProduct")
                    .tagit({
                        availableTags: tagReferences,
                        allowOnlyAvailableTags: true,
                        tagLimit: 5,
                        autocomplete: {
                            source: TuilsUtil.tagItAutocomplete
                        }
                    });
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
        switchShipping: function (obj) {
            if (this.$("#chkIsShipEnabled").prop("checked"))
                this.$("[tuils-for='shipping']").show();
            else
                this.$("[tuils-for='shipping']").hide();
        },
        switchSupplies: function (obj) {
            if (this.$("#chkIncludeSupplies").prop("checked")) {
                this.$("[tuils-for='no-supplies']").hide();
            }
            else {
                this.$("[tuils-for='no-supplies']").show();
            }
        },
        save: function () {
            this.model.set({ FullDescription: this.$("#productHtml_textarea").val() });
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

