var SelectCategoryView = Backbone.View.extend({

    events: {
        "click li": "loadCategories",
        "keyup input[type='text']": "filterCategories",
        "click .btnFinishSelection": "finishSelection"
    },

    //Tipos de productos posibles
    productType: undefined,

    //Nivel Actual de profundidad en el arbol
    currentLevel: 0,

    //Categoria que se está seleccionando
    currentCategory: 0,

    breadCrumbCategories : undefined,

    //Solo comienza a hacer filtro cuando se pone más de una letra
    minCharactersForFiltering: 1,

    //Solo permite filtrar cuando hay más de 5 categorias
    minChildrenCategoriesForFiltering: 5,

    divShowCategories: undefined,

    template: _.template($("#templateCategorySelector").html()),

    initialize: function (args) {

        this.productType = args.productType;
        this.loadControls();
        this.loadDefaultCategories();
        this.breadCrumbCategories = new Array();

        this.render();
    },
    render: function () {
        return this;
    },
    loadControls: function () {
        this.divShowCategories = this.$(".divShowCategories");
    },
    loadChildrenCategories: function (parentId) {
        var category = new CategoryModel();
        category.on("sync", this.showCategories, this);
        category.get(this.currentCategory);
    },
    loadDefaultCategories: function () {
        this.currentCategory = this.productType;
        this.loadChildrenCategories();
    },
    loadCategories: function (obj) {
        obj = $(obj.currentTarget);
        
        var categoryId = parseInt(obj.attr("tuils-id"));
        //Si la selección llega a ser del botón entonces no la tiene en cuenta
        if (!isNaN(categoryId)) {

            this.currentCategory = categoryId;
            var selectedLevel = parseInt(obj.attr("tuils-level"));
            this.currentLevel = ++selectedLevel;
            
            //actualiza la miga de pan
            this.breadCrumbCategories = _.first(this.breadCrumbCategories, this.currentLevel -1);
            this.breadCrumbCategories.push(obj.text());

            //Elimina las columnas de niveles inferiores
            this.divShowCategories.find("ul").slice(selectedLevel).remove();

            this.loadChildrenCategories(this.currentCategory);
            this.trigger("categories-middle-selected", categoryId);
        }

    },
    showCategories: function (category) {
        var obj = category.toJSON();
        obj['currentLevel'] = this.currentLevel;
        this.divShowCategories.append(this.template(obj));

        if (obj.ChildrenCategories.length > 0) {
            //Solo permite mostrar el buscador para más de 5 categorias
            if (obj.ChildrenCategories.length < this.minChildrenCategoriesForFiltering)
                this.divShowCategories.find("ul:last-child input[type='text']").hide();
        }

        this.trigger("categories-loaded");
    },
    filterCategories: function (obj) {

        var parentObj = $(obj.target.parentElement);
        obj = $(obj.target);

        var filterText = obj.val();

        if (filterText.length > this.minCharactersForFiltering) {
            parentObj.find("li").hide();
            parentObj.find("li[tuils-filter*='" + filterText.toLowerCase() + "']").show();
        }
        else {
            parentObj.find("li").show();
        }
    },
    finishSelection: function () {
        this.trigger("category-selected", this.currentCategory);
    }
});