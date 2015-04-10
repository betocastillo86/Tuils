define(['jquery', 'underscore', 'backbone'], function ($, _, Backbone) {
    var HtmlEditorView = Backbone.View.extend({



        prefix: '',

        template: _.template($("#templateHtmlEditor").html()),

        editor: undefined,

        divContainerHtml: undefined,

        initialize: function (args) {
            if (args.prefix)
                this.prefix = args.prefix;

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
