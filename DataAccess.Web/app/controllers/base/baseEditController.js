'use strict';
angular.module('DataAccessApp').controller('baseEditController',
['$scope', '$loading', '$state', BaseEditController]);
function BaseEditController($scope, $loading, $state) {

    if (!$scope.options) {
        return;
    }

    //---------------------   Init  ------------------------------//

    $scope.init = function () {

        $scope.entity = $scope.options.entity;

        if ($state && $state.params && $state.current && $state.current.mode) {
            $scope.mode = $scope.options.mode || 'default';

            _actions[$scope.mode].modeInit();
        }
    };

    $scope.editInit = function () {
        $loading.start('main');
        if (!$state.params || !$state.params.id) {
            console.log('Not found id');
        }

        $scope.options.serviceMethods.getById($state.params.id)
            .success(function (response) {
                $scope.afterEditInit(response);
            })
            .error(function (responseError) {
                $scope.getByIdErrorHeandle(responseError);
            })
            .finally(function () {
                $loading.stop('main');
            });
    };

    $scope.afterEditInit = function (response) {
        angular.extend($scope.entity, response);
    };

    $scope.getByIdErrorHeandle = function (error) {
        console.log(error.exceptionMessage);
    };

    $scope.createInit = function () {

        if ($scope.options.onCreate) {
            $scope.options.onCreate($scope.entity);
        }
    };

    $scope.save = function () {
        _actions[$scope.mode].save();
    };

    //---------------------   Edit  ------------------------------//

    $scope.edit = function () {

        $scope.beforeEdit();

        $scope.editOnServer($scope.entity)
            .success(function (response) {
                $scope.afterEdit(response);
            }).
            error(function (error) {
                $scope.editErrorHeandle(error);
            })
            .finally(function () {
                $loading.stop('main');
            });
    };

    $scope.beforeEdit = function () {
        $loading.start('main');

        //Not Impelemented
    };

    $scope.editOnServer = function (entity) {
        return $scope.options.serviceMethods.edit(entity);
    };

    $scope.afterEdit = function () {
        $state.go($scope.options.backState, { reload: true });
        //Not Implemented in Base
    };

    $scope.editErrorHeandle = function (error) {
        console.log(error.exceptionMessage);
    };

    //---------------------   Create  -------------------------------//

    $scope.create = function () {

        $scope.beforeCreate();

        $scope.createOnServer($scope.entity)
            .success(function (response) {
                $scope.afterCreate(response);
            }).
            error(function (error) {
                $scope.createErrorHeandle(error);
            })
            .finally(function () {
                $loading.stop('main');
            });
    };

    $scope.beforeCreate = function () {
        $loading.start('main');

        //Not Impelemented
    };

    $scope.createOnServer = function (entity) {
        return $scope.options.serviceMethods.create(entity);
    };

    $scope.afterCreate = function (response) {
        $state.go($scope.options.backState);
        //Not Implemented in Base
    };

    $scope.createErrorHeandle = function (error) {
        console.log(error.exceptionMessage);
    };

    ///Cancel
    $scope.cancel = function () {
        $state.go($scope.options.backState);
    }

    var _actions = {
        'edit': {
            modeInit: $scope.editInit,
            save: $scope.edit
        },
        'create': {
            modeInit: $scope.createInit,
            save: $scope.create
        },
        'default': {
            //modeInit: function () { console.log('Not found mode'); },
            save: function () { console.log('Not found mode'); },
        },
    };

    //Start
    $scope.init();
}
