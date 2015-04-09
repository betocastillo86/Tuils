var PublishProductView = BaseView.extend({


    //Views
    viewSelectCategory: undefined,
    viewProductDetail: undefined,
    viewImageSelector: undefined,
    viewSummary: undefined,
    //EndViews

    currentStep :1,

    model: undefined,

    images: undefined,

    productType : 0,

    initialize: function (args) {        
        this.productType = args.productType;
        this.model = new ProductModel('ProductTypeId', this.productType );
        this.loadControls();
        this.render();
    },
    render: function ()
    {
        return this;
    },
    loadControls : function()
    {
        this.showStep();
        this.showCategories();
    },
    showCategories: function ()
    {
        this.viewSelectCategory = new SelectCategoryView({ el: "#divStep_1", productType: this.productType });
        this.viewSelectCategory.on("category-selected", this.showProductDetail, this);
        this.viewSelectCategory.once("categories-loaded", TuilsStorage.loadBikeReferences);
        this.viewSelectCategory.on("categories-middle-selected", this.restartNextStep, this);
    },
    showProductDetail: function (categoryId)
    {
        this.showNextStep();
        this.model.set('CategoryId', categoryId);

        if (!this.viewProductDetail)
        {
            this.viewProductDetail = new ProductDetailView({ el: "#divStep_2", productType: this.productType, selectedCategory: categoryId, model: this.model });
            this.viewProductDetail.on("detail-product-finished", this.showPictures, this);
            this.viewProductDetail.on("detail-product-back", this.showStepBack, this);
        }
    },
    showPictures: function(model)
    {
        this.model = model;
        this.showNextStep();
        if (!this.viewImageSelector)
        {
            this.viewImageSelector = new ImagesSelectorView({ el: "#divStep_3" });
            this.viewImageSelector.on("images-save", this.showSummary, this);
            this.viewImageSelector.on("images-back", this.showStepBack, this);
        }
    },
    showSummary: function (images) {
        this.showNextStep();
        this.images = images;
        this.viewSummary = new SummaryView({ el: "#divStep_4", product: this.model, images: this.images, productType: this.productType, breadCrumb: this.viewSelectCategory.breadCrumbCategories });
        this.viewSummary.on("summary-back", this.showStepBack, this);
        this.viewSummary.on("summary-save", this.save, this);
    },
    showStep: function ()
    {
        this.$("div[id^='btnPublishProductStep']").removeClass('selectedStep');
        this.$("#btnPublishProductStep"+this.currentStep).addClass('selectedStep');
        this.$("div[id^='divStep_']").hide();
        this.$("#divStep_"+this.currentStep).show();
    },
    showStepBack: function ()
    {
        this.showStep(--this.currentStep);
    },
    showNextStep : function()
    {
        this.showStep(++this.currentStep);
    },
    //Reinicia la vista del siguiente paso xq el DOM cargado ya no va ser valido
    //Ejemplo: En detalle da clic en atras y selecciona una nueva categoria, esto inhbilita lo del siguiente paso
    restartNextStep: function ()
    {
        if (this.currentStep == 1 && this.viewProductDetail)
        {
            this.viewProductDetail.undelegateEvents();
            this.viewProductDetail = undefined;
        } 
        else if (this.currentStep == 2 && this.viewImageSelector)
            this.viewImageSelector.undelegateEvents();
    },
    errorOnSaving : function()
    {
        
    },
    productSaved : function(model)
    {
        this.viewSelectCategory.remove();
        this.viewImageSelector.remove();
        this.viewProductDetail.remove();
        this.viewSummary.remove();
        this.showNextStep();
        AppTuils.router.navigate("quiero-vender/publicacion-exitosa/" + model.get('Id'));
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