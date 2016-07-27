'use strict';
angular.module('DataAccessApp').controller('userController',
['$controller', '$scope', 'userService', 'userModel', UserController]);

function UserController($controller, $scope, userService, userModel) {
    $scope.gridConfig = {
        grid: {
            getDataUrl: userService.getByPageUrl,
            model: userModel,
            columns: [
                {
                    field: 'firstName',
                    title: 'user.firstName'
                },
                {
                    field: 'lastName',
                    title: 'user.lastName'
                },
                {
                    field: 'email',
                    title: 'user.email'
                },
                {
                    field: 'phoneNumber',
                    title: 'user.phoneNumber'
                },
                {
                    field: 'role()',
                    title: 'user.role',
                    sortable: {
                        compare: 'roleId'
                    },
                    template: '<span i18n="#: role() #"></span>'
                },
                {
                    type: 'action',
                    value: ['edit', 'delete'],
                },
            ]
        },
        delete: {
            text: 'user.deleteThisUser',
            confirmButtonText: 'common.delete',
            serviceMethod: userService.delete
        },

        createState: 'app.main.user.create',
        editState: 'app.main.user.edit',

        view: {
            title: 'user.users',
            breadcrumb: [
                {
                    title: 'common.home',
                    sref: 'app.main.home'
                },
                {
                    title: 'user.users',
                },
            ],
            buttons: [
                {
                    text: 'common.create',
                    state: 'app.main.user.create'
                },
            ],
        }
    };
}

