'use strict';
angular.module('DataAccessApp').service('authService',
['$http', 'promiseFactory', 'ngUrlSettings', '$localStorage', AuthService]);
function AuthService($http, promiseFactory, ngUrlSettings, $localStorage) {
    var serverApiUri = ngUrlSettings.serverApiUri;
    var tokenUrl = ngUrlSettings.apiTokenUrl;
    var authServiceFactory = {};


    var resetAuthData = function () {
        authServiceFactory.authentication = {
            isAuthorized: false,
            token: "",
            role: "",
            id: "",
            useRefreshTokens: false
        };
    }

    resetAuthData();

    authServiceFactory.login = function (loginData) {
        var data = 'grant_type=password&userName=' + loginData.email + '&password=' + loginData.password;

        if (loginData.useRefreshTokens) {
            data = data + '&client_id=' + ngUrlSettings.clientId;
        }

        var deferred = promiseFactory.defer();

        $http.post(tokenUrl, data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
            .success(function (response) {
                if (loginData.useRefreshTokens) {
                    // adjust when refresh token will be done
                    $localStorage.authorizationData = {
                        id: response.id,
                        firstName: response.firstName,
                        lastName: response.lastName,
                        token: response.access_token,
                        role: JSON.parse(response.role),
                        refreshToken: response.refresh_token,
                        useRefreshTokens: true,
                        isAuthorized: true
                    };
                } else {
                    $localStorage.authorizationData =
                        {
                            token: response.access_token,
                            role: response.role,
                            id: response.id,
                            firstName: response.firstName,
                            lastName: response.lastName,
                            refreshToken: "",
                            useRefreshTokens: false,
                            isAuthorized: true
                        };
                }

                authServiceFactory.loadAuthenticationData();

                deferred.resolve(response);
            })
            .error(function (err) {
                deferred.reject(err);
            });

        return deferred.promise;
    };

    authServiceFactory.isAuthorized = function (handler) {
        return $http.get(serverApiUri + 'AuthenticationApi/IsAuthorized')
            .then(handler);
    };

    authServiceFactory.checkRole = function (roles) {
        var res = false;
        for (var i = 0; i < roles.length; ++i) {
            if (authServiceFactory.authentication.role.indexOf(roles[i]) > -1) {
                res = true;
                break;
            }
        }
        return res;
    }

    authServiceFactory.logout = function () {
        var deferred = promiseFactory.defer();

        authServiceFactory.clearAuthenticationData();

        deferred.resolve(true);

        return deferred.promise;
    };

    authServiceFactory.loadAuthenticationData = function () {

        var authData = $localStorage.authorizationData;

        if (authData) {
            authServiceFactory.authentication.id = authData.id;
            authServiceFactory.authentication.firstName = authData.firstName,
            authServiceFactory.authentication.lastName = authData.lastName,
            authServiceFactory.authentication.isAuthorized = authData.isAuthorized;
            authServiceFactory.authentication.token = authData.token;
            authServiceFactory.authentication.role = authData.role;
            authServiceFactory.authentication.useRefreshTokens = authData.useRefreshTokens;
        }

        return authServiceFactory.authentication;
    };

    authServiceFactory.clearAuthenticationData = function () {
        $localStorage.$reset();
        resetAuthData();
    };

    authServiceFactory.refreshToken = function () {
        var deferred = promiseFactory.defer();

        var authData = $localStorage.authorizationData;

        if (authData && authData.useRefreshTokens) {

            var data = 'grant_type=refresh_token&refresh_token=' + authData.refreshToken + '&client_id=' + ngUrlSettings.clientId;


            $http.post(tokenUrl, data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
                .success(function (response) {

                    $localStorage.authorizationData = {
                        id: response.id,
                        firstName: response.firstName,
                        lastName: response.lastName,
                        token: response.access_token,
                        role: JSON.parse(response.role),
                        refreshToken: response.refresh_token,
                        useRefreshTokens: true,
                        isAuthorized: true
                    };

                    authServiceFactory.loadAuthenticationData();

                    deferred.resolve(response);

                }).error(function (err, status) {
                    authServiceFactory.clearAuthenticationData();
                    authServiceFactory.logout();
                    deferred.reject(err);
                });
        } else {
            deferred.reject();
        }

        return deferred.promise;
    };
    return authServiceFactory;
}
