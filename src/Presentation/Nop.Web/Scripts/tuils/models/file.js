define([ 'underscore', 'backbone'], function ( _, Backbone) {

    var FileModel = Backbone.Model.extend({
        baseUrl: '/api/files',

        url: '/api/files',

        idAttribute: 'fileGuid',

        initialize: function () {

        },
        upload: function (args) {

            var saveUrl = '/api/files/upload';
            if (args && args.saveUrl)
                saveUrl = args.saveUrl;


            var file = this.get('file');
            if (file) {
                var data = new FormData();
                data.append('file', file);

                var context = this;

                $.ajax({
                    url: saveUrl,
                    data: data,
                    cache: false,
                    contentType: false,
                    processData: false,
                    type: 'POST',
                    success: function (data) {
                        context.fileUploaded(data);
                    },
                    error: function (data) {
                        context.errorUploaded(data.responseJSON);
                    }
                });
            }

        },
        fileUploaded: function (resp) {
            this.set('guid', resp.fileGuid);
            this.trigger("file-saved", this);
        },
        errorUploaded: function (resp) {
            this.trigger("file-error", resp);
        },
        remove: function () {
            this.url = this.baseUrl + '/' + this.removeExtension(this.get('fileGuid'));
            this.destroy();
        },
        removeExtension: function (fileName)
        {
            var nameSplit = fileName.split('.');
            var noExtension = "";
            for (var i = 0; i < nameSplit.length - 1; i++) {
                noExtension = noExtension + nameSplit[i];
            }
            return noExtension;
        }


    });

    return FileModel;
});

