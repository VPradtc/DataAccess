'use strict';
angular.module('DataAccessApp').service('roleSpecificViewLocator',
['enumRoleIdentifier', RoleSpecificViewLocator]);

function RoleSpecificViewLocator(enumRoleIdentifier) {

    var _viewMap = {
    };

    var _getViewComponent = function (state, roleIdentifier) {

        var viewConfig = _viewMap[state];

        if (viewConfig === undefined) {
            throw String.format('Unable to find configuration for state: {0}', state);
        }

        var targetViewDefinition = viewConfig.views.find(function (viewDefinition) {
            return viewDefinition.role === roleIdentifier;
        });

        var targetComponent = targetViewDefinition !== undefined
            ? targetViewDefinition.component
            : viewConfig.default;

        if (targetComponent === undefined) {
            throw String.format('Unable to find component for state: {0}, role: {1}', state, roleIdentifier);
        }

        return targetComponent;
    }

    return {
        getViewComponent: _getViewComponent,
    };
}
