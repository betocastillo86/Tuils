﻿define(['jquery', 'underscore', 'baseView', 'fileModel', 'fileCollection', 'configuration', 'util', 'resize'],
    function ($, _, BaseView, FileModel, FileCollection, TuilsConfiguration, TuilsUtilities) {
    var ImagesSelectorView = BaseView.extend({
        events: {
            "click .addImageGalery": "addImage",
            "click .icon-delete": "removeImage",
            "change input[type=file]": "uploadImage",
            "click .btnNext": "save",
            "click .btnBack": "back"
        },
        //controls
        fileUpload: undefined,
        currentControlImage: -1,
        //controls
        attributeFile: "tuils-file",
        attributeFileUsed: "tuils-used",

        minFilesUploaded : 1,

        collection: undefined,

        urlSave : undefined,

        resizer: undefined,

        onBackRemoveImages: false,

        initialize: function (args) {
            
            this.loadControls();

            if (args.onBackRemoveImages)
                this.onBackRemoveImages = args.onBackRemoveImages;

            //Cuando viene modelo valida si ya se han cargado previamente imagenes
            if (args.model)
            {
                this.model = args.model;
                this.loadPreviousImages();
            }

            if (args.minFilesUploaded)
                this.minFilesUploaded = args.minFilesUploaded;
                
            if (args.urlSave)
                this.urlSave = args.urlSave;

            
        },
        render: function () {
            return this;
        },
        loadControls: function () {
            this.fileUpload = this.$("input[type='file']");
            this.collection = new FileCollection();
            this.resizer = new window.resize();
            this.resizer.init();
        },
        loadPreviousImages: function () {

            var that = this;
            //Recorre cada uno de los archivos temporales que se habian cargado anteriormente
            _.each(this.model.get('TempFiles'), function (element, index) {
                //Busca la imagen en orden y la empieza a cargar una a una
                that.currentControlImage = that.$('.addImageGalery:eq(' + index + ')');
                var fileName = '/TempFiles/' + element;
                var currentModel = new FileModel({ guid: element, src: fileName, control : that.currentControlImage });
                that.fileUploaded(currentModel);
                that.switchImage(fileName, that.currentControlImage);
            });
        },
        addImage: function (obj) {
            
            if ($(obj.target).is(".icon-delete")) return false;

            var target = $(obj.currentTarget);
            this.currentControlImage = target;

            var allowLoad = true;

            var attrFile = target.attr(this.attributeFile);
            if (attrFile) {
                allowLoad = this.removeImage(target);
            }

            if (allowLoad)
                this.fileUpload.click();

        },
        removeImage: function (obj) {
            var isRemove = false;
            if (obj.target)
            {
                obj.preventDefault();
                obj = $(obj.target).parent();
                this.currentControlImage = obj;
                isRemove = true;
            }
               

            if (confirm("¿Deseas "+(isRemove ? "eliminar" : "cambiar")+" la imagen?")) {
                
                //Quita la imagen de la lista y la desvincula del control
                var fileToRemove = obj.attr(this.attributeFile);
                this.collection.remove(this.collection.findWhere({ guid: fileToRemove }))
                this.switchImage(undefined, this.currentControlImage);
                //Elimina la imagen del servidor
                var fileModel = new FileModel({ fileGuid: fileToRemove });
                fileModel.remove();
                return true;
            }
            else {
                return false;
            }
        },
        uploadImage: function (obj) {

            var controlImage = this.currentControlImage;

            for (var iFile = 0; iFile < obj.target.files.length; iFile++) {
                var file = obj.target.files[iFile];
                var that = this;
                if (file) {

                    if (TuilsUtilities.isValidSize(obj.target)) {
                        if (TuilsUtilities.isValidExtension(obj.target, 'image')) {
                            var fileModel = new FileModel();

                            //Si ya tiene cargado un archivo, toma otro archivo que no tenga imagen, si no hay espacio no sube la imagen
                            if (controlImage.attr(this.attributeFileUsed))
                            {
                                var availableControls = this.$('.addImageGalery:not(['+this.attributeFileUsed+'])');
                                if (availableControls.length > 0) {
                                    controlImage = availableControls.first();
                                }
                                else
                                    return false;
                            }
                                
                            controlImage.attr('tuils-used', '1');
                            fileModel.set('control', controlImage);
                            this.showLoadingBack(fileModel, controlImage);
                            fileModel.on("file-saved", this.fileUploaded, this);
                            fileModel.on("file-error", this.fileErrorUpload, this);
                            //this.resizeImage(file, fileModel);
                            //Atacha evento para actualizar la imagen
                            fileModel.on('sync', function (model) {
                                console.log("sync method->" + model.get('guid'));
                                that.switchImage('/TempFiles/' + model.get('thumbnail'), model.get('control'));
                            }, this);

                            fileModel.set('file', file);
                            fileModel.upload({ saveUrl: this.urlSave });
                        }
                        else {
                            this.alert("Las imágenes tienen que estar en formatos .jpg, .gif o .png.");
                            return false;
                        }

                    }
                    else {
                        this.alert("El tamaño máximo permitido para tus archivos es de " + TuilsConfiguration.maxFileUploadSize / 1024000 + "Mb");
                        return false;
                    }

                }
            }

            

        },
        //////resizeImage: function (file, fileModel) {
        //////    var that = this;
        //////    this.resizer.photo(file, TuilsConfiguration.media.productImageMaxSizeResize, 'file', function (resizedFile) {

        //////        that.resizer.photoCrop(resizedFile, 400, 'dataURL', function (thumbnail) {
        //////            fileModel.set({ src: thumbnail, file: resizedFile });
        //////            //Hasta que la imagen no haya sido subida 
        //////            fileModel.on('sync', function () {
        //////                that.switchImage(thumbnail, fileModel.get('control'));
        //////            });
        //////            fileModel.upload({ saveUrl: that.urlSave });

        //////        });
        //////    });
        //////},
        switchImage : function(urlImage, ctrl)
        {
            if (urlImage) {
                ctrl.find("img").attr("src", urlImage).show();
                ctrl.find("span").hide();
            }
            else {
                ctrl.find("img").removeAttr("src").hide();
                ctrl.removeAttr(this.attributeFile)
                .removeAttr(this.attributeFileUsed)
                .removeClass('loadingBack');
                ctrl.find("span").removeAttr('style');
            }

            ctrl.find(".icon-delete").css('display', urlImage ? 'block' : 'none');
        },
        fileUploaded: function (model) {
            //var srcImage = this.currentControlImage.find("img", "src");
            console.log("FileUploaded method->" + model.get('guid'));
            var guidImage = model.get('guid');
            this.collection.add(model);
            var control = model.get('control');
            control.attr(this.attributeFile, guidImage);
        },
        fileErrorUpload: function (resp) {
            this.switchImage(undefined, resp.model.get('control'));
            this.alert({ message: 'Error guardando la imagen, intenta de nuevo' , duration: 2500 });
            console.log('Error guardando la imagen');
        },
        save: function () {
            if (this.collection.length >=  this.minFilesUploaded) {
                this.trigger("images-save", this.collection);
            }
            else {
                this.alert("Para publicar tu anuncio debes subir por lo menos " + this.minFilesUploaded + " imágen" + (this.minFilesUploaded > 1 ? "es" : ""));
            }
        },
        back: function () {
            if (this.onBackRemoveImages)
                this.removeAllImages();

            this.trigger("images-back");
        },
        removeAllImages: function () {

        }

    });

    return ImagesSelectorView;
});

