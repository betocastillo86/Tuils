define(['jquery', 'underscore', 'util', 'baseView', 'tuils/collections/addresses', 'handlebars', 'tuils/views/utilities/singleMapView'],
    function ($, _, TuilsUtilities, BaseView, AddressCollection, Handlebars, SingleMapView) {

        var OfficesView = BaseView.extend({
            events: {
                'click #btnBack': 'backOffice',
                'click #btnNext': 'nextOffice'
            },

            //templateOffice: Handlebars.compile($("#templateSelectedOffice").html()),

            viewMap : undefined,

            id: 0,

            currentOffice : 0,

            officeCollection: undefined,

            galleryCollection: undefined,

            domOffices: undefined,

            domCurrentOffice: undefined,

            initialize: function (args)
            {
                //this.templateOffice= Handlebars.compile($("#templateSelectedOffice").html());
                this.id = args.id;
                this.loadControls();
                this.render();
            },
            loadControls: function () {
                this.loadMap();
                this.loadOffices();
                
            },
            loadOffices : function()
            {
                /*this.collection = new AddressCollection();
                this.collection.on("sync", this.showOffices, this);
                this.collection.getOfficesByVendor(this.id);*/


                //por defecto solo muestra la primera oficina
                this.domOffices = this.$('.caja-sede');
                var objOffice = this.domOffices.first();
                if (objOffice.length)
                {
                    this.showMap();
                    var officeId = parseInt(objOffice.attr('data-id'));
                    this.changeOffice(officeId);
                }
                
                if(this.domOffices.length <= 1)
                    this.$('.btn-sedes').hide();
            },
            loadMap : function()
            {
                this.viewMap = new SingleMapView({ el: ".mapa" });
                this.viewMap.on("point-selected", this.changeOffice, this);
            },
            /*showOffices: function (resp) {
                this.officeCollection = resp.toJSON();
                this.showMap();

                if (this.officeCollection.length > 0)
                    this.changeOffice(this.officeCollection[0].Id);
                else
                {
                    this.$(".mapa").hide();
                    this.$("#divOfficesNoResults").show();
                    this.$("#btnConfigOfficesWithRows").hide();
                }

                this.$('.btn-sedes').css('display', this.officeCollection.length > 1 ? "block" : "none");
                return this;
            },*/
            changeOffice: function (officeId) {

                this.domOffices.hide();
                this.domCurrentOffice = this.domOffices.closest("[data-id='" + officeId + "']").show();
                this.viewMap.setCurrentMarker(officeId);
                this.currentOffice = officeId;
                this.loadGallery();
            },
            backOffice: function () {
                this.moveOffice(-1);            
            },
            nextOffice: function () {
                this.moveOffice(1);
            },
            moveOffice : function(move)
            {
                var that = this;
                var index = _.findLastIndex(this.officeCollection, { id: this.currentOffice });;
                index = index == -1 ? 0 : index;
                //Si el valor es positivo intenta mover la colección hacia el indice. Si se pasa del tamaño de la lista toma el primero, si es menor que 0 toma el ultimo
                //Envia la siguiente o anterior oficina
                this.changeOffice(this.officeCollection[move > 0 ? (index + move >= this.officeCollection.length ? 0 : index + move) : index + move < 0 ? this.officeCollection.length - 1 : index+move].id);
            },
            showMap : function()
            {
                var that = this;
                this.officeCollection = new Array();
                _.each(this.domOffices, function (element, index) {
                    element = $(element);
                    that.officeCollection.push({ lat: parseFloat(element.attr('data-lat').replace(",", ".")), lon: parseFloat(element.attr('data-lon').replace(",", ".")), id: parseInt(element.attr('data-id')) });
                });

                this.viewMap.loadMap({ locations: this.officeCollection, draggable : false });
            },
            loadGallery : function()
            {
                if (!this.galleryCollection) {
                    this.galleryCollection = new AddressCollection();  
                }
                
                this.galleryCollection.on('sync', this.showGallery, this);
                this.galleryCollection.getPictures(this.currentOffice);
            },
            showGallery : function()
            {
                var show = this.galleryCollection.toJSON().length > 0;
                var btnGallery = this.domCurrentOffice.find('.btnViewGallery');
                btnGallery.css('display', show ? '' : 'none');
                if (show)
                {
                    var pictures = new Array();
                    _.each(this.galleryCollection.toJSON(), function (element, index) {
                        pictures.push({ src: element.FullSizeImageUrl });
                    });

                    btnGallery.magnificPopup({ items: pictures, gallery: { enabled: true }, type: 'image' });
                }
            },
            render: function () {
                //this.templateOffice = Handlebars.compile(this.$("#templateSelectedOffice").html());
                return this;
            }
        });

        return OfficesView;
});