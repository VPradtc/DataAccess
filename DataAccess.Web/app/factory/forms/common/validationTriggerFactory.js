'use strict'
angular.module('DataAccessApp').factory('validationTriggerFactory',
[ValidationTriggerFactory]);

function ValidationTriggerFactory() {

    var _triggerFactoryMap = {
        'submit': function (form, fieldName) {
            return _createFormValidationTrigger('$submitted', form);
        },
        default: function (form, fieldName) {
            return function () { return true; };
        },
    };

    var _createFormValidationTrigger = function (trigger, form) {
        return function () {
            return form[trigger];
        }
    }

    var _create = function (triggerName, form, fieldName) {

        if (typeof triggerName === 'function') {
            return triggerName;
        }

        var targetTriggerFactory = _triggerFactoryMap[triggerName] || _triggerFactoryMap.default;

        var validaTionTrigger = targetTriggerFactory(form, fieldName);

        return validaTionTrigger;
    }

    return {
        create: _create,
    }
}
