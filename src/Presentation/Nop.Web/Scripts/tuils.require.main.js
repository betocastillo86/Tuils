require.config({
    baseUrl: '/Scripts',
    shim: {
        tagit: {
            deps: ['jquery', 'jqueryui'],
            exports:'tagit'
        },
        backbone: {
            deps: ['underscore', 'jquery'],
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
        jquery: 'jquery-1.10.2',
        underscore: 'underscore',
        backbone: 'backbone',
        stickit: 'backbone.stickit',
        jqueryui: 'jquery-ui-1.10.3.custom',
        validations : 'backbone-validation.min',
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
        //Colecciones
        productCollection: 'tuils/collections/products',
        manufacturerCollection: 'tuils/collections/manufacturers',
        fileCollection: 'tuils/collections/files',
        //Vistas
        baseView: 'tuils/views/baseView',
        publishProductView: 'tuils/views/publishProduct/publishProduct',
        publishProductSelectCategoryView: 'tuils/views/publishProduct/selectCategory',
        publishProductProductDetailView: 'tuils/views/publishProduct/productDetail',
        publishProductSummaryView: 'tuils/views/publishProduct/summary',
        htmlEditorView: 'tuils/views/utilities/htmlEditorView',
        imageSelectorView: 'tuils/views/utilities/imagesSelectorView',
        
        //Externas
        tagit: 'tag-it',
        wysihtml5: 'wysihtml5-0.3.0.min',
        resize: 'resize',
        canvasBlob: 'canvas-to-blob.min'
    }
    
});

require(['tuils/app'], function (TuilsApp) {
    TuilsApp.init();
});