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
                'click .icon-foto': 'showCoverOptions'
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
                if (this.allowEdit && !this.isMobile())
                    this.loadImageCover();
                    
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
            showCoverOptions: function () {
                var display = $('.cover-editor .menu-cover').css('display') == 'none' ? 'block' : 'none';
                this.$('.cover-editor .menu-cover').css('display', display);
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
                                this.showLoadingBack(this.model, this.$('.img_perfil'));
                                this.model.saveLogo(file);
                            }
                                
                        }
                        else {
                            alert("La extensión del archivo no es valida");
                        }
                        
                    }
                    else
                    {
                        alert("El tamaño excede el limite");
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
                
                //Activa las propiedades para poder mover el fondo
                this.$('#divMainCover').css('overflow', 'hidden').css('height', '300px').css("cursor", "move");
                this.$('#btnMoveCover').hide();
                this.$('#btnChangeCover').hide();
                this.$('#btnEditVendorHeader').hide();
                this.$('#btnSaveCoverPosition').show();

                var cover = this.$('.coverPerfil');
                cover.css('position', 'relative');

                var that  = this;
                $('.coverPerfil').draggable({
                    axis: 'y',
                    stop: function (event, ui) {
                        //Quita el top para reubicar la imagen y mueve el fondo en un porcentaje
                        var perMovement = ((ui.originalPosition.top - ui.position.top) / that.heightBackground) * 100;
                        that.initPositionBackground += perMovement;
                        cover.css("background-position", 'center ' + that.initPositionBackground + '%');
                        cover.css("top", '');
                    }

                });
            },
            restartCoverPosition : function()
            {
                this.initPositionBackground = 0;
                this.$('.coverPerfil').css("background-position", 'center 0%');
            },
            saveCoverPosition : function()
            {
                this.model.set('BackgroundPosition', parseInt(this.initPositionBackground));
                this.model.saveCoverPosition();
                this.$('.coverPerfil').draggable('destroy');
                this.$('#btnMoveCover').show();
                this.$('#btnChangeCover').show();
                this.$('#btnEditVendorHeader').show();
                
                this.$('#btnSaveCoverPosition').hide();
                this.$('#divMainCover').css('data-pos', this.initPositionBackground);
                this.$('#divMainCover').css('overflow', '').css('height', '').css("cursor", "");
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