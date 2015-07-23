
define(['jquery', 'underscore', 'backbone', 'configuration', 'util', 'handlebars', 'extensionNumbers'],
    function ($, _, Backbone, TuilsConfiguration, TuilsUtil, Handlebars) {

        var SummaryView = Backbone.View.extend({
            events: {
                "click .btnFinish": "save",
                "click .btnBack": "back"
            },
            

            productType: undefined,
            btnFinish:undefined,
            //Propiedades que se van a mostrar en el resumen, esto depende del tipo de producto
            productProperties: undefined,

            initialize: function (args) {
                this.template= Handlebars.compile($("#templateSummary").html());
                this.productType = args.productType;
                this.loadControls(args);
                this.model.on("error", this.showButtonBar, this);
                this.model.on("unauthorized", this.showButtonBar, this);
            },
            render: function () {
                this.$el.html(this.template({ Images: this.images != undefined ? this.images.toJSON() : undefined, Properties: this.productProperties }));
                
                return this;
            },
            loadControls: function (args)
            {
                this.model = args.product;
                this.images = args.images;
                this.loadFields({ breadCrumb: args.breadCrumb });
                this.render();
            },
            loadFields: function (args) {
                this.productProperties = new Array();

                pushProperty(this, 'Name');
                
                this.productProperties.push({ name: this.model.labels.Price, value: this.model.get('Price').toPesos() });


                if (this.productType == TuilsConfiguration.productBaseTypes.product) {
                    pushProperty(this, 'ManufacturerId', true);
                }
                else if (this.productType == TuilsConfiguration.productBaseTypes.bike) {
                    pushProperty(this, "CarriagePlate");
                    pushProperty(this, "Condition", true);
                    pushProperty(this, "Color", true);
                    pushProperty(this, "Year");
                    pushProperty(this, "Kms");
                    this.productProperties.push({ name: this.model.labels.Accesories, value: TuilsUtil.toStringWithSeparator(this.model.get('AccesoriesName'), ',') });
                    this.productProperties.push({ name: this.model.labels.Negotiation, value: TuilsUtil.toStringWithSeparator(this.model.get('NegotiationName'), ',') });
                }
                else {
                    pushProperty(this, "IncludeSupplies", true);
                    pushProperty(this, "Supplies", true);
                    
                    if (!this.model.get('IncludeSupplies'))
                        this.productProperties.push({ name: this.model.labels.SuppliesValue, value: this.model.get('SuppliesValue').toPesos() });
                }

                
                //Muestra los valores de envio
                pushProperty(this, "IsShipEnabled", true);

                //if (this.model.get('IsShipEnabled'))
                //{
                //    this.productProperties.push({ name: this.model.labels.AdditionalShippingCharge, value: this.model.get('AdditionalShippingCharge').toPesos() });
                //    if (this.model.get('DetailShipping'))
                //        pushProperty(this, 'DetailShipping');
                //}
                

                this.productProperties.push({ name: 'Fecha Cierre Publicacion', value: TuilsConfiguration.catalog.limitDaysOfProductPublished + ' dias' });
                this.productProperties.push({ name: 'Categoria', value: TuilsUtil.toStringWithSeparator(args.breadCrumb, ' > ') });

                function pushProperty(ctx, field, isName) {
                    ctx.productProperties.push({ name: ctx.model.labels[field] ? ctx.model.labels[field] : field, value: ctx.model.get(field + (isName ? 'Name' : '')) });
                }
            },
            showButtonBar : function()
            {
                this.switchButtonBar(true);
            },
            switchButtonBar: function (show) {
                this.$("#buttonsBar input[type='button']").prop("disabled", !show);
            },
            save: function () {
                if (this.$("#chkConditions").is(":checked")) {
                    this.switchButtonBar(false);
                    this.trigger("summary-save");
                }
                else {
                    alert("Debes aceptar terminos y condiciones");
                }
            },
            back: function () {
                this.trigger("summary-back");
            }
        });


        return SummaryView;
    });