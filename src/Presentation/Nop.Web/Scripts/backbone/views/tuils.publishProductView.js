var PublishProductView = Backbone.View.extend({


    //Views
    viewSelectCategory: undefined,
    viewProductDetail : undefined,
    //EndViews

    currentStep :1,

    model : undefined,

    productType : 0,

    initialize: function (args) {        
        this.productType = args.productType;
        this.model = new ProductModel();
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
        
    },
    showProductDetail: function (categoryId)
    {
        this.showStep(++this.currentStep);
        this.model.set('CategoryId', categoryId);
        this.viewProductDetail = new ProductDetailView({ el: "#divStep_2", productType: this.productType, selectedCategory: categoryId, model: this.model });
        this.viewProductDetail.on("detail-product-finished", this.showPictures, this);
        //TuilsApp.router.navigate("#Detalle");
    },
    showPictures: function()
    {
        this.showStep(++this.currentStep);
    },
    showStep: function ()
    {
        this.$("div[id^='btnPublishProductStep']").removeClass('selectedStep');
        this.$("#btnPublishProductStep"+this.currentStep).addClass('selectedStep');
        this.$("div[id^='divStep_']").hide();
        this.$("#divStep_"+this.currentStep).show();
    }
});