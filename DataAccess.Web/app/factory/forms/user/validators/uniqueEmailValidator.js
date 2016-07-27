angular.module('DataAccessApp').factory('uniqueEmailValidator',
['$q', 'userService', uniqueEmailValidator]);

function uniqueEmailValidator($q, userService) {

    var _validate = function (email) {

        var deferred = $q.defer();

        userService.isUniqueEmail(email)
        .then(
            function (response) {
                response.isUnique
                    ? deferred.resolve()
                    : deferred.reject();
            },
            function (error) {
                deferred.reject();
            });

        return deferred.promise;
    }

    return {
        validate: _validate,
    }
}
