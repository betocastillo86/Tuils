define(['canvasBlob'], function () {
    window.resize = (function () {

        'use strict';

        function Resize() {
            //
        }

        Resize.prototype = {

            init: function (outputQuality) {
                this.outputQuality = (outputQuality === 'undefined' ? 0.8 : outputQuality);
            },

            photo: function (file, maxSize, outputType, callback) {

                var _this = this;

                var reader = new FileReader();
                reader.onload = function (readerEvent) {
                    _this.resize(readerEvent.target.result, maxSize, outputType, callback);
                }
                reader.readAsDataURL(file);

            },

            photoCrop: function (file, maxSize, outputType, callback) {

                var _this = this;

                var reader = new FileReader();
                reader.onload = function (readerEvent) {
                    _this.crop(readerEvent.target.result, maxSize, outputType, callback);
                }
                reader.readAsDataURL(file);

            },

            resize: function (dataURL, maxSize, outputType, callback) {

                var _this = this;

                var image = new Image();
                image.onload = function (imageEvent) {

                    // Resize image
                    var canvas = document.createElement('canvas'),
                        width = image.width,
                        height = image.height;
                    if (width > height) {
                        if (width > maxSize) {
                            height *= maxSize / width;
                            width = maxSize;
                        }
                    } else {
                        if (height > maxSize) {
                            width *= maxSize / height;
                            height = maxSize;
                        }
                    }
                    canvas.width = width;
                    canvas.height = height;
                    canvas.getContext('2d').drawImage(image, 0, 0, width, height);

                    _this.output(canvas, outputType, callback);

                }
                image.src = dataURL;

            },

            crop: function (dataURL, maxSize, outputType, callback) {

                var _this = this;

                var image = new Image();
                image.onload = function (imageEvent) {

                    // Resize image
                    var canvas = document.createElement('canvas'),
                        width = image.width,
                        height = image.height;

                    var sourceWidth = 0;
                    var sourceHeight = 0;

                    if (width > height)
                        sourceWidth = sourceHeight = height;
                    else
                        sourceWidth = sourceHeight = width;

                    var sourceX = (width - sourceWidth) / 2;
                    var sourceY = (height - sourceHeight) / 2;


                    var destWidth = sourceWidth;
                    var destHeight = sourceHeight;
                    var destX = 0;
                    var destY = 0;

                    canvas.width = sourceWidth;
                    canvas.height = sourceHeight;
                    //canvas.getContext('2d').drawImage(image, 0, 0, width, height);
                    canvas.getContext('2d').drawImage(image, sourceX, sourceY, sourceWidth, sourceHeight, destX, destY, destWidth, destHeight);

                    _this.output(canvas, outputType, callback);

                }
                image.src = dataURL;

            },

            output: function (canvas, outputType, callback) {

                switch (outputType) {

                    case 'file':
                        canvas.toBlob(function (blob) {
                            callback(blob);
                        }, 'image/jpeg', 0.8);
                        break;

                    case 'dataURL':
                        callback(canvas.toDataURL('image/jpeg', 0.8));
                        break;

                }

            }

        };

        return Resize;

    }());
});
