'use strict'
angular.module('DataAccessApp').directive('ixDropzone', ['authService', DropzoneDirective]);

function DropzoneDirective(authService) {

    var _url = "";

    var _getUrl = function () {
        return _url;
    }

    var _lastXhr = undefined;

    return function (scope, element, attrs) {

        attrs.$observe('url', function (value) {
            _url = value;
        });

        var maxFiles = attrs.maxFiles;
        var acceptedFiles = attrs.acceptedFiles;

        element.dropzone({
            url: _getUrl,
            maxFilesize: 500,
            maxFiles: maxFiles,
            acceptedFiles: acceptedFiles,
            uploadMultiple: true,
            parallelUploads: 100,
            maxThumbnailFilesize: 500,
            autoProcessQueue: true,
            addRemoveLinks: true,
            previewTemplate: '<div class="dz-preview dz-file-preview">' +
                    '<div class="dz-details">' +
                        '<div class="dz-filename"><span data-dz-name></span></div>' +
                        '<div class="dz-size" data-dz-size></div>' +
                        '<img data-dz-thumbnail />' +
                    '</div>' +
                    '<div class="dz-progress"><span class="dz-upload" data-dz-uploadprogress></span></div>' +
                    '<div class="dz-success-mark"><span>✔</span></div>' +
                    '<div class="dz-error-mark"><span>✘</span></div>' +
                    '<div class="dz-error-message"><span data-dz-errormessage></span></div>' +
                '</div>',
            sending: function (file, xhr) {
                if (xhr === _lastXhr) {
                    return;
                }

                _lastXhr = xhr;

                var token = authService.authentication.token;
                xhr.setRequestHeader("Authorization", "Bearer " + token);
            },
            error: function (file, response) {

                var message = typeof (response) === "string"
                    ? response
                    : response.message;

                file.previewElement.classList.add("dz-error");
                var ref = file.previewElement.querySelectorAll("[data-dz-errormessage]");
                var results = [];
                for (var i = 0, _len = ref.length; i < _len; i++) {
                    var node = ref[i];
                    results.push(node.textContent = message);
                }
                return results;
            },
        });
    };
}
