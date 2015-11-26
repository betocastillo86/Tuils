define(['jquery', 'underscore', 'baseView', 'baseModel', 'resources', 'configuration'],
    function ($, _, BaseView, BaseModel, TuilsResources, TuilsConfiguration) {
        var ShowPlansView = BaseView.extend({
            currentTab: undefined,

            selectedPlan : undefined,

            events: {
                'click nav.menu_planes a': 'changeTab',
                'click #btnGetPlanVendor': 'authToVendorPlans',
                'click .selectNewPlan': 'selectPlan',
                'click .linkPlanSpecDescription': 'showSpecDescription'
            },
            initialize: function (args) {
                this.on('user-authenticated', this.redirectVendorPlans, this);
                var defaultTab = args.tab ? args.tab : 'personas';
                this.activeTab(defaultTab);
                this.model = new BaseModel();
                this.render();
            },
            changeTab: function (obj) {

                this.activeTab($(obj.target).attr('for') == 'plan_personas' ? 'personas' : 'empresas');
            },
            authToVendorPlans: function () {
                this.model.set('ga_action', 'ComprarPlanTienda');
                //agrega la propiedad para que por defecto el registro sea para tiendas
                this.model.set('default_reg', 'empresas');
                this.validateAuthorization();
                this.showLogin(this.model);
            },
            redirectVendorPlans: function (model) {
                if (model.get('VendorType') == 1) {
                    var selectedPlan = this.$('input[name="SelectedPlan"]:checked').length > 0 ? '?plan=' + this.$('input[name="SelectedPlan"]:checked').val() : '';
                    document.location.href = '/mis-productos/seleccionar-plan' + selectedPlan;
                }
                else {
                    this.alert({ message: TuilsResources.loginMessages.getPlanMarketLikeUserError, duration: 10000 });
                    this.activeTab('personas');
                }

            },
            showSpecDescription: function (obj) {
                var description = $(obj.currentTarget).attr('data-description');
                this.alert(description);
            },
            selectPlan: function (obj) {
                var plan = $(obj.currentTarget).attr('data-id');
                this.selectedPlan = plan;
                this.$("#selectedPlan_" + plan).click();
                this.$("[data-sel='" + plan + "']").addClass("selected");
                this.$("[data-sel!='" + plan + "']").removeClass("selected");
                //Deshabilita el boton de comprar plan cuando es gratis
                var btnBuy = this.$('a.btn_continue[for="show_plan_' + this.currentTab + '"]');
                if(btnBuy.length > 0)
                    btnBuy.css('display', this.isFreePlan() ? 'none' : 'block');
            },
            activeTab: function (tab) {
                this.currentTab = tab;
                var previuosTab = tab == 'personas' ? 'empresas' : 'personas';
                this.$("[for='show_plan_" + tab + "']").show();
                this.$("[for='show_plan_" + previuosTab + "']").hide();

                this.$("a[for='plan_" + tab + "']").addClass('active');
                this.$("a[for='plan_" + previuosTab + "']").removeClass('active');

                Backbone.history.navigate('planes/' + tab);
            },

            isFreePlan: function () {
                return this.selectedPlan == TuilsConfiguration.plan.planProductsFree || this.selectedPlan == TuilsConfiguration.plan.planStoresFree;
            },
            render: function () {
                return this;
            }
        });

        return ShowPlansView;
    });