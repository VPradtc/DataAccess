'use strict';
angular.module('DataAccessApp').controller('registerController',
[
    '$scope',
    'userService',
    'registerUserFormFactory',
    'uniqueEmailValidator',
    'passwordMatchValidatorFactory',
    '$state',
    '$q',
    RegisterController]);

function RegisterController(
    $scope,
    userService,
    formFactory,
    uniqueEmailValidator,
    passwordMatchValidatorFactory,
    $state,
    $q) {

    var vm = $scope;

    vm.model = {
        form: {},
        entity: {}
    };

    var _getPassword = function () {
        return vm.model.entity.password;
    }

    var _getPasswordRepeat = function () {
        return vm.model.entity.passwordRepeat;
    }

    var _validateMatchPassword = passwordMatchValidatorFactory.create(_getPassword, _getPasswordRepeat);

    vm.options = {
        fields: formFactory.create(uniqueEmailValidator.validate, _validateMatchPassword),
    };

    vm.login = function () {
        $state.go('app.auth.login');
    }


    vm.register = function () {
        if (!vm.model.form.$valid) {
            vm.model.form.submitted = true;
        }

        userService.register(vm.model.entity)
        .then(vm.login);
    }
}
