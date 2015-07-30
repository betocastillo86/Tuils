define(['jquery', 'underscore', 'backbone', 'categoryModel', 'handlebars', 'configuration', 'baseView', 'handlebarsh'],
    function ($, _, Backbone, CategoryModel, Handlebars, TuilsConfiguration, BaseView) {
        
        var SelectCategoryView = BaseView.extend({

            events: {
                "click li": "loadCategories",
                "change select": "loadCategories",
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

            initialize: function (args) {
                this.template = Handlebars.compile($("#templateCategorySelector").html());
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
                category.getCategory(this.currentCategory);
                this.showLoading(category, true);
                
            },
            loadDefaultCategories: function () {
                this.currentCategory = this.productType;
                this.loadChildrenCategories();
            },
            loadCategories: function (obj) {
                obj = $(obj.currentTarget);
                var isSelect = obj.is('select');
                var categoryId = parseInt(isSelect ? obj.val() : obj.attr("tuils-id"));
                var selectedLevel = parseInt(obj.attr("tuils-level"));

                //Si la selección llega a ser del botón entonces no la tiene en cuenta
                if (!isNaN(categoryId)) {

                    if (!isSelect) {
                        obj.parent().find(".cat_select").removeClass("cat_select");
                        obj.addClass("cat_select");
                    }

                    this.currentCategory = categoryId;
                    this.currentLevel = ++selectedLevel;

                    //actualiza la miga de pan
                    this.breadCrumbCategories = _.first(this.breadCrumbCategories, this.currentLevel - 1);
                    this.breadCrumbCategories.push(obj.text());

                    //Elimina las columnas de niveles inferiores
                    this.divShowCategories.find(".category-column").slice(selectedLevel).remove();

                    this.loadChildrenCategories(this.currentCategory);
                    this.trigger("categories-middle-selected", categoryId);
                }
                else {
                    //Elimina las columnas de niveles inferiores
                    this.divShowCategories.find(".category-column").slice(selectedLevel+1).remove();
                }

            },
            showCategories: function (category) {
                var obj = category.toJSON();
                obj['currentLevel'] = this.currentLevel;
                

                if (obj.ChildrenCategories.length > 0) {
                    this.divShowCategories.append(this.template(obj));
                    //Solo permite mostrar el buscador para más de 5 categorias
                    if (!this.isMobile() && obj.ChildrenCategories.length < this.minChildrenCategoriesForFiltering)
                        this.divShowCategories.find("ul:last-child input[type='text']").hide();
                    if (this.isMobile())
                        this.scrollFocusObject(".category-column:last", -50);

                    this.trigger("categories-loaded");
                    this.$(".btn_continue").hide();
                }
                else {
                    this.$(".btn_continue").show();
                    this.scrollFocusObject(".btn_continue", -150);
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

