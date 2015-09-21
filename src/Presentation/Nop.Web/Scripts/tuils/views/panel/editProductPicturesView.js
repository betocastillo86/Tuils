define(['jquery', 'underscore', 'baseView', 'handlebars', 'tuils/models/picture', 'tuils/collections/pictures',
'configuration'],
    function ($, _, BaseView, Handlebars, PictureModel, PictureCollection, TuilsConfiguration) {

        var EditProductPicturesView = BaseView.extend({
            events: {
                'click .icon-delete': 'removeImage',
                'click .addImageGalery': 'changeImage',
                "change input[type='file']": "saveImage",
            },
            viewPictures: undefined,

            collection: undefined,

            fileUpload: undefined,

            selectedPictureId: 0,

            template : undefined,

            productId : 0,

            initialize: function (args) {
                this.productId = args.productId;
                this.loadControls();
            },
            loadControls: function () {
                this.template = Handlebars.compile($("#templatePictures").html());
                this.collection = new PictureCollection();
                this.collection.on('sync', this.showPictures, this);
                this.collection.on('add', this.showPictures, this);
                this.collection.on('remove', this.showPictures, this);
                this.loadPictures();
            },
            loadPictures: function () {
                this.collection.getPicturesByProductId(this.productId);
            },
            showPictures: function () {
                this.$el.html(this.template({ allowMoreImages: TuilsConfiguration.catalog.limitNumPictures > this.collection.length, pictures: this.collection.toJSON() }));
                this.render();
            },
            changeImage: function (obj) {

                if ($(obj.target).is(".icon-delete")) return false;
                obj.preventDefault();
                var selectedPicture = parseInt($(obj.target).attr('data-id'));
                //Si es crear imagen o si acepta cambiar la imagen la carga
                if(selectedPicture == 0 || confirm('¿Seguro deseas cambiar esta imagen?'))
                {
                    this.selectedPictureId = selectedPicture;
                    this.fileUpload.click();
                }
            },
            saveImage: function (obj) {
                var file = obj.target.files[0];
                //Valida que la extensión y tamaño sean validos
                if (this.isValidFileUpload(file, obj.target, 'image'))
                {
                    //busca la imagen que desea eliminar
                    var picture = this.selectedPictureId == 0 ? new PictureModel() : this.collection.findWhere({ Id: this.selectedPictureId });
                    picture.on('sync', function () {
                        if (this.selectedPictureId == 0)
                            this.collection.add(picture);
                    }, this);
                    this.showLoadingBack(picture, this.$('.picture-uploader li[data-id="' + this.selectedPictureId + '"]'));
                    //picture.savePictureToProduct(file, this.productId, 1800, TuilsConfiguration.media.productthumbpicturesizeonproductdetailspage);
                    picture.savePictureToProduct(file, this.productId, undefined, undefined);
                }
            },
            removeImage: function (obj) {
                if (confirm('¿Deseas eliminar esta imagen?'))
                {
                    var pictureId = parseInt($(obj.target).attr("data-id"));
                    var picture = this.collection.findWhere({ Id: pictureId });
                    picture.on('error', function () { this.alert('Error eliminando la imagen, intenta de nuevo'); }, this);
                    picture.removeFromProduct(this.productId);
                }
            },
            render: function () {
                this.fileUpload = this.$("input[type='file']");
                return this;
            }
        });

        return EditProductPicturesView;
});