
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

                pushProperty(this, 'Name');
                this.productProperties.push({ name: this.model.labels.Price, value: accounting.formatMoney(this.model.get('Price'), { precision: 0 }) });


                if (this.productType == TuilsConfiguration.productBaseTypes.product) {
                    pushProperty(this, 'ManufacturerId', true);
                }
                else if (this.productType == TuilsConfiguration.productBaseTypes.bike)
                {
                    pushProperty(this,"CarriagePlate");
                    pushProperty(this, "Condition", true);
                    pushProperty(this, "Color", true);
                    pushProperty(this, "Year");
                    pushProperty(this, "Kms");
                    this.productProperties.push({ name: this.model.labels.Accesories, value: TuilsUtil.toStringWithSeparator(this.model.get('AccesoriesName'), ',') });
                    this.productProperties.push({ name: this.model.labels.Negotiation, value: TuilsUtil.toStringWithSeparator(this.model.get('NegotiationName'), ',') });
                }

                this.productProperties.push({ name: 'Fecha Cierre Publicacion', value: '30 dias' });
                this.productProperties.push({ name: 'Categoria', value: TuilsUtil.toStringWithSeparator(args.breadCrumb, ' > ') });

                function pushProperty(ctx, field, isName) {
                    ctx.productProperties.push({ name: ctx.model.labels[field] ? ctx.model.labels[field] : field, value: ctx.model.get(field + (isName ? 'Name' : '')) });
                }
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