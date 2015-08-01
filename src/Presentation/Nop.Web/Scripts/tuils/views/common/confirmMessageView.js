define(['jquery', 'underscore', 'Backbone', 'resources'],
    function ($, _, Backbone, TuilsResources) {
        var ConfirmMessageView = Backbone.View.extend({

            tagName : 'div',

            message: undefined,

            autoclose : true,

            initialize: function (args) {
                if (args && args.autoclose)
                    this.autoclose = args.autoclose;

                this.render();
            },
            loadControls: function () {
                var that = this;
                this.$el.dialog({
                        autoOpen : false,
                        modal: true,
                        height: 150,
                        hide: {
                            effect: 'fade',
                            duration: 200
                        },
                        buttons: {
                            'Cerrar': function () {
                                $(this).dialog('close');
                            }
                        },
                        open: function (event, ui) {
                            if (that.autoclose)
                            {
                                setTimeout(function () {
                                    that.$el.dialog('close');
                                }, 1000);
                            }
                        }
                    });
            },
            show: function (args) {
                if (!this.$el.hasClass('ui-dialog-content'))
                {
                    this.loadControls();
                }

                var message = '';
                if (typeof (args) == 'string')
                    message = args;
                if (args.message)
                    message = args.message;

                this.$el.html(message)
                this.$el.dialog('open');
            },
            render: function () {
                //this.loadControls();
                return this;
            }
        });
        return ConfirmMessageView;
    });