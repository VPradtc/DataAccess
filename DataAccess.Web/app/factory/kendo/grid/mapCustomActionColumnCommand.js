'use strict'
angular.module('DataAccessApp').factory('mapCustomActionColumnCommand',
['actionColumnFactory', MapCustomActionColumnCommand]);

function MapCustomActionColumnCommand(actionColumnFactory) {

    var _execute = function (action, scope) {

        var actionCopy = angular.copy(action);
        var clickDefinition = actionCopy.value.click;

        scope[clickDefinition.name] = clickDefinition.method;

        delete actionCopy.value.click;

        return actionColumnFactory.create(actionCopy.value, clickDefinition.call);
    }

    return {
        execute: _execute,
    };
}
