define(['underscore', 'backbone'],
    function (_, Backbone) {

        'use strict'

        var MyProductModel = Backbone.Model.extend({
            baseUrl: "/api/products",
            url: "/api/products",

            idAttribute: 'Id',

            initialize: function () {

            },
            enable: function () {
                this.url = this.baseUrl + '/' + this.get('Id') + '/enable';
                this.save();
            }
        });

        return MyProductModel;
    });



