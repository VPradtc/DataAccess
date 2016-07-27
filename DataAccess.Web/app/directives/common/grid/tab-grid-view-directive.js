'use strict'
angular.module('DataAccessApp').directive('ixTabGridView', function () {
    return {
        restrict: 'EA',
        scope: {
            config: '<',
        },
        templateUrl: 'app/views/common/grid/tab-grid-view.html',
        controller: 'gridViewController',
    };
});
