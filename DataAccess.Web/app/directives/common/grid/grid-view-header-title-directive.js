'use strict'
angular.module('DataAccessApp').directive('ixGridViewHeaderTitle', function () {
    return {
        restrict: 'EA',
        scope: {
            options: '<',
        },
        templateUrl: 'app/views/common/grid/grid-view-header-title.html',
        controller: ['$scope', GridViewHeaderTitleController],
    };
});

function GridViewHeaderTitleController($scope) {

    var vm = $scope;
}