'use strict'
angular.module('DataAccessApp').factory('detailsStrategyFactory',
[
    'editDetailsStrategy',
    'createDetailsStrategy',
    'loadingService',
    '$state',
    DetailsStrategyFactory]);

function DetailsStrategyFactory(
    editDetailsStrategy,
    createDetailsStrategy,
    loadingService,
    $state) {

    var _stateStrategyMap = {
        'edit': editDetailsStrategy,
        'create': createDetailsStrategy,
    };

    var _stopLoading = function () {
        $loading.stop('main');
    };

    var _logError = function (error) {
        console.log(error.exceptionMessage);
    };

    var _goBack = function (options) {
        $state.go(options.backState);
    };

    var _init = function (options) {
        _stateStrategyMap[options.mode].init(options);
    }

    var _cancel = function (options) {
        _goBack(options);
    }

    var _save = function (options) {
        loadingService.startLoading('main');

        _stateStrategyMap[options.mode].save(options)
            .success(function (response) {
                _goBack(options);
            }).
            error(function (error) {
                _logError(error);
            })
            .finally(function () {
                loadingService.stopLoading('main');
            });
    }

    var _create = function (options) {

        return {
            init: function () { _init(options); },
            save: function () { _save(options); },
            cancel: function () { _cancel(options); },
        };
    };

    return {
        create: _create,
    };
}
