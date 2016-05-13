define(['jquery', 'underscore', 'baseView', 'tuils/models/vendor/vendorHeader', 'util', 'configuration'],
    function ($, _, BaseView, VendorHeaderModel, TuilsUtilities, TuilsConfiguration) {

        var VendorHeaderView = BaseView.extend({

            events: {
                "click #btnEditVendorHeader": "edit",
                "click #btnChangeCover": "changeBackgroundPicture",
                "click .img_perfil .cover-editor": "changePicture",
                "change input[type='file']": "saveImage",
                "click #btnMoveCover": "enableMoveCover",
                "click #btnSaveCoverPosition": "saveCoverPosition",
                'click #divMainCover .cover-editor': 'showCoverOptions'
            },
            //No hace binding real pero sirve para marcar los errores
            bindings: {
                '#Name': 'Name',
                '#Description' : 'Description'
            },

            id: 0,

            allowEdit : false,

            isEditing: false,

            imageType : undefined,

            btnEdit : undefined,

            fileUpload : undefined,
            
            initPositionBackground :0,

            heightBackground :0,

            initialize: function (args) {
                
                this.id = args.id;

                this.model = new VendorHeaderModel({ Id : args.id });
                this.model.on("sync", this.saved, this);
                this.model.on("file-saved", this.imageSaved, this);
                this.model.on("file-error", this.imageError, this);

                this.allowEdit = args.allowEdit;
                this.loadControls();
                this.render();
            },
            render : function()
            {
                this.bindValidation();
                return this;
            },
            loadControls : function()
            {
                this.btnEdit = this.$("#btnEditVendorHeader");
                this.fileUpload = this.$("input[type='file']");
                if (this.allowEdit  && !this.isMobile())
                {
                    this.loadImageCover();
                    require(['draggable_background']);
                }
                    
            },
            loadImageCover : function()
            {
                //es necesario cargar la imagen original para obtener el tamaño real del fondo y poder realizar los calculos en porcentaje
                this.$('#btnMoveCover').show();
                var url = this.$("#divMainCover").attr("data-path");
                var that = this;
                var img = new Image();
                img.src = url;
                img.onload = function () {
                    that.heightBackground = this.height;
                    img = $(this);
                    img.hide();
                };
                $("#divMainCover").prepend(img);

                this.initPositionBackground = parseFloat(this.$("#divMainCover").attr("data-pos"));

            },
            edit: function ()
            {
                if (!this.isEditing) {
                    this.switchEnabled(true);
                }
                else {
                    this.save();
                }

                this.isEditing = !this.isEditing;
            },
            save : function()
            {
                this.model.set(
                    { 
                        Id : this.id,
                        Name: this.$("#Name").val(),
                        Description: this.$("#Description").val(),
                        EnableShipping: this.$("#EnableShipping").is(":checked"),
                        EnableCreditCardPayment: this.$("#EnableCreditCardPayment").is(":checked")
                    });
                this.validateControls();
                if (this.model.isValid())
                    this.model.saveHeader();
            },
            saved : function()
            {
                this.$('.tit-perfil h2').html(this.model.get('Name'));
                this.$('.pr-perfil p').html(this.model.get('Description'));
                this.$('#liShipping').css('display', this.model.get('EnableShipping') ? 'block' : 'none');
                this.$('#liCreditCard').css('display', this.model.get('EnableCreditCardPayment') ? 'block' : 'none');
                this.switchEnabled(false);
            },
            changeBackgroundPicture: function ()
            {
                this.imageType = 'back';
                this.fileUpload.click();
            },
            changePicture: function () {
                this.imageType = 'main';
                this.fileUpload.click();
            },
            showCoverOptions: function (e) {

                if (e && e.target.id == 'btnMoveCover')
                    return;

                //Si es desde un dispositivo movil abre automáticamente la carga de archivos
                if (!this.isMobile())
                {
                    var display = $('.cover-editor .menu-cover').css('display') == 'none' ? 'block' : 'none';
                    this.$('.cover-editor .menu-cover').css('display', display);
                }
                else
                    this.changeBackgroundPicture();

                
            },
            saveImage : function (obj)
            {
                var file = obj.target.files[0];
                if (file )
                {
                    if(TuilsUtilities.isValidSize(obj.target))
                    {
                        if (TuilsUtilities.isValidExtension(obj.target, 'image')) {
                            if (this.imageType == 'back')
                            {
                                var that = this;
                                this.validateImageSize(file, TuilsConfiguration.vendor.minWidthCover, TuilsConfiguration.vendor.minHeightCover, function () {
                                    that.showLoadingBack(that.model.fileModel, that.$('.coverPerfil'));
                                    that.model.saveBackground(file);
                                },
                                function (model) {
                                    this.alert(model.message);
                                }, this);
                               // this.model.saveBackground(file);
                            }
                            else if (this.imageType == 'main')
                            {
                                this.$('.img_perfil img').attr('src', '');
                                this.showLoadingBack(this.model.fileModel, this.$('.img_perfil'));
                                this.model.saveLogo(file);
                            }
                                
                        }
                        else {
                            this.alertError("La extensión del archivo no es valida");
                        }
                        
                    }
                    else
                    {
                        this.alertError("El tamaño excede el limite máximo de peso");
                    }
                }
            },
            imageSaved : function(image)
            {
                if (this.imageType == 'main')
                    this.$(".img_perfil img").attr("src", image.toJSON().src);
                else
                {
                    this.restartCoverPosition();
                    this.$(".coverPerfil").css("background-image", "url(" + image.toJSON().src + ")");
                }
            },
            enableMoveCover: function () {

                this.$('#divMainCover').css("cursor", "move");
                //Se agrega dinamicamente el estilo para mostrar y ocultar los controles para mover el fondo
                $('<style type="text/css" id="styleMoveCover">.out_move_cover{ display: none; } .in_move_cover{ display:block !important; } </style>').appendTo('head');
                
                //Calcula cuanto debe mover el fondo con relación a la resoliución
                var variableHeight = screen.height * 0.078;
                var coverPerfil = this.$('.coverPerfil');
                var heightCover = parseInt(coverPerfil.height());
                var imageHeightCalc = this.heightBackground - heightCover + variableHeight;
                //Calcula a cuantos pixeles corresponde el procentaje asignado 
                coverPerfil.css('background-position-y', '-' + (parseInt(coverPerfil.css('background-position-y')) / 100) * imageHeightCalc + 'px');

                var that = this;
                this.$('.coverPerfil').backgroundDraggable(
                    {
                        axis: 'y',
                        done: function () {
                            //Calcula cuanto porcentaje de la imagen se movió con relación a los pixeles 
                            var perMovement = (parseInt(coverPerfil.css('background-position-y')) / imageHeightCalc) * 100;
                            that.initPositionBackground = perMovement *-1;
                        }
                    }
                );
            },
            restartCoverPosition : function()
            {
                this.initPositionBackground = 0;
                this.$('.coverPerfil').css("background-position", 'center 0%');
            },
            saveCoverPosition : function()
            {
                //Guarda con un nuevo modelo ya que se debe sincronizar diferente que el modelo general
                var modelCover = new VendorHeaderModel({ Id: this.model.get('Id'), BackgroundPosition: parseInt(this.initPositionBackground) });
                modelCover.on('sync', this.coverPositionSaved, this);
                modelCover.saveCoverPosition();
            },
            coverPositionSaved: function () {
                //Remueve los estilos relacionados con editar el header
                $('#styleMoveCover').remove();
                this.$('#divMainCover').css('data-pos', this.initPositionBackground).css("cursor", '');
                this.showCoverOptions();
            },
            switchEnabled: function (editing)
            {
                this.btnEdit.html(this.btnEdit.attr(!editing ? "tuils-textedit" : "tuils-textsave"));
                this.$("[tuils-for='show']").css("display", editing ? "none" : "block");
                this.$("[tuils-for='edit']").css("display", !editing ? "none" : "block");
            }

        });

        return VendorHeaderView;
    });