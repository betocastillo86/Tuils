define(['underscore', 'backbone'], function (_, Backbone) {
    var SearchVendorModel = Backbone.Model.extend({

        baseUrl: "/api/vendors/addresses",

        url: "/api/vendors/addresses",

        validation: {
            CategoryId: {
                required: false
            },
            VendorId: {
                required:false,
            },
            SubTypeId: {
                required: false
            },
            StateProvinceId: {
                required:true
            }
        }
    });

    return SearchVendorModel;
});