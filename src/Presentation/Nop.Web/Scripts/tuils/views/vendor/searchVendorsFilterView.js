define(['jquery', 'underscore', 'baseView', 'tuils/models/vendor/searchVendorModel', 'tuils/collections/addresses',
    'configuration', 'storage', 'handlebars'],
    function ($, _, BaseView, SearchVendorModel, AddressCollection,
        TuilsConfiguration, TuilsStorage, Handlebars) {

        var SearchVendorsFilterView = BaseView.extend({
            events: {
                'click #subTypeSelector a': 'filterBySubtype',
                'click #divSelectedFilter .tagit-close' : 'closeSelectedItem'
            },
            bindings: {
                '#ddlStateProvinceId' : 'StateProvinceId'
            },
            //Ubicacion principal del mapa
            mapLocation: undefined,

            templateSelected: undefined,

            templateCount : undefined,

            listCategories : [],

            initialize: function (args) {
                //Carga el filtro
                this.model = new SearchVendorModel();
                this.model.on('change', this.searchOffices, this);

                //La colección de direcciones que vienen en el filtro
                this.collection = new AddressCollection();
                this.collection.on('sync', this.officesLoaded, this);
                
                //Carga las caregorias de los productos para posteriormente cargarlas como opciones
                TuilsStorage.loadProductCategories(this.loadOptionCategories, this);
                TuilsStorage.loadServiceCategories(this.loadOptionCategories, this);

                this.mapLocation = args.location;
                this.loadControls();
                this.render();
            },
            loadControls: function () {
                //La ubicación principal de la ciudad debe venir por defecto
                this.setCity(this.mapLocation);
                this.templateCount = Handlebars.compile($('#templateOfficesCount').html());
                //this.loadAutoComplete();
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
                var that = this;

                this.templateSelected = Handlebars.compile($('#templateSelectedItem').html());

                this.$('#txtFilter').autocomplete({
                    delay: 200,
                    minLength: 3,
                    source: function (request, response){
                        //El source es un llamado ajax que debe agregar la respuesta
                        $.ajax({
                            url: '/api/vendors/autocompleteSearch',
                            data: {
                                term: request.term
                            }
                        })
                        .done(function (data) {
                            var searchedValue = that.$('#txtFilter').val();

                            //Une los resultados del response y del filtro sobre las categorías
                            data = _.union(data, that.listCategories.filter(function (itemCategory) {
                                return itemCategory.filter.indexOf(searchedValue) != -1;
                            }));

                            return response(data);
                        });
                    },
                    select: function (event, ui) {
                        //Oculta la caja de texto y aplica el template
                        that.$('#txtFilter').hide();
                        that.$('#divSelectedFilter').html(that.templateSelected({ label: ui.item.value, type: ui.item.type }));
                        that.model.set(ui.item.type == 'ven' ? 'VendorId' : 'CategoryId', ui.item.id);
                    }
                })
                .data("ui-autocomplete")._renderMenu = function (ul, items) {
                    var thatRender = this;
                    var type = '';
                    $.each(items, function (index, item) {
                        var domItem = thatRender._renderItemData(ul, item).html("<a>" + item.value + "</a>");
                        //Si cambia el tipo de item agrega un divisor
                        if (type != item.type) {
                            var html = '<a>------' + (item.type == 'ven' ? 'Negocio' : 'Servicio o Producto') + '------</a>';
                            domItem.prepend(html);
                            type = item.type;
                        }
                    });

                };
                
            },
            loadOptionCategories: function (ctx) {
                //Solo si los productos y servicios han cargado se realiza la busqueda
                if(TuilsStorage.productCategories && TuilsStorage.serviceCategories)
                {
                    var that = this;

                    var addTag = function (element, parent) {
                        var label = (parent ? parent.Name + '->' : '') + element.Name;
                        that.listCategories.push({ id: element.Id, filter: label.toLowerCase(), value: label, label: label, type: 'cat' });
                    };

                    var recursiveCategories = function (categories, parent) {
                        _.each(categories, function (element, index) {
                            addTag(element, parent);
                            recursiveCategories(element.ChildrenCategories, element);
                        });
                    };

                    //Agrega todos los tags que se van a usar para realizar las busquedas
                    recursiveCategories(_.union(TuilsStorage.serviceCategories, TuilsStorage.productCategories), undefined);

                    this.loadAutoComplete();

                }
            },
            closeSelectedItem: function () {
                this.$('#divSelectedFilter').empty();
                this.$('#txtFilter').show().val('');
                this.model.set({ CategoryId : undefined, VendorId : undefined })
            },
            searchOffices: function () {
                this.showLoadingAll(this.collection);
                this.collection.searchOffices(this.model.toJSON());
            },
            officesLoaded: function () {

                this.$('#divVendorResult').html(this.templateCount(this.collection.length));

                this.trigger('offices-loaded', { offices: this.collection, city: this.model.get('StateProvinceId'), filter : this.model.toJSON() });
            },
            render: function () {
                this.stickThem();
            }
        });

        return SearchVendorsFilterView;
    });