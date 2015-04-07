var FileModel = Backbone.Model.extend({
	baseUrl : '/api/files',

	url: '/api/files',

	idAttribute: 'fileGuid',

	initialize: function () {

	},
	upload: function () {

	    var file = this.get('file');
	    if (file)
	    {
	        var data = new FormData();
	        data.append('file', file);

	        var context = this;

	        $.ajax({
	            url: '/api/files/upload',
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
	errorUploaded : function(resp)
	{
	    this.trigger("file-error", resp);
	},
	remove: function ()
	{
		this.url = this.baseUrl + '/' + this.get('fileGuid');
		this.destroy();
	}


});

var FileCollection = Backbone.Collection.extend({
    baseUrl: '/api/files',

    url: '/api/files'
});