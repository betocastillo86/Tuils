var PublishProductView = Backbone.View.extend({


    //Views
    selectCategoryView: undefined,

    //EndViews

    currentStep :1,

    model : undefined,

    productType : 0,

    initialize: function (args) {        
        this.productType = args.productType;
        this.model = new ProductModel();
        this.loadControls();
        this.loadCategories();
        this.render();
    },
    render: function ()
    {
        return this;
    },
    loadControls : function()
    {
        this.showStep();
    },
    loadCategories: function ()
    {
        this.selectCategoryView = new SelectCategoryView({ el: "#divStep1", productType: this.productType });
        this.selectCategoryView.on("category-selected", this.selectedCategory, this);
    },
    selectedCategory: function (categoryId)
    {
        this.model.set('CategoryId', categoryId);
    },
    showStep: function ()
    {
        this.$("div[id^='divStep_']").hide();
        this.$("div[id^='divStep_"+this.currentStep+"']").show();
    }
});