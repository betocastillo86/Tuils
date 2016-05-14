define(['jquery', 'backbone', 'popup'],
    function ($, Backbone) {
    var PopupView = Backbone.View.extend({

        tagName: 'div',

        events: {
            'click .btn_close': 'close'
        },

        message: undefined,

        autoclose: true,

        height: 150,

        afterClose: function () { },

        duration: 3000,

        pu: undefined,

        alertType : 'confirm',

        initialize: function (args) {
            if (args && args.autoclose)
                this.autoclose = args.autoclose;

            if (args && args.duration)
                this.duration = args.duration;

            this.render();
        },
        show: function (args) {
            var that = this;

            if (!args.alertType)
                this.alertType = args.alertType;

            var containerClass = undefined;
            //TODO:Cargar el estilo dependiendo del tipo de contenido
            if (this.alertType == 'window')
                containerClass = undefined;
            if (this.alertType == 'error')
                containerClass = undefined;

            var options = {
                containerClass: containerClass,
                modal: args.modal,
                closeContent: '<div class="popup_close"><span class="icon-close"></span></div>',
                afterClose: function () {
                    $('body').removeClass('body-noscroll');
                    if (args.afterClose)
                        args.afterClose.call(args.ctx ? args.ctx : that);
                }
            };

            this.message = '';
            if (typeof (args) == 'string')
                this.message = args;
            if (args.message)
                this.message = args.message;

            if (!this.pu)
                this.pu = new $.Popup(options);

            this.pu.open(this.message, 'html');

            if (args.scrolly)
                $('.popup_content').css('overflow-y', 'auto');

            //if (args.alertType)
            //    $('.popup_content').css('padding', '50px');

            if (args.hideCloseButton)
                $('.popup_cont .icon-close').hide();

            $('body').addClass('body-noscroll');

            this.relocateZindex();
            this.validateImages();
            this.pu.center();
        },
        //En los casos en los que hay más de un popup, reacomoda los zindex de los layers
        relocateZindex: function () {
            if ($('.popup_back').length > 1) {
                _.each($('.popup_back'), function (element, index) {
                    //El primero no lo toca
                    if (index) {
                        var $element = $(element);
                        var zindex = parseInt($element.css('z-index')) + (index * 2);
                        $element.css('z-index', zindex);
                    }
                });
                _.each($('.popup_cont'), function (element, index) {
                    if (index) {
                        var $element = $(element);
                        var zindex = parseInt($element.css('z-index')) + (index * 2);
                        $element.css('z-index', zindex);
                    }
                });
            }
        },
        validateImages: function () {
            // recalcular cuando todas las imagenes cargen.
            var images = this.pu.el.find('img').filter(function () { return !$(this).prop('complete'); });
            
            if (images.length) {
                images.on('load error', function () {
                    images.splice(images.index(this), 1);
                    if (images.length == 0) {
                        //that.pu.center();
                        console.log('Sin imagenes para cargar');
                    }
                });
            }
            else {
                this.pu.center();
            }
        },
        close: function () {
            this.pu.close();
        },
        render: function () {
            return this;
        }
    });
    return PopupView;
});
