app.directive('ixAsyncValidator', function () {
    return {
        require: 'ngModel',
        scope: {
            validators: '<validators',
        },
        link: function (scope, elm, attrs, ctrl) {

            if (scope.validators === undefined) {
                return;
            }

            scope.validators.forEach(function (definition) {

                ctrl.$asyncValidators[definition.validation] = function (modelValue, viewValue) {

                    if (ctrl.$isEmpty(modelValue)) {
                        return $q.when();
                    }

                    return definition.validator(modelValue);
                };
            });
        }
    };
});