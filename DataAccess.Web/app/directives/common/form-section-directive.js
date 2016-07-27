'use strict'
angular.module('DataAccessApp').directive('ixFormSection', function () {
    return {
        restrict: 'EA',
        scope: {
            fields: '<',
            model: '<',
        },
        templateUrl: 'app/views/common/form-section.html',
        controller: [
            '$scope',
            FormSectionController
        ],
    };
});

function FormSectionController($scope) {

    var vm = $scope;
}
