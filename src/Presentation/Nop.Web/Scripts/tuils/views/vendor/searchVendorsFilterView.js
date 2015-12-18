define(['jquery', 'underscore', 'baseView', 'tuils/models/vendor/searchVendorModel', 'tuils/collections/addresses', 'configuration'],
    function ($, _, BaseView, SearchVendorModel, AddressCollection, TuilsConfiguration) {

        var SearchVendorsFilterView = BaseView.extend({
            events: {
                'click #subTypeSelector a': 'filterBySubtype'
            },
            bindings: {
                '#ddlStateProvinceId' : 'StateProvinceId'
            },
            //Ubicacion principal del mapa
            mapLocation : undefined,

            initialize: function (args) {
                //Carga el filtro
                this.model = new SearchVendorModel();
                this.model.on('change', this.searchOffices, this);

                //La colección de direcciones que vienen en el filtro
                this.collection = new AddressCollection();
                this.collection.on('sync', this.officesLoaded, this);

                this.mapLocation = args.location;
                this.loadControls();
                this.render();
            },
            loadControls: function () {
                //La ubicación principal de la ciudad debe venir por defecto
                this.setCity(this.mapLocation);
                this.loadAutoComplete();
            },
            //Cuando el mapa carga selecciona la posición
            setCity: function (location) {

                var selectedCity = this.$("#ddlStateProvinceId option").filter(function () {
                    return this.text == location.cityName;
                });

                if (selectedCity.length)
                    this.model.set('StateProvinceId', selectedCity.val());
            },
            filterBySubtype: function (obj) {
                obj = $(obj.currentTarget);
                var subTypeSelected = parseInt(obj.attr('data-id'));

                if (obj.hasClass('active'))
                {
                    //Si el botón se está desactivando invierte el tipo de que se desea filtrar
                    obj.removeClass('active');
                    subTypeSelected = subTypeSelected == TuilsConfiguration.vendor.subTypeStore ? TuilsConfiguration.vendor.subTypeRepairShop : TuilsConfiguration.vendor.subTypeStore;
                }
                else
                    obj.addClass('active');


                //Si ya se habia seleccionado algún subtipo para filtrar, se manda a null xq significa que no se va a filtrar incluyentemente
                if (this.model.get('SubTypeId'))
                    this.model.set('SubTypeId', undefined);
                else
                    this.model.set('SubTypeId', subTypeSelected);
            },
            loadAutoComplete: function () {
                this.$('#txtFilter').autocomplete({
                    delay: 200,
                    minLength: 3,
                    source: '/api/vendors/autocompleteSearch',
                    select: function (event, ui) {
                        /*that.$("#small-searchterms").val(ui.item.value);
                        this.form.submit();*/
                        alert('selected');
                    }
                })
                .data("ui-autocomplete")._renderItem = function (ul, item) {
                    var t = item.label;
                    //html encode
                    t = htmlEncode(t);
                    return $("<li></li>")
                        .data("item.autocomplete", item)
                        .append("<a>" + t + "</a>")
                    .appendTo(ul);
                };
            },
            searchOffices: function () {
                this.showLoadingAll(this.collection);
                this.collection.searchOffices(this.model.toJSON());
            },
            officesLoaded: function () {
                this.trigger('offices-loaded', this.collection);
            },
            render: function () {
                this.stickThem();
            }
        });

        return SearchVendorsFilterView;
    });