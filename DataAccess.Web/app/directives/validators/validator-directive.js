app.directive('ixValidator', ['$q', function ($q) {

    var _addValidator = function (ctrl, validatorDefinition) {

        ctrl.$validators[validatorDefinition.validation] = function (modelValue, viewValue) {

            if (ctrl.$isEmpty(modelValue)) {
                return true;
            }

            return validatorDefinition.validator(modelValue);
        };
    };

    var _addAsyncValidator = function (ctrl, validatorDefinition) {

        ctrl.$asyncValidators[validatorDefinition.validation] = function (modelValue, viewValue) {

            if (ctrl.$isEmpty(modelValue)) {
                return $q.when();
            }

            return validatorDefinition.validator(modelValue);
        };
    };

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

                var targetMethod =
                    definition.async === true
                    ? _addAsyncValidator
                    : _addValidator;

                targetMethod(ctrl, definition);
            });
        },
    };
}]);