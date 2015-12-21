define(['jquery', 'underscore', 'baseView', 'configuration', 'handlebars'],
    function ($, _, BaseView, TuilsConfiguration, Handlebars) {

        var SearchVendorsMapView = BaseView.extend({

            lat: 4.57262365310281,
            lon: -74.0970325469971,

            //Usado para localizar las direcciones
            geocoder: undefined,

            templateInfo: undefined,

            infoWindow : undefined,

            marks: [],

            currentCity : 0,

            initialize: function (args) {
                this.loadEmptyMap(args);
                this.templateInfo = Handlebars.compile($('#templateMapInfoOffice').html());
                this.infoWindow = new google.maps.InfoWindow({ content: '' });
                var that = this;
                google.maps.event.addListener(this.infoWindow, 'closeclick', function () {
                    that.trigger('selected', undefined);
                });
            },

            loadEmptyMap: function (args) {
                if (args.lat && args.lon && args.zoom) {
                    this.lat = parseFloat(args.lat);
                    this.lon = parseFloat(args.lon);
                    this.updateLocation(parseInt(args.zoom));
                }
                else {
                    //Si no viene previamente seleccionada la posición del mapa carga la actual
                    this.getCurrentLocation();
                }
                
            },

            getCurrentLocation: function () {
                if (navigator.geolocation) {
                    var that = this;
                    navigator.geolocation.getCurrentPosition(function (position) {
                        that.lat = position.coords.latitude;
                        that.lon = position.coords.longitude;
                        that.updateLocation(15);
                    });
                }
                else {
                    //Si no tiene geolocalización lo ubica en la posición por defecto
                    this.updateLocation(12);
                }
            },
            updateLocation: function (zoom) {
                var latlng = new google.maps.LatLng(this.lat, this.lon);
                var myOptions = {
                    zoom: zoom,
                    center: latlng,
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                };
                this.map = new google.maps.Map(this.$el[0], myOptions);
                this.loadCity();

                var that = this;
                google.maps.event.addListener(this.map, 'idle', function () {
                    that.updateFilterList();
                });
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
                marker.office = office;
                marker.officeId = office.id;
                //Agrega el evento del clic
                var that = this;
                google.maps.event.addListener(marker, 'click', function () {
                    that.infoWindow.setContent(that.templateInfo(this.office));
                    that.infoWindow.open(that.map, this);
                    that.trigger('selected', this.office);
                });

                return marker;
                //this.markersArray.push(marker);
            },
            updateFilterList: function () {
                var that = this;

                var officesOnList = [];
                var currentBounds = this.map.getBounds();
                _.each(this.marks, function (element, index) {
                    //Si ningun punto se ha mostrado hasta ahora sigue validando hasta cambiar la bandera
                    if (currentBounds.contains(element.position))
                    {
                        officesOnList.push(element.office);
                    }
                });
                this.trigger('list-filtered', { offices: officesOnList, lat: this.map.getCenter().lat(), lon: this.map.getCenter().lng(), zoom: this.map.getZoom() });
            },
            showOffices: function (response) {

                var offices = response.offices;

                //Limpia las marcas antes de cargarlas de nuevo
                this.clearMarks();
                var that = this;

                //Varibale que controla si se está mostrando o no algún punto
                var isShowingPoint = false;
                var currentBounds = this.map.getBounds();

                _.each(offices, function (element, index) {
                   // var location = new google.maps.LatLng(element.lat, element.lon);
                    var mark = that.placeMarker(element);
                    that.marks.push(mark);
                });

                
                if (!that.marks.length)
                    this.alert('No hay resultados que coincidan con tu busqueda');
                else
                {
                    //Si hay resultados y la ciudad cambió se debe mover hacia la ciudad
                    if(this.currentCity && this.currentCity != response.city)
                        this.map.setCenter(that.marks[0].position);
                }

                //Actualiza la ciudad
                this.currentCity = response.city;
                this.updateFilterList();
            },
            selectOffice: function (office) {
                var mark = _.findWhere(this.marks, { officeId: office.id });
                if (mark) {
                    this.infoWindow.setContent(this.templateInfo(office));
                    this.infoWindow.open(this.map, mark);
                }
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