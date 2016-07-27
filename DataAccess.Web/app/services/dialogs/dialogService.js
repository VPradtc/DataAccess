'use strict';
angular.module('DataAccessApp').service('dialogService',
['ngDialog', DialogService]);

function DialogService(ngDialog) {

    var _dialogs = {
        confirm: {
            template: 'app/views/dialogs/confirmDialog.html',
        },
        success: {
            template: 'app/views/dialogs/successDialog.html',
        },
        failure: {
            template: 'app/views/dialogs/failureDialog.html',
        },
        default: {
            className: 'ngdialog-theme-plain',
            controller: 'dialogController',
            data: {
                labels: {
                    title: 'common.areYouSure',
                    confirmButtonText: 'common.confirm',
                    cancelButtonText: 'common.cancel'
                },
            },
        }
    }

    var _showDialog = function (dialog, params) {

        var dialogOptions = angular.merge({}, dialog, params);

        return ngDialog.openConfirm(dialogOptions);
    }

    var _showPremadeDialog = function (dialog, params) {

        var dialogTemplate = angular.merge({}, _dialogs.default, dialog);

        return _showDialog(dialogTemplate, params);
    }

    return {
        showConfirmDialog: function (params) {
            return _showPremadeDialog(_dialogs.confirm, params);
        },
        showSuccessDialog: function (params) {
            return _showPremadeDialog(_dialogs.success, params);
        },
        showFailureDialog: function (params) {
            return _showPremadeDialog(_dialogs.failure, params);
        },

        showCustomDialog: function (dialog, params) {
            return _showDialog(dialog, params);
        },
    };
}
