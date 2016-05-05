
define(['jquery', 'underscore', 'baseView'],
    function ($, _, BaseView) {

        var ConfirmationView = BaseView.extend({


            initialize: function (args) {
                if (this.isMobile())
                    $('.btn_anuncia').hide();
            },
            
        });


        return ConfirmationView;
    });