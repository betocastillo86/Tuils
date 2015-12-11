define(['jquery', 'underscore', 'baseView'],
    function ($, _, BaseView) {
        var PublishView = BaseView.extend({

            initialize: function (args) {
                this.hidePublishButton();
                this.render();
            },
            render: function () {
                return this;
            }
        });

        return PublishView;
    });