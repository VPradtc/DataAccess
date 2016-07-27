'use strict';
angular.module('DataAccessApp').controller('homeController',
[
    '$scope',
    '$state',
    'homeRouteProvider',
    HomeController]);

function HomeController(
    $scope,
    $state,
    homeRouteProvider
    ) {

    var vm = $scope;

    var _init = function () {

        var homeRoute = homeRouteProvider.getRoute();

        $state.go(homeRoute);
    }

    _init();
}

