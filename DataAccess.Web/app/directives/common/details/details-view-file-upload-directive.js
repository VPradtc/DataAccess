'use strict'
angular.module('DataAccessApp').directive('ixDetailsViewFileUpload', function () {
    return {
        restrict: 'EA',
        scope: {
            options: '<',
            model: '<',
            fileUploadUrl: '<',
        },
        templateUrl: 'app/views/common/details/details-view-file-upload.html',
        controller: ['$scope', DetailsViewFileUploadController]
    };
});

function DetailsViewFileUploadController($scope) {

    var vm = $scope;

    var _dropzoneTypeMapping = {
        'metadata': 'ixMetadataFileDropzone',
        default: 'ixAudioFileDropzone'
    };

    var _createDropzoneDirective = function (fileType) {
        var directive = _dropzoneTypeMapping[fileType] || _dropzoneTypeMapping.default;

        return directive;
    }

    var _init = function () {
        var _dropzoneDirective = _createDropzoneDirective(vm.options.fileType);

        vm.dropzoneAttrs = {};
        vm.dropzoneAttrs[_dropzoneDirective] = '';
    }

    _init();
}