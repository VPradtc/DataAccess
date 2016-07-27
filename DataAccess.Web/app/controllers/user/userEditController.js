'use strict';
angular.module('DataAccessApp').controller('userEditController',
['$scope', 'userService', '$controller', '$state', 'detailsUserFormFactory', 'uniqueEmailValidator', '$q', UserEditController]);
function UserEditController($scope, userService, $controller, $state, formFactory, uniqueEmailValidator, $q) {

    var vm = $scope;

    var _labelMap = {
        'edit': {
            title: 'user.editUser',
        },
        'create': {
            title: 'user.createUser',
        }
    };

    var _getLabels = function () {
        return _labelMap[$state.current.mode];
    };

    var _init = function (userModel) {

        var emailValidator = function (email) {

            var deferred = $q.defer();
            var promise = deferred.promise;

            if ($state.current.mode === 'edit' && email === userModel.email) {
                deferred.resolve();
            }
            else {
                promise = uniqueEmailValidator.validate(email)
            }

            return promise;
        }

        vm.options = {
            backState: 'app.main.user.index',
            fields: formFactory.create(emailValidator),
            serviceMethods: {
                getById: userService.getById,
                edit: userService.update,
                create: userService.create,
            },
            view: {
                breadcrumb: [
                        {
                            title: 'common.home',
                            sref: 'app.main.home'
                        },
                        {
                            title: 'user.user',
                            sref: 'app.main.user.index'
                        },
                ],
                labels: _getLabels(),
            },
        };
    }

    var preInitPromise = $state.current.mode === 'create'
        ? $q.when({})
        : userService.getById($state.params.id)

    preInitPromise.then(_init);
}

