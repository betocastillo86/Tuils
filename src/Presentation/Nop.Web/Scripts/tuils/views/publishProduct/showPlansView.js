define(['jquery', 'underscore', 'baseView'],
    function ($, _, BaseView, TuilsConfiguration, OrderPlanModel) {
        var ShowPlansView = BaseView.extend({
            
            events: {
                'click nav.menu_planes a' : 'changeTab'
            },
            initialize: function (args) {
                
                var defaultTab = args.tab ? args.tab : 'personas';
                this.activeTab(defaultTab);
                this.render();
            },
            changeTab: function (obj) {
                this.activeTab($(obj.target).attr('for') == 'plan_personas' ? 'personas' : 'empresas');
            },
            activeTab : function(tab)
            {
                var previuosTab = tab == 'personas' ? 'empresas' : 'personas';
                this.$("[for='show_plan_" + tab + "']").show();
                this.$("[for='show_plan_" + previuosTab + "']").hide();

                this.$("a[for='plan_" + tab + "']").addClass('active');
                this.$("a[for='plan_" + previuosTab + "']").removeClass('active');

                Backbone.history.navigate('planes/'+tab);
            },
            render: function () {
                return this;
            }
        });

        return ShowPlansView;
    });