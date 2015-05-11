define(['underscore', 'backbone', 'categoryModel', 'handlebars', 'configuration', 'baseView', 'handlebarsh'],
    function ( _, Backbone, CategoryModel, Handlebars, TuilsConfiguration, BaseView) {
        
        var SelectCategoryView = BaseView.extend({

            events: {
                "click li": "loadCategories",
                "keyup input[type='text']": "filterCategories",
                "click .btn_continue": "finishSelection"
            },

            //Tipos de productos posibles
            productType: undefined,

            //Nivel Actual de profundidad en el arbol
            currentLevel: 0,

            //Categoria que se está seleccionando
            currentCategory: 0,

            breadCrumbCategories: undefined,

            //Solo comienza a hacer filtro cuando se pone más de una letra
            minCharactersForFiltering: 1,

            //Solo permite filtrar cuando hay más de 5 categorias
            minChildrenCategoriesForFiltering: 5,

            divShowCategories: undefined,

            template: Handlebars.compile($("#templateCategorySelector").html()),

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

                var button = this.$(".nav-category li");
                if(this.productType == TuilsConfiguration.productBaseTypes.product)
                    button.addClass("hub-std");
                else if(this.productType == TuilsConfiguration.productBaseTypes.bike)
                    button.addClass("hub-mot");
                else
                    button.addClass("hub-srv");

            },
            loadChildrenCategories: function (parentId) {
                var category = new CategoryModel();
                category.on("sync", this.showCategories, this);
                category.getCategory(this.currentCategory);
                this.showLoading(category, true);
                
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

                    obj.parent().find(".cat_select").removeClass("cat_select");
                    obj.addClass("cat_select");
                    

                    this.currentCategory = categoryId;
                    var selectedLevel = parseInt(obj.attr("tuils-level"));
                    this.currentLevel = ++selectedLevel;

                    //actualiza la miga de pan
                    this.breadCrumbCategories = _.first(this.breadCrumbCategories, this.currentLevel - 1);
                    this.breadCrumbCategories.push(obj.text());

                    //Elimina las columnas de niveles inferiores
                    this.divShowCategories.find(".category-column").slice(selectedLevel).remove();

                    this.loadChildrenCategories(this.currentCategory);
                    this.trigger("categories-middle-selected", categoryId);
                }

            },
            showCategories: function (category) {
                var obj = category.toJSON();
                obj['currentLevel'] = this.currentLevel;
                

                if (obj.ChildrenCategories.length > 0) {
                    this.divShowCategories.append(this.template(obj));
                    //Solo permite mostrar el buscador para más de 5 categorias
                    if (obj.ChildrenCategories.length < this.minChildrenCategoriesForFiltering)
                        this.divShowCategories.find("ul:last-child input[type='text']").hide();
                    this.trigger("categories-loaded");
                    this.$(".btn_continue").hide();
                }
                else {
                    this.$(".btn_continue").show();
                }

                
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


        return SelectCategoryView;


    });

