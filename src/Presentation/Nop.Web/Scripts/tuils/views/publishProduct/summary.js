
define(['jquery', 'underscore', 'backbone', 'configuration', 'util', 'handlebars', 'accounting'],
    function ($, _, Backbone, TuilsConfiguration, TuilsUtil, Handlebars, accounting) {

        var SummaryView = Backbone.View.extend({
            events: {
                "click .btnFinish": "save",
                "click .btnBack": "back"
            },
            template: Handlebars.compile($("#templateSummary").html()),

            productType: undefined,
            //Propiedades que se van a mostrar en el resumen, esto depende del tipo de producto
            productProperties: undefined,

            initialize: function (args) {
                this.model = args.product;
                this.images = args.images;
                this.productType = args.productType;
                this.loadFields({ breadCrumb: args.breadCrumb });
                this.render();
            },
            render: function () {
                this.$el.html(this.template({ Images: this.images.toJSON(), Properties: this.productProperties }));
                return this;
            },
            loadFields: function (args) {
                this.productProperties = new Array();

                this.productProperties.push({ name: 'Titulo', value: this.model.get('Name') });
                this.productProperties.push({ name: 'Valor', value: accounting.formatMoney(this.model.get('Price'), { precision : 0 }) });

                if (this.productType == TuilsConfiguration.productBaseTypes.product) {
                    this.productProperties.push({ name: 'Marca', value: this.model.get('ManufacturerName') });
                }

                this.productProperties.push({ name: 'Fecha Cierre Publicacion', value: '30 dias' });
                this.productProperties.push({ name: 'Categoria', value: TuilsUtil.toStringWithSeparator(args.breadCrumb, ' > ') });
            },
            save: function () {
                this.trigger("summary-save");
            },
            back: function () {
                this.trigger("summary-back");
            }
        });


        return SummaryView;
    });