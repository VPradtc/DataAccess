'use strict'
angular.module('DataAccessApp').factory('activateActionFactory',
['dialogService', 'buttonTemplateFactory', ActivateActionFactory]);

function ActivateActionFactory(dialogService, buttonTemplateFactory) {

    var _template = buttonTemplateFactory.create('default', 'warning', { type: 'text', value: '{{ dataItem.labelToggleActive() }}' });

    var _toggleActive = function (entity, activateDialog, deactivateDialog, callback) {

        var targetDialog = entity.isActive
            ? deactivateDialog
            : activateDialog;

        dialogService.showConfirmDialog(targetDialog)
        .then(function () {
            return callback(entity.id, !entity.isActive);
        });
    }

    var _create = function (
        activateDialog,
        deactivateDialog,
        callback) {

        return {
            template: _template,
            click: {
                name: 'toggleActive',
                method: function (entity) {
                    return _toggleActive(entity, activateDialog, deactivateDialog, callback);
                },
                call: 'toggleActive(dataItem)',
            },
        };
    }

    return {
        create: _create,
    };
}
