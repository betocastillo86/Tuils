define(['jquery', 'underscore', 'baseView', 'productModel', 'storage',
    'configuration', 'publishProductSelectCategoryView', 'publishProductProductDetailView',
    'publishProductFinishedView', 'publishProductSummaryView', 'tuils/views/publishProduct/previousProductCreatedView', 'imageSelectorView'],
    function ($, _, BaseView, ProductModel, TuilsStorage,
        TuilsConfiguration, SelectCategoryView, ProductDetailView,
        PublishFinishedView, SummaryView, PreviousProductCreatedView, ImagesSelectorView) {

        var PublishProductView = BaseView.extend({

            events: {
                'click .wizard-breadcrumb li': 'showStepByBreadcrumb'
            },
            //Views
            viewPreviousProductCreated: undefined,
            viewSelectCategory: undefined,
            viewProductDetail: undefined,
            viewImageSelector: undefined,
            viewSummary: undefined,
            viewPublishFinished: undefined,
            //EndViews

            currentStep: 1,

            model: undefined,

            images: undefined,

            productType: 0,

            productTypeName: '',

            //Numero del paso que ya fue terminado
            //Sirve para validar los pasos cuando se hacen por medio de las pestañas
            stepFinished: 0,

            initialize: function (args) {
                this.productType = args.productType;
                if (this.productType == TuilsConfiguration.productBaseTypes.product)
                    this.productTypeName = 'producto';
                else if (this.productType == TuilsConfiguration.productBaseTypes.service)
                    this.productTypeName = 'servicio-especializado';
                else
                    this.productTypeName = 'moto';

                this.loadControls();

                this.render();
            },
            render: function () {
                return this;
            },
            loadControls: function () {
                this.model = new ProductModel({ 'ProductTypeId': this.productType });
                this.on("user-authenticated", this.save, this);
                this.model.on('sync', this.productSaved, this);
                this.model.on('error', this.errorOnSaving, this);

                if (this.hasPreviousProductStorage()) {
                    this.showPreviousProductCreated();
                }
                else {
                    this.showStep();
                    this.showCategories();
                }

                if (this.isMobile())
                    $('.btn_anuncia').hide();
            },
            showPreviousProductCreated: function () {
                this.viewPreviousProductCreated = new PreviousProductCreatedView({ el: '#divStep_0', model: this.model });
                this.viewPreviousProductCreated.on('previousProduct-createNew', this.loadControls, this);
                this.viewPreviousProductCreated.on('previousProduct-recover', this.recoverProduct, this);
                this.currentStep = 0;
                this.showStep();
            },
            showCategories: function (autoSelectCategories) {
                var that = this;
                that.viewSelectCategory = new SelectCategoryView({ el: "#divStep_1", productType: that.productType, model: this.model, autoSelectCategories: autoSelectCategories });
                that.viewSelectCategory.on("category-selected", that.showProductDetail, that);
                //that.viewSelectCategory.once("categories-loaded", TuilsStorage.loadBikeReferences);
                that.viewSelectCategory.on("categories-middle-selected", that.restartNextStep, that);
            },
            showProductDetail: function (model) {
                this.showNextStep();
                //Solo toma el modelo si viene valor, sino es el mismo de la vista general
                if (model)
                    this.model = model;

                if (!this.viewProductDetail) {

                    var that = this;
                    that.viewProductDetail = new ProductDetailView({ el: "#divStep_2", productType: that.productType, selectedCategory: this.model.get('CategoryId'), model: that.model });
                    that.viewProductDetail.on("detail-product-finished", that.showPictures, that);
                    that.viewProductDetail.on("detail-product-back", that.showStepBack, that);
                }
            },
            recoverProduct: function () {
                this.currentStep = 1;
                //Envia parametro en el que debe autoseleccionar las categorias previamente escogidas en el modelo
                this.showCategories(true);
                this.showProductDetail();
            },
            //Valia si hay un producto almacenado valido para esta sección
            //Solo es valido si es del mismo tipo. Es decir si el usuario está entrando en 
            //publicar un servicio el producto en el storage debe ser de tipo servicio
            //de lo contrario es eliminado
            hasPreviousProductStorage: function () {
                var publishedProduct = TuilsStorage.getPublishProduct();
                if (publishedProduct && publishedProduct.ProductTypeId == this.productType) {
                    ////Si hay datos pasa automáticamente al paso 2
                    this.model.set(publishedProduct);
                    //this.showProductDetail(publishedProduct.CategoryId);
                    return true;
                }
                else {
                    this.currentStep = 1;
                    TuilsStorage.setPublishProduct(undefined);
                    return false;
                }

            },
            showPictures: function (model) {
                this.model = model;
                this.showNextStep();
                var that = this;

                if (this.productType != TuilsConfiguration.productBaseTypes.service) {
                    if (!this.viewImageSelector) {
                        //Si es de tipo motocicleta debe cargar 4 imagenes
                        var minFilesUploaded = this.productType == TuilsConfiguration.productBaseTypes.bike ? 4 : 1;
                        that.viewImageSelector = new ImagesSelectorView({ el: "#divStep_3", model: that.model, minFilesUploaded: minFilesUploaded });
                        that.viewImageSelector.on("images-save", that.showSummary, that);
                        that.viewImageSelector.on("images-back", that.showStepBack, that);
                    }
                }
                else {
                    this.showSummary();
                }

            },
            showSummary: function (images) {


                this.images = images;
                this.model.set('TempFiles', _.pluck(this.images ? this.images.toJSON() : undefined, 'guid'));
                this.showNextStep();

                if (!this.viewSummary) {

                    var that = this;
                    that.viewSummary = new SummaryView({ el: "#divStep_4", product: that.model, images: that.images, productType: that.productType });
                    that.viewSummary.on("summary-back", that.showStepBack, that);
                    that.viewSummary.on("summary-save", that.save, that);
                }
                else {
                    this.viewSummary.loadControls({ product: this.model, images: this.images, breadCrumb: this.viewSelectCategory.breadCrumbCategories });
                }

            },
            showStep: function () {

                if (this.currentStep < 5) {
                    this.$(".wizard-current").removeClass('wizard-current').addClass("wizard-step");
                    this.$("#btnPublishProductStep" + this.currentStep).removeClass("wizard-step").addClass('wizard-current');
                    this.$("div[id^='divStep_']").hide();
                    this.scrollFocusObject('.wizard-breadcrumb', -50);

                    //Actualiza el producto en el storage y realiza la navegación
                    if (this.currentStep > 1)
                        this.model.on('change', TuilsStorage.setPublishProduct);
                    if (this.currentStep)
                        Backbone.history.navigate('quiero-vender/' + this.productTypeName + '/' + this.currentStep);
                }
                //Si le dan atras en el navegador y no tiene registrado el paso, debe redireccionar al paso principal
                if (this.currentStep !== null)
                    this.$("#divStep_" + this.currentStep).show();
                else
                    document.location.href = '/quiero-vender';
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
            showStepByBreadcrumb: function (obj) {
                var step = parseInt($(obj.currentTarget).attr("data-id"));

                //no puede haber pasos superiores al ultimo finalizado
                if (step <= this.stepFinished) {
                    //hasta que alcance el paso actual   desea  empieza a disminuir o aumentar los pasos
                    while (this.currentStep != step) {
                        if (step > this.currentStep)
                        {
                            //if (step != 4)
                                this.showNextStep();
                            //else
                            ///    this.showSummary();
                        }
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
                else if (this.currentStep == 4 && this.viewSummary) {
                    this.viewSummary.undelegateEvents();
                    this.viewSummary = undefined;
                }
                //Cuando se reinicia el paso siguiente también se reinicia el paso finalizado
                this.stepFinished = this.currentStep;
            },
            errorOnSaving: function (model, response) {
                if (response.responseJSON.ModelState &&
                    (response.responseJSON.ModelState.ErrorCode == TuilsConfiguration.errorCodes.publishInvalidCategory
                        || response.responseJSON.ModelState.ErrorCode == TuilsConfiguration.errorCodes.hasReachedLimitOfProducts))
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
                if (this.viewImageSelector) this.viewImageSelector.remove();
                this.viewProductDetail.remove();
                this.viewSummary.remove();
                this.showFinish();
                this.trackGAEvent('Publicacion', 'Exitosa');
                Backbone.history.navigate("quiero-vender/publicacion-exitosa");
                TuilsStorage.setPublishProduct(undefined);
            },
            save: function () {
                this.validateAuthorization();
                this.model.publish();
            }
        });

        return PublishProductView;
    });

