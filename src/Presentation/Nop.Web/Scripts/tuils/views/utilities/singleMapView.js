define(['jquery', 'underscore', 'backbone', 'async!maps'],
    function ($, _, Backbone) {

    var SelectPointMapView = Backbone.View.extend({
        map: undefined,

        lat: 4.57262365310281,
        lon: -74.0970325469971,

        markersArray: [],

        initialize: function () {
            
            this.render();
        },
        loadMap: function (args) {
            this.locations = args.locations;
            var myOptions = {
                zoom: 14,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            this.map = new google.maps.Map(this.$el[0], myOptions);

            var that = this;
            _.each(args.locations, function (element, index) {
                var location = new google.maps.LatLng(element.lat, element.lon);
                that.placeMarker(location, element.id);
                if (index === 0) that.map.setCenter(location);
            });
            
        },
        deleteOverlays: function () {
            if (this.markersArray) {
                for (i in this.markersArray) {
                    this.markersArray[i].setMap(null);
                }
                this.markersArray.length = 0;
            }
        },
        placeMarker: function (location, id) {

            var marker = new google.maps.Marker({
                position: location,
                map: this.map
            });
            marker.selectedId = id;
            //Agrega el evento del clic
            var that = this;
            google.maps.event.addListener(marker, 'click', function () {
                that.trigger('point-selected', this.selectedId);
            });

            this.markersArray.push(marker);
        },
        setCurrentMarker : function(id)
        {
            var location = _.findWhere(this.locations, { id: id });
            this.map.setCenter(new google.maps.LatLng(location.lat, location.lon));
        },
        render: function () {
            return this;
        }
    });

    return SelectPointMapView;
});