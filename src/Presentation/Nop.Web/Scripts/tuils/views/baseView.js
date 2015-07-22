define(['jquery', 'underscore', 'backbone', 'util', 'validations', 'stickit'],
    function ($, _, Backbone, TuilsUtil) {
    
    var BaseView = Backbone.View.extend({

        viewLogin: undefined,

        viewCreateUser : undefined,

        viewLogin: undefined,

        //Lista de vistas anidadas que requieren autenticación para realizar alguna acción
        //Todas las vistas que se encuentren en esta propiedad se les agregará el evento unautorized
        requiredViewsWithAuthentication : [],

        loadingTemplate: '<img id="divLoadingback" src="/Content/loading_2x.gif" />',

        minSizeDesktopWith: 1024,

        dialogBasicOptions: {
            modal: true,
            draggable: false,
            resizable: false
        },

        showLogin: function (model)
        {
            this.trigger('unauthorized');
        },
        validateAuthorization: function ()
        {
            this.model.once('unauthorized', this.showLogin, this);
        },
        userAuthenticated: function () {
            //Relanza el evento que el usuario fue autenticado, para que la vista que hereda lo pueda capturar
            this.trigger("user-authenticated");
        },
        stickThem: function () {
            this.stickit();
            this.basicValidations();
        },
        basicValidations : function()
        {
            //agrega las caracteristicas de tipos de datos a los combos
            this.$("input[tuils-val='int']").on("keypress", TuilsUtil.onlyNumbers);
            this.$("input[tuils-val='none']").on("keypress", function () { return false; });
        },
        showLoading: function(model, append)
        {
            model.once("sync", this.removeLoading, this);
            model.once("error", this.removeLoading, this);
            append ? this.$el.append(this.loadingTemplate) : this.$el.html(this.loadingTemplate);
        },
        handleResize : function(){
            var that = this;
            jQuery(window).resize(function () {
                // get browser width
                if (!that.isMinSize())
                    that.trigger("window-resized-max");
                else
                    that.trigger("window-resized-min");
            });
        },
        removeLoading : function(){
            this.$el.find("#divLoadingback").remove();
        },
        validateControls: function (model) {
            //Formatea los mensajes de respuesta contra los label
            
            this.removeErrors();

            if (!model)
                model = this.model;

            var errors = model.validate();

            //Si notiene bindings no valida los campos
            if (this.bindings) {
                var that = this;

                //invierte los bindings para obtener todos los campos y objetos del formulario
                var fieldsToMark = new Object();
                _.each(that.bindings, function (element, index) {
                    //Si es un objeto busca la propiedad en el campo observe
                    if (_.isObject(element)) {
                        fieldsToMark[element['observe']] = element['controlToMark'] ? element['controlToMark'] : index;
                    }
                    else {
                        fieldsToMark[element] = index;
                    }
                });

                _.each(errors, function (errorField, index) {
                    //recorre los errores y marca solo los que tienen objeto DOM
                    var domObj = that.$(fieldsToMark[index]);
                    if (domObj)
                        domObj.addClass("input-validation-error");
                    //busca el mensaje, si existe lo marca
                    var domMessage = that.$("span[tuils-val-for='" + index + "']");
                    if (domMessage)
                    {
                        domMessage.text(errorField);
                        domMessage.addClass("field-validation-error");
                    }
                        

                    
                        
                });
            }

            return errors;
        },
        bindValidation : function()
        {
            Backbone.Validation.bind(this);
        },
        //Con el fin de evitar muchos clicks inhabilita el boton unos segundos
        disableButtonForSeconds: function (obj, seconds) {
            seconds = !seconds ? 4 : seconds;
            obj.attr("disabled", 'disabled');
            setTimeout(function () {
                obj.removeAttr("disabled");
            }, seconds*1000);
        },
        removeErrors: function () {
            this.$el.find(".input-validation-error").removeClass("input-validation-error");
            this.$el.find(".field-validation-error").text("").removeClass("input-validation-error");
        },
        isMobile: function ()
        {
            var check = false;
            (function (a) { if (/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino/i.test(a) || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(a.substr(0, 4))) check = true })(navigator.userAgent || navigator.vendor || window.opera);
            return check;
        },
        isMinSize: function () {
            var currentWidth = window.innerWidth || document.documentElement.clientWidth;
            return currentWidth <= this.minSizeDesktopWith;
        }
    });

    return BaseView;
});




