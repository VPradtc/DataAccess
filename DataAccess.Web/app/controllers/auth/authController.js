'use strict';
angular.module('DataAccessApp').controller('authController',
['$scope', '$state', 'authService', AuthController]);

function AuthController($scope, $state, authService) {

    var vm = $scope;

    vm.isLoading = false;

    vm.loginData = {
        useRefreshTokens: true,
        email: "",
        password: ""
    };

    vm.register = function () {
        $state.go('app.auth.register');
    }

    vm.login = function () {
        vm.$applyAsync(function () {
            vm.isLoading = true;
            vm.error = '';
        });
        authService.login(vm.loginData).success(function (result) {
            $state.go('app.main.home');
            vm.$applyAsync(function () {
                vm.isLoading = false;
            });
        }).error(function (error) {
            vm.$applyAsync(function () {
                vm.isLoading = false;
            });
            if (error && error.error_description) {
                vm.error = error.error_description;
            }
        });
    };
}