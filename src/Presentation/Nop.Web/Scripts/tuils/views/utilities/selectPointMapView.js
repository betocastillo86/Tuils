define(['jquery', 'underscore', 'backbone', 'async!maps'],
    function ($, _, Backbone) {
   
    var SelectPointMapView = Backbone.View.extend({
        map: undefined,

        markersArray: [],

        lat: 4.57262365310281,
        lon: -74.0970325469971,

        //Retorna también la dirección en texto
        showAddress: false,
        //Usado para localizar las direcciones
        geocoder: undefined,

        initialize: function (args) {
            this.render();
        },
        loadMap: function (args) {

            var setMarker = false;
            
            if (args)
            {
                if (args.lat && args.lon) {
                    setMarker = true;
                    this.lat = args.lat;
                    this.lon = args.lon;
                }

                if (args.showAddress != undefined)
                    this.showAddress = args.showAddress;
            }


            //Si ya viene previamente las coordenadas ubica la posición, sino  ubica la posición actual del usuario
            if (setMarker)
            {
                this.updateLocation(true);
                this.placeMarker(new google.maps.LatLng(this.lat, this.lon));
            }
            else
            {
                this.getCurrentLocation();
            }
        },
        deleteOverlays: function () {
            if (this.markersArray) {
                for (i in this.markersArray) {
                    this.markersArray[i].setMap(null);
                }
                this.markersArray.length = 0;
            }
        },
        placeMarker: function (location) {
            // first remove all markers if there are any
            this.deleteOverlays();

            var marker = new google.maps.Marker({
                position: location,
                map: this.map
            });

            // add marker in markers array
            this.markersArray.push(marker);

            //this.map.setCenter(location);
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
        updateLocation : function(zoom)
        {
            var latlng = new google.maps.LatLng(this.lat, this.lon);
            var myOptions = {
                zoom: zoom ? 15 : 12,
                center: latlng,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            this.map = new google.maps.Map(this.$el[0], myOptions);

            var that = this;
            // add a click event handler to the map object
            google.maps.event.addListener(this.map, "click", function (event) {
                // place a marker
                that.placeMarker(event.latLng);

                // display the lat/lng in your form's lat/lng fields
                that.lat = event.latLng.lat();
                that.lon = event.latLng.lng();
                that.trigger('set-position', { lat: that.lat, lon: that.lon });

                that.setAddress(event.latLng);
            });
        },
        setAddress: function (latLng) {
            if (this.showAddress) {

                if(!this.geocoder)
                    this.geocoder = new google.maps.Geocoder();
                var that = this;
                this.geocoder.geocode({ 'latLng': latLng }, function (results, status) {
                    if (status == google.maps.GeocoderStatus.OK) {
                        if (results[0]) {
                            //that.trigger('set-address', results[0].formatted_address);
                            //Se hace un format para las direcciones de Colombia que son más complejas
                            var cityName = '';
                            _.each(results[0].address_components, function (element, index) {
                                if (_.contains(element.types, 'administrative_area_level_1'))
                                {
                                    cityName = element.long_name;
                                    return;
                                }
                            });

                            that.trigger('set-address', { address: results[0].formatted_address.split(' a ')[0], cityName : cityName });
                        } else {
                            this.alertError('No se encontraron resultados del mapa');
                        }
                    } else {
                        this.alertError('Geocoder failed due to: ' + status);
                    }
                });
            }
        },
        render: function () {
            return this;
        }
    });

    return SelectPointMapView;
});