
define(['jquery', 'underscore', 'baseView'],
    function ($, _, BaseView) {

        var AboutUsView = BaseView.extend({
            initialize: function () {
                this.loadLikes();
            },
            loadLikes: function () {
                var that = this;
                $.ajax({
                    url: 'https://api.facebook.com/method/fql.query?query=select%20like_count%20from%20link_stat%20where%20url=%27https://www.facebook.com/mototuils%27&format=json',
                })
                .done(function (resp) {
                    that.$('#spanFollowers').html(Math.ceil((resp[0].like_count+1000)/100)*100);
                });
            },
            render: function () {
                return this;
            }
        });

        return AboutUsView;
    });



