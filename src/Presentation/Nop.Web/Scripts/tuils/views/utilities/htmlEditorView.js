define(['jquery', 'underscore', 'backbone', 'handlebars', 'baseView'], function ($, _, Backbone, Handlebars, BaseView) {
    var HtmlEditorView = BaseView.extend({


        prefix: '',


        editor: undefined,

        divContainerHtml: undefined,

        initialize: function (args) {
            if (args.prefix)
                this.prefix = args.prefix;

            this.divContainerHtml = this.$(".divContainerHtml");

            if (this.isMobile())
            {
                this.template = Handlebars.compile($("#templateHtmlEditorMobile").html());
                this.divContainerHtml.html(this.template());
            }
            else
            {
                this.template = Handlebars.compile($("#templateHtmlEditor").html());
                this.loadEditor();
            }
                
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
