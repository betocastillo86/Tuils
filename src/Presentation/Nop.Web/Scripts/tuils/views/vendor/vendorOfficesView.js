define(['underscore', 'util', 'baseView', 'tuils/collections/addresses', 'handlebars', 'tuils/views/utilities/singleMapView'],
    function (_, TuilsUtilities, BaseView, AddressCollection, Handlebars, SingleMapView) {

        var OfficesView = BaseView.extend({
            events: {
                'click #btnBack': 'backOffice',
                'click #btnNext': 'nextOffice'
            },

            templateOffice: Handlebars.compile($("#templateSelectedOffice").html()),

            viewMap : undefined,

            id: 0,

            currentOffice : 0,

            officeCollection: undefined,

            galleryCollection : undefined,

            initialize: function (args)
            {
                this.id = args.id;
                this.loadControls();
                this.render();
            },
            loadControls: function () {
                this.loadOffices();
                this.loadMap();
            },
            loadOffices : function()
            {
                this.collection = new AddressCollection();
                this.collection.on("sync", this.showOffices, this);
                this.collection.getOfficesByVendor(this.id);
            },
            loadMap : function()
            {
                this.viewMap = new SingleMapView({ el: ".mapa" });
                this.viewMap.on("point-selected", this.changeOffice, this);
            },
            showOffices: function (resp) {
                this.officeCollection = resp.toJSON();
                this.showMap();

                if (this.officeCollection.length > 0)
                    this.changeOffice(this.officeCollection[0].Id);

                this.$('.btn-sedes').css('display', this.officeCollection.length > 1 ? "block" : "none");
                return this;
            },
            changeOffice: function (officeId) {
                var office = _.findWhere(this.officeCollection, { Id: officeId });
                this.$(".caja-sede").html(this.templateOffice(office));
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
                var index = _.findLastIndex(this.officeCollection, { Id: this.currentOffice });;
                index = index == -1 ? 0 : index;
                //Si el valor es positivo intenta mover la colección hacia el indice. Si se pasa del tamaño de la lista toma el primero, si es menor que 0 toma el ultimo
                //Envia la siguiente o anterior oficina
                this.changeOffice(this.officeCollection[move > 0 ? (index + move >= this.officeCollection.length ? 0 : index + move) : index + move < 0 ? this.officeCollection.length - 1 : index+move].Id);
            },
            showMap : function()
            {
                var locations = new Array();
                _.each(this.officeCollection, function (element, index) {
                    locations.push({ lat : element.Latitude, lon : element.Longitude, id : element.Id });
                });

                this.viewMap.loadMap({ locations : locations});
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
                this.$('#btnViewGallery').css('display', show ? 'inline' : 'none');
                if (show)
                {
                    var pictures = new Array();
                    _.each(this.galleryCollection.toJSON(), function (element, index) {
                        pictures.push({ src: element.FullSizeImageUrl });
                    });

                    this.$('#btnViewGallery').magnificPopup({ items: pictures, gallery: { enabled: true }, type: 'image' });
                }
            },
            render: function () {
                //this.templateOffice = Handlebars.compile(this.$("#templateSelectedOffice").html());
                return this;
            }
        });

        return OfficesView;
});