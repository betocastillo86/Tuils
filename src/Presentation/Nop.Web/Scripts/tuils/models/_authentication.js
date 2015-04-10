define(['backbone'], function (Backbone) {

    function wrapBackboneError(model, options) {
        var error = options.error;

        options.error = function (response) {
            if (response.status === 403) {
                model.trigger("unauthorized", model);
            } else {
                if (error) error(response);
            }
        };
    }

    var AuthenticationModel = Backbone.Model.extend({
        sync: function (method, model, options) {
            wrapBackboneError(model, options);
            Backbone.Model.prototype.sync.apply(this, arguments);
        }
    });

    return AuthenticationModel;
})




