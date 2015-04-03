var HtmlEditorView = Backbone.View.extend({

    prefix: '',

    editor : undefined,

    initialize: function (args)
    {
        if (args.prefix)
            this.prefix = args.prefix;

        this.loadEditor();
    },
    render: function ()
    {
        return this;
    },
    loadEditor: function ()
    {
        
        this.editor = new wysihtml5.Editor(this.prefix + "_textarea", {
            toolbar: this.prefix + "_toolbar",
            stylesheets: "stylesheet.css",
            parserRules: wysihtml5ParserRules
        });
    }
});