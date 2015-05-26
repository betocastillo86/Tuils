define(['underscore', 'backbone', 'baseView', 'storage', 'util', 'tuils/models/vendor', 'categoryCollection', 'configuration', 'tagit'],
    function (_, Backbone, BaseView, TuilsStorage, TuilsUtil, VendorModel, CategoryCollection, TuilsConfiguration) {
    var VendorServicesView = BaseView.extend({

        events: {
            
        },

        bindings: {
            "#BikeReferencesString": {
                observe: "BikeReferencesString",
                onSet: function (value) {
                    var brands = new Array();
                    _.each(value.split(','), function (element) {
                        brands.push({ SpecialTypeId: TuilsConfiguration.specialCategoriesVendor.bikeBrand, CategoryId: parseInt(element) });
                    });
                    return brands;
                }
            },
            "#SpecializedCategoriesString": {
                observe: "SpecializedCategoriesString",
                onSet: function (value) {
                    var brands = new Array();
                    _.each(value.split(','), function (element) {
                        brands.push({ SpecialTypeId: TuilsConfiguration.specialCategoriesVendor.specializedCategory, CategoryId: parseInt(element) });
                    });
                    return brands;
                }
            }
        },

        initialize: function (args) {
            this.model = new VendorModel();
            this.loadControls();
            this.render();
        },
        loadControls: function () {
            TuilsStorage.loadBikeReferences(this.loadReferences);
            var services = new CategoryCollection();
            services.on("sync", this.loadServices, this);
            services.getServices();
        },
        loadReferences: function () {
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

            this.$("#BikeReferencesString")
                .tagit({
                    availableTags: tagReferences,
                    allowOnlyAvailableTags: true,
                    tagLimit: 5,
                    autocomplete: {
                        source: TuilsUtil.tagItAutocomplete
                    }
                });
        },
        loadServices: function (services) {
            var tagReferences = [];

            var addTag = function (element) {
                tagReferences.push({ label: element.Name, value: element.Id });
            };
            var searchChildrenCategories = function (element) {
                _.each(element.ChildrenCategories, function (child, index) {
                    child.Name = child.Name;
                    addTag(child);
                    searchChildrenCategories(child);
                });
            }
            _.each(services.toJSON(), function (element, index) {
                addTag(element);
                searchChildrenCategories(element);
                //_.each(element.ChildrenCategories, function (child, index) {
                //    child.Name = element.Name + ' ' + child.Name;
                //    addTag(child);
                //});
            });

            $("#SpecializedCategoriesString")
                .tagit({
                    availableTags: tagReferences,
                    allowOnlyAvailableTags: true,
                    tagLimit: 5,
                    autocomplete: {
                        source: TuilsUtil.tagItAutocomplete
                    }
                });
        },
        render: function () {
            this.basicValidations();
            this.stickThem();
            return this;
        }
    });

    return VendorServicesView;
});