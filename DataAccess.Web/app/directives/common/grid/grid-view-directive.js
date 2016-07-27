'use strict'
angular.module('DataAccessApp').directive('ixGridView', function () {
    return {
        restrict: 'EA',
        scope: {
            config: '<',
        },
        templateUrl: 'app/views/common/grid/grid-view.html',
        controller: 'gridViewController',
    };
});
