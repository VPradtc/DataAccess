'use strict'
angular.module('DataAccessApp').factory('mapBasicActionColumnCommand',
    ['crudColumnFactory', MapBasicActionColumnCommand]);

function MapBasicActionColumnCommand(crudColumnFactory) {

    var _execute = function (action) {
        return crudColumnFactory.create(action.value);
    }

    return {
        execute: _execute,
    };
}
