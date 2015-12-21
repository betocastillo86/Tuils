define(['jquery', 'underscore', 'baseView', 'tuils/views/vendor/searchVendorsMapView', 'tuils/views/vendor/searchVendorsFilterView', 'tuils/views/vendor/searchVendorsFilterListView'],
    function ($, _, BaseView, SearchVendorsMapView, SearchVendorsFilterView, SearchVendorsFilterListView) {

        var SearchVendorsView = BaseView.extend({
            map: undefined,
            filter: undefined,
            filterList: undefined,
            preselectedFilter: undefined,
            urlValues : undefined,
            initialize: function (args) {
                this.loadControls(args);
            },
            loadControls: function (args) {
                this.preselectedFilter = {
                    StateProvinceId: args.stateProvinceId,
                    CategoryId: args.categoryId,
                    VendorId: args.vendorId,
                    SubTypeId: args.subTypeId,
                    lat: args.lat,
                    lon: args.lon,
                    zoom: args.zoom
                };

                this.map = new SearchVendorsMapView({ el: '#divMap', lat :args.lat, lon: args.lon, zoom:args.zoom  });
                this.map.on('set-city', this.cityLoaded, this);
                this.map.on('list-filtered', this.listFiltered, this);
                this.map.on('selected', this.officeSelected, this);
                this.urlValues = new Object();
            },
            //Cuando el mapa carga puede cargar ahora el filtro
            cityLoaded: function (location) {
                this.loadFilter(location);
            },
            loadFilter: function (location) {
                this.filter = new SearchVendorsFilterView({ el: '#divVendorFilter', location: location, preselectedFilter : this.preselectedFilter });
                this.filter.on('offices-loaded', this.loadMap, this);
            },
            //evento disparado cuando se actualiza la lista de oficinas de acuerdo al mapa actual
            listFiltered: function (offices) {
                if (!this.filterList)
                {
                    this.filterList = new SearchVendorsFilterListView({ el: '#divFilteredList' });
                    this.filterList.on('selected', this.officeSelectedFromList, this);
                }
                    
                this.filterList.showOffices(offices.offices);

                //Actualiza la posicion en el mapa y la navegacion
                this.urlValues['lat'] = offices.lat;
                this.urlValues['lon'] = offices.lon;
                this.urlValues['zoom'] = offices.zoom;
                Backbone.history.navigate('buscar-negocios/'
                    + this.urlValues.StateProvinceId + '/'
                    + this.urlValues.CategoryId + '/'
                    + this.urlValues.VendorId + '/'
                    + this.urlValues.SubTypeId + '/'
                    + this.urlValues.lat + '/'
                    + this.urlValues.lon + '/'
                    + this.urlValues.zoom 
                    );
            },
            officeSelected: function (office) {
                this.filterList.selectOffice(office);
            },
            officeSelectedFromList: function (office) {
                this.map.selectOffice(office);
            },
            loadMap: function (response) {
                this.urlValues['StateProvinceId'] = response.filter.StateProvinceId;
                this.urlValues['CategoryId'] = response.filter.CategoryId;
                this.urlValues['VendorId'] = response.filter.VendorId;
                this.urlValues['SubTypeId'] = response.filter.SubTypeId;
                this.map.showOffices({ offices: response.offices.toJSON(), city: response.city });
            },
        });

        return SearchVendorsView;
    });

