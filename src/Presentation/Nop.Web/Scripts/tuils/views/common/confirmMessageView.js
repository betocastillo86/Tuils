define(['jquery', 'underscore', 'backbone', 'resources'],
    function ($, _, Backbone, TuilsResources) {
        var ConfirmMessageView = Backbone.View.extend({

            tagName : 'div',

            message: undefined,

            autoclose: true,

            afterClose: function () { },

            duration:3000,

            initialize: function (args) {
                if (args && args.autoclose)
                    this.autoclose = args.autoclose;

                if (args && args.duration)
                    this.duration = args.duration;

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
                            'Aceptar': function () {
                                $(this).dialog('close');
                                that.afterClose();
                            }
                        },
                        open: function (event, ui) {
                            if (that.autoclose)
                            {
                                setTimeout(function () {
                                    that.$el.dialog('close');
                                    that.afterClose();
                                }, that.duration);
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
                if (args.duration)
                    this.duration = args.duration;

                if (args && args.afterClose)
                    this.afterClose = args.afterClose;

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