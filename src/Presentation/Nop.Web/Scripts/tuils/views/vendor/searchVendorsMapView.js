define(['jquery', 'underscore', 'baseView', 'configuration'],
    function ($, _, BaseView, TuilsConfiguration) {

        var SearchVendorsMapView = BaseView.extend({

            lat: 4.57262365310281,
            lon: -74.0970325469971,

            //Usado para localizar las direcciones
            geocoder: undefined,

            marks : [],

            initialize: function () {
                this.loadEmptyMap();
            },

            loadEmptyMap: function () {
                this.getCurrentLocation();
            },

            getCurrentLocation: function () {
                if (navigator.geolocation) {
                    var that = this;
                    navigator.geolocation.getCurrentPosition(function (position) {
                        that.lat = position.coords.latitude;
                        that.lon = position.coords.longitude;
                        that.updateLocation(true);
                    });
                }
                else {
                    //Si no tiene geolocalización lo ubica en la posición por defecto
                    this.updateLocation();
                }
            },
            updateLocation: function (zoom) {
                var latlng = new google.maps.LatLng(this.lat, this.lon);
                var myOptions = {
                    zoom: zoom ? 15 : 12,
                    center: latlng,
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                };
                this.map = new google.maps.Map(this.$el[0], myOptions);
                this.loadCity();
            },
            loadCity: function () {
                var that = this;
                var latlng = new google.maps.LatLng(this.lat, this.lon);

                if (!this.geocoder)
                    this.geocoder = new google.maps.Geocoder();

                this.geocoder.geocode({ 'latLng': latlng }, function (results, status) {
                    if (status == google.maps.GeocoderStatus.OK) {
                        if (results[0]) {
                            //Busca de acuerdo a la geolocalización actual, cual es la ciudad a la que pertenece
                            var cityName = '';
                            _.each(results[0].address_components, function (element, index) {
                                if (_.contains(element.types, 'administrative_area_level_1')) {
                                    cityName = element.long_name;
                                    return;
                                }
                            });

                            that.trigger('set-city', { cityName: cityName });
                        } else {
                            alert('Map:No results found');
                        }
                    } else {
                        alert('Map:Geocoder failed due to: ' + status);
                    }
                });
            },
            placeMarker: function (office, id) {

                var marker = new google.maps.Marker({
                    position: new google.maps.LatLng(office.lat, office.lon),
                    map: this.map,
                    icon: '/Content/Images/icons/' + (office.vType == TuilsConfiguration.vendor.subTypeStore ? 'tienda' : 'taller') + '.png'
                });
                marker.selectedId = id;
                //Agrega el evento del clic
                var that = this;
                google.maps.event.addListener(marker, 'click', function () {
                    that.trigger('point-selected', this.selectedId);
                });

                return marker;

                //this.markersArray.push(marker);
            },
            showOffices: function (offices) {
                //Limpia las marcas antes de cargarlas de nuevo
                this.clearMarks();
                var that = this;
                _.each(offices, function (element, index) {
                   // var location = new google.maps.LatLng(element.lat, element.lon);
                    that.marks.push(that.placeMarker(element));
                });
            },
            clearMarks: function () {
                _.each(this.marks, function (element, index) {
                    element.setMap(null);
                });
                this.marks = [];
            }

        });

        return SearchVendorsMapView;
    });