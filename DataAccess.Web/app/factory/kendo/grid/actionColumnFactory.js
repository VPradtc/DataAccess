'use strict'
angular.module('DataAccessApp').factory('actionColumnFactory', ActionColumnFactory);

function ActionColumnFactory() {

    var _columnTemplate = '<div class="action-column-wrapper" ng-click="{0}">{1}</div>'

    var _create = function (action, clickCall) {

        var result = angular.copy(action);
        delete result.template;
        result.command = {
            template: String.format(_columnTemplate, clickCall, action.template)
        }

        result.width = '30%';
        result.attributes = {
            class: 'center',
        };

        return result;
    };

    return {
        create: _create
    };
}
