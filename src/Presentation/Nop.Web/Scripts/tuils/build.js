﻿({
    baseUrl: '../../Scripts',
    mainConfigFile: 'main.js',
    out: 'built.js',
    name: 'tuils/main',
    stubModules: ['text'],
    optimizeAllPluginResources: false,
    findNestedDependencies: true,
    //optimize: "none",
    paths: {
        //No agrega al bundle
        maps: 'empty:',
        configuration: 'empty:',
        resources : 'empty:',
       //, backbone: 'empty:',
       // underscore: 'empty:',
       // stickit: 'empty:',
       // validations: 'empty:',
        handlebars: 'empty:',
        jquery: 'empty:',
        jqueryui: 'empty:',
        'jquery.weekline.min': 'empty:',
        'draggable_background' : 'empty:'
       // jqueryunobtrusive: 'empty:',
       // jqueryvalidate: 'empty:',
       // jquerymigrate: 'empty:'
    }
});