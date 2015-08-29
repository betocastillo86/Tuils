define(['jquery', 'underscore', 'baseView', 'fileModel', 'fileCollection', 'configuration', 'util', 'resize'],
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
                var currentModel = new FileModel({ guid: element, src: fileName });
                that.fileUploaded(currentModel);
                that.switchImage(fileName);
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
                this.switchImage();
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
            var file = obj.target.files[0];
            var that = this;
            if (file) {

                if (TuilsUtilities.isValidSize(obj.target)) {
                    if (TuilsUtilities.isValidExtension(obj.target, 'image')) {
                        var fileModel = new FileModel();
                        this.showLoadingBack(fileModel, this.currentControlImage);
                        fileModel.on("file-saved", this.fileUploaded, this);
                        fileModel.on("file-error", this.fileErrorUpload, this)
                        this.resizer.photo(file, TuilsConfiguration.media.productImageMaxSizeResize, 'file', function (resizedFile) {

                            that.resizer.photoCrop(resizedFile, 400, 'dataURL', function (thumbnail) {
                                fileModel.set({ src: thumbnail, file: resizedFile });
                                //Hasta que la imagen no haya sido subida 
                                fileModel.on('sync', function () {
                                    that.switchImage(thumbnail);
                                });
                                fileModel.upload({ saveUrl: that.urlSave });

                            });
                        });
                    }
                    else {
                        this.alert("Las imágenes tienen que estar en formatos .jpg, .gif o .png.");
                        return false;
                    }

                }
                else {
                    this.alert("El tamaño máximo permitidopar tus archivos es de " + TuilsConfiguration.maxFileUploadSize / 1024000 + "Mb");
                    return false;
                }

            }

        },
        switchImage : function(urlImage)
        {
            if (urlImage) {
                this.currentControlImage.find("img").attr("src", urlImage).show();
                this.currentControlImage.find("span").hide();
            }
            else {
                this.currentControlImage.find("img").removeAttr("src").hide();
                this.currentControlImage.removeAttr(this.attributeFile);
                this.currentControlImage.find("span").show();
            }

            this.currentControlImage.find(".icon-delete").css('display', urlImage ? 'block' : 'none');
        },
        fileUploaded: function (model) {
            //var srcImage = this.currentControlImage.find("img", "src");
            var guidImage = model.get('guid');
            this.collection.add(model);
            this.currentControlImage.attr(this.attributeFile, guidImage);
        },
        fileErrorUpload: function (resp) {
            this.switchImage();
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

