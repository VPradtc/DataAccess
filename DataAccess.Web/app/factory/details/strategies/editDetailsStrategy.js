'use strict'
angular.module('DataAccessApp').factory('editDetailsStrategy',
[
    '$state',
    'loadingService',
    EditDetailsStrategy]);

function EditDetailsStrategy(
    $state,
    loadingService) {

    var _getEntityId = function () {
        return $state.params.id;
    }

    var _logError = function (error) {
        console.log(error.exceptionMessage);
    };

    var _init = function (options) {

        var id = _getEntityId();

        if (id === undefined) {
            console.log('Not found id');
            return;
        }

        loadingService.startLoading('main');

        options.serviceMethods.getById(id)
            .success(function (response) {
                angular.extend(options.entity, response);
            })
            .error(function (responseError) {
                _logError(responseError);
            })
            .finally(function () {
                loadingService.stopLoading('main');
            });
    };

    var _save = function (options) {
        return options.serviceMethods.edit(options.entity);
    }

    return {
        init: _init,
        save: _save,
    };
}
