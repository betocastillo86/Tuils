define(['jquery', 'underscore', 'backbone', 'baseView', 'storage', 'util', 'tuils/models/vendor', 'categoryCollection', 'configuration', 'tagit'],
    function ($, _, Backbone, BaseView, TuilsStorage, TuilsUtil, VendorModel, CategoryCollection, TuilsConfiguration) {
    var VendorServicesView = BaseView.extend({

        events: {
            
        },
        initialize: function (args) {
            this.model = new VendorModel();
            this.loadControls();
            this.render();
        },
        loadControls: function () {
            TuilsStorage.loadBikeReferences(this.loadReferences, this);
            var services = new CategoryCollection();
            services.on("sync", this.loadServices, this);
            services.getServices();
            if (this.$('.confirmHiddenField').length > 0)
                this.alert({ message: this.$('.confirmHiddenField').val() });
        },
        loadReferences: function (ctx) {
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

            ctx.$("#BikeReferencesString")
                .tagit({
                    availableTags: tagReferences,
                    allowOnlyAvailableTags: true,
                    tagLimit: 5,
                    autocomplete: {
                        source: TuilsUtil.tagItAutocomplete
                    },
                    allowSpaces:true
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
            });

            this.$("#SpecializedCategoriesString")
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
            return this;
        }
    });

    return VendorServicesView;
});