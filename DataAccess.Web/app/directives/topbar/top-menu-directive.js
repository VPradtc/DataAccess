'use strict'
angular.module('DataAccessApp').directive('ixTopMenu', function () {
    return {
        restrict: 'EA',
        transclude: true,
        templateUrl: 'app/views/topbar/top-menu.html',
    };
});
