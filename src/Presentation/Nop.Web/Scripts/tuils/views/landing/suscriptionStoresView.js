
define(['jquery', 'underscore', 'baseView', 'tuils/models/landingSuscription'],
    function ($, _, BaseView, LandingSuscriptionModel) {

        var SuscriptionStoresView = BaseView.extend({
            events: {
                'click #btnSendLanding' : 'send'
            },
            bindings: {
                '#txtName': 'Name',
                '#txtEmail': 'Email',
                '#txtPhone': 'Phone',
                '#txtCompany' :'Company'
            },
            initialize: function (args) {
                this.model = new LandingSuscriptionModel({ Type: 'landing-' + args.type });
                this.model.on('sync', this.saved, this);
                this.model.on('error', this.errorSaving, this);
                this.render();
            },
            send: function () {
                this.validateControls();
                if(this.model.isValid())
                    this.model.save();
            },
            saved: function () {
                this.alert({ message : $('#templateModal').html(), afterClose : this.dispose, ctx : this });
            },
            errorSaving: function () {
                this.alertError('Ocurrió un error guardando, intenta de nuevo');
            },
            render: function () {
                Backbone.Validation.bind(this);
                this.stickThem();
                return this;
            }
        });

        return SuscriptionStoresView;
    });