'use strict'
angular.module('DataAccessApp').directive('ixBreadcrumb', function () {
    return {
        restrict: 'EA',
        scope: {
            nodes: '<',
        },
        templateUrl: 'app/views/common/breadcrumb.html',
        controller: ['$scope', BreadcrumbController],
    };
});

function BreadcrumbController($scope) {

    var ctrl = this;
    var vm = $scope;
}
