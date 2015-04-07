var HtmlEditorView = Backbone.View.extend({



    prefix: '',

    template : _.template($("#templateHtmlEditor").html()),

    editor: undefined,

    divContainerHtml : undefined,

    initialize: function (args)
    {
        if (args.prefix)
            this.prefix = args.prefix;

        this.divContainerHtml = this.$(".divContainerHtml");

        this.loadEditor();
        this.render();
    },
    render: function ()
    {
        return this;
    },
    loadEditor: function ()
    {
        this.divContainerHtml.html(this.template());
        this.editor = new wysihtml5.Editor(this.prefix + "_textarea", {
            toolbar: this.prefix + "_toolbar",
            stylesheets: "stylesheet.css",
            parserRules: wysihtml5ParserRules
        });
    }
});