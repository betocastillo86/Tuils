require.config({
    baseUrl: '/Scripts',
    shim: {
        tagit: {
            //deps: ['jquery', 'jqueryui'],
            exports:'tagit'
        },
        backbone: {
            //deps: ['underscore', 'jquery'],
            deps: ['underscore'],
            exports:'Backbone'
        },
        validations: {
            deps: ['backbone']
        },
        stickit: {
            deps: ['backbone']
        },
        resize: {
            deps: ['canvasBlob']
        }
    },
    paths: {
        //Externas
        //jquery: 'jquery-1.10.2',
        underscore: 'underscore-min',
        backbone: 'backbone-min',
        stickit: 'backbone.stickit.min',
        jqueryui: 'jquery-ui-1.10.3.custom.min',
        validations: 'backbone-validation.min',
        handlebars: 'handlebars.min',
        handlebarsh: 'handelbars.helpers',
        maps : 'http://maps.google.com/maps/api/js?sensor=false',
        //Basic Tuils
        router: 'tuils/router',
        configuration: 'tuils/configuration',
        storage: 'tuils/storage',
        util: 'tuils/utilities',
        //Modelos
        _authenticationModel: 'tuils/models/_authentication',
        productModel: 'tuils/models/product',
        categoryModel: 'tuils/models/category',
        manufacturerModel: 'tuils/models/manufacturer',
        fileModel: 'tuils/models/file',
        specificationAttributeModel: 'tuils/models/specificationAttribute',
        //Colecciones
        productCollection: 'tuils/collections/products',
        manufacturerCollection: 'tuils/collections/manufacturers',
        categoryCollection : 'tuils/collections/categories',
        fileCollection: 'tuils/collections/files',
        specificationAttributeCollection : 'tuils/collections/specificationAttribute',
        //Vistas
        baseView: 'tuils/views/baseView',
        publishProductView: 'tuils/views/publishProduct/publishProduct',
        publishProductSelectCategoryView: 'tuils/views/publishProduct/selectCategory',
        publishProductProductDetailView: 'tuils/views/publishProduct/productDetail',
        publishProductSummaryView: 'tuils/views/publishProduct/summary',
        publishProductFinishedView: 'tuils/views/publishProduct/finish',

        loginView: 'tuils/views/login/createUser',
        loginCreateUserView: 'tuils/views/login/createUser',
        

        htmlEditorView: 'tuils/views/utilities/htmlEditorView',
        imageSelectorView: 'tuils/views/utilities/imagesSelectorView',
        
        //Tuils Extensions
        extensionNumbers : 'tuils/extensions/numbers',
        //Externas
        tagit: 'tag-it.min',
        wysihtml5: 'wysihtml5-0.3.0.min',
        resize: 'resize',
        canvasBlob: 'canvas-to-blob.min',
        accounting: 'accounting.min',
        imagedrag: 'jquery.imagedrag'
    }
    
});

require(['tuils/app'], function (TuilsApp) {
    TuilsApp.init();
});