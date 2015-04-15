define(['jquery', 'underscore', 'baseView', 'productModel', 'storage'],
    function ($, _, BaseView, ProductModel, TuilsStorage) {

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
            this.model = new ProductModel({ 'ProductTypeId': this.productType });
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
            var that = this;
            require(['publishProductSelectCategoryView'], function (SelectCategoryView) {
                that.viewSelectCategory = new SelectCategoryView({ el: "#divStep_1", productType: that.productType });
                that.viewSelectCategory.on("category-selected", that.showProductDetail, that);
                that.viewSelectCategory.once("categories-loaded", TuilsStorage.loadBikeReferences);
                that.viewSelectCategory.on("categories-middle-selected", that.restartNextStep, that);
            });
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
            //this.$("div[id^='btnPublishProductStep']").removeClass('wizard-current').addClass();
            if (this.currentStep < 5)
            {
                this.$(".wizard-current").removeClass('wizard-current').addClass("wizard-step");
                this.$("#btnPublishProductStep" + this.currentStep).removeClass("wizard-step").addClass('wizard-current');
                this.$("div[id^='divStep_']").hide();
            }
            
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
            alert("Ocurrió un error, intentalo de nuevo");
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
            this.model.set('TempFiles', _.pluck(this.images.toJSON(), 'guid'));
            this.model.on('sync', this.productSaved, this);
            this.model.on('error', this.errorOnSaving, this);
            this.validateAuthorization();
            this.model.publish();
        }
    });

    return PublishProductView;
});

