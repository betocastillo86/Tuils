define(['jquery', 'underscore', 'baseView', 'configuration', 'tuils/models/orderPlan'],
    function ($, _, BaseView, TuilsConfiguration, OrderPlanModel) {
        var SelectPlanView = BaseView.extend({
            productId : 0,
            events: {
                'click #btnPay' : 'createOrder',
                'click .selectNewPlan' : 'selectPlan'
            },
            bindings: {
                'input[name="SelectedPlan"]': {
                    observe: 'PlanId',
                    preBind : false
                },
                '#ddlStateProvinceId': 'StateProvinceId',
                '#CustomerAddressInformation_Address': 'Address',
                '#CustomerAddressInformation_City': 'City',
                '#CustomerAddressInformation_PhoneNumber': 'PhoneNumber',
                '#CustomerAddressInformation_AddressId': 'AddressId'
            },
            initialize: function (args) {
                this.productId = args.id;
                //Carga los datos de la orden con el ide del producto que se va a destacar
                this.model = new OrderPlanModel({ ProductId: this.productId });
                this.model.on('change:PlanId', this.changePlan, this);
                this.model.on('error', this.errorCreatingOrder, this);
                this.model.on('sync', this.successCreatingOrder, this);

                var preselectedPlan = parseInt($("input[name='SelectedPlan']:checked").val());
                if (!isNaN(preselectedPlan))
                    this.model.set('PlanId', preselectedPlan);
                
                this.render();
            },
            selectPlan: function (obj) {
                var plan = $(obj.currentTarget).attr('data-id');
                this.$("#selectedPlan_" + plan).click();
                this.$("[data-sel='" + plan + "']").addClass("selected");
                this.$("[data-sel!='" + plan + "']").removeClass("selected");
                this.scrollFocusObject('#divAdditionalInfo', -200);
                
            },
            changePlan: function (obj) {
                var isFree = this.isFreePlan();
                this.$("#divAdditionalInfo").css('display', isFree ? 'none' : 'block');
                this.$("#btnContinue").css('display', isFree ? 'block' : 'none');
                this.$("#btnPay").css('display', isFree ? 'none' : 'block');
            },
            createOrder: function () {
                var errors = this.validateControls();
                if (this.model.isValid())
                {
                    this.model.newOrder();
                }
            },
            successCreatingOrder: function (resp) {
                this.$("#merchantId").val(resp.get('MerchantId'));
                this.$("#referenceCode").val(resp.get('ReferenceCode'));
                this.$("#amount").val(resp.get('Amount'));
                this.$("#accountId").val(resp.get('AccountId'));
                this.$("#signature").val(resp.get('Signature'));
                this.$("#currency").val(resp.get('Currency'));
                this.$("#responseUrl").val(resp.get('ResponseUrl'));
                this.$("#confirmationUrl").val(resp.get('ConfirmationUrl'));
                this.$("#description").val(this.$('input[name="SelectedPlan"]:checked').attr('data-name'));
                this.$("form").attr("action", resp.get('UrlPayment'));
                this.$("form").submit();
            },
            errorCreatingOrder: function (resp, err) {
                if (err.responseJSON.ExceptionMessage)
                    this.alert(err.responseJSON.ExceptionMessage);
                else if (err.responseJSON.Message)
                    this.alert(err.responseJSON.Message);
            },
            isFreePlan : function(){
                var selectedPlan = this.model.get('PlanId');
                return selectedPlan == TuilsConfiguration.plan.planProductsFree || selectedPlan == TuilsConfiguration.plan.planStoresFree;
            },
            render: function () {
                this.stickThem(true);
                this.bindValidation();
                return this;
            }
        });

        return SelectPlanView;
    });