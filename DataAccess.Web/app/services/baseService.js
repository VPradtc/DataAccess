'use strict';
angular.module('DataAccessApp').service('baseService',
['$http', 'promiseFactory', 'urlBuilder','loadingService', BaseService]);
function BaseService($http, promiseFactory, urlBuilder, loadingService) {

    return {
        callGet: function (action, params, loader) {

            var loaderId = loader || 'main';

            var defObj = promiseFactory.defer();

            loadingService.startLoading(loaderId);

            var actionQuery = urlBuilder.build(action, params);

            $http.get(actionQuery, {
            }).success(
                    function (response) {

                        defObj.resolve(response);
                    })
                .error(function (response) {
                    return defObj.reject(response);
                })
                .finally(function () {
                    loadingService.stopLoading(loaderId);
                });

            return defObj.promise;
        },
        callPost: function (action, data, loader) {

            var loaderId = loader || 'main';

            var defObj = promiseFactory.defer();

            loadingService.startLoading(loaderId);

            $http.post(action, data).success(
                    function (response) {

                        defObj.resolve(response);
                    })
                .error(function (response) {
                    return defObj.reject(response);
                })
                .finally(function () {
                    loadingService.stopLoading(loaderId);
                });

            return defObj.promise;
        },
        callDelete: function (action) {

            var defObj = promiseFactory.defer();

            $http.delete(action).success(
                    function (response) {

                        defObj.resolve(response);
                    })
                .error(function (response) {
                    return defObj.reject(response);
                });

            return defObj.promise;
        },
        getById: function (action, id) {

            return this.callGet(action + id);
        }
    };
}

