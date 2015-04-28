define(['underscore', 'baseView'],
    function (_, BaseView) {

        var VendorHeaderView = BaseView.extend({

            events: {
                "click #btnEditVendorHeader" : "editEnabled"
            },

            id: 0,

            allowEdit : false,

            initialize: function (args) {
                this.id = args.id;
                this.allowEdit = args.allowEdit;
            },
            editEnabled: function ()
            {
                this.switchEnabled(true);
            },
            switchEnabled: function (editing)
            {
                this.$(".tit-perfil h2").css("display", editing ? "none" : "block");
                this.$(".pr-perfil p").css("display", editing ? "none" : "block");
                this.$(".tit-perfil #Name").css("display", !editing ? "none" : "block");
                this.$(".pr-perfil #Description").css("display", !editing ? "none" : "block");
            }

        });

        return VendorHeaderView;
    });