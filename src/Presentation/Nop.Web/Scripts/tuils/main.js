require.config({
    baseUrl: '/Scripts',
    waitSeconds : 15,
    shim: {
        //tagit: {
        //    deps :['jquery', 'jqueryui'],
        //    exports: 'tagit'
        //},
        jquery: {
            exports: ['jQuery', '$']
        },
        //backbone: {
        //    deps: ['underscore', 'jquery'],
        //    exports: 'Backbone'
        //},
        //jqueryvalidate: {
        //    deps:['jquery'],
        //    exports : 'jqueryvalidate'
        //},
        //jqueryui: {
        //    deps: ['jquery'],
        //    exports : 'jqueryui'
        //},
        validations: {
            deps: ['backbone'],
            exports: 'Backbone'
        },
        simpleMenu: {
            deps: ['modernizr', 'jquery'],
            exports: 'simpleMenu'
        },
        //mmenunav: {
        //    deps : ['mmenu']
        //}
        //stickit: {
        //    deps: ['backbone']
        //},
        //resize: {
        //    deps: ['canvasBlob']
        //},
        //mmenu: {
        //    deps: ['jquery'],
        //    exports: 'mmenu'
        //},
        //slide: {
        //    deps: ['jquery', 'jqueryui'],
        //    exports:'slide'
        //},
        //carousel: {
        //    deps: ['jquery', 'jqueryui'],
        //    exports : 'carousel'
        //},
        //jzoom: {
        //    deps: ['jquery', 'jqueryui'],
        //    exports : 'jzoom'
        //},
        //jpopup: {
        //    deps: ['jquery']
        //},
        //jtabs: {
        //    deps: ['jquery'],
        //    exports: 'jQuery'
        //}
        //'jtabs': ['jquery']
    },
    paths: {
        //Externas
        jquery: [
            'https://code.jquery.com/jquery-1.10.2.min',
            'jquery-1.10.2'
        ],
        jqueryui: [
            'http://code.jquery.com/ui/1.11.4/jquery-ui.min',
            'jquery-ui-1.10.3.custom.min'
        ],
        //jqueryunobtrusive: [
        //    'http://ajax.aspnetcdn.com/ajax/mvc/5.0/jquery.validate.unobtrusive.min',
        //    'jquery.validate.unobtrusive.min'
        //],
        //jqueryvalidate: [
        //    'http://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.13.1/jquery.validate.min',
        //    'jquery.validate.min'
        //],
        //underscore: [
        //    'http://cdnjs.cloudflare.com/ajax/libs/underscore.js/1.8.3/underscore-min',
        //    'underscore-min'
        //],
        //backbone: [
        //    'http://cdnjs.cloudflare.com/ajax/libs/backbone.js/1.2.1/backbone-min',
        //    'backbone'
        //],
        //stickit:[
        //    'https://cdnjs.cloudflare.com/ajax/libs/backbone.stickit/0.8.0/backbone.stickit.min',
        //    'backbone.stickit.min'
        //],
        //validations: [
        //    'http://cdnjs.cloudflare.com/ajax/libs/backbone.validation/0.11.5/backbone-validation-min',
        //    'backbone-validation.min'
        //],
        handlebars: [
            'https://cdnjs.cloudflare.com/ajax/libs/handlebars.js/3.0.3/handlebars.amd.min',
            'handlebars.min'
        ],

        //jquery: 'jquery-1.10.2',
        //jqueryui: 'jquery-ui-1.10.3.custom.min',
        jqueryunobtrusive: 'jquery.validate.unobtrusive.min',
        jqueryvalidate: 'jquery.validate.min',
        underscore: 'underscore-min',
        backbone : 'backbone-min',
        stickit: 'backbone.stickit.min',
        validations:'backbone-validation.min',
        //handlebars: 'handlebars.min',
        accounting: 'accounting.min',
        


        modernizr: 'modernizr.custom.min',
        simpleMenu: 'simpleMenu',

        handlebarsh: 'handelbars.helpers',
        maps: 'http://maps.google.com/maps/api/js?sensor=false',
        //Basic Tuils
        router: 'tuils/router',
        configuration: 'tuils/configuration',
        resources: 'tuils/resources',
        storage: 'tuils/storage',
        util: 'tuils/utilities',
        //Modelos
        _authenticationModel: 'tuils/models/_authentication',
        productModel: 'tuils/models/product',
        categoryModel: 'tuils/models/category',
        baseModel: 'tuils/models/baseModel',
        manufacturerModel: 'tuils/models/manufacturer',
        fileModel: 'tuils/models/file',
        specificationAttributeModel: 'tuils/models/specificationAttribute',
        //Colecciones
        productCollection: 'tuils/collections/products',
        manufacturerCollection: 'tuils/collections/manufacturers',
        categoryCollection: 'tuils/collections/categories',
        fileCollection: 'tuils/collections/files',
        specificationAttributeCollection: 'tuils/collections/specificationAttribute',
        //Vistas
        baseView: 'tuils/views/baseView',
        publishProductView: 'tuils/views/publishProduct/publishProduct',
        publishProductSelectCategoryView: 'tuils/views/publishProduct/selectCategory',
        publishProductProductDetailView: 'tuils/views/publishProduct/productDetail',
        publishProductSummaryView: 'tuils/views/publishProduct/summary',

        confirm : 'tuils/views/common/confirmMessageView',

        loginView: 'tuils/views/login/createUser',
        loginCreateUserView: 'tuils/views/login/createUser',


       
        imageSelectorView: 'tuils/views/utilities/imagesSelectorView',

        //NopCommerce
        ajaxCart: 'public.ajaxcart',
        nopCommon: 'public.common',

        //Tuils Extensions
        extensionNumbers: 'tuils/extensions/numbers',
        //Externas
        //mmenu: 'mmenu/js_umd/jquery.mmenu.umd',
        //mmenunav: 'mmenu/js_umd/addons/jquery.mmenu.navbars.umd',
        tagit: 'tag-it',
        resize: 'resize',
        canvasBlob: 'canvas-to-blob.min',
        carousel: 'owl.carousel.min',
        slide: 'responsiveslides.min',
        jzoom: 'jquery.jqzoom-core-pack',
        jpopup: 'jquery.magnific-popup',
        jtabs: 'jquery.easytabs'
        //dlmenu :'jquery.dlmenu'
    }

});

require(['tuils/app', 'jquery'], function (TuilsApp, $) {
    TuilsApp.init();
    window.jQuery = $;
    window.$ = $;
});