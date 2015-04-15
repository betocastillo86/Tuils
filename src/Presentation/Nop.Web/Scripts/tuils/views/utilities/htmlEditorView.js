define(['jquery', 'underscore', 'backbone', 'handlebars'], function ($, _, Backbone, Handlebars) {
    var HtmlEditorView = Backbone.View.extend({


        prefix: '',


        editor: undefined,

        divContainerHtml: undefined,

        initialize: function (args) {
            if (args.prefix)
                this.prefix = args.prefix;

            this.template = Handlebars.compile($("#templateHtmlEditor").html());
            this.divContainerHtml = this.$(".divContainerHtml");

            this.loadEditor();
            this.render();
        },
        render: function () {
            return this;
        },
        loadEditor: function () {
            var that = this;
            require(['wysihtml5'], function () {
                that.divContainerHtml.html(that.template());
                that.editor = new wysihtml5.Editor(that.prefix + "_textarea", {
                    toolbar: that.prefix + "_toolbar",
                    stylesheets: "stylesheet.css",
                    parserRules: wysihtml5ParserRules
                });
            });
        }
    });

    return HtmlEditorView;
});
