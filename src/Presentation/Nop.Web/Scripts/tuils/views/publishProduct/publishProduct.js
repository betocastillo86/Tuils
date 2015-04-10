﻿define(['jquery', 'underscore', 'baseView', 'productModel', 'publishProductSelectCategoryView', 'storage'],
    function ($, _, BaseView, ProductModel, SelectCategoryView, TuilsStorage) {

    var PublishProductView = BaseView.extend({


        //Views
        viewSelectCategory: undefined,
        viewProductDetail: undefined,
        viewImageSelector: undefined,
        viewSummary: undefined, 
        //EndViews

        currentStep: 1,

        model: undefined,

        images: undefined,

        productType: 0,

        initialize: function (args) {
            this.productType = args.productType;
            this.model = new ProductModel('ProductTypeId', this.productType);
            this.loadControls();
            this.render();
        },
        render: function () {
            return this;
        },
        loadControls: function () {
            this.showStep();
            this.showCategories();
        },
        showCategories: function () {
            this.viewSelectCategory = new SelectCategoryView({ el: "#divStep_1", productType: this.productType });
            this.viewSelectCategory.on("category-selected", this.showProductDetail, this);
            this.viewSelectCategory.once("categories-loaded", TuilsStorage.loadBikeReferences);
            this.viewSelectCategory.on("categories-middle-selected", this.restartNextStep, this);
        },
        showProductDetail: function (categoryId) {
            this.showNextStep();
            this.model.set('CategoryId', categoryId);

            if (!this.viewProductDetail) {

                var that = this;

                require(['publishProductProductDetailView'], function (ProductDetailView) {
                    that.viewProductDetail = new ProductDetailView({ el: "#divStep_2", productType: that.productType, selectedCategory: categoryId, model: that.model });
                    that.viewProductDetail.on("detail-product-finished", that.showPictures, that);
                    that.viewProductDetail.on("detail-product-back", that.showStepBack, that);
                });

            }
        },
        showPictures: function (model) {
            this.model = model;
            this.showNextStep();
            var that = this;
            if (!this.viewImageSelector) {
                require(['imageSelectorView'], function (ImagesSelectorView) {
                    that.viewImageSelector = new ImagesSelectorView({ el: "#divStep_3" });
                    that.viewImageSelector.on("images-save", that.showSummary, that);
                    that.viewImageSelector.on("images-back", that.showStepBack, that);
                });
            }
        },
        showSummary: function (images) {
            this.showNextStep();
            this.images = images;

            var that = this;
            require(['publishProductSummaryView'], function (SummaryView) {
                that.viewSummary = new SummaryView({ el: "#divStep_4", product: that.model, images: that.images, productType: that.productType, breadCrumb: that.viewSelectCategory.breadCrumbCategories });
                that.viewSummary.on("summary-back", that.showStepBack, that);
                that.viewSummary.on("summary-save", that.save, that);
            });
            
        },
        showStep: function () {
            this.$("div[id^='btnPublishProductStep']").removeClass('selectedStep');
            this.$("#btnPublishProductStep" + this.currentStep).addClass('selectedStep');
            this.$("div[id^='divStep_']").hide();
            this.$("#divStep_" + this.currentStep).show();
        },
        showStepBack: function () {
            this.showStep(--this.currentStep);
        },
        showNextStep: function () {
            this.showStep(++this.currentStep);
        },
        //Reinicia la vista del siguiente paso xq el DOM cargado ya no va ser valido
        //Ejemplo: En detalle da clic en atras y selecciona una nueva categoria, esto inhbilita lo del siguiente paso
        restartNextStep: function () {
            if (this.currentStep == 1 && this.viewProductDetail) {
                this.viewProductDetail.undelegateEvents();
                this.viewProductDetail = undefined;
            }
            else if (this.currentStep == 2 && this.viewImageSelector)
                this.viewImageSelector.undelegateEvents();
        },
        errorOnSaving: function () {

        },
        productSaved: function (model) {
            this.viewSelectCategory.remove();
            this.viewImageSelector.remove();
            this.viewProductDetail.remove();
            this.viewSummary.remove();
            this.showNextStep();
            Backbone.history.navigate("quiero-vender/publicacion-exitosa/" + model.get('Id'));
        },
        save: function () {

            if (this.$("#chkConditions").is(":checked")) {
                this.model.set('TempFiles', _.pluck(this.images.toJSON(), 'guid'));
                this.model.on('sync', this.productSaved, this);
                this.model.on('error', this.errorOnSaving, this);
                this.validateAuthorization();
                this.model.publish();
            }
            else {
                alert("Debes aceptar terminos y condiciones");
            }

        }
    });

    return PublishProductView;
});

