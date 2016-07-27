'use strict';
angular.module('DataAccessApp').service('userService',
['ngUrlSettings', 'baseService', UserService]);

function UserService(ngUrlSettings, baseService) {

    var api = ngUrlSettings.serverApiUri + 'api/user/';

    return {
        getByPageUrl: api + 'GetByPage',
        getById: function (id) {
            return baseService.getById(api + 'GetById/', id);
        },
        update: function (entity) {
            return baseService.callPost(api + 'Update', entity);
        },
        create: function (entity) {
            return baseService.callPost(api + 'Create', entity);
        },
        delete: function (id) {
            return baseService.callDelete(api + 'Delete/ ' + id);
        },
        isUniqueEmail: function (email) {
            return baseService.callGet(api + 'IsUniqueEmail', { 'email': email });
        },
        register: function (entity) {
            return baseService.callPost(api + 'Register', entity);
        },
    };
}

