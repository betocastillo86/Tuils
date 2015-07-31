define(['jquery', 'underscore', 'baseView'],
    function ($, _, BaseView) {
        var PublishView = BaseView.extend({

            initialize: function (args) {
                if (this.isMobile())
                    $('.btn_anuncia').hide();
                this.render();
            },
            render: function () {
                return this;
            }
        });

        return PublishView;
    });