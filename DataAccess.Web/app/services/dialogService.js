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

        var dialogOptions = angular.merge({}, _dialogs.default, dialog, params);

        return ngDialog.openConfirm(dialogOptions);
    }

    return {
        showConfirmDialog: function (params) {
            return _showDialog(_dialogs.confirm, params);
        },
        showSuccessDialog: function (params) {
            return _showDialog(_dialogs.success, params);
        },
        showFailureDialog: function (params) {
            return _showDialog(_dialogs.failure, params);
        }
    };
}
