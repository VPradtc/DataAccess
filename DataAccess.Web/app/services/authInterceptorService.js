'use strict';
angular.module('DataAccessApp').factory('authInterceptorService',
['$q', '$location', '$localStorage', '$injector', AuthInterceptorService]);

function AuthInterceptorService($q, $location, $localStorage, $injector) {

    var authInterceptorService = {};

    var _refreshTokenInProgress = false;
    var _pendingRequests = [];

    var _request = function (config) {
        config.headers = config.headers || {};

        var authData = $localStorage.authorizationData;
        if (authData) {
            config.headers.Authorization = 'Bearer ' + authData.token;
        }

        return config;
    }

    var _responseError = function (rejection) {
        var deferred = $q.defer();

        var authService = $injector.get('authService');

        if (rejection.status !== 401) {

            deferred.reject(rejection);
            return deferred.promise;
        }

        var authData = $localStorage.authorizationData;

        if (!(authData && authData.useRefreshTokens)) {

            _redirectToLogin(rejection, deferred);
            return deferred;
        }

        _pendingRequests.push({
            data: rejection.config,
            deferred: deferred
        });

        if (_refreshTokenInProgress) {
            return deferred.promise;
        }

        _refreshTokenInProgress = true;

        authService.refreshToken().then(function (response) {

            _refreshTokenInProgress = false;

            for (var i = 0; i < _pendingRequests.length; i++) {
                _retryHttpRequest(_pendingRequests[i]);
            }

            _pendingRequests = [];
        }, function () {
            _refreshTokenInProgress = false;
            _redirectToLogin(rejection, deferred);
        });

        return deferred.promise;
    }

    var _redirectToLogin = function (rejection, deferred) {

        var authService = $injector.get('authService');
        authService.logout();
        $injector.get('$state').go('app.auth.login');
        deferred.reject(rejection);
    }

    var _retryHttpRequest = function (config) {
        var $http = $injector.get('$http');
        $http(config.data).then(function (response) {
            config.deferred.resolve(response);
        });
    }

    authInterceptorService.request = _request;
    authInterceptorService.responseError = _responseError;

    return authInterceptorService;
}
