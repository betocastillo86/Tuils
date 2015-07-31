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

            //Cuando está en true significa que debe seleccionar en background el arbol de categorías
            //que se encuentran en cache
            autoSelectCategories : false,

            breadCrumbCategories: undefined,

            //Secuencia de categorias seleccionadas
            arrayCategories: [],

            //Variable temporal que contiene las categorias que se seleccionaron en un producto que se desea retomar
            //Este array se desocupará en la medida que automáticamente carguen las categorias escogídas
            _tempPreviousCategories : undefined,

            //Solo comienza a hacer filtro cuando se pone más de una letra
            minCharactersForFiltering: 1,

            //Solo permite filtrar cuando hay más de 5 categorias
            minChildrenCategoriesForFiltering: 5,

            divShowCategories: undefined,

            initialize: function (args) {
                this.template = Handlebars.compile($("#templateCategorySelector").html());
                this.productType = args.productType;
                this.autoSelectCategories = args.autoSelectCategories;
                this.breadCrumbCategories = new Array();
                this.arrayCategories = new Array();
                
                this.model = args.model;
                this.loadControls();
                this.render();
            },
            render: function () {
                return this;
            },
            loadControls: function () {

                if (this.autoSelectCategories) {
                    //Toma el valor original de las categorias e intenta para realizar las cargas automáticas
                    this._tempPreviousCategories = this.model.get('_arrayCategories');
                }

                this.loadDefaultCategories();
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
                    //Empuja al array la secuencia de categorías seleccionadas
                    this.arrayCategories = _.first(this.arrayCategories, this.currentLevel - 1);
                    this.arrayCategories.push(categoryId);
                    this.model.set('_arrayCategories', this.arrayCategories);

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
            autoselectCategory: function () {
                //Si se está autoseleccionando y todavía hay categorias pendientes de autoseleccionar 
                //realiza un click automático sobre la categoría seleccionada
                if (this.autoSelectCategories && this._tempPreviousCategories.length > 0)
                {
                    var newCategory = this._tempPreviousCategories[0];
                    if (this.isMobile())
                        this.$('select[tuils-level="' + this.currentLevel + '"]').val(newCategory).change();
                    else
                        this.$('li[tuils-id="' + newCategory + '"]').click();
                    //Elimina la posición ya fue cargada
                    this._tempPreviousCategories.splice(0, 1);
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
                    if (!this.autoSelectCategories && this.isMobile())
                        this.scrollFocusObject(".category-column:last", -50);

                    //Intenta autoseleccionar categorias si es necesario
                    this.autoselectCategory();

                    this.trigger("categories-loaded");
                    this.$(".btn_continue").hide();
                }
                else {
                    this.$(".btn_continue").show();
                    if (!this.autoSelectCategories)
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
                this.model.set('CategoryId', this.currentCategory);
                this.trigger("category-selected", this.model);
            }
        });


        return SelectCategoryView;


    });

