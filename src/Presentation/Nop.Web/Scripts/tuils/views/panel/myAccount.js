define(['underscore', 'backbone',  'categoryModel', 'util', 'baseView', 'jqueryui'], function (_, Backbone, CategoryModel, TuilsUtilities, BaseView) {
    var MyAccountView = BaseView.extend({

        events : {
            "change #BikeBrandId": "loadReferences",
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
            this.ddlBikeBrand = this.$("#BikeBrandId");
            this.ddlBikeReference = this.$("#BikeReferenceId");
        },
        loadReferences: function () {

            if (this.ddlBikeBrand.val()) {
                var brand = new CategoryModel();
                brand.on("sync", this.showReferences, this);
                brand.getCategory(this.ddlBikeBrand.val());
            }
            else {
                this.ddlBikeReference.empty();
            }
            
        },
        showDatepicker: function () {
            this.$("#DateOfBirth").datepicker('show');
        },
        showReferences: function (category) {
            TuilsUtilities.loadDropDown(this.ddlBikeReference, category.toJSON().ChildrenCategories);
        },
        loadDateOfBirth: function () {
            this.$("#DateOfBirth").datepicker({ maxDate: '-15y' });
        },
        render: function () {
            this.basicValidations();
            return this;
        }
    });

    return MyAccountView;
});