define(['jquery', 'underscore', 'baseView', 'storage'],
function ($, _, BaseView, TuilsStorage) {

    var VersusView = BaseView.extend({
        events: {
            'click #btnVersus': 'changeVersus'
        },
        initialize: function () {
            //TuilsStorage.loadBikeReferencesSameLevel(this.showBikeReferences, this);
        },
        changeVersus: function () {
            var slug1 = this.$('#ddlCategory1 option:selected').data('url') + '-' + this.$('#ddlYear1').val();
            var slug2 = this.$('#ddlCategory2 option:selected').data('url') + '-' + this.$('#ddlYear2').val();
            document.location.href = '/comparacion/'+slug1+'-versus-'+slug2;
        }
    });

    return VersusView;
});