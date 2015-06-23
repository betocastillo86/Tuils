({
    baseUrl: '../../Scripts',
    mainConfigFile: 'main.js',
    out: 'built.js',
    name: 'tuils/main',
    stubModules: ['text'],
    optimizeAllPluginResources: false,
    findNestedDependencies: false,
    //optimize: "none",
    paths: {
        //No agrega al bundle
        maps: 'empty:',
        backbone: 'empty:',
        underscore: 'empty:',
        stickit: 'empty:',
        validations: 'empty:',
        handlebars: 'empty:',
        accounting: 'empty:',
        jquery: 'empty:',
        jqueryui: 'empty:',
        jqueryunobtrusive: 'empty:',
        jqueryvalidate: 'empty:',
        jquerymigrate: 'empty:'
    }
});