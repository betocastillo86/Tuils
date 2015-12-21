﻿define(['jquery', 'underscore', 'baseView', 'tuils/views/vendor/searchVendorsMapView', 'tuils/views/vendor/searchVendorsFilterView'],
    function ($, _, BaseView, SearchVendorsMapView, SearchVendorsFilterView) {

        var SearchVendorsView = BaseView.extend({
            map: undefined,
            filter : undefined,
            initialize: function () {
                this.loadControls();
            },
            loadControls: function () {
                this.map = new SearchVendorsMapView({ el: '#divMap' });
                this.map.on('set-city', this.cityLoaded, this);
            },
            //Cuando el mapa carga puede cargar ahora el filtro
            cityLoaded: function (location) {
                this.loadFilter(location);
            },
            loadFilter: function (location) {
                this.filter = new SearchVendorsFilterView({ el: '#divVendorFilter', location: location });
                this.filter.on('offices-loaded', this.loadMap, this);
            },
            loadMap: function (response) {
                Backbone.history.navigate('buscar-negocios/' + response.filter.StateProvinceId + '/' + response.filter.CategoryId + '/' + response.filter.VendorId + '/' + response.filter.SubTypeId);
                this.map.showOffices({ offices: response.offices.toJSON(), city: response.city });
            },
        });

        return SearchVendorsView;
    });

