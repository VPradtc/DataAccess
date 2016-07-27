angular.module('DataAccessApp').factory('passwordMatchValidatorFactory',
['$q', 'passwordMatchValidator', PasswordMatchValidatorFactory]);

function PasswordMatchValidatorFactory($q, passwordMatchValidator) {

    var _create = function (passwordModelValueSelector, passwordRepeatModelValueSelector) {

        return function () {

            var password = passwordModelValueSelector();
            var passwordRepeat = passwordRepeatModelValueSelector();

            return passwordMatchValidator.validate(password, passwordRepeat);
        };
    }

    return {
        create: _create,
    }
}
