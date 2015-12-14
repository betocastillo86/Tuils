
define(['jquery', 'underscore', 'baseView', 'configuration', 'util', 'handlebars', 'extensionNumbers'],
    function ($, _, BaseView, TuilsConfiguration, TuilsUtil, Handlebars) {

        var SummaryView = BaseView.extend({
            events: {
                "click .btnFinish": "save",
                "click .btnBack": "back"
            },
            errors: {
                'PhoneNumber' : 'El número de contacto es obligatorio'
            },
            //bindings: {
            //    "PhoneNumber" : "#PhoneNumber"
            //},
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
                this.model.on("change", this.loadControls, this);
            },
            render: function () {
                this.$el.html(this.template({ Images: this.images != undefined ? this.images.toJSON() : undefined, Properties: this.productProperties }));

                if (this.productType == TuilsConfiguration.productBaseTypes.service)
                    this.$("#divImageSummary").hide();

                return this;
            },
            loadControls: function (args)
            {

                if (args.product)
                    this.model = args.product;
                if(args.images)
                    this.images = args.images;

                this.loadFields();
                this.render();
            },
            loadFields: function (args) {
                this.productProperties = new Array();

                pushProperty(this, 'Name');


                if (this.model.get('Price'))
                    this.productProperties.push({ name: this.model.labels.Price, value: this.model.get('Price').toPesos() });
                

                if (this.productType == TuilsConfiguration.productBaseTypes.product) {
                    pushProperty(this, 'ManufacturerId', true);
                }
                else if (this.productType == TuilsConfiguration.productBaseTypes.bike) {
                    pushProperty(this, "CarriagePlate");
                    //pushProperty(this, "Condition", true);
                    //pushProperty(this, "Color", true);
                    this.productProperties.push({ name: this.model.labels.Kms, value: parseInt(this.model.get('Kms')).toKms() + ' Kms.' });
                    pushProperty(this, "Year");
                    //pushProperty(this, "Kms");
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
                if (this.productType != TuilsConfiguration.productBaseTypes.bike)
                    this.productProperties.push({ name: this.model.labels['IsShipEnabled'], value: this.model.get('IsShipEnabled') ? 'Si' : 'No' });

                //if (this.model.get('IsShipEnabled'))
                //{
                //    this.productProperties.push({ name: this.model.labels.AdditionalShippingCharge, value: this.model.get('AdditionalShippingCharge').toPesos() });
                //    if (this.model.get('DetailShipping'))
                //        pushProperty(this, 'DetailShipping');
                //}
                

                //this.productProperties.push({ name: 'Fecha Cierre Publicación', value: TuilsConfiguration.catalog.limitDaysOfProductPublished + ' dias' });
                this.productProperties.push({ name: 'Categoría', value: TuilsUtil.toStringWithSeparator(this.model.get('breadCrumb'), ' > ') });

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
            validateForm: function () {
                //Realiza la validación manual, sin bindings porque el telefóno solo es necesario en el ultimo paso
                this.removeErrors();
                var phoneNumber = this.$("#PhoneNumber").val();
                if (phoneNumber.length > 6)
                {
                    this.model.set('PhoneNumber', phoneNumber, {silent:true});
                    return true;
                }
                else
                {
                    this.markErrorsOnForm(this.errors, { "PhoneNumber": "#PhoneNumber" });
                    return false;
                }
            },
            save: function () {
                if (!this.validateForm())
                    return;
                else if (this.$("#chkConditions").is(":checked")) {
                    this.switchButtonBar(false);
                    this.trigger("summary-save");
                }
                else {
                    this.alert("Para publicar tu anuncio debes aceptar nuestros Términos y Condiciones.");
                }
            },
            back: function () {
                this.trigger("summary-back");
            }
        });


        return SummaryView;
    });