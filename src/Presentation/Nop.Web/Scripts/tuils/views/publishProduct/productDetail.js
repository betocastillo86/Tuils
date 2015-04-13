define(['jquery', 'underscore', 'backbone', 'baseView', 'manufacturerModel', 'manufacturerCollection', 'storage', 'util', 'htmlEditorView', 'configuration', 'tagit', 'validations', 'stickit'],
    function ($, _, Backbone, BaseView, ManufacturerModel, ManufacturerCollection, TuilsStorage, TuilsUtil, HtmlEditorView, TuilsConfiguration) {
    "use strict"
    var ProductDetailView = BaseView.extend({

        events: {
            "click .btnNext": "save",
            "click .btnBack": "back",
            "change #chkIsShipEnabled": "switchShipping"
        },

        bindings: {
            "#txtName": "Name",
            "select#ddlManufacturerId": {
                observe: 'ManufacturerId',
                selectOptions: {
                    collection: 'this.manufacturersCollection',
                    labelPath: 'Name',
                    valuePath: 'Id',
                    defaultOption: { label: '-', value: undefined }
                }
            },
            "#chkIsShipEnabled": "IsShipEnabled",
            "#txtAdditionalShippingCharge": "AdditionalShippingCharge",
            "#txtPrice": "Price",
            "#txtBikeReferencesProduct": {
                observe: "SpecialCategories",
                onGet: function (values) {


                },
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
            }
        },

        //views
        viewHtmlEditor: undefined,
        //views

        productType: undefined,

        selectedCategory: undefined,

        manufacturersCollection: undefined,

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
            this.loadHtmlEditor();
            this.switchShipping();
        },
        loadHtmlEditor: function () {
            this.viewHtmlEditor = new HtmlEditorView({ el: this.el, prefix: 'productHtml' });
        },
        loadManufacturersByCategory: function () {
            this.manufacturersCollection = new ManufacturerCollection();
            this.manufacturersCollection.on("sync", this.myStickit, this);
            this.manufacturersCollection.getByCategoryId(this.selectedCategory);
        },
        myStickit: function () {
            this.stickThem();
        },
        tagBikeReferences: function () {
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
        },
        switchShipping: function (obj) {
            if (this.$("#chkIsShipEnabled").prop("checked"))
                this.$("[tuils-for='shipping']").show();
            else
                this.$("[tuils-for='shipping']").hide();
        },
        save: function () {
            this.model.set({ FullDescription: this.$("#productHtml_textarea").val() });
            this.validateControls();
            if (this.model.isValid()) {
                this.model.set('ManufacturerName', this.$("#ddlManufacturerId :selected").text());
                this.trigger("detail-product-finished", this.model);
            }
        },
        back: function () {
            this.trigger("detail-product-back");
        }

    });

    return ProductDetailView;
});

