define(['jquery', 'underscore', 'backbone', 'categoryModel', 'util', 'baseView', 'configuration'],
    function ($, _, Backbone, CategoryModel, TuilsUtilities, BaseView, TuilsConfiguration) {
    var MyAccountView = BaseView.extend({

        events : {
            "change #BikeBrand_CategoryId": "loadReferences",
            'click #divDateOfBirth .datepickerButton': 'showDatepicker'
        },

        ddlBikeBrand: undefined,

        ddlBikeReference: undefined,

        initialize: function (args) {
            this.loadControls();
            this.render();
        },
        loadControls: function () {
            this.loadDateOfBirth();
            this.ddlBikeBrand = this.$("#BikeBrand_CategoryId");
            this.ddlBikeReference = this.$("#BikeReferenceId");
            
            if (this.$('.confirmHiddenField').length > 0)
                this.alert({ message: this.$('.confirmHiddenField').val() });
        },
        loadReferences: function () {

            if (this.ddlBikeBrand.val()) {
                var brand = new CategoryModel();
                brand.on("sync", this.showReferences, this);
                brand.getCategory(this.ddlBikeBrand.val(), true);
            }
            else {
                this.ddlBikeReference.empty();
            }
            
        },
        showDatepicker: function () {
            this.$("#DateOfBirth").datepicker('show');
        },
        showReferences: function (category) {

            category = category.toJSON();
            this.$("#imgBrand").attr("src", category.PictureModel.ImageUrl);
            TuilsUtilities.loadDropDown(this.ddlBikeReference, category.ChildrenCategories);
        },
        loadDateOfBirth: function () {
            this.$("#DateOfBirth").datepicker({ maxDate: '-15y', changeYear: true, yearRange: 'c-50:c', dateFormat: TuilsConfiguration.jquery.dateFormat });
        },
        render: function () {
            this.basicValidations();
            return this;
        }
    });

    return MyAccountView;
});