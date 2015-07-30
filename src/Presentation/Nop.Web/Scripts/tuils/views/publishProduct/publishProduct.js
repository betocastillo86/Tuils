define(['jquery', 'underscore', 'baseView', 'productModel', 'storage',
    'configuration', 'publishProductSelectCategoryView', 'publishProductProductDetailView',
    'publishProductFinishedView', 'publishProductSummaryView'],
    function ($, _, BaseView, ProductModel, TuilsStorage,
        TuilsConfiguration, SelectCategoryView, ProductDetailView,
        PublishFinishedView, SummaryView) {

    var PublishProductView = BaseView.extend({

        events :{
            'click .wizard-breadcrumb li': 'showStepByBreadcrumb'
        },
        //Views
        viewSelectCategory: undefined,
        viewProductDetail: undefined,
        viewImageSelector: undefined,
        viewSummary: undefined,
        viewPublishFinished : undefined,
        //EndViews

        currentStep: 1,

        model: undefined,

        images: undefined,

        productType: 0,

        //Numero del paso que ya fue terminado
        //Sirve para validar los pasos cuando se hacen por medio de las pestañas
        stepFinished: 0,

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
            this.on("user-authenticated", this.save, this);
            $('.btn_anuncia').hide();
        },
        showCategories: function () {
            var that = this;
            that.viewSelectCategory = new SelectCategoryView({ el: "#divStep_1", productType: that.productType });
            that.viewSelectCategory.on("category-selected", that.showProductDetail, that);
            that.viewSelectCategory.once("categories-loaded", TuilsStorage.loadBikeReferences);
            that.viewSelectCategory.on("categories-middle-selected", that.restartNextStep, that);
        },
        showProductDetail: function (categoryId) {
            this.showNextStep();
            this.model.set('CategoryId', categoryId);

            if (!this.viewProductDetail) {

                var that = this;
                that.viewProductDetail = new ProductDetailView({ el: "#divStep_2", productType: that.productType, selectedCategory: categoryId, model: that.model });
                that.viewProductDetail.on("detail-product-finished", that.showPictures, that);
                that.viewProductDetail.on("detail-product-back", that.showStepBack, that);
            }
        },
        showPictures: function (model) {
            this.model = model;
            this.showNextStep();
            var that = this;

            if (this.productType != TuilsConfiguration.productBaseTypes.service) {
                if (!this.viewImageSelector) {
                    require(['imageSelectorView'], function (ImagesSelectorView) {
                        that.viewImageSelector = new ImagesSelectorView({ el: "#divStep_3" });
                        that.viewImageSelector.on("images-save", that.showSummary, that);
                        that.viewImageSelector.on("images-back", that.showStepBack, that);
                    });
                }
            }
            else {
                this.showSummary();
            }

        },
        showSummary: function (images) {
            
            this.showNextStep();
            this.images = images;

            if (!this.viewSummary) {

                var that = this;
                that.viewSummary = new SummaryView({ el: "#divStep_4", product: that.model, images: that.images, productType: that.productType, breadCrumb: that.viewSelectCategory.breadCrumbCategories });
                that.viewSummary.on("summary-back", that.showStepBack, that);
                that.viewSummary.on("summary-save", that.save, that);
            }
            else {
                this.viewSummary.loadControls({ product: this.model, images: this.images, breadCrumb: this.viewSelectCategory.breadCrumbCategories });
            }
            
        },
        showStep: function () {
            if (this.currentStep < 5)
            {
                this.$(".wizard-current").removeClass('wizard-current').addClass("wizard-step");
                this.$("#btnPublishProductStep" + this.currentStep).removeClass("wizard-step").addClass('wizard-current');
                this.$("div[id^='divStep_']").hide();
                this.scrollFocusObject('.wizard-breadcrumb', -50);
            }
            
            this.$("#divStep_" + this.currentStep).show();
        },
        showStepBack: function () {
            this.showStep(--this.currentStep);
            //Para los casos en que es sevicio, valida que no muestre nunca las imagenes
            if (this.productType == TuilsConfiguration.productBaseTypes.service && this.currentStep == 3) this.showStep(--this.currentStep);
        },
        showNextStep: function () {
            this.showStep(++this.currentStep);
            //Cuando el paso actual va más avanzado que los pasos terminados por el usuario lo iguala
            //con el fin de tener el valor real del paso terminado
            if (this.currentStep > this.stepFinished)
                this.stepFinished = this.currentStep;
        },
        showStepByBreadcrumb : function(obj){
            var step = parseInt($(obj.currentTarget).attr("data-id"));

            //no puede haber pasos superiores al ultimo finalizado
            if (step <= this.stepFinished)
            {
                //hasta que alcance el paso actual   desea  empieza a disminuir o aumentar los pasos
                while (this.currentStep != step) {
                    if (step > this.currentStep)
                        this.showNextStep();
                    else
                        this.showStepBack();
                        
                }
            }
            
        },
        //Reinicia la vista del siguiente paso xq el DOM cargado ya no va ser valido
        //Ejemplo: En detalle da clic en atras y selecciona una nueva categoria, esto inhbilita lo del siguiente paso
        restartNextStep: function () {
            if (this.currentStep == 1 && this.viewProductDetail) {
                this.viewProductDetail.cleanView();
                this.viewProductDetail = undefined;
            }
            else if (this.currentStep == 2 && this.viewImageSelector)
                this.viewImageSelector.undelegateEvents();
            else if (this.currentStep == 4 && this.viewSummary)
            {
                this.viewSummary.undelegateEvents();
                this.viewSummary = undefined;
            }
            //Cuando se reinicia el paso siguiente también se reinicia el paso finalizado
            this.stepFinished = this.currentStep;
        },
        errorOnSaving: function (model, response) {
            if (response.responseJSON.ModelState && response.responseJSON.ModelState.ErrorCode == TuilsConfiguration.errorCodes.publishInvalidCategory)
                alert(response.responseJSON.ModelState.ErrorMessage);
            else
                alert("Ocurrió un error, intentalo de nuevo");
        },
        showFinish: function () {
            this.showNextStep();
            this.viewPublishFinished = new PublishFinishedView({ el: '#divStep_5', model: this.model, images: this.images });
            this.$(".wizard-breadcrumb").hide();
        },
        productSaved: function (model) {
            this.viewSelectCategory.remove();
            if(this.viewImageSelector) this.viewImageSelector.remove();
            this.viewProductDetail.remove();
            this.viewSummary.remove();
            this.showFinish();
            Backbone.history.navigate("quiero-vender/publicacion-exitosa/" + model.get('Id'));
        },
        save: function () {
            this.model.set('TempFiles', _.pluck(this.images ? this.images.toJSON() : undefined, 'guid'));
            this.model.once('sync', this.productSaved, this);
            this.model.once('error', this.errorOnSaving, this);
            this.validateAuthorization();
            this.model.publish();
        }
    });

    return PublishProductView;
});

