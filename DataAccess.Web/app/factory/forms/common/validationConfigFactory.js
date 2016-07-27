angular.module('DataAccessApp').factory('validationConfigFactory',
[ValidationConfigFactory]);

function ValidationConfigFactory() {

    var _create = function (fieldConfig) {

        return {
            name: fieldConfig.attrs.name,
            messages: fieldConfig.messages,
            trigger: fieldConfig.messageTrigger,
            mode: fieldConfig.mode,
        };
    }

    return {
        create: _create,
    }
}
