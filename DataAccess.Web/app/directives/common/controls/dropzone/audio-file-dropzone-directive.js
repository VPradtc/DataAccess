'use strict'
angular.module('DataAccessApp').directive('ixAudioFileDropzone', function () {
    return {
        restrict: 'EA',
        scope: {
            url: '<'
        },
        templateUrl: 'app/views/common/controls/dropzone.html',
        controller: ['$scope', AudioFileDropzoneController],
    };
});

function AudioFileDropzoneController($scope) {

    var vm = $scope;

    vm.maxFiles = 10000;
    vm.acceptedFiles = 'audio/x-wav,audio/wav';
}
