'use strict';
angular.module('DataAccessApp').service('loadingService',
['$loading', LoadingService]);

function LoadingService($loading) {

    var _startLoading = function (loader) {

        if (loader !== undefined) {
            $loading.start(loader);
        };
    };

    var _stopLoading = function (loader) {

        if (loader !== undefined) {
            $loading.stop(loader);
        };
    };

    return {
        startLoading: _startLoading,
        stopLoading: _stopLoading,
    };
}
