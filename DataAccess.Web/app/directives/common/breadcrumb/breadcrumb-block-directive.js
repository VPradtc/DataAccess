'use strict'
angular.module('DataAccessApp').directive('ixBreadcrumbBlock', function () {
    return {
        restrict: 'EA',
        scope: {
            nodes: '<',
        },
        templateUrl: 'app/views/common/breadcrumb/breadcrumb-block.html',
        controller: ['$scope', BreadcrumbBlockController],
    };
});

function BreadcrumbBlockController($scope) {

    var ctrl = this;
    var vm = $scope;
}
