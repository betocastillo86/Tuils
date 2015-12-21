define(['jquery', 'underscore', 'baseView', 'handlebars'],
    function ($, _, BaseView, Handlebars) {

        var SearchVendorsFilterListView = BaseView.extend({
            events: {
                'click .office-box' : 'select'
            },
            bindings: {
                
            },
            templateList: undefined,
            selectedOffice : undefined,
            initialize: function (args) {
                this.loadControls();
            },
            loadControls: function (args) {
                this.templateList = Handlebars.compile($('#templateFilteredList').html());
            },
            showOffices: function (collection) {
                this.collection = collection;
                this.$el.html(this.templateList(this.collection));
                this.markSelectedOffice();
            },
            selectOffice: function (office) {
                this.selectedOffice = office;
                this.markSelectedOffice();
            },
            markSelectedOffice: function () {
                this.$('.office-box').removeClass('active').css('border', '');
                if (this.selectedOffice) {
                    this.$('.office-box[data-id="' + this.selectedOffice.id + '"]').addClass('active').css('border', 'solid 1px red');
                }
            },
            select: function (obj) {
                //Lanza el evento que la sede ha sido seleccionada ddesde la lista
                var id = parseInt(obj.currentTarget.attributes['data-id'].value);
                this.selectedOffice = _.findWhere(this.collection, { id: id });
                this.trigger('selected', this.selectedOffice);
                this.markSelectedOffice();
            },
            render: function () {
                return this;
            }
        });

        return SearchVendorsFilterListView;
    });