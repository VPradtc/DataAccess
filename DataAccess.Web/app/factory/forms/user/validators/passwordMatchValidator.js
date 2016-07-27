angular.module('DataAccessApp').factory('passwordMatchValidator',
['$q', PasswordMatchValidator]);

function PasswordMatchValidator($q) {

    var _validate = function (password, passwordRepeat) {

        var deferred = $q.defer();

        passwordRepeat === password
            ? deferred.resolve()
            : deferred.reject();

        return deferred.promise;
    }

    return {
        validate: _validate,
    }
}
