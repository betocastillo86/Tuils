
define(['jquery', 'underscore', 'baseView', 'tuils/views/landing/suscriptionStoresView'],
    function ($, _, BaseView, SuscriptionStoresView) {

        var LandingStoresView = BaseView.extend({
            type : undefined,
            initialize: function (args) {
                this.type = args.type;
                this.viewSuscription = new SuscriptionStoresView({el : '#formLanding', type : args.type });
                this.loadLikes();
            },
            loadLikes: function () {
                /*var that = this;
                $.ajax({
                    url: 'https://api.facebook.com/method/fql.query?query=select%20like_count%20from%20link_stat%20where%20url=%27https://www.facebook.com/mototuils%27&format=json',
                })
                .done(function (resp) {
                    that.$('#spanFollowers').html(Math.ceil((resp[0].like_count + 1000) / 100) * 100);
                });*/
            },
            render: function () {
                return this;
            }
        });

        return LandingStoresView;
    });